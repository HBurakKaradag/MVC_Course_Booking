using BookingSystem.Domain.WebUI;
using BookingSystem.Domain.WebUI.Attributes;
using BookingSystem.Domain.WebUI.Filters;
using BookingSystem.Service.Services;
using BookingSystem.WebUI.Models.DataTableRequest;
using BookingSystem.WebUI.Models.DataTableResponse;
using BookingSystem.WebUI.Models.Response;
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
        public JsonResult SaveAttribute(AttributeVM model)
        {
            if (!ModelState.IsValid)
                return base.JSonModelStateHandle();

            ServiceResultModel<AttributeVM> serviceResult = model.Id <= 0
                ? _attributeService.SaveAttribute(model)
                : _attributeService.UpdateAttribute(model);

            return Json(base.UIResponse = new UIResponse()
            {
                ResultType = serviceResult.ResultType,
                Data = serviceResult.Data,
                Message = serviceResult.Message
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetAttributeList(DataTableRequest<AttributeFilter> model)
        {
            int page = model.start;
            int rowsPerPage = model.length;

            ServiceResultModel<List<AttributeVM>> serviceResult = _attributeService.GetAllAttributeList(model.FilterRequest);
            var gridRecords = serviceResult.Data.Skip(page).Take(rowsPerPage).ToList();
            DataTablesResponse tableResult = new DataTablesResponse(model.draw, gridRecords, serviceResult.Data.Count, serviceResult.Data.Count);

            return Json(tableResult, JsonRequestBehavior.AllowGet);
        }
    }
}