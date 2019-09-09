using BookingSystem.Core.CustomAttribute;
using BookingSystem.Domain.WebUI.AuditLog;
using BookingSystem.Service.Services;
using BookingSystem.WebUI.Models.GridRequest;
using BookingSystem.WebUI.Models.GridResponse;
using System.Web.Mvc;

namespace BookingSystem.WebUI.Controllers
{
    [Authorize]
    public class DashboardController : ControllerBase
    {
        private readonly AuditLogService _auditLogService;
        private readonly DashboardService _dashBoardService;

        public DashboardController()
        {
            _dashBoardService = new DashboardService();
            _auditLogService = new AuditLogService();
        }

        [AuditLogIgnore]
        [ActionName("_GetLeftMenu")]
        public PartialViewResult GetMenu()
        {
            Session["ActiveMenuPath"] = HttpContext.Request.Path;
            var menuVM = _dashBoardService.GetMenu();
            return PartialView("_LeftMenu", menuVM);
        }

        [HttpGet]
        public ActionResult AuditLog()
        {
            return View();
        }

        public JsonResult GetAuditLogs(GridRequest<AuditLogVM> model)
        {
            var records = _auditLogService.GetAuditLogs();

            GridResponse tableResult = new GridResponse(model.draw, records.Data, records.Data.Count, records.Data.Count);

            return Json(tableResult, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}