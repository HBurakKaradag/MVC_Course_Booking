using BookingSystem.Core;
using BookingSystem.Core.Extensions;
using BookingSystem.Data.Context;
using BookingSystem.Domain.Entity;
using BookingSystem.Domain.WebUI;
using BookingSystem.Domain.WebUI.Filters;
using BookingSystem.Service.Extensions;
using System;
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

        public ServiceResultModel<HotelTypeVM> SaveHotelType(HotelTypeVM model)
        {
            using (EFBookingContext context = new EFBookingContext())
            {
                bool isAlreadyExists = context.HotelTypes.Any(p => p.Title == model.Title);
                if (isAlreadyExists)
                    return new ServiceResultModel<HotelTypeVM>
                    {
                        Code = ServiceResultCode.Duplicate,
                        Data = null,
                        ResultType = OperationResultType.Warn,
                        Message = "This record already exists"
                    };

                context.HotelTypes.Add(model.MapProperties<HotelType>());
                context.SaveChanges();

                return ServiceResultModel<HotelTypeVM>.OK(model);
            }
        }
    }
}