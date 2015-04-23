// ------------------------------------------------------------------------------------
//      Copyright (c) uhavemyword(at)gmail.com. All rights reserved.
//      Created by Ben at 2013/2/5 14:07:40
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Models.Entities
{
    using CP.NLayer.Models.LocalizedDataAnnotations;
    using CP.NLayer.Resources.Model;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;

    [DataContract(IsReference = true)]
    public class Permission : EntityBase
    {
        #region ctor

        public Permission()
        {
            this.Roles = new List<Role>();
        }

        #endregion

        #region Properties

        [DataMember]
        [Required2, StringLength2(50)]
        [Display(Name = MResourceNames.Code, ResourceType = typeof(MResources))]
        public string Code { get; set; }

        [DataMember]
        public PermissionCodeEnum CodeEnum
        {
            get
            {
                PermissionCodeEnum p;
                if (!Enum.TryParse(Code, true, out p))
                {
                    p = PermissionCodeEnum.None;
                }
                return p;
            }
            set
            {
                Code = value.ToString();
            }
        }

        [DataMember]
        [Required2, StringLength2(50)]
        [Display(Name = MResourceNames.Name, ResourceType = typeof(MResources))]
        public string Name { get; set; }

        [DataMember]
        [StringLength2(200)]
        [Display(Name = MResourceNames.Description, ResourceType = typeof(MResources))]
        public string Description { get; set; }

        [DataMember]
        [Display(Name = MResourceNames.DisplayOrder, ResourceType = typeof(MResources))]
        public int DisplayOrder { get; set; }

        [DataMember]
        [StringLength2(50)]
        [Display(Name = MResourceNames.Group, ResourceType = typeof(MResources))]
        public string Group { get; set; }

        #endregion

        #region Navegation Properties

        [DataMember]
        public virtual ICollection<Role> Roles { get; set; }

        #endregion

        #region Methods

        public override string ToString()
        {
            return this.Name;
        }

        #endregion
    }
}