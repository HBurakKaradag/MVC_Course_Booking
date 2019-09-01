using BookingSystem.Domain.WebUI;
using BookingSystem.Domain.WebUI.Filters;
using BookingSystem.Domain.WebUI.Room;
using BookingSystem.Service.Services;
using BookingSystem.WebUI.Models.DataTableRequest;
using BookingSystem.WebUI.Models.DataTableResponse;
using BookingSystem.WebUI.Models.Response;
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

        [HttpPost]
        public JsonResult DeleteRoomType(int id)
        {
            if (id <= 0)
                return Json(base.UIResponse = new UIResponse
                {
                    ResultType = Core.OperationResultType.Error,
                    Message = string.Format("id is not valid, this Id = {0}", id)
                }, JsonRequestBehavior.AllowGet);

            ServiceResultModel<RoomTypeVM> serviceResult = _roomTypeService.DeleteHotelType(id);
            return Json(base.UIResponse = new UIResponse
            {
                ResultType = serviceResult.ResultType,
                Data = serviceResult.Data,
                Message = serviceResult.ResultType == Core.OperationResultType.Success ? "Record Deleted Successfully" : string.Format("Warning.. {0}", serviceResult.Message)
            });
        }

        [HttpPost]
        public JsonResult SaveRoomType(RoomTypeVM model)
        {
            if (!ModelState.IsValid)
                return base.JSonModelStateHandle();

            ServiceResultModel<RoomTypeVM> serviceResult = _roomTypeService.SaveRoomType(model);

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
        public JsonResult UpdateRoomType(RoomTypeVM model)
        {
            if (model.Id <= 0)
                RedirectToAction(nameof(RoomTypeList)); // ErrorHandle eklenecek

            if (!ModelState.IsValid)
                return base.JSonModelStateHandle();

            ServiceResultModel<RoomTypeVM> serviceResult = _roomTypeService.UpdateRoomType(model);

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
    }
}