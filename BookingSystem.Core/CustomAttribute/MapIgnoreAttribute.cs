using System;

namespace BookingSystem.Core.CustomAttribute
{
    /// <summary>
    /// Custom Ignore attribute
    /// Attribute Name'e bakılarak property'ler Ignore edilir.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MapIgnoreAttribute : Attribute
    {
    }
}