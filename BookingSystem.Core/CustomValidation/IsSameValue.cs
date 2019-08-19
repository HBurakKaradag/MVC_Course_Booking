using System;
using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Core.CustomValidation
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
    public class IsSameValue : ValidationAttribute
    {
        private readonly string _dependProp;

        public IsSameValue(string dependProp)
        {
            _dependProp = dependProp;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            bool isSuccess = false;
            var targetProp = validationContext.ObjectType.GetProperty(_dependProp);
            if (targetProp == null)
                return new ValidationResult(string.Format(@"Property Cannot find in class {0}", this._dependProp),
                                                                                     new string[] { validationContext.MemberName });

            var targetValue = targetProp.GetValue(validationContext.ObjectInstance, null);

            isSuccess = targetValue.Equals(value);

            return isSuccess
                        ? ValidationResult.Success
                        : new ValidationResult(string.Format(@"Entry cannot pair {0}", targetProp.Name),
                                                                                    new string[] { validationContext.MemberName });
        }
    }
}