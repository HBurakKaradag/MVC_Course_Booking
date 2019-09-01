using BookingSystem.Core.Extensions;
using System;
using System.Globalization;
using System.Web.Mvc;

namespace BookingSystem.WebUI.Models
{
    public class DateTimeModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var cultureInfo = controllerContext.HttpContext.Request.Cookies["cultureInfo"];
            var currentCulcure = "tr-TR";
            if (cultureInfo != null)
                currentCulcure = cultureInfo.Value;

            var valueProvider = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (valueProvider == null)
                return null;
            if (valueProvider.AttemptedValue.IsNull())
                return null;
            //var parseResult = DateTime.TryParse(valueProvider.AttemptedValue, new CultureInfo(currentCulcure), DateTimeStyles.None, out dateTime);
            //if (parseResult)
            //    return dateTime;
            DateTime dateTime;
            if (DateTime.TryParse(valueProvider.AttemptedValue, new CultureInfo(currentCulcure), DateTimeStyles.None, out dateTime))
                return dateTime;

            bindingContext.ModelState.AddModelError(bindingContext.ModelName, "Invalid Date");

            return null;
        }
    }
}