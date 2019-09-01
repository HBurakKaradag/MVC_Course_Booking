using System;
using System.Web.Mvc;

namespace BookingSystem.WebUI.Controllers
{
    [Authorize]
    public class ErrorController : ControllerBase
    {
        public ActionResult Http404Error(Exception ex)
        {
            return View();
        }

        public ActionResult Http500Error(Exception ex)
        {
            return View();
        }
    }
}