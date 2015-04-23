namespace CP.NLayer.Common.License
{
    using Microsoft.Win32;
    using System;
    using System.ComponentModel;

    public class MyLicenseProvider : LicenseProvider
    {
        public override License GetLicense(LicenseContext context, Type type, object instance, bool allowExceptions)
        {
            //#if DEBUG
            //            return new MyLicense(this, "Developer", DateTime.Now.AddMonths(1), false);
            //#endif
            var machineKey = MachineKey.Create();
            if (context != null && context.UsageMode == LicenseUsageMode.Designtime)
            {
                var productKey = ProductKey.Create(machineKey, DateTime.Now.AddDays(30), Version.GetDefault(), true);
                return new MyLicense(this, productKey);
            }
            else
            {
                ProductKey productKey;
                RegistryKey rk = Registry.CurrentUser.CreateSubKey("SOFTWARE\\CP_NLayer");
                string keyName = "pk"; //product key
                var keyValue = rk.GetValue(keyName) as string;

                if (keyValue == null)
                {
                    // first run
                    productKey = ProductKey.Create(machineKey, DateTime.Now.AddDays(30), Version.GetDefault(), true);
                    rk.SetValue(keyName, productKey.Key);
                }
                else
                {
                    productKey = new ProductKey(keyValue);
                    if (!productKey.IsValid || productKey.MachineKey.Key != machineKey.Key)
                    {
                        productKey = null;
                    }
                }

                return new MyLicense(this, productKey);
            }
        }
    }
}