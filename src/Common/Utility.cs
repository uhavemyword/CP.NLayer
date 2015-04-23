namespace CP.NLayer.Common
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Security.AccessControl;
    using System.Security.Cryptography;
    using System.Text;
    using System.Xml.Serialization;

    public static class Utility
    {
        #region DirectorySecurity

        // Adds an ACL entry on the specified directory for the specified account.
        // http://msdn.microsoft.com/zh-cn/library/system.security.accesscontrol.directorysecurity.aspx
        public static void AddDirectorySecurity(string path, string acount, FileSystemRights rights, AccessControlType controlType)
        {
            // Create a new DirectoryInfo object.
            DirectoryInfo dInfo = new DirectoryInfo(path);

            // Get a DirectorySecurity object that represents the
            // current security settings.
            DirectorySecurity dSecurity = dInfo.GetAccessControl();

            // Add the FileSystemAccessRule to the security settings.
            dSecurity.AddAccessRule(new FileSystemAccessRule(acount, rights, controlType));

            // Set the new access settings.
            dInfo.SetAccessControl(dSecurity);
        }

        public static void EnsureFloderWritable(string path)
        {
            var di = new DirectoryInfo(path);
            var ds = new DirectorySecurity(path, AccessControlSections.Access);
            Utility.AddDirectorySecurity(path, "Everyone", FileSystemRights.Write, AccessControlType.Allow);
        }

        #endregion

        public static int GetMajorVersion()
        {
            return Assembly.GetEntryAssembly().GetName().Version.Major;
        }

        #region Algorithm

        public static string GetMd5Hash(string input)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                // Convert the input string to a byte array and compute the hash.
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Create a new Stringbuilder to collect the bytes and create a string.
                StringBuilder sBuilder = new StringBuilder();

                // Loop through each byte of the hashed data and format each one as a hexadecimal string.
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                // Return the hexadecimal string.
                return sBuilder.ToString();
            }
        }

        public static string GetBase64String(string input)
        {
            var bytes = Encoding.ASCII.GetBytes(input);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Gets a numeric string with given length from input
        /// </summary>
        public static string GetNumericHash(string input, int length)
        {
            if (length <= 0)
            {
                throw new ArgumentException("length");
            }

            if (string.IsNullOrEmpty(input))
            {
                return new string('0', length);
            }

            var hash = GetMd5Hash(input);
            do
            {
                hash = GetBase64String(hash);
            }
            while (hash.Length < length);

            char[] charResult = hash.ToCharArray(0, length);
            var result = new StringBuilder();
            foreach (var c in charResult)
            {
                int n = ((int)c) % 10;
                result.Append(n);
            }

            return result.ToString();
        }

        #endregion

        #region XmlSerialize/XmlDeserialize

        public static string XmlSerialize<T>(T obj)
        {
            string result = null;
            try
            {
                using (var stringWriter = new StringWriter())
                {
                    var serializer = new XmlSerializer(typeof(T));
                    serializer.Serialize(stringWriter, obj);
                    result = stringWriter.ToString();
                }
            }
            catch
            {
            }
            return result;
        }

        public static T XmlDeserialize<T>(string xmlString) where T : new()
        {
            T result;
            try
            {
                if (string.IsNullOrEmpty(xmlString))
                    return new T();
                using (var stringWriter = new StringReader(xmlString))
                {
                    var serializer = new XmlSerializer(typeof(T));
                    result = (T)serializer.Deserialize(stringWriter);
                }
            }
            catch
            {
                result = new T();
            }
            return result;
        }

        #endregion
    }
}