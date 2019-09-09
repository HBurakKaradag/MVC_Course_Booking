using BookingSystem.Data.Context;
using BookingSystem.Domain.WebUI;
using BookingSystem.Domain.WebUI.Definitions;
using BookingSystem.Service.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace BookingSystem.Service.Services
{
    public class DefinitionService : ServiceBase
    {
        #region Cities

        public ServiceResultModel<List<CityDefinitionVM>> GetCities()
        {
            List<CityDefinitionVM> resultList = new List<CityDefinitionVM>();
            using (EFBookingContext context = new EFBookingContext())
            {
                var cityDefs = context.CityDefinitions.Select(p => p).ToList();

                cityDefs.ForEach(p =>
                {
                    CityDefinitionVM cityVM = new CityDefinitionVM();
                    cityVM = p.MapToViewModel<CityDefinitionVM>();
                    cityVM.Districts = p.Districts.Select(c => c.MapToViewModel<DistrictDefinitionVM>()).ToList();
                    resultList.Add(cityVM);
                });
            }

            return ServiceResultModel<List<CityDefinitionVM>>.OK(resultList);
        }

        #endregion Cities

        #region Districts

        public ServiceResultModel<List<DistrictDefinitionVM>> GetDistrict(int? cityId)
        {
            List<DistrictDefinitionVM> resultList = new List<DistrictDefinitionVM>();
            using (EFBookingContext context = new EFBookingContext())
            {
                var districts = context.DistrictDefinition.ToList();
                if (cityId.HasValue)
                {
                    districts = districts.Where(p => p.CityId == cityId.Value).ToList();
                }

                resultList.AddRange(districts.Select(p => p.MapToViewModel<DistrictDefinitionVM>()));
            }

            return ServiceResultModel<List<DistrictDefinitionVM>>.OK(resultList);
        }

        #endregion Districts
    }
}