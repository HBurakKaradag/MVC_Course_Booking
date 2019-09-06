using BookingSystem.Core;
using BookingSystem.Domain.WebUI;
using BookingSystem.Domain.WebUI.Definitions;
using BookingSystem.Domain.WebUI.Filters;
using BookingSystem.Domain.WebUI.Hotel;
using BookingSystem.Service.Services;
using BookingSystem.WebUI.Models;
using BookingSystem.WebUI.Models.DataTableRequest;
using BookingSystem.WebUI.Models.DataTableResponse;
using BookingSystem.WebUI.Models.Response;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BookingSystem.WebUI.Controllers
{
    [Authorize]
    public class HotelController : ControllerBase
    {
        private readonly AttributeService _attributeService;
        private readonly DefinitionService _definitionService;
        private readonly HotelTypeService _hotelTypeService;
        private readonly HotelDefinitionService _hotelDefinitionService;

        public HotelController()
        {
            _attributeService = new AttributeService();
            _definitionService = new DefinitionService();
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
            if (!model.IsSuccess)
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

        #region HotelDefinition

        [HttpGet]
        public ActionResult HotelDefinitionList()
        {
            var hoteltypes = _hotelTypeService.GetAllHotelTypes(new HotelTypeFilter()).Data
                                                                                      .Select(p => new BSelectListItem
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

        public JsonResult SaveHotelDefinition(HotelDefinitionVM model)
        {
            if (!ModelState.IsValid)
                return base.JSonModelStateHandle();

            //  ServiceResultModel<HotelDefinitionVM> serviceResult = _hotelDefinitionService.SaveHotel(model);

            return Json("");
        }

        #endregion HotelDefinition

        #region HotelDefinitionAdd

        #region TestSil

        public JsonResult GetDistricts(int cityId)
        {
            List<DistrictDefinitionVM> definitionVMList = new List<DistrictDefinitionVM>();
            var serviceResult = _definitionService.GetDistrictByCityId(cityId);
            if (serviceResult.IsSuccess)
                definitionVMList = serviceResult.Data;

            return Json(definitionVMList, JsonRequestBehavior.AllowGet);
        }

        #endregion TestSil

        public ActionResult HotelDefinitionAddEdit(int? hotelId)
        {
            /// <summary>
            /// View model için Entityden farklı propery'ler içerebileceğini ve view 'a göre düzenlenebileceğinden bahsetmiştik.
            /// HotelTypes datasını ViewBag üzerinden gönderebileceğimiz gibi VM içerisinden de gönderebiliriz.
            /// </summary>


            HotelDefinitionVM hotelDefinition = new HotelDefinitionVM();

            if (hotelId.HasValue)
                hotelDefinition = _hotelDefinitionService.GetHotel(hotelId.Value).Data;

            var allCitiesData = _definitionService.GetCities().Data;
            hotelDefinition.Cities = allCitiesData.Select(p => new BSelectListItem
            {
                ParentValue = "0",
                Value = p.Id.ToString(),
                Text = p.Name,
                Selected = hotelId.HasValue ? hotelDefinition.CityId == p.Id : false
            }).AsEnumerable();

            hotelDefinition.Districts = Enumerable.Empty<BSelectListItem>();

            if (hotelId.HasValue && hotelDefinition?.CityId >0 && hotelDefinition?.DistrictId > 0)
            {
                hotelDefinition.Districts = allCitiesData.SelectMany(p => p.Districts)
                                                         .Where(p => p.CityId == hotelDefinition.CityId)
                                                         .Select(c => new BSelectListItem
                                                         {
                                                             ParentValue = c.CityId.ToString(),
                                                             Value = c.Id.ToString(),
                                                             Text = c.Name,
                                                             Selected = hotelId.HasValue ? hotelDefinition.DistrictId == c.Id : false
                                                         }).AsEnumerable();

            }
              
            hotelDefinition.HotelTypes = _hotelTypeService.GetAllHotelTypes(new HotelTypeFilter()).Data;

            List<CheckBoxListTemplate> attributes = _attributeService.GetAllAttributeList(new AttributeFilter { AttributeType = (int)AttributeType.Hotel }).Data
                                                           .Select(p => new CheckBoxListTemplate
                                                           {
                                                               Id = p.Id,
                                                               Text = p.Name,
                                                               IsSelected = hotelDefinition.Id > 0 && hotelDefinition.HotelAttributes.Any(c => c.AttributeId == p.Id)
                                                           }).ToList();

            hotelDefinition.Attributes = attributes;


            return View(hotelDefinition);
        }

        #endregion HotelDefinitionAdd
    }
}