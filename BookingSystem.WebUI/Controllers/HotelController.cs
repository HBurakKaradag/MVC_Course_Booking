using BookingSystem.Service.Services;
using System.Web.Helpers;
using System.Web.Mvc;

namespace BookingSystem.WebUI.Controllers
{
    [Authorize]
    public class HotelController : ControllerBase
    {
        private readonly HotelTypeService _hotelTypeService;

        public HotelController()
        {
            _hotelTypeService = new HotelTypeService();
        }

        public ActionResult HotelTypeList()
        {
            return View();
        }

        public JsonResult GetHotelTypeList()
        {
            return Json("");
        }
    }
}