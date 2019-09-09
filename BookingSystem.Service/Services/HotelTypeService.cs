﻿using BookingSystem.Core;
using BookingSystem.Core.Extensions;
using BookingSystem.Data.Context;
using BookingSystem.Domain.Entity;
using BookingSystem.Domain.WebUI;
using BookingSystem.Domain.WebUI.Filters;
using BookingSystem.Domain.WebUI.Hotel;
using BookingSystem.Service.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace BookingSystem.Service.Services
{
    public class HotelTypeService : ServiceBase
    {
        #region HotelTypes

        public ServiceResultModel<HotelTypeVM> GetHotelType(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            HotelTypeVM currentItem = null;
            using (EFBookingContext context = new EFBookingContext())
            {
                currentItem = context.HotelTypes.FirstOrDefault(p => p.Id == id).MapToViewModel<HotelTypeVM>();
            }

            return ServiceResultModel<HotelTypeVM>.OK(currentItem);
        }

        public ServiceResultModel<List<HotelTypeVM>> GetAllHotelTypes(HotelTypeFilter filter)
        {
            List<HotelTypeVM> resultList = new List<HotelTypeVM>();

            using (EFBookingContext context = new EFBookingContext())
            {
                IQueryable<HotelType> hotelTypeList = context.HotelTypes;

                if (filter.Title.IsNotNull())
                {
                    hotelTypeList = hotelTypeList.Where(p => p.Title.Contains(filter.Title));
                }

                if (filter.IsActive.HasValue && filter.IsActive.Value)
                {
                    hotelTypeList = hotelTypeList.Where(p => p.IsActive == filter.IsActive);
                }

                hotelTypeList.ToList().ForEach(p =>
                {
                    resultList.Add(p.MapToViewModel<HotelTypeVM>());
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
                {
                    return new ServiceResultModel<HotelTypeVM>
                    {
                        Code = ServiceResultCode.Duplicate,
                        Data = null,
                        ResultType = OperationResultType.Warn,
                        Message = "This record already exists"
                    };
                }

                var recordItem = context.HotelTypes.Add(model.MapToEntityModel<HotelType>());
                context.SaveChanges();

                return ServiceResultModel<HotelTypeVM>.OK(recordItem.MapToViewModel<HotelTypeVM>());
            }
        }

        public ServiceResultModel<HotelTypeVM> UpdateHotelType(HotelTypeVM model)
        {
            using (EFBookingContext context = new EFBookingContext())
            {
                var currentItem = context.HotelTypes.FirstOrDefault(p => p.Id == model.Id);
                if (currentItem != null)
                {
                    // mevcut kayıt haricinde title ile aynı kayıt olamaz kontrol ediyoruz
                    if (context.HotelTypes.Any(p => p.Id != model.Id && p.Title.Equals(model.Title)))
                    {
                        return new ServiceResultModel<HotelTypeVM>
                        {
                            Code = ServiceResultCode.Duplicate,
                            Data = currentItem.MapToViewModel<HotelTypeVM>(),
                            ResultType = OperationResultType.Warn,
                            Message = "This title using other records "
                        };
                    }
                    currentItem.Title = model.Title;
                    currentItem.Description = model.Description;

                    context.Entry<HotelType>(currentItem).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                }

                return ServiceResultModel<HotelTypeVM>.OK(currentItem.MapToViewModel<HotelTypeVM>());
            }
        }

        public ServiceResultModel<HotelTypeVM> DeleteHotelType(int id)
        {
            using (EFBookingContext context = new EFBookingContext())
            {
                var deleteItem = context.HotelTypes.FirstOrDefault(p => p.Id == id);
                context.HotelTypes.Remove(deleteItem);
                context.SaveChanges();

                return ServiceResultModel<HotelTypeVM>.OK(deleteItem.MapToViewModel<HotelTypeVM>());
                /*
                 veya bu şeklide de yazabilirsiniz.
                 EF Tracking sistemi ile çalışır. 2. kodda tracking 'e deleteıtem kaydının Hoteltype tablosunda deleted olarak Track'lendiğini bildiriyoruz.
                 sonrasında commit 'de ilgili kayıt silinir.

                var deleteItem = context.HotelTypes.FirstOrDefault(p => p.Id == id);
                context.Entry<HotelType>(deleteItem).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();

                 */
            }
        }

        #endregion HotelTypes
    }
}