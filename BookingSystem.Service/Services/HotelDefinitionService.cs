using BookingSystem.Core;
using BookingSystem.Core.Extensions;
using BookingSystem.Data.Context;
using BookingSystem.Domain.Entity;
using BookingSystem.Domain.WebUI;
using BookingSystem.Domain.WebUI.Filters;
using BookingSystem.Domain.WebUI.Hotel;
using BookingSystem.Service.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookingSystem.Service.Services
{
    public class HotelDefinitionService : ServiceBase
    {
        public ServiceResultModel<List<HotelDefinitionVM>> GetHotels(HotelFilter filter)
        {
            List<HotelDefinitionVM> resultList = new List<HotelDefinitionVM>();

            using (EFBookingContext context = new EFBookingContext())
            {
                IQueryable<HotelDefinition> hotelDefinitionList = context.HotelDefinitions;

                if (filter.HotelName.IsNotNull())
                    hotelDefinitionList = hotelDefinitionList.Where(p => p.Title.Contains(filter.HotelName));

                if (filter.HotelTypeId > 0)
                    hotelDefinitionList = hotelDefinitionList.Where(p => p.HoteTypeId == filter.HotelTypeId);

                hotelDefinitionList.ToList().ForEach(p =>
                {
                    resultList.Add(p.MapProperties<HotelDefinitionVM>());
                });

                return ServiceResultModel<List<HotelDefinitionVM>>.OK(resultList);
            }
        }
    }
}