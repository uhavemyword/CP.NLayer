// ------------------------------------------------------------------------------------
//      Copyright (c) uhavemyword(at)gmail.com All rights reserved.
//      Created by Ben at 1/4/2013 11:44:26 AM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Models
{
    using CP.NLayer.Common;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Runtime.Serialization;

    [DataContract(IsReference = true)]
    public abstract class DataErrorInfo : IDataErrorInfo//, IValidatableObject
    {
        #region IDataErrorInfo

        private static bool _ignoreValidationOnFirstTime = false; // TODO: make it configurable, true to not validate properties when the model is new created.

        private HashSet<string> _raisedProperties = new HashSet<string>();

        public virtual string Error
        {
            get { return string.Empty; }
        }

        public string this[string propertyName]
        {
            get
            {
                if (_ignoreValidationOnFirstTime)
                {
                    if (!_raisedProperties.Contains(propertyName))
                    {
                        _raisedProperties.Add(propertyName);
                        return string.Empty;
                    }
                }

                var errors = ValidateProperty(propertyName).Select(x => x.ErrorMessage).ToArray();
                return string.Join(Environment.NewLine, errors).TrimEnd();
            }
        }

        #endregion IDataErrorInfo

        /// <summary>
        /// Gets an error message indicating what is wrong with the properties with ValidationAttribute & object level errors
        /// </summary>
        /// <returns>ValidationResult list</returns>
        public List<ValidationResult> GetValidationResults()
        {
            var results = new List<ValidationResult>();

            var properties = this.GetType().GetProperties();
            foreach (var p in properties)
            {
                if (p.GetCustomAttributes(typeof(ValidationAttribute), true).Count() > 0)
                {
                    var propertyErrors = ValidateProperty(p.Name);
                    results = results.Union(propertyErrors).ToList();
                }
            }

            var objectErrors = ValidateObject();
            results = results.Union(objectErrors).ToList();

            return results;
        }

        /// <summary>
        /// Validates current instance on object level
        /// </summary>
        /// <returns>ValidationResult list</returns>
        protected virtual List<ValidationResult> ValidateObject()
        {
            var results = new List<ValidationResult>();
            return results;
        }

        /// <summary>
        /// Validates current instance properties using Data Annotations.
        /// </summary>
        /// <param name="propertyName">This instance property to validate.</param>
        /// <returns>ValidationResult list</returns>
        protected virtual List<ValidationResult> ValidateProperty(string propertyName)
        {
            var results = new List<ValidationResult>();
            if (!string.IsNullOrEmpty(propertyName))
            {
                try
                {
                    var value = this.GetType().GetProperty(propertyName).GetValue(this, null);
                    var context = new ValidationContext(this, null, null) { MemberName = propertyName };
                    Validator.TryValidateProperty(value, context, results);
                }
                catch (Exception ex)
                {
                    results.Add(new ValidationResult(ex.MostInnerException().Message, new string[] { propertyName }));
                }
            }
            return results;
        }
    }
}