using BookingSystem.Core.Extensions;
using BookingSystem.Data.Context;
using BookingSystem.Domain.Entity;
using BookingSystem.Domain.WebUI;
using BookingSystem.Domain.WebUI.Attributes;
using BookingSystem.Domain.WebUI.Filters;
using BookingSystem.Service.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingSystem.Service.Services
{
    public class AttributeService : ServiceBase
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

        //public ServiceResultModel<List<HotelTypeVM>> GetAllHotelTypes(HotelTypeFilter filter)
        //{
        //    List<HotelTypeVM> resultList = new List<HotelTypeVM>();

        //    using (EFBookingContext context = new EFBookingContext())
        //    {
        //        IQueryable<HotelType> hotelTypeList = context.HotelTypes;

        //        if (filter.Title.IsNotNull())
        //            hotelTypeList = hotelTypeList.Where(p => p.Title.Contains(filter.Title));

        //        if (filter.IsActive.HasValue && filter.IsActive.Value)
        //            hotelTypeList = hotelTypeList.Where(p => p.IsActive == filter.IsActive);

        //        hotelTypeList.ToList().ForEach(p =>
        //        {
        //            resultList.Add(p.MapProperties<HotelTypeVM>());
        //        });

        //        return ServiceResultModel<List<HotelTypeVM>>.OK(resultList);
        //    }
        //}

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

        //        context.HotelTypes.Add(model.MapProperties<HotelType>());
        //        context.SaveChanges();

        //        return ServiceResultModel<HotelTypeVM>.OK(model);
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
        public ServiceResultModel<List<AttributesVM>> GetAllAttributeList(AttributeFilter filter)
        {
            List<AttributesVM> resultList = new List<AttributesVM>();

            using (EFBookingContext context = new EFBookingContext())
            {
                IEnumerable<Attributes> attributeList = context.Attributes;

                if (filter.Name.IsNotNull())
                    attributeList = attributeList.Where(p => p.Name.Equals(filter.Name));

                // Foreach çalışırken listedeki elemanları sırasıyla her sefer 1 eleman şeklinde döner.
                // Paralel üzerinde foreach çalıştırılırsa sıra bağımsız option varsa option init'e göre
                // işlemi çok daha kısa sürede tamamlar.
                // Paralelde dikkat edilmesi gereken en önemli konu aynı kayıda birden fazla thread'ın erişip
                // aynı kaydı set edebilme durumudur. Microsoft buna garanti vermiyor.
                // burada Item Lock ile kitlenebilir veya concurency ( thread-safe ) list kullanılablir

                // https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.paralleloptions.maxdegreeofparallelism?redirectedfrom=MSDN&view=netframework-4.8#System_Threading_Tasks_ParallelOptions_MaxDegreeOfParallelism
                // link'i inceleyiniz.

                var syncLockObj = new object();
                Parallel.ForEach(attributeList,
                     new ParallelOptions { MaxDegreeOfParallelism = 4 },
                     currentAttribute =>
                     {
                         lock (syncLockObj)
                         {
                             resultList.Add(currentAttribute.MapProperties<AttributesVM>());
                         }
                     });
            }

            return ServiceResultModel<List<AttributesVM>>.OK(resultList);
        }
    }
}