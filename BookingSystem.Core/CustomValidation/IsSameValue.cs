using System;
using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Core.CustomValidation
{
    /// <summary>
    /// @Kodluyoruz-MVC-Bootcamp  13.07.2019 – 09.09.2019
    /// H.Burak Karadağ
    /// ViewModel üzerinde CustomValidation yapmamızı sağlayan Attribute sınıfı
    /// Modelde set edilen propery nin Attribute'a parametre ile gönderilen property ile valuelarının
    /// aynı olup olmadığını validate eder. Sonucu modelState'e ekler.
    /// </summary>
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
            var targetProp = validationContext.ObjectType.GetProperty(_dependProp);
            if (targetProp == null)
            {
                return new ValidationResult(string.Format(@"Property Cannot find in class {0}", this._dependProp),
                                                                                     new string[] { validationContext.MemberName });
            }

            var targetValue = targetProp.GetValue(validationContext.ObjectInstance, null);

            bool isSuccess = targetValue.Equals(value);

            return isSuccess
                        ? ValidationResult.Success
                        : new ValidationResult(string.Format(@"Entry cannot pair {0}", targetProp.Name),
                                                                                    new string[] { validationContext.MemberName });
        }
    }
}