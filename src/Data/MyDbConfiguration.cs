// ------------------------------------------------------------------------------------
//      Copyright (c) uhavemyword(at)gmail.com All rights reserved.
//      Created by Ben at 11/3/2013 4:27:01 PM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Data
{
    using System.Data.Entity;
    using System.Data.Entity.SqlServer;

    public class MyDbConfiguration : DbConfiguration
    {
        public MyDbConfiguration()
        {
            SetProviderServices(SqlProviderServices.ProviderInvariantName, SqlProviderServices.Instance);
        }
    }
}