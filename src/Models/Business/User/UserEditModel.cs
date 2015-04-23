// ------------------------------------------------------------------------------------
//      Copyright (c) uhavemyword(at)gmail.com. All rights reserved.
//      Created by Ben at 2/20/2013 4:03:13 PM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Models.Business
{
    using CP.NLayer.Models.Entities;
    using CP.NLayer.Resources.Model;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;

    [DataContract(IsReference = true)]
    public class UserEditModel : EditModelBase
    {
        public UserEditModel()
        {
            this.RoleList = new List<CheckListItem>();
        }

        [DataMember]
        public User Target { get; set; }

        [DataMember]
        [Display(Name = MResourceNames.Password, ResourceType = typeof(MResources))]
        public string Password { get; set; }

        [DataMember]
        public IList<CheckListItem> RoleList { get; set; }

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