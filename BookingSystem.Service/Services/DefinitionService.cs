using BookingSystem.Data.Context;
using BookingSystem.Domain.Entity;
using BookingSystem.Domain.WebUI;
using BookingSystem.Domain.WebUI.AuditLog;
using BookingSystem.Domain.WebUI.Definitions;
using BookingSystem.Service.Extensions;
using System;
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

        public ServiceResultModel<List<DistrictDefinitionVM>> GetDistrictByCityId(int cityId)
        {
            List<DistrictDefinitionVM> resultList = new List<DistrictDefinitionVM>();
            using (EFBookingContext context = new EFBookingContext())
            {
                var districts = context.DistrictDefinition.Where(p => p.CityId == cityId).ToList();
                resultList.AddRange(districts.Select(p => p.MapProperties<DistrictDefinitionVM>()));
            }

            return ServiceResultModel<List<DistrictDefinitionVM>>.OK(resultList);
        }
    }
}