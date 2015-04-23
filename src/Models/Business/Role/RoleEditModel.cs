// ------------------------------------------------------------------------------------
//      Copyright (c) uhavemyword(at)gmail.com. All rights reserved.
//      Created by Ben at 2/26/2013 9:07:45 PM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Models.Business
{
    using CP.NLayer.Models.Entities;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;

    [DataContract(IsReference = true)]
    public class RoleEditModel : EditModelBase<Role>
    {
        public RoleEditModel()
        {
            this.PermissionList = new List<CheckListItem>();
        }

        [DataMember]
        public IList<CheckListItem> PermissionList { get; set; }

        protected override List<ValidationResult> ValidateObject()
        {
            if (Target == null)
            {
                return new List<ValidationResult>();
            }

            return Target.GetValidationResults();
        }
    }
}