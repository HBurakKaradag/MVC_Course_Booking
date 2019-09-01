using BookingSystem.Core.CustomException;
using BookingSystem.WebUI.Controllers;
using BookingSystem.WebUI.Models;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace BookingSystem.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ModelBinders.Binders.Add(typeof(DateTime), new DateTimeModelBinder());
            ModelBinders.Binders.Add(typeof(DateTime?), new DateTimeModelBinder());
        }

        protected void Application_Error(object sernder, EventArgs e)
        {
            const string controllerName = "Error";
            string actionName = string.Empty;
            Exception ex = Server.GetLastError();
            Response.Clear();
            Server.ClearError();

            HttpException httpException = ex as HttpException;

            if (httpException != null)
            {
                var httpStatusCode = httpException.GetHttpCode();
                actionName = httpStatusCode == 404 ? "Http404Error" :
                             httpStatusCode == 500 ? "Http500Error" : "Error";
            }
            else if (ex.GetType() == typeof(DatabaseException) || ex.InnerException.GetType() == typeof(DatabaseException))
            {
            }

            RouteData routeData = new RouteData();

            routeData.Values.Add("Controller", controllerName);
            routeData.Values.Add("action", actionName);
            routeData.Values.Add("Exception", ex);

            IController errorController = new ErrorController();
            errorController.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
        }
    }
}