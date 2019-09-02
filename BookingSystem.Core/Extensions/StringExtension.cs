using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;

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

        public static string Titilize(this string p)
        {
            if (p.IsNull())
                return "";

            return string.Join("", p.Normalize(NormalizationForm.FormD).Where(c => char.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)).Replace("ı", "i");
        }
    }
}