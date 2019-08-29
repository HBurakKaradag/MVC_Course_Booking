using BookingSystem.Core;
using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Domain.WebUI.Attributes
{
    public class AttributeVM : IModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public AttributeType AttributeType { get; set; }

        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
}