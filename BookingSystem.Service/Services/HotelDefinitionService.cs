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
                    hotelDefinitionList = hotelDefinitionList.Where(p => p.HotelTypeId == filter.HotelTypeId);

                hotelDefinitionList.ToList().ForEach(p =>
                {
                    resultList.Add(p.MapProperties<HotelDefinitionVM>());
                });

                return ServiceResultModel<List<HotelDefinitionVM>>.OK(resultList);
            }
        }

        public ServiceResultModel<HotelDefinitionVM> GetHotel(int id)
        {
            HotelDefinitionVM hotel = null;
            using (EFBookingContext context = new EFBookingContext())
            {
                context.HotelDefinitions.Include("HotelAttribute");
                var hotelEntity = context.HotelDefinitions.FirstOrDefault(p =>p.Id == id);
                hotel = hotelEntity.MapProperties<HotelDefinitionVM>();
                hotel.HotelAttributes = hotel.HotelAttributes.Select(p => p.MapProperties<HotelAttributeVM>()).ToList();
            }

            return ServiceResultModel<HotelDefinitionVM>.OK(hotel);
        }

        //public ServiceResultModel<HotelDefinitionVM> SaveHotel(HotelDefinitionVM model)
        //{
        //    using (EFBookingContext context = new EFBookingContext())
        //    {
        //        using (var transaction = context.Database.BeginTransaction())
        //        {
        //            try
        //            {
        //                HotelDefinition hotelDef = model.

        //                transaction.Commit();
        //            }
        //            catch (Exception)
        //            {
        //                // using olduğu için RollBack eklemesek de olur
        //                // transaction.Rollback();
        //                throw;
        //            }
        //        }
        //    }
        //}
    }
}