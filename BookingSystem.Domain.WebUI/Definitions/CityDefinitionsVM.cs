using System.Collections.Generic;

namespace BookingSystem.Domain.WebUI.Definitions
{
    public class CityDefinitionVM : IModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }

        public List<DistrictDefinitionVM> Districts { get; set; }
    }
}