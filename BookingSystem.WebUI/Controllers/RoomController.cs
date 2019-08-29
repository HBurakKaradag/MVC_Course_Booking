using BookingSystem.Domain.WebUI;
using BookingSystem.Domain.WebUI.Filters;
using BookingSystem.Domain.WebUI.Room;
using BookingSystem.Service.Services;
using BookingSystem.WebUI.Models.DataTableRequest;
using BookingSystem.WebUI.Models.DataTableResponse;
using System.Linq;
using System.Web.Mvc;

namespace BookingSystem.WebUI.Controllers
{
    [Authorize]
    public class RoomController : ControllerBase
    {
        private readonly RoomTypeService _roomTypeService;

        public RoomController()
        {
            _roomTypeService = new RoomTypeService();
        }

        #region RoomTypesMethods

        public ActionResult RoomTypeList()
        {
            return View(new RoomTypeFilter());
        }

        [ActionName("RoomTypeAddEdit")]
        public PartialViewResult _RoomTypeAddEdit(int? id)
        {
            RoomTypeVM roomTypeVm = null;

            if (id.HasValue)
            {
                ServiceResultModel<RoomTypeVM> serviceResult = _roomTypeService.GetRoomType(id.Value);
                if (serviceResult.IsSuccess)
                    roomTypeVm = serviceResult.Data;
            }

            roomTypeVm = roomTypeVm ?? new RoomTypeVM();

            return PartialView("_RoomTypeAddEdit", roomTypeVm);
        }

        public JsonResult GetRoomTypeList(DataTableRequest<RoomTypeFilter> model)
        {
            var page = model.start;
            var rowsPerPage = model.length;

            var filteredData = _roomTypeService.GetAllRoomTypes(model.FilterRequest);
            var gridPageRecord = filteredData.Data.Skip(page).Take(rowsPerPage).ToList();

            DataTablesResponse tableResult = new DataTablesResponse(model.draw, gridPageRecord, filteredData.Data.Count, filteredData.Data.Count);

            return Json(tableResult, JsonRequestBehavior.AllowGet);
        }

        #endregion RoomTypesMethods

        //#region HotelTypesMethods

        //#region List-HotelType

        ///// <summary>
        ///// HotelTypesList Action
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet]
        //public ActionResult HotelTypeList()
        //{
        //    return View();
        //}

        ///// <summary>
        ///// HotelTypeList Action Grid Fill Method
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        //public JsonResult GetHotelTypeList(DataTableRequest<HotelTypeFilter> model)
        //{
        //    var page = model.start;
        //    var rowsPerPage = model.length;

        //    var filteredData = _hotelTypeService.GetAllHotelTypes(model.FilterRequest);
        //    var gridPageRecord = filteredData.Data.Skip(page).Take(rowsPerPage).ToList();

        //    DataTablesResponse tableResult = new DataTablesResponse(model.draw, gridPageRecord, filteredData.Data.Count, filteredData.Data.Count);

        //    return Json(tableResult, JsonRequestBehavior.AllowGet);
        //}

        //#endregion List-HotelType

        //#region AddEdit-HotelType

        //[HttpGet]
        //public ActionResult HotelTypeAdd()
        //{
        //    return View(new HotelTypeVM());
        //}

        //[HttpGet]
        //public ActionResult HotelTypeEdit(int id)
        //{
        //    var model = _hotelTypeService.GetHotelType(id);
        //    if (model == null)
        //        RedirectToAction(nameof(HotelTypeList));

        //    return View(model.Data);
        //}

        //[HttpPost]
        //public JsonResult SaveHotelType(HotelTypeVM model)
        //{
        //    if (!ModelState.IsValid)
        //        return base.JSonModelStateHandle();

        //    ServiceResultModel<HotelTypeVM> serviceResult = _hotelTypeService.SaveHotelType(model);

        //    if (!serviceResult.IsSuccess)
        //    {
        //        base.UIResponse = new UIResponse
        //        {
        //            Message = string.Format("Operation Is Not Completed, {0}", serviceResult.Message),
        //            ResultType = serviceResult.ResultType,
        //            Data = serviceResult.Data
        //        };
        //    }
        //    else
        //    {
        //        base.UIResponse = new UIResponse
        //        {
        //            Data = serviceResult.Data,
        //            ResultType = serviceResult.ResultType,
        //            Message = "Success"
        //        };
        //    }

        //    return Json(base.UIResponse, JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost]
        //public JsonResult DeleteHotelType(int id)
        //{
        //    if (id <= 0)
        //        return Json(base.UIResponse = new UIResponse
        //        {
        //            ResultType = Core.OperationResultType.Error,
        //            Message = string.Format("id is not valid, this Id = {0}", id)
        //        }, JsonRequestBehavior.AllowGet);

        //    ServiceResultModel<HotelTypeVM> serviceResult = _hotelTypeService.DeleteHotelType(id);
        //    return Json(base.UIResponse = new UIResponse
        //    {
        //        ResultType = serviceResult.ResultType,
        //        Data = serviceResult.Data,
        //        Message = serviceResult.ResultType == Core.OperationResultType.Success ? "Record Deleted Successfully" : string.Format("Warning.. {0}", serviceResult.Message)
        //    });
        //}

        //[HttpPost]
        //public JsonResult UpdateHotelType(HotelTypeVM model)
        //{
        //    if (model.Id <= 0)
        //        RedirectToAction(nameof(HotelTypeList)); // ErrorHandle eklenecek

        //    if (!ModelState.IsValid)
        //        return base.JSonModelStateHandle();

        //    ServiceResultModel<HotelTypeVM> serviceResult = _hotelTypeService.UpdateHotelType(model);

        //    if (!serviceResult.IsSuccess)
        //    {
        //        base.UIResponse = new UIResponse
        //        {
        //            Message = string.Format("Operation Is Not Completed, {0}", serviceResult.Message),
        //            ResultType = serviceResult.ResultType,
        //            Data = serviceResult.Data
        //        };
        //    }
        //    else
        //    {
        //        base.UIResponse = new UIResponse
        //        {
        //            Data = serviceResult.Data,
        //            ResultType = serviceResult.ResultType,
        //            Message = "Success"
        //        };
        //    }

        //    return Json(base.UIResponse, JsonRequestBehavior.AllowGet);
        //}

        //#endregion AddEdit-HotelType

        //#endregion HotelTypesMethods

        //#region HotelRoomTypesMethods

        ////public ActionResult HotelRoomTypeList()
        ////{
        ////    return View();
        ////}

        //#endregion HotelRoomTypesMethods
    }
}