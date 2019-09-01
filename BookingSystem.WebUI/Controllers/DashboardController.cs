using BookingSystem.Core.CustomAttribute;
using BookingSystem.Domain.WebUI.AuditLog;
using BookingSystem.Service.Services;
using BookingSystem.WebUI.Models.DataTableRequest;
using BookingSystem.WebUI.Models.DataTableResponse;
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

        public JsonResult GetAuditLogs(DataTableRequest<AuditLogVM> model)
        {
            var records = _auditLogService.GetAuditLogs();

            DataTablesResponse tableResult = new DataTablesResponse(model.draw, records.Data, records.Data.Count, records.Data.Count);

            return Json(tableResult, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}