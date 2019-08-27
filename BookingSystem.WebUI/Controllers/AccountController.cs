using BookingSystem.Core;
using BookingSystem.Core.Interfaces;
using BookingSystem.Domain.WebUI;
using BookingSystem.Domain.WebUI.Account;
using BookingSystem.Service.Services;
using BookingSystem.WebUI.WebCore;
using System.Web.Mvc;

namespace BookingSystem.WebUI.Controllers
{
    /*

       Authroize ile authentication sağlanıyor.
       AllowAnonymous Authhorize filtresine takılmadan methodlara request'in gelebilmesini sağlar.
       ServiceResultModel PrentationLayer'in kabul ettiği generic tip. İçerisindeki errorCode 'a göre aksiyon alınabilir.

      */

    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IFormsAuthentication _formsAuthentication;
        private readonly AccountService _accountService;

        public AccountController()
        {
            _accountService = new AccountService();
            _formsAuthentication = new BookingFormsAuthentication();
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {
            return View(new UserVM());
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(UserVM user)
        {
            if (!ModelState.IsValid)
                return View(user);

            ServiceResultModel<UserVM> serviceResult = _accountService.LoginUser(user);

            if (!serviceResult.IsSuccess)
            {
                if (serviceResult.Code == ServiceResultCode.NotFound)
                {
                    ModelState.AddModelError("", serviceResult.Message);
                    return View(user);
                }

                if (serviceResult.Code == ServiceResultCode.EMailIsNotConfirmed)
                {
                    return RedirectToAction("Confirm", "Account",
                        new { email = serviceResult.Data.EMail, displayName = serviceResult.Data.DisplayName });
                }

                return View(user);
            }

            _formsAuthentication.SetAuthCookie(this.HttpContext, UserAuthTicketBuilder.CreateAuthTicket(serviceResult.Data));

            return RedirectToAction("Index", "Dashboard");
        }

        public ActionResult SingOut()
        {
            _formsAuthentication.SingOut();
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Confirm(string email, string displayName)
        {
            ViewBag.EmailInfo = email;
            ViewBag.DisplayName = displayName;
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Register()
        {
            return View(new RegisterVM());
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Register(RegisterVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            ServiceResultModel<UserVM> serviceResult = _accountService.RegisterUser(model);

            if (!serviceResult.IsSuccess)
            {
                ModelState.AddModelError("", serviceResult.Message);
                return View(model);
            }

            return RedirectToAction("Confirm", "Account",
             new { email = serviceResult.Data.EMail, displayName = serviceResult.Data.DisplayName });
        }

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
    }
}