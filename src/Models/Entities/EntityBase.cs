// ------------------------------------------------------------------------------------
//      Copyright (c) uhavemyword(at)gmail.com All rights reserved.
//      Created by Ben at 12/24/2012 7:13:16 PM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Models.Entities
{
    using CP.NLayer.Resources.Model;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;

    [DataContract(IsReference = true)]
    public abstract class EntityBase : EditModelBase
    {
        [DataMember]
        [Key]
        [Display(Name = MResourceNames.Id, ResourceType = typeof(MResources))]
        public long Id { get; set; }

        [DataMember]
        [Timestamp]
        public byte[] RowVersion { get; set; }

        public virtual object ShallowCopy()
        {
            return this.MemberwiseClone();
        }
    }
}