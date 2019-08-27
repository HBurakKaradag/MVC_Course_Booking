using BookingSystem.Service.Services;
using System.Web.Mvc;

namespace BookingSystem.WebUI.Controllers
{
    [Authorize]
    public class DashboardController : ControllerBase
    {
        private readonly DashboardService _dashBoardService;

        public DashboardController()
        {
            _dashBoardService = new DashboardService();
        }

        [ActionName("_GetLeftMenu")]
        public PartialViewResult GetMenu()
        {
            var menuVM = _dashBoardService.GetMenu();
            return PartialView("_LeftMenu", menuVM);
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}