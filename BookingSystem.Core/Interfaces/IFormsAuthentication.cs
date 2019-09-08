using System.Web;
using System.Web.Security;

namespace BookingSystem.Core.Interfaces
{
    /// <summary>
    /// @Kodluyoruz-MVC-Bootcamp  13.07.2019 – 09.09.2019
    /// H.Burak Karadağ
    /// FormsAuthentication interface implementasyonu
    /// ************************
    /// ************************
    /// </summary>
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