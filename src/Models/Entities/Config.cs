namespace CP.NLayer.Models.Entities
{
    using CP.NLayer.Models.LocalizedDataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.Serialization;

    [DataContract(IsReference = true)]
    public class Config : EntityBase
    {
        public Config()
        {
        }

        [DataMember]
        [Required2, StringLength2(200), Index("IX_Category_Name", IsUnique = true, Order = 1)]
        public string Category { get; set; }

        [DataMember]
        [Required2, StringLength2(200), Index("IX_Category_Name", IsUnique = true, Order = 2)]
        public string Name { get; set; }

        [DataMember]
        public string Value { get; set; }

        [DataMember]
        public string DefaultValue { get; set; }

        [DataMember]
        [StringLength2(200)]
        public string Description { get; set; }
    }
}