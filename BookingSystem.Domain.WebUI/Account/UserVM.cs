using BookingSystem.Core.CustomAttribute;
using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Domain.WebUI.Account
{
    public class UserVM : IModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

        [MapIgnore]
        public string DisplayName { get { return this.Name + " " + this.LastName; } }

        public string AvatarUrl { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string EMail { get; set; }

        public bool IsActive { get; set; }
        public bool Deleted { get; set; }
        public bool EmailConfirmation { get; set; }
        public string TokenId { get; set; }
    }
}