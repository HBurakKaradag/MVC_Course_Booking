using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Core.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNull(this string p)
        {
            return string.IsNullOrWhiteSpace(p);
        }

        public static bool IsNotNull(this string p)
        {
            return !IsNull(p);
        }

        public static bool IsValidEmail(this string p)
        {
            return new EmailAddressAttribute().IsValid(p);
        }
    }
}