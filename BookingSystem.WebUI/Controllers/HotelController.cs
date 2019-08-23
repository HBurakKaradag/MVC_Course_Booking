using BookingSystem.Domain.WebUI.Filters;
using BookingSystem.Service.Services;
using BookingSystem.WebUI.Models.DataTableRequest;
using BookingSystem.WebUI.Models.DataTableResponse;
using System.Linq;
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

        public JsonResult GetHotelTypeList(DataTableRequest<HotelTypeFilter> model)
        {
            var result = _hotelTypeService.GetHotelTypes(model.FilterRequest);
            result.Data.Skip(model.start).Take(model.length);

            DataTablesResponse tableResult = new DataTablesResponse(model.draw, result.Data, result.Data.Count, result.Data.Count);

            return Json(tableResult, JsonRequestBehavior.AllowGet);
        }
    }
}