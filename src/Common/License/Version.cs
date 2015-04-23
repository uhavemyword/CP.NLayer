namespace CP.NLayer.Common.License
{
    using System;
    using System.ComponentModel;

    public enum ApplicationEnum
    {
        [Description("Unspecified")]
        Unspecified,

        [Description("App 1")]
        App1 = 1,

        [Description("App 2")]
        App2 = 2
    }

    public enum EditionEnum
    {
        [Description("Unspecified")]
        Unspecified,

        [Description("Starter Edition")]
        Starter,

        [Description("Lite Edition")]
        Lite,

        [Description("Pro Edition")]
        Pro
    }

    public enum CountryEnum
    {
        [Description("Unspecified")]
        Unspecified,

        America,
        China
    }

    public class Version
    {
        public Version(ApplicationEnum application, EditionEnum edition, CountryEnum country)
        {
            this.Applicagtion = application;
            this.Edition = edition;
            this.Country = country;
        }

        public Version(int version)
        {
            if (version >= 4096)
            {
                throw new ArgumentException("Version number should less than 4096!");
            }

            int c = version & 0x0F;
            int e = (version >> 4) & 0x0F;
            int a = (version >> 8) & 0x0F;
            this.Applicagtion = (ApplicationEnum)a;
            this.Edition = (EditionEnum)e;
            this.Country = (CountryEnum)c;
        }

        public ApplicationEnum Applicagtion { get; private set; }
        public EditionEnum Edition { get; private set; }
        public CountryEnum Country { get; private set; }

        public static Version GetDefault()
        {
            return new Version(ApplicationEnum.Unspecified, EditionEnum.Unspecified, CountryEnum.Unspecified);
        }

        /// <summary>
        /// Gets the number value representing current version.
        /// </summary>
        /// <returns>A number less than 4096.</returns>
        public int GetNumber()
        {
            int version;
            int a = ((int)Applicagtion) & 0x0F; //1001 0101 1010
            int e = ((int)Edition) & 0x0F;
            int c = ((int)Country) & 0x0F;
            version = (a << 8) + (e << 4) + c;  //version<4096
            return version;
        }

        /// <summary>
        /// Converts the version to a numeric string containing 4 digits.
        /// </summary>
        /// <returns>A numeric string containing 4 digits.</returns>
        public override string ToString()
        {
            return GetNumber().ToString().PadLeft(4, '0');
        }
    }
}