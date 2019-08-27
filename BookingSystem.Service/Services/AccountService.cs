using BookingSystem.Core;
using BookingSystem.Data.Context;
using BookingSystem.Domain.Entity;
using BookingSystem.Domain.WebUI;
using BookingSystem.Domain.WebUI.Account;
using BookingSystem.Service.Extensions;
using System;
using System.Linq;

namespace BookingSystem.Service.Services
{
    public class AccountService : ServiceBase
    {
        public ServiceResultModel<UserVM> LoginUser(UserVM user)
        {
            User userInfo = null;

            using (EFBookingContext context = new EFBookingContext())
            {
                userInfo = context.Users.FirstOrDefault(p => p.IsActive && p.EMail == user.EMail &&
                                                                  p.Password == user.Password);
            }

            if (userInfo == null)
                return new ServiceResultModel<UserVM>
                {
                    Code = ServiceResultCode.NotFound,
                    ResultType = OperationResultType.Warn,
                    Message = "User Not Found, Please check UserName and Password",
                    Data = null
                };

            if (!userInfo.IsActive)
                return new ServiceResultModel<UserVM>
                {
                    ResultType = OperationResultType.Warn,
                    Message = "This user is InActive, please contact your administrator",
                    Data = userInfo.MapProperties<UserVM>()
                };

            if (!userInfo.EmailConfirmation)
                return new ServiceResultModel<UserVM>
                {
                    Code = ServiceResultCode.EMailIsNotConfirmed,
                    ResultType = OperationResultType.Warn,
                    Message = "Please confirm your account, Check mailbox",
                    Data = userInfo.MapProperties<UserVM>()
                };

            return ServiceResultModel<UserVM>.OK(userInfo.MapProperties<UserVM>());
        }

        public ServiceResultModel<UserVM> RegisterUser(RegisterVM model)
        {
            using (EFBookingContext context = new EFBookingContext())
            {
                var userInfo = context.Users.FirstOrDefault(p => p.EMail == model.EMail);
                if (userInfo != null)
                {
                    return new ServiceResultModel<UserVM>
                    {
                        Code = ServiceResultCode.Duplicate,
                        Message = "Duplicate User",
                        ResultType = OperationResultType.Warn
                    };
                }

                User registeredUser = context.Users.Add(new User
                {
                    Name = model.Name,
                    LastName = model.LastName,
                    Deleted = false,
                    EMail = model.EMail,
                    EmailConfirmation = false,
                    IsActive = true,
                    Password = model.Password,
                    TokenId = Guid.NewGuid().ToString()
                });

                context.SaveChanges();

                return ServiceResultModel<UserVM>.OK(registeredUser.MapProperties<UserVM>());
            }
        }
    }
}