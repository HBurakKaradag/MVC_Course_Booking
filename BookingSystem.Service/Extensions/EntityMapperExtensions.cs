using BookingSystem.Core.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BookingSystem.Service.Extensions
{
    public static class EntityMapperExtensions
    {
        private static void MatchAndMap<TSource, TDestination>(this TSource source, TDestination destination)
               where TSource : class, new()
               where TDestination : class, new()
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
                catch (Exception ex)
                {
                }
            }
        }

        public static TDestination MapProperties<TDestination>(this object source)
            where TDestination : class, new()
        {
            var destination = Activator.CreateInstance<TDestination>();
            MatchAndMap(source, destination);
            return destination;
        }
    }
}