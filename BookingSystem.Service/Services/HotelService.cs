﻿using BookingSystem.Core.Extensions;
using BookingSystem.Data.Context;
using BookingSystem.Domain.Entity;
using BookingSystem.Domain.WebUI;
using BookingSystem.Domain.WebUI.Filters;
using BookingSystem.Domain.WebUI.Hotel;
using BookingSystem.Service.Extensions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;

namespace BookingSystem.Service.Services
{
    public class HotelService : ServiceBase
    {
        #region Hotels

        public ServiceResultModel<List<HotelDefinitionVM>> GetHotels(HotelFilter filter)
        {
            List<HotelDefinitionVM> resultList = new List<HotelDefinitionVM>();

            using (EFBookingContext context = new EFBookingContext())
            {
                IQueryable<HotelDefinition> hotelDefinitionList = context.HotelDefinitions;

                if (filter.HotelName.IsNotNull())
                {
                    hotelDefinitionList = hotelDefinitionList.Where(p => p.Title.Contains(filter.HotelName));
                }

                if (filter.HotelTypeId > 0)
                {
                    hotelDefinitionList = hotelDefinitionList.Where(p => p.HotelTypeId == filter.HotelTypeId);
                }

                hotelDefinitionList.ToList().ForEach(p =>
                {
                    resultList.Add(p.MapToViewModel<HotelDefinitionVM>());
                });

                return ServiceResultModel<List<HotelDefinitionVM>>.OK(resultList);
            }
        }

        public ServiceResultModel<HotelDefinitionVM> GetHotel(int id)
        {
            HotelDefinitionVM hotel = null;
            using (EFBookingContext context = new EFBookingContext())
            {
                //context.HotelDefinitions.Include("HotelAttribute");
                var hotelEntity = context.HotelDefinitions.FirstOrDefault(p => p.Id == id);
                hotel = hotelEntity.MapToViewModel<HotelDefinitionVM>();
                hotel.HotelAttributes = hotelEntity.HotelAttributes.Select(p => p.MapToViewModel<HotelAttributeVM>()).ToList();
                hotel.HotelRooms = hotelEntity.HotelRooms.Select(p => p.MapToViewModel<HotelRoomVM>()).ToList();
            }

            return ServiceResultModel<HotelDefinitionVM>.OK(hotel);
        }

        public ServiceResultModel<bool> SaveHotel(HotelDefinitionVM model)
        {
            using (EFBookingContext context = new EFBookingContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var deletedAttributes = context.HotelAttributes.Where(p => p.HotelId == model.Id);
                        context.HotelAttributes.RemoveRange(deletedAttributes);

                        HotelDefinition hotel = new HotelDefinition();
                        hotel = model.MapToEntityModel<HotelDefinition>();

                        context.HotelDefinitions.Add(hotel);
                        var savedHotel = context.SaveChanges();

                        foreach (var item in model.Attributes.Where(p => p.IsSelected))
                        {
                            context.HotelAttributes.Add(new HotelAttribute
                            {
                                AttributeId = item.Id,
                                HotelId = hotel.Id,
                                IsActive = true,
                                IsDeleted = false,
                                CreateDate = DateTime.Now
                            });
                            // throw new Exception();
                        }

                        context.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }

            return ServiceResultModel<bool>.OK(true);
        }

        #endregion Hotels

        #region HotelRooms

        public ServiceResultModel<List<HotelRoomVM>> GetHotelRooms(HotelRoomFilter filterRequest)
        {
            List<HotelRoomVM> hotelRooms = new List<HotelRoomVM>();
            using (EFBookingContext context = new EFBookingContext())
            {
                var parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@HotelId", filterRequest.HotelId);

                hotelRooms = context.Database
                    .SqlQuery<HotelRoomVM>("GetHotelRooms @HotelId", parameters)
                    .ToList();
            }

            return ServiceResultModel<List<HotelRoomVM>>.OK(hotelRooms);
        }

        public ServiceResultModel<int> SaveHotelRoom(HotelRoomVM hotel, string mapPath)
        {
            using (EFBookingContext context = new EFBookingContext())
            {
                int result = default(int);
                string savePath = string.Empty;
                try
                {
                    HttpPostedFileBase file = hotel.RoomImage;
                    if (file != null && file.ContentLength > 0)
                    {
                        string fileExt = Path.GetExtension(file.FileName);
                        string fileName = string.Format("{0}_{1}{2}", Path.GetFileNameWithoutExtension(file.FileName), DateTime.Now.Ticks, fileExt);
                        savePath = Path.Combine(mapPath, fileName);
                        file.SaveAs(savePath);
                    }

                    context.HotelRooms.Add(new HotelRoom
                    {
                        HotelId = hotel.HotelId,
                        ImageUrl = savePath,
                        MaxCapacity = hotel.RoomCapacity,
                        Price = hotel.Price,
                        RoomTypeId = hotel.RoomTypeId,
                        CreateDate = DateTime.Now,
                        IsActive = true,
                        IsDeleted = false
                    });

                    result = context.SaveChanges();
                }
                catch (Exception)
                {
                    if (File.Exists(savePath))
                    {
                        File.Delete(savePath);
                    }

                    // error ekle
                }

                return ServiceResultModel<int>.OK(result);
            }
        }

        #endregion HotelRooms

        #region TestFile

        public ServiceResultModel<bool> SaveTestFile(HotelTestFileVM model, string mapPath)
        {
            string fileName = string.Empty;
            string fileExtension = string.Empty;

            if (model != null)
            {
                HttpPostedFileBase file = model.HotelImage;

                fileName = Path.GetFileNameWithoutExtension(file.FileName);
                fileExtension = Path.GetExtension(file.FileName);

                string fullName = Path.Combine(mapPath, $"{fileName}_{DateTime.Now.Ticks}{fileExtension}");

                file.SaveAs(fullName);
            }

            return ServiceResultModel<bool>.OK(true);
        }

        #endregion TestFile
    }
}