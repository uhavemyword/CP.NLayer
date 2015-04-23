namespace CP.NLayer.Common.License
{
    using System.ComponentModel;

    public class MyLicense : License
    {
        public MyLicense(MyLicenseProvider provider, ProductKey productKey)
        {
            this.Provider = provider;
            this.ProductKey = productKey;
        }

        public MyLicenseProvider Provider { get; private set; }
        public ProductKey ProductKey { get; private set; }

        public override string LicenseKey
        {
            get
            {
                return ProductKey == null ? null : ProductKey.Key;
            }
        }

        public override void Dispose()
        {
        }
    }
}