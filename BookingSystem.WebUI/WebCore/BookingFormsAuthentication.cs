using BookingSystem.Core.Interfaces;
using System;
using System.Web;
using System.Web.Security;

namespace BookingSystem.WebUI.WebCore
{
    public class BookingFormsAuthentication : IFormsAuthentication
    {
        public FormsAuthenticationTicket Decrypt(string encryptedTicket)
        {
            return FormsAuthentication.Decrypt(encryptedTicket);
        }

        public void SetAuthCookie(HttpContextBase httpContext, FormsAuthenticationTicket authenticationTicket)
        {
            var ticket = FormsAuthentication.Encrypt(authenticationTicket);
            httpContext.Response.Cookies.Add(
                new HttpCookie(FormsAuthentication.FormsCookieName, ticket) { Expires = CalculateExpireTime() });

            //this.SetAuthCookie(httpContext, authenticationTicket);
        }

        public void SetAuthCookie(HttpContext httpContext, FormsAuthenticationTicket authenticationTicket)
        {
            var ticket = FormsAuthentication.Encrypt(authenticationTicket);
            httpContext.Response.Cookies.Add(
                new HttpCookie(FormsAuthentication.FormsCookieName, ticket) { Expires = CalculateExpireTime() });
        }

        public void SingOut()
        {
            FormsAuthentication.SignOut();
        }

        private DateTime CalculateExpireTime()
        {
            return DateTime.Now.Add(FormsAuthentication.Timeout);
        }
    }
}