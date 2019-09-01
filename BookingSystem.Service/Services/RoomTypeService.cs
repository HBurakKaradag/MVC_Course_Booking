using BookingSystem.Core;
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
        public ServiceResultModel<RoomTypeVM> GetRoomType(int id)
        {
            if (id <= 0)
                return null;
            RoomTypeVM currentItem = null;
            using (EFBookingContext context = new EFBookingContext())
            {
                currentItem = context.RoomTypes.FirstOrDefault(p => p.Id == id).MapProperties<RoomTypeVM>();
            }

            return ServiceResultModel<RoomTypeVM>.OK(currentItem);
        }

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

        public ServiceResultModel<RoomTypeVM> SaveRoomType(RoomTypeVM model)
        {
            using (EFBookingContext context = new EFBookingContext())
            {
                bool isAlreadyExists = context.RoomTypes.Any(p => p.Title == model.Title);
                if (isAlreadyExists)
                    return new ServiceResultModel<RoomTypeVM>
                    {
                        Code = ServiceResultCode.Duplicate,
                        Data = null,
                        ResultType = OperationResultType.Warn,
                        Message = "This record already exists"
                    };

                var recordItem = context.RoomTypes.Add(model.MapProperties<RoomType>());
                context.SaveChanges();

                return ServiceResultModel<RoomTypeVM>.OK(recordItem.MapProperties<RoomTypeVM>());
            }
        }

        public ServiceResultModel<RoomTypeVM> UpdateRoomType(RoomTypeVM model)
        {
            using (EFBookingContext context = new EFBookingContext())
            {
                var currentItem = context.RoomTypes.FirstOrDefault(p => p.Id == model.Id);
                if (currentItem != null)
                {
                    // mevcut kayıt haricinde title ile aynı kayıt olamaz kontrol ediyoruz
                    if (context.RoomTypes.Any(p => p.Id != model.Id && p.Title.Equals(model.Title)))
                    {
                        return new ServiceResultModel<RoomTypeVM>
                        {
                            Code = ServiceResultCode.Duplicate,
                            Data = currentItem.MapProperties<RoomTypeVM>(),
                            ResultType = OperationResultType.Warn,
                            Message = "This title using other records "
                        };
                    }
                    currentItem.Title = model.Title;
                    currentItem.Description = model.Description;

                    context.Entry<RoomType>(currentItem).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                }

                return ServiceResultModel<RoomTypeVM>.OK(currentItem.MapProperties<RoomTypeVM>());
            }
        }

        public ServiceResultModel<RoomTypeVM> DeleteHotelType(int id)
        {
            using (EFBookingContext context = new EFBookingContext())
            {
                var deleteItem = context.RoomTypes.FirstOrDefault(p => p.Id == id);
                context.RoomTypes.Remove(deleteItem);
                context.SaveChanges();
                return ServiceResultModel<RoomTypeVM>.OK(deleteItem.MapProperties<RoomTypeVM>());
            }
        }
    }
}