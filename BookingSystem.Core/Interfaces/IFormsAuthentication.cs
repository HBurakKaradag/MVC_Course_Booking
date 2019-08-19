using System.Web;
using System.Web.Security;

namespace BookingSystem.Core.Interfaces
{
    public interface IFormsAuthentication
    {
        void SingOut();

        void SetAuthCookie(HttpContextBase httpContext,
            FormsAuthenticationTicket authenticationTicket);

        void SetAuthCookie(HttpContext httpContext,
            FormsAuthenticationTicket authenticationTicket);

        FormsAuthenticationTicket Decrypt(string encryptedTicket);
    }
}