using BookingSystem.Core;

namespace BookingSystem.Domain.WebUI
{
    public class ServiceResultModel<T> 
    {
        #region Ctor

        public ServiceResultModel()
        {
        }

        public ServiceResultModel(T data)
        {
            this.Data = data;
        }

        public ServiceResultModel(OperationResultType resultType, T data)
        {
            this.ResultType = resultType;
            this.Data = data;
        }

        #endregion Ctor

        #region Prop

        public ServiceResultCode Code { get; set; }
        public string Message { get; set; }

        public OperationResultType ResultType { get; set; }

        public T Data { get; set; }

        #endregion Prop

        public bool IsSuccess
        {
            get
            {
                return this.ResultType == OperationResultType.Success && this.Data != null;
            }
        }

        public static ServiceResultModel<T> OK(T data) => new ServiceResultModel<T>(OperationResultType.Success, data);

        //public ServiceResultModel<T> OK(T data)
        //{
        //    return new ServiceResultModel<T>(OperationResultType.Success, data);
        //}
    }
}