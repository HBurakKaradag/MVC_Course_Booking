using BookingSystem.Core;
using BookingSystem.Core.Interfaces;
using BookingSystem.Domain.WebUI;
using BookingSystem.Domain.WebUI.Definitions;
using BookingSystem.Domain.WebUI.Filters;
using BookingSystem.Domain.WebUI.Hotel;
using BookingSystem.Service.Services;
using BookingSystem.WebUI.Models.GridRequest;
using BookingSystem.WebUI.Models.GridResponse;
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
        private readonly HotelService _hotelService;
        private readonly RoomTypeService _roomTypeService;

        public HotelController()
        {
            _attributeService = new AttributeService();
            _definitionService = new DefinitionService();
            _hotelTypeService = new HotelTypeService();
            _hotelService = new HotelService();
            _roomTypeService = new RoomTypeService();
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
        public JsonResult GetHotelTypeList(GridRequest<HotelTypeFilter> model)
        {
            var page = model.start;
            var rowsPerPage = model.length;

            var filteredData = _hotelTypeService.GetAllHotelTypes(model.FilterRequest);
            var gridPageRecord = filteredData.Data.Skip(page).Take(rowsPerPage).ToList();

            GridResponse tableResult = new GridResponse(model.draw, gridPageRecord, filteredData.Data.Count, filteredData.Data.Count);

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
            {
                RedirectToAction(nameof(HotelTypeList));
            }

            return View(model.Data);
        }

        [HttpPost]
        public JsonResult SaveHotelType(HotelTypeVM model)
        {
            if (!ModelState.IsValid)
            {
                return base.JSonModelStateHandle();
            }

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
            {
                return Json(base.UIResponse = new UIResponse
                {
                    ResultType = Core.OperationResultType.Error,
                    Message = string.Format("id is not valid, this Id = {0}", id)
                }, JsonRequestBehavior.AllowGet);
            }

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
            {
                RedirectToAction(nameof(HotelTypeList)); // ErrorHandle eklenecek
            }

            if (!ModelState.IsValid)
            {
                return base.JSonModelStateHandle();
            }

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

        public JsonResult GetHotels(GridRequest<HotelFilter> model)
        {
            var page = model.start;
            var rowsPerPage = model.length;

            var filteredData = _hotelService.GetHotels(model.FilterRequest);
            var gridPageRecord = filteredData.Data.Skip(page).Take(rowsPerPage).ToList();

            GridResponse tableResult = new GridResponse(model.draw, gridPageRecord, filteredData.Data.Count, filteredData.Data.Count);

            return Json(tableResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveHotelDefinition(HotelDefinitionVM model)
        {
            if (!ModelState.IsValid)
            {
                return base.JSonModelStateHandle();
            }

            ServiceResultModel<bool> serviceResult = _hotelService.SaveHotel(model);

            return Json(base.UIResponse = new UIResponse
            {
                Message = string.Format("Operation Is Completed"),
                ResultType = serviceResult.ResultType,
                Data = serviceResult.Data
            });
        }

        #endregion HotelDefinition

        #region HotelDefinitionAdd

        public JsonResult GetDistricts(int cityId)
        {
            List<DistrictDefinitionVM> definitionVMList = new List<DistrictDefinitionVM>();
            var serviceResult = _definitionService.GetDistrict(cityId);
            if (serviceResult.IsSuccess)
            {
                definitionVMList = serviceResult.Data;
            }

            return Json(definitionVMList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult HotelDefinitionAddEdit(int? hotelId)
        {
            //HotelDefinitionVM hotelDefinition = new HotelDefinitionVM();

            //if (hotelId.HasValue)
            //    hotelDefinition = _hotelDefinitionService.GetHotel(hotelId.Value).Data;

            //var allCitiesData = _definitionService.GetCities().Data;

            //hotelDefinition.Cities = allCitiesData.Select(p => new BSelectListItem
            //{
            //    Disabled = false,
            //    Text = p.Name,
            //    Value = p.Id.ToString(),
            //    Selected = hotelDefinition?.CityId > 0 ? p.Id == hotelDefinition.CityId : false
            //});

            //hotelDefinition.Districts = allCitiesData.SelectMany(p => p.Districts)
            //                                         .Select(p => new BSelectListItem
            //                                         {
            //                                             Text = p.Name,
            //                                             Value = p.Id.ToString(),
            //                                             ParentValue = p.CityId.ToString(),
            //                                             Disabled = false,
            //                                             Selected = hotelDefinition?.DistrictId > 0
            //                                                                   ? p.Id == hotelDefinition.DistrictId
            //                                                                   : false
            //                                         });

            //hotelDefinition.HotelTypes = _hotelTypeService.GetAllHotelTypes(new HotelTypeFilter()).Data;

            HotelDefinitionVM hotelDefinition = new HotelDefinitionVM();

            if (hotelId.HasValue)
            {
                hotelDefinition = _hotelService.GetHotel(hotelId.Value).Data;
            }

            ICacheManager cache = new MemCacheManager();
            var data = cache.GetFromCache<List<CityDefinitionVM>>("Cities",
                                                                   () => _definitionService.GetCities().Data,
                                                                   null);

            var allCitiesData = _definitionService.GetCities().Data;
            hotelDefinition.Cities = allCitiesData.Select(p => new BSelectListItem
            {
                ParentValue = "0",
                Value = p.Id.ToString(),
                Text = p.Name,
                Selected = hotelId.HasValue ? hotelDefinition.CityId == p.Id : false
            }).AsEnumerable();

            hotelDefinition.Districts = Enumerable.Empty<BSelectListItem>();

            if (hotelId.HasValue && hotelDefinition?.CityId > 0 && hotelDefinition?.DistrictId > 0)
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

        #region HotelRoom

        public ActionResult HotelRoomList()
        {
            ViewBag.Hotels = _hotelService.GetHotels(new HotelFilter()).Data.Select(p => new BSelectListItem
            {
                Text = p.HotelName,
                Value = p.Id.ToString(),
                Selected = false
            }).AsEnumerable();

            return View(new HotelRoomFilter());
        }

        public JsonResult GetHotelRooms(GridRequest<HotelRoomFilter> model)
        {
            if (model == null || model.FilterRequest.HotelId <= 0)
            {
                return Json(new GridResponse(model.draw, new List<HotelRoomVM>(), 0, 0), JsonRequestBehavior.AllowGet);
            }

            var page = model.start;
            var rowsPerPage = model.length;

            var filteredData = _hotelService.GetHotelRooms(model.FilterRequest);
            var gridPageRecord = filteredData.Data.Skip(page).Take(rowsPerPage).ToList();

            GridResponse tableResult = new GridResponse(model.draw, gridPageRecord, filteredData.Data.Count, filteredData.Data.Count);
            return Json(tableResult, JsonRequestBehavior.AllowGet);
        }

        public ActionResult HotelRoomAdd(int hotelId)
        {
            var hotelInfo = _hotelService.GetHotel(hotelId).Data;
            HotelRoomVM hotelRoom = new HotelRoomVM()
            {
                HotelId = hotelInfo.Id,
                HotelName = hotelInfo.HotelName
            };

            ViewBag.RoomTypes = _roomTypeService.GetAllRoomTypes(new RoomTypeFilter())
                                                .Data.Select(p => new BSelectListItem
                                                {
                                                    Disabled = false,
                                                    Selected = false,
                                                    Text = p.Title,
                                                    Value = p.Id.ToString()
                                                });

            return View(hotelRoom);
        }

        public JsonResult SaveHotelRoom(HotelRoomVM hotel)
        {
            // validastyonlar yapılacak
            ServiceResultModel<int> serviceResult = _hotelService.SaveHotelRoom(hotel, Server.MapPath("~/App_Data/uploads"));
            // result kontrol edilecek

            return Json(base.UIResponse = new UIResponse
            {
                Message = string.Format("Operation Is Completed"),
                ResultType = serviceResult.ResultType,
                Data = serviceResult.Data
            });
        }

        #endregion HotelRoom

        #region HoteLTestFile

        public ActionResult HotelTestFile()
        {
            return View();
        }

        public JsonResult SaveTestFile(HotelTestFileVM model)
        {
            ServiceResultModel<bool> serviceResult = _hotelService.SaveTestFile(model, Server.MapPath("~/App_Data/uploads"));

            return Json("");
        }

        #endregion HoteLTestFile
    }
}