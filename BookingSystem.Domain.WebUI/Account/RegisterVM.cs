using BookingSystem.Core.CustomValidation;
using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Domain.WebUI.Account
{
    public class RegisterVM : IModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [IsSameValue("Password")]
        public string PasswordConfirm { get; set; }

        [Required]
        public string EMail { get; set; }

        [Required]
        public bool IsAcceptTerm { get; set; }

        public string TokenId { get; set; }
    }
}