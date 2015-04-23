// ------------------------------------------------------------------------------------
//      Copyright (c) 2013 uhavemyword(at)gmail.com. All rights reserved.
//      Created by Ben at 1/27/2013 5:15:35 PM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Models.Business
{
    using CP.NLayer.Models.LocalizedDataAnnotations;
    using CP.NLayer.Resources.Model;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;

    [DataContract(IsReference = true)]
    public class LoginModel : EditModelBase
    {
        [DataMember]
        [Required2]
        [Display(Name = MResourceNames.UserName, ResourceType = typeof(MResources))]
        public string UserName { get; set; }

        [DataMember]
        [Required2]
        [Display(Name = MResourceNames.Password, ResourceType = typeof(MResources))]
        public string Password { get; set; }

        [DataMember]
        [Display(Name = MResourceNames.RememberMe, ResourceType = typeof(MResources))]
        public bool RememberMe { get; set; }
    }
}