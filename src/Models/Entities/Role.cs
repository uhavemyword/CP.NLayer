﻿// ------------------------------------------------------------------------------------
//      Copyright (c) 2012 uhavemyword(at)gmail.com. All rights reserved.
//      Created by Ben at 12/24/2012 7:13:16 PM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Models.Entities
{
    using CP.NLayer.Models.LocalizedDataAnnotations;
    using CP.NLayer.Resources.Model;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.Serialization;

    [DataContract(IsReference = true)]
    public class Role : EntityBase
    {
        #region ctor

        public Role()
        {
            this.Users = new List<User>();
            this.Permissions = new List<Permission>();
        }

        #endregion

        #region Properties

        [DataMember]
        [Required2, StringLength2(50), Index("IX_Name", IsUnique = true)]
        [Display(Name = MResourceNames.Role_Name, ResourceType = typeof(MResources))]
        public string Name { get; set; }

        [DataMember]
        [StringLength2(200)]
        [Display(Name = MResourceNames.Description, ResourceType = typeof(MResources))]
        public string Description { get; set; }

        #endregion

        #region Navigation Properties

        [DataMember]
        public virtual ICollection<User> Users { get; set; }

        [DataMember]
        public virtual ICollection<Permission> Permissions { get; set; }

        #endregion

        #region Methods

        public override string ToString()
        {
            return this.Name;
        }

        #endregion
    }
}