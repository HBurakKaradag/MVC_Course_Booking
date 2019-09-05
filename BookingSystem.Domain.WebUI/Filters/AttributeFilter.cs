using System.ComponentModel;

namespace BookingSystem.Domain.WebUI.Filters
{
    public class AttributeFilter
    {
        [DisplayName("Attribute Name")]
        public string Name { get; set; }

        public int AttributeType { get; set; }

        //[DisplayName("Active Records")]
        //public bool? IsActive { get; set; }
    }
}