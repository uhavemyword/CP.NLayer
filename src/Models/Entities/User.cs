// ------------------------------------------------------------------------------------
//      Copyright (c) 2012 uhavemyword(at)gmail.com. All rights reserved.
//      Created by Ben at 12/24/2012 7:13:16 PM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Models.Entities
{
    using CP.NLayer.Models.LocalizedDataAnnotations;
    using CP.NLayer.Resources.Model;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.Serialization;

    [DataContract(IsReference = true)]
    public class User : EntityBase
    {
        #region Ctor

        public User()
        {
            this.PasswordHash = Guid.NewGuid().ToString("N");
            this.Roles = new List<Role>();
        }

        #endregion

        #region Properties

        [DataMember]
        [Required2, UserName2, StringLength2(50), Index("IX_Name", IsUnique = true)]
        [Display(Name = MResourceNames.UserName, ResourceType = typeof(MResources))]
        public string UserName { get; set; }

        [DataMember]
        [Required2, StringLength2(200)]
        public string PasswordHash { get; set; }

        [DataMember]
        [StringLength2(50)]
        public string PasswordSalt { get; set; }

        [DataMember]
        [Required2, StringLength2(50)]
        [Display(Name = MResourceNames.User_FullName, ResourceType = typeof(MResources))]
        public string FullName { get; set; }

        [DataMember]
        [Email2, StringLength2(200)]
        public string Email { get; set; }

        [DataMember]
        [StringLength2(200)]
        public string ContactInfo { get; set; }

        [DataMember]
        [Display(Name = MResourceNames.IsActive, ResourceType = typeof(MResources))]
        public bool IsActive { get; set; }

        [DataMember]
        public DateTime? LastLoginAt { get; set; }

        [DataMember]
        [StringLength2(100)]
        public string LastLoginIP { get; set; }

        [DataMember]
        [StringLength2(100)]
        public string LastLoginLocation { get; set; }

        [DataMember]
        [StringLength2(2000)]
        public string MapData { get; set; }

        #endregion

        #region Navigation Properties

        [DataMember]
        public long? DepartmentId { get; set; }

        [DataMember]
        public virtual Department Department { get; set; }

        [DataMember]
        public virtual ICollection<Role> Roles { get; set; }

        #endregion

        #region Methods

        public override string ToString()
        {
            return this.UserName;
        }

        public bool HasPermission(PermissionCodeEnum codeEnum)
        {
            var permissionCodes = new List<PermissionCodeEnum>();
            permissionCodes.Add(PermissionCodeEnum.None);
            if (this.Roles != null)
            {
                foreach (var role in this.Roles)
                {
                    if (role.Permissions != null)
                    {
                        foreach (var permission in role.Permissions)
                        {
                            permissionCodes.Add(permission.CodeEnum);
                        }
                    }
                }
            }

            return permissionCodes.Contains(codeEnum);
        }

        #endregion
    }
}