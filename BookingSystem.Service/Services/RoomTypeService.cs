﻿using BookingSystem.Core;
using BookingSystem.Core.Extensions;
using BookingSystem.Data.Context;
using BookingSystem.Domain.Entity;
using BookingSystem.Domain.WebUI;
using BookingSystem.Domain.WebUI.Filters;
using BookingSystem.Domain.WebUI.Room;
using BookingSystem.Service.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace BookingSystem.Service.Services
{
    public class RoomTypeService : ServiceBase
    {
        //public ServiceResultModel<HotelTypeVM> GetHotelType(int id)
        //{
        //    if (id <= 0)
        //        return null;
        //    HotelTypeVM currentItem = null;
        //    using (EFBookingContext context = new EFBookingContext())
        //    {
        //        currentItem = context.HotelTypes.FirstOrDefault(p => p.Id == id).MapProperties<HotelTypeVM>();
        //    }

        //    return ServiceResultModel<HotelTypeVM>.OK(currentItem);
        //}

        public ServiceResultModel<List<RoomTypeVM>> GetAllRoomTypes(RoomTypeFilter filter)
        {
            List<RoomTypeVM> resultList = new List<RoomTypeVM>();

            using (EFBookingContext context = new EFBookingContext())
            {
                IQueryable<RoomType> roomTypeList = context.RoomTypes;

                if (filter.Title.IsNotNull())
                    roomTypeList = roomTypeList.Where(p => p.Title.Contains(filter.Title));

                roomTypeList.ToList().ForEach(p =>
                {
                    resultList.Add(p.MapProperties<RoomTypeVM>());
                });

                return ServiceResultModel<List<RoomTypeVM>>.OK(resultList);
            }
        }

        //public ServiceResultModel<HotelTypeVM> SaveHotelType(HotelTypeVM model)
        //{
        //    using (EFBookingContext context = new EFBookingContext())
        //    {
        //        bool isAlreadyExists = context.HotelTypes.Any(p => p.Title == model.Title);
        //        if (isAlreadyExists)
        //            return new ServiceResultModel<HotelTypeVM>
        //            {
        //                Code = ServiceResultCode.Duplicate,
        //                Data = null,
        //                ResultType = OperationResultType.Warn,
        //                Message = "This record already exists"
        //            };

        //        var recordItem = context.HotelTypes.Add(model.MapProperties<HotelType>());
        //        context.SaveChanges();

        //        return ServiceResultModel<HotelTypeVM>.OK(recordItem.MapProperties<HotelTypeVM>());
        //    }
        //}

        //public ServiceResultModel<HotelTypeVM> UpdateHotelType(HotelTypeVM model)
        //{
        //    using (EFBookingContext context = new EFBookingContext())
        //    {
        //        var currentItem = context.HotelTypes.FirstOrDefault(p => p.Id == model.Id);
        //        if (currentItem != null)
        //        {
        //            // mevcut kayıt haricinde title ile aynı kayıt olamaz kontrol ediyoruz
        //            if (context.HotelTypes.Any(p => p.Id != model.Id && p.Title.Equals(model.Title)))
        //            {
        //                return new ServiceResultModel<HotelTypeVM>
        //                {
        //                    Code = ServiceResultCode.Duplicate,
        //                    Data = currentItem.MapProperties<HotelTypeVM>(),
        //                    ResultType = OperationResultType.Warn,
        //                    Message = "This title using other records "
        //                };
        //            }
        //            currentItem.Title = model.Title;
        //            currentItem.Description = model.Description;

        //            context.Entry<HotelType>(currentItem).State = System.Data.Entity.EntityState.Modified;
        //            context.SaveChanges();
        //        }

        //        return ServiceResultModel<HotelTypeVM>.OK(currentItem.MapProperties<HotelTypeVM>());
        //    }
        //}

        //public ServiceResultModel<HotelTypeVM> DeleteHotelType(int id)
        //{
        //    using (EFBookingContext context = new EFBookingContext())
        //    {
        //        var deleteItem = context.HotelTypes.FirstOrDefault(p => p.Id == id);
        //        context.HotelTypes.Remove(deleteItem);
        //        context.SaveChanges();

        //        return ServiceResultModel<HotelTypeVM>.OK(deleteItem.MapProperties<HotelTypeVM>());
        //        /*
        //         veya bu şeklide de yazabilirsiniz.
        //         EF Tracking sistemi ile çalışır. 2. kodda tracking 'e deleteıtem kaydının Hoteltype tablosunda deleted olarak Track'lendiğini bildiriyoruz.
        //         sonrasında commit 'de ilgili kayıt silinir.

        //        var deleteItem = context.HotelTypes.FirstOrDefault(p => p.Id == id);
        //        context.Entry<HotelType>(deleteItem).State = System.Data.Entity.EntityState.Deleted;
        //        context.SaveChanges();

        //         */
        //    }
        //}
    }
}