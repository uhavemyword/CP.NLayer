// ------------------------------------------------------------------------------------
//      Copyright (c) uhavemyword(at)gmail.com. All rights reserved.
//      Created by Ben at 2/20/2013 4:01:33 PM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Models.Business
{
    using CP.NLayer.Models.Entities;
    using System.Runtime.Serialization;

    [DataContract(IsReference = true)]
    public class DepartmentDisplayModel : DisplayModelBase<Department>
    {
        public override string GetDisplayName()
        {
            return Target == null ? this.ToString() : Target.Name;
        }
    }
}