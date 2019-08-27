using BookingSystem.Domain.WebUI;
using BookingSystem.Domain.WebUI.Attributes;
using BookingSystem.Domain.WebUI.Filters;
using BookingSystem.Service.Services;
using BookingSystem.WebUI.Models.DataTableRequest;
using BookingSystem.WebUI.Models.DataTableResponse;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BookingSystem.WebUI.Controllers
{
    [Authorize]
    public class AttributeController : ControllerBase
    {
        private readonly AttributeService _attributeService;

        public AttributeController()
        {
            _attributeService = new AttributeService();
        }

        [HttpGet]
        public ActionResult AttributeList()
        {
            return View(new AttributeFilter());
        }

        [HttpPost]
        public JsonResult GetAttributeList(DataTableRequest<AttributeFilter> model)
        {
            int page = model.start;
            int rowsPerPage = model.length;

            ServiceResultModel<List<AttributesVM>> serviceResult = _attributeService.GetAllAttributeList(model.FilterRequest);
            var gridRecords = serviceResult.Data.Skip(page).Take(rowsPerPage).ToList();
            DataTablesResponse tableResult = new DataTablesResponse(model.draw, gridRecords, serviceResult.Data.Count, serviceResult.Data.Count);

            return Json(tableResult, JsonRequestBehavior.AllowGet);
        }
    }
}