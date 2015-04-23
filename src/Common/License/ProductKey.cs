namespace CP.NLayer.Common.License
{
    using System;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Defines the product code for registration.
    /// </summary>
    public class ProductKey
    {
        private static Random _random = new Random();
        private static DateTime _baseDate = new DateTime(2014, 1, 1);

        /// <summary>
        /// Constructs a ProductKey by a given key string.
        /// </summary>
        /// <param name="key">
        /// A valid key should be at least a numeric string
        /// containing 17 digits and 1 checksum.
        /// </param>
        public ProductKey(string key)
        {
            this.IsTrial = true;
            if (Validate(key))
            {
                this.IsValid = true;
                Parse(key);
            }
        }

        public string Key { get; private set; }
        public bool IsValid { get; private set; }
        public MachineKey MachineKey { get; private set; }
        public DateTime ExpireDate { get; private set; }
        public Version Version { get; private set; }
        public bool IsTrial { get; private set; }

        /// <summary>
        /// Validates whether the <paramref name="key"/> is valid.
        /// </summary>
        /// <param name="key">key string</param>
        /// <returns>true if the key is a numeric string containing 17 digits and 1 checksum.</returns>
        public static bool Validate(string key)
        {
            long n;
            if (key != null && key.Length == 18 && long.TryParse(key, out n))
            {
                key = Decode(key);
                string checksum = Utility.GetNumericHash(key.Substring(0, 17), 1);
                return checksum == key.Last().ToString();
            }
            return false;
        }

        /// <summary>
        /// Generates a valid ProductKey.
        /// </summary>
        /// <remarks>
        ///A valid key should contain 18 digits in 5 parts:
        ///a: 1 random value & odd means isTrial=true, even means isTrail=false.
        ///b: 2~8 machine key
        ///c: 9~13 left days
        ///d: 14~17 version
        ///e: 18 checksum
        ///</remarks>
        public static ProductKey Create(MachineKey machineKey, DateTime expireDate, Version version, bool isTrial)
        {
            if (!machineKey.IsValid || (expireDate - _baseDate).Days > 99999)
            {
                return null;
            }

            int a = _random.Next(1, 10); //[1, 9]
            if ((isTrial && a % 2 == 0) || (!isTrial && a % 2 == 1))
            {
                a++;
            }

            int b = int.Parse(machineKey.Key.Substring(0, 7));
            int c = expireDate > _baseDate ? (expireDate - _baseDate).Days : 0;

            string abcd = a.ToString() + machineKey.Key.Substring(0, 7) + c.ToString().PadLeft(5, '0') + version.GetNumber().ToString().PadLeft(4, '0');
            string e = Utility.GetNumericHash(abcd, 1);
            string result = abcd + e;
            return new ProductKey(Encode(result));
        }

        private static string Encode(string s)
        {
            var result = new StringBuilder();
            int first = int.Parse(s.Substring(0, 1));
            result.Append(first.ToString());
            for (int i = 1; i < s.Length; i++)
            {
                int n = int.Parse(s[i].ToString());
                n = (n + first * i) % 10;
                result.Append((n).ToString());
            }
            return result.ToString();
        }

        private static string Decode(string s)
        {
            var result = new StringBuilder();
            int first = int.Parse(s.Substring(0, 1));
            result.Append(first.ToString());
            for (int i = 1; i < s.Length; i++)
            {
                int n = int.Parse(s[i].ToString());
                n = (n - first * i) % 10;
                if (n < 0)
                {
                    n = 10 + n;
                }
                result.Append((n).ToString());
            }
            return result.ToString();
        }

        private void Parse(string key)
        {
            this.Key = key;
            var decodeKey = Decode(key);

            int a = int.Parse(decodeKey.Substring(0, 1));
            this.IsTrial = (a % 2 == 1);

            string b = decodeKey.Substring(1, 7);
            this.MachineKey = new MachineKey(b + Utility.GetNumericHash(b, 1));

            int c = int.Parse(decodeKey.Substring(8, 5));
            this.ExpireDate = _baseDate.AddDays(c);

            int d = int.Parse(decodeKey.Substring(13, 4));
            this.Version = new Version(d);
        }
    }
}