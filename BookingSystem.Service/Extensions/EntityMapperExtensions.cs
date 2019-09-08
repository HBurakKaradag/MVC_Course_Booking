using BookingSystem.Core.CustomAttribute;
using BookingSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BookingSystem.Service.Extensions
{
    public static class EntityMapperExtensions
    {
        /// <summary>
        /// @Kodluyoruz-MVC-Bootcamp  13.07.2019 – 09.09.2019
        /// H.Burak Karadağ
        /// EntityToVm - VmToEntity convert işlemlerinde kullandığımız Convert sınıfı
        /// Destination ve source sınıfları içerisindeki property'leri Name üzerinden eşleştirir.
        /// Eşleştirme esnasında Ignore edilecek property kontrolleri yapılır.
        /// </summary>
        private static void MatchProp<T, K>(this T source, K destination)
               where T : IDomain
               where K : IDomain
        {
            if (source == null || destination == null)
                return;

            List<PropertyInfo> sourceProp = source.GetType().GetProperties().ToList();
            List<PropertyInfo> destinationProp = destination.GetType().GetProperties().ToList();

            foreach (PropertyInfo sourceProperty in sourceProp)
            {
                PropertyInfo destinationInfo = destinationProp.Find(item => item.Name == sourceProperty.Name);
                if (destinationInfo == null || Attribute.IsDefined(destinationInfo, typeof(MapIgnoreAttribute)))
                    continue;

                try
                {
                    destinationInfo.SetValue(destination, sourceProperty.GetValue(source, null), null);
                }
                catch (Exception)
                {
                    // TODO : Propery map edilemediğinde alınacak aksiyon yazılır.
                }
            }
        }

        /// <summary>
        /// Daha önceki örneğimizde metodun adı MapProperties ve T nesnesi class 'dan türemişti.
        /// Bu durum mimari olarak çok doğru bir yaklaşım olmamıştı.
        /// Bu sınıfın amacı VMToEntity veya EntityToVM converter olmalı, fakat class olarak işaretlendiğinde
        /// oluşturduğumuz herhangi başka bir class  instance'ı için de MapProperties metodunu görebiliyorduk.
        /// Bu nedenle ;
        /// IEntity interface'in implement olduğu bir class için MapToViewModel
        /// IModel  interface'in implement olduğu bir class için MapToEntity methodlarının çalışmaı gerekir.
        /// MapToVm veya MapToEntity metotlarının harici bir class'da işleme girebilme yetileri değişiklikle kaldırıldı.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T MapToViewModel<T>(this IEntity source)
            where T : IModel
        {
            var destination = Activator.CreateInstance<T>();
            MatchProp(source, destination);
            return destination;
        }

        public static T MapToEntityModel<T>(this IModel source)
          where T : IEntity
        {
            var destination = Activator.CreateInstance<T>();
            MatchProp(source, destination);
            return destination;
        }
    }
}