namespace CP.NLayer.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;

    [DataContract(IsReference = true)]
    public abstract class EditModelBase<T> : EditModelBase where T : DataErrorInfo
    {
        [DataMember]
        public T Target { get; set; }

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