namespace BookingSystem.Service.Services
{
    public class HotelTypeService : ServiceBase
    {
        //public ServiceResultModel<HotelTypeVM> LoginUser(HotelTypeVM user)
        //{
        //    User userInfo = null;

        //    using (EFBookingContext context = new EFBookingContext())
        //    {
        //        userInfo = context.Users.FirstOrDefault(p => p.IsActive && p.EMail == user.EMail &&
        //                                                          p.Password == user.Password);
        //    }

        //    if (userInfo == null)
        //        return new ServiceResultModel<UserVM>
        //        {
        //            ResultType = OperationResultType.Warn,
        //            Message = "User Not Found",
        //            Data = null
        //        };

        //    if (!userInfo.IsActive)
        //        return new ServiceResultModel<UserVM>
        //        {
        //            ResultType = OperationResultType.Warn,
        //            Message = "This user is InActive, please contact your administrator",
        //            Data = userInfo.MapProperties<UserVM>()
        //        };

        //    if (!userInfo.EmailConfirmation)
        //        return new ServiceResultModel<UserVM>
        //        {
        //            Code = ServiceResultCode.EMailIsNotConfirmed,
        //            ResultType = OperationResultType.Warn,
        //            Message = "Please confirm your account, Check mailbox",
        //            Data = userInfo.MapProperties<UserVM>()
        //        };

        //    return ServiceResultModel<UserVM>.OK(userInfo.MapProperties<UserVM>());
        //}
    }
}