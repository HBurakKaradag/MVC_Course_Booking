using BookingSystem.Core.CustomAttribute;
using BookingSystem.Domain.WebUI.Account;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace BookingSystem.WebUI.Filters.ActionFilters
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class AuditLogAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            bool checkIsIgnore = filterContext.ActionDescriptor.IsDefined(typeof(AuditLogIgnore), true);
            if (checkIsIgnore)
                return;
            string controllerName = string.Empty;
            string actionName = string.Empty;
            var sessionId = filterContext.HttpContext.Session.SessionID;
            var routeData = filterContext.RouteData.Values;
            if (routeData != null && routeData.Any())
            {
                actionName = routeData["action"].ToString();
                controllerName = routeData["controller"].ToString();
            }

            DateTime logTime = filterContext.HttpContext.Timestamp;

            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                var userData = ((FormsIdentity)filterContext.HttpContext.User.Identity).Ticket?.UserData;
                var userVMObj = JsonConvert.DeserializeObject<UserVM>(userData);

#if DEBUG
                // debug stuff goes here
#else

                // Log yazdım
                Task.Run(() =>
                {
                    AuditLogService auditLogService = new AuditLogService();
                    auditLogService.SaveAuditLog(new AuditLogVM
                    {
                        ActionName = actionName,
                        ControllerName = controllerName,
                        SessionId = sessionId,
                        CreateDateTime = logTime,
                        UserDataJson = userData,
                        LoginName = userVMObj.Name
                    });
                });
#endif
            }
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
        }
    }
}