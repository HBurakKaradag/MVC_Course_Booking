using BookingSystem.Core.Extensions;
using BookingSystem.Data.Context;
using BookingSystem.Domain.Entity;
using BookingSystem.Domain.WebUI;
using BookingSystem.Domain.WebUI.Filters;
using BookingSystem.Service.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace BookingSystem.Service.Services
{
    public class HotelTypeService : ServiceBase
    {
        public ServiceResultModel<List<HotelTypeVM>> GetHotelTypes(HotelTypeFilter filter)
        {
            List<HotelTypeVM> resultList = new List<HotelTypeVM>();

            using (EFBookingContext context = new EFBookingContext())
            {
                IQueryable<HotelType> hotelTypeList = context.HotelTypes;

                if (filter.Title.IsNotNull())
                    hotelTypeList = hotelTypeList.Where(p => p.Title.Contains(filter.Title));

                if (filter.IsActive.HasValue && filter.IsActive.Value)
                    hotelTypeList = hotelTypeList.Where(p => p.IsActive == filter.IsActive);

                hotelTypeList.ToList().ForEach(p =>
                {
                    resultList.Add(p.MapProperties<HotelTypeVM>());
                });

                return ServiceResultModel<List<HotelTypeVM>>.OK(resultList);
            }
        }
    }
}