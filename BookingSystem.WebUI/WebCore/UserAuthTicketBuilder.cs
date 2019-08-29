using BookingSystem.Domain.WebUI.Account;
using Newtonsoft.Json;
using System;
using System.Web.Security;

namespace BookingSystem.WebUI.WebCore
{
    public class UserAuthTicketBuilder
    {
        public static FormsAuthenticationTicket CreateAuthTicket(UserVM userVM)
        {
            var ticket = new FormsAuthenticationTicket(
                 1,
                 userVM.Name,
                 DateTime.Now,
                 DateTime.Now.Add(FormsAuthentication.Timeout),
                 false,
                 JsonConvert.SerializeObject(userVM)
                );

            return ticket;
        }
    }
}