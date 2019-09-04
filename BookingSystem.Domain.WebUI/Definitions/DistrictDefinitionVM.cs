using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Domain.WebUI.Definitions
{
    public class DistrictDefinitionVM : IModel
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
}