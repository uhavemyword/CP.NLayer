namespace CP.NLayer.Models.Business.Dashboard
{
    using CP.NLayer.Resources.Model;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;

    [DataContract(IsReference = true)]
    public class RoleModel
    {
        [DataMember]
        [Display(Name = MResourceNames.Name, ResourceType = typeof(MResources))]
        public string Name { get; set; }

        [DataMember]
        [Display(Name = MResourceNames.Color, ResourceType = typeof(MResources))]
        public string ColorHexValue { get; set; }
    }
}