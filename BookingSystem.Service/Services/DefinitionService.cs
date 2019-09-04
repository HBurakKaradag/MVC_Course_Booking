using BookingSystem.Data.Context;
using BookingSystem.Domain.Entity;
using BookingSystem.Domain.WebUI;
using BookingSystem.Domain.WebUI.AuditLog;
using BookingSystem.Domain.WebUI.Definitions;
using BookingSystem.Service.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace BookingSystem.Service.Services
{
    public class DefinitionService : ServiceBase
    {
        public ServiceResultModel<List<CityDefinitionVM>> GetCities()
        {
            List<CityDefinitionVM> resultList = new List<CityDefinitionVM>();
            using (EFBookingContext context = new EFBookingContext())
            {
                var cityDefs = context.CityDefinitions.Select(p => p).ToList();

                cityDefs.ForEach(p =>
                {
                    CityDefinitionVM cityVM = new CityDefinitionVM();
                    cityVM = p.MapProperties<CityDefinitionVM>();
                    cityVM.Districts = p.Districts.Select(c => c.MapProperties<DistrictDefinitionVM>()).ToList();
                    resultList.Add(cityVM);
                });
            }

            return ServiceResultModel<List<CityDefinitionVM>>.OK(resultList);
        }
    }
}