namespace CP.NLayer.Common.License
{
    using System.Linq;

    /// <summary>
    /// Defines the machine code.
    /// </summary>
    public class MachineKey
    {
        /// <summary>
        /// Constructs a MachineKey by a given key string.
        /// </summary>
        /// <param name="key">
        /// A valid key should be at least a numeric string
        /// containing 7 digits and 1 checksum.
        /// </param>
        public MachineKey(string key)
        {
            if (Validate(key))
            {
                this.IsValid = true;
                Parse(key);
            }
        }

        public string Key { get; private set; }
        public bool IsValid { get; private set; }

        /// <summary>
        /// Validates whether the <paramref name="key"/> is valid.
        /// </summary>
        /// <param name="key">key string</param>
        /// <returns>true if the key is a numeric string containing 7 digits and 1 checksum.</returns>
        public static bool Validate(string key)
        {
            long n;
            if (key != null && key.Length == 8 && long.TryParse(key, out n))
            {
                string checksum = Utility.GetNumericHash(key.Substring(0, 7), 1);
                return checksum == key.Last().ToString();
            }
            return false;
        }

        /// <summary>
        /// Generates a valid MachineKey based on the hash of computer's hardward & software information.
        /// </summary>
        /// <remarks>
        ///A valid key should contain 8 digits in 2 parts:
        ///1~7 client code
        ///8 checksum
        ///</remarks>
        public static MachineKey Create()
        {
            string cpuId = MachineHelper.GetInfo("Win32_Processor", "ProcessorId");
            string mbId = MachineHelper.GetInfo("Win32_BaseBoard", "Product") + MachineHelper.GetInfo("Win32_BaseBoard", "SerialNumber");
            string mac = MachineHelper.GetInfo("Win32_NetworkAdapterConfiguration", "MACAddress");
            string version = Utility.GetMajorVersion().ToString();
            string hash = Utility.GetNumericHash(cpuId + mbId + version + mac, 7);
            string checksum = Utility.GetNumericHash(hash, 1);
            string key = hash + checksum;
            return new MachineKey(key);
        }

        private void Parse(string key)
        {
            this.Key = key;
        }
    }
}