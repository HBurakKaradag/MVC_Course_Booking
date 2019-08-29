using BookingSystem.Core;
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
        public ServiceResultModel<List<AttributeVM>> GetAllAttributeList(AttributeFilter filter)
        {
            List<AttributeVM> resultList = new List<AttributeVM>();

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
                             resultList.Add(currentAttribute.MapProperties<AttributeVM>());
                         }
                     });
            }

            return ServiceResultModel<List<AttributeVM>>.OK(resultList);
        }

        public ServiceResultModel<AttributeVM> SaveAttribute(AttributeVM model)
        {
            using (EFBookingContext context = new EFBookingContext())
            {
                if (context.Attributes.Any(p => p.Name.Equals(model.Name) && p.AttributeType == model.AttributeType))
                    return new ServiceResultModel<AttributeVM>
                    {
                        Code = ServiceResultCode.Duplicate,
                        Data = model,
                        ResultType = OperationResultType.Warn,
                        Message = "This record already exitst "
                    };

                var recordItem = context.Attributes.Add(model.MapProperties<Attributes>());
                context.SaveChanges();

                return ServiceResultModel<AttributeVM>.OK(recordItem.MapProperties<AttributeVM>());
            }
        }

        public ServiceResultModel<AttributeVM> UpdateAttribute(AttributeVM model)
        {
            using (EFBookingContext context = new EFBookingContext())
            {
                var currentItem = context.Attributes.FirstOrDefault(p => p.Id == model.Id);
                if (currentItem != null)
                {
                    if (context.Attributes.Any(p => p.Id != model.Id && (p.Name.Equals(model.Name) && p.AttributeType == model.AttributeType)))
                    {
                        return new ServiceResultModel<AttributeVM>
                        {
                            Code = ServiceResultCode.Duplicate,
                            Data = currentItem.MapProperties<AttributeVM>(),
                            ResultType = OperationResultType.Warn,
                            Message = "This title using other records "
                        };
                    }
                    currentItem.Name = model.Name;
                    currentItem.Description = model.Description;

                    context.Entry<Attributes>(currentItem).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                }

                return ServiceResultModel<AttributeVM>.OK(currentItem.MapProperties<AttributeVM>());
            }
        }
    }
}