using BookingSystem.Domain.WebUI;
using BookingSystem.Domain.WebUI.Filters;
using BookingSystem.Domain.WebUI.Hotel;
using BookingSystem.Service.Services;
using BookingSystem.WebUI.Models.DataTableRequest;
using BookingSystem.WebUI.Models.DataTableResponse;
using BookingSystem.WebUI.Models.Response;
using System.Linq;
using System.Web.Mvc;

namespace BookingSystem.WebUI.Controllers
{
    [Authorize]
    public class HotelController : ControllerBase
    {
        private readonly HotelTypeService _hotelTypeService;
        private readonly HotelDefinitionService _hotelDefinitionService;

        public HotelController()
        {
            _hotelTypeService = new HotelTypeService();
            _hotelDefinitionService = new HotelDefinitionService();
        }

        #region HotelTypesMethods

        #region List-HotelType

        /// <summary>
        /// HotelTypesList Action
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult HotelTypeList()
        {
            return View();
        }

        /// <summary>
        /// HotelTypeList Action Grid Fill Method
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonResult GetHotelTypeList(DataTableRequest<HotelTypeFilter> model)
        {
            var page = model.start;
            var rowsPerPage = model.length;

            var filteredData = _hotelTypeService.GetAllHotelTypes(model.FilterRequest);
            var gridPageRecord = filteredData.Data.Skip(page).Take(rowsPerPage).ToList();

            DataTablesResponse tableResult = new DataTablesResponse(model.draw, gridPageRecord, filteredData.Data.Count, filteredData.Data.Count);

            return Json(tableResult, JsonRequestBehavior.AllowGet);
        }

        #endregion List-HotelType

        #region AddEdit-HotelType

        [HttpGet]
        public ActionResult HotelTypeAdd()
        {
            return View(new HotelTypeVM());
        }

        [HttpGet]
        public ActionResult HotelTypeEdit(int id)
        {
            var model = _hotelTypeService.GetHotelType(id);
            if (model == null)
                RedirectToAction(nameof(HotelTypeList));

            return View(model.Data);
        }

        [HttpPost]
        public JsonResult SaveHotelType(HotelTypeVM model)
        {
            if (!ModelState.IsValid)
                return base.JSonModelStateHandle();

            ServiceResultModel<HotelTypeVM> serviceResult = _hotelTypeService.SaveHotelType(model);

            if (!serviceResult.IsSuccess)
            {
                base.UIResponse = new UIResponse
                {
                    Message = string.Format("Operation Is Not Completed, {0}", serviceResult.Message),
                    ResultType = serviceResult.ResultType,
                    Data = serviceResult.Data
                };
            }
            else
            {
                base.UIResponse = new UIResponse
                {
                    Data = serviceResult.Data,
                    ResultType = serviceResult.ResultType,
                    Message = "Success"
                };
            }

            return Json(base.UIResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteHotelType(int id)
        {
            if (id <= 0)
                return Json(base.UIResponse = new UIResponse
                {
                    ResultType = Core.OperationResultType.Error,
                    Message = string.Format("id is not valid, this Id = {0}", id)
                }, JsonRequestBehavior.AllowGet);

            ServiceResultModel<HotelTypeVM> serviceResult = _hotelTypeService.DeleteHotelType(id);
            return Json(base.UIResponse = new UIResponse
            {
                ResultType = serviceResult.ResultType,
                Data = serviceResult.Data,
                Message = serviceResult.ResultType == Core.OperationResultType.Success ? "Record Deleted Successfully" : string.Format("Warning.. {0}", serviceResult.Message)
            });
        }

        [HttpPost]
        public JsonResult UpdateHotelType(HotelTypeVM model)
        {
            if (model.Id <= 0)
                RedirectToAction(nameof(HotelTypeList)); // ErrorHandle eklenecek

            if (!ModelState.IsValid)
                return base.JSonModelStateHandle();

            ServiceResultModel<HotelTypeVM> serviceResult = _hotelTypeService.UpdateHotelType(model);

            if (!serviceResult.IsSuccess)
            {
                base.UIResponse = new UIResponse
                {
                    Message = string.Format("Operation Is Not Completed, {0}", serviceResult.Message),
                    ResultType = serviceResult.ResultType,
                    Data = serviceResult.Data
                };
            }
            else
            {
                base.UIResponse = new UIResponse
                {
                    Data = serviceResult.Data,
                    ResultType = serviceResult.ResultType,
                    Message = "Success"
                };
            }

            return Json(base.UIResponse, JsonRequestBehavior.AllowGet);
        }

        #endregion AddEdit-HotelType

        #endregion HotelTypesMethods

        #region HotelRoomTypesMethods

        public ActionResult HotelRoomTypeList()
        {
            return View();
        }

        #endregion HotelRoomTypesMethods

        [HttpGet]
        public ActionResult HotelDefinitionList()
        {
            var hoteltypes = _hotelTypeService.GetAllHotelTypes(new HotelTypeFilter()).Data
                                                                                      .Select(p => new SelectListItem
                                                                                      {
                                                                                          Value = p.Id.ToString(),
                                                                                          Text = p.Title,
                                                                                          Selected = false
                                                                                      }).ToList().AsEnumerable();
            ViewBag.HotelTypesData = hoteltypes;

            return View();
        }

        public JsonResult GetHotels(DataTableRequest<HotelFilter> model)
        {
            var page = model.start;
            var rowsPerPage = model.length;

            var filteredData = _hotelDefinitionService.GetHotels(model.FilterRequest);
            var gridPageRecord = filteredData.Data.Skip(page).Take(rowsPerPage).ToList();

            DataTablesResponse tableResult = new DataTablesResponse(model.draw, gridPageRecord, filteredData.Data.Count, filteredData.Data.Count);

            return Json(tableResult, JsonRequestBehavior.AllowGet);
        }
    }
}