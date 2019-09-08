using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;

namespace BookingSystem.Core.Extensions
{
    /// <summary>
    /// @Kodluyoruz-MVC-Bootcamp  13.07.2019 – 09.09.2019
    /// H.Burak Karadağ
    /// Proje üzerinde string işlemlerimizin düzen, kod okunabilirliği
    /// standartlı kod yazma ve tekrarların önüne geçme kod standart alışkanlığı kazanma
    /// amaçlı yazdığımız stringExtension
    /// </summary>
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
            {
                return "";
            }

            return string.Join("", p.Normalize(NormalizationForm.FormD).Where(c => char.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)).Replace("ı", "i");
        }
    }
}