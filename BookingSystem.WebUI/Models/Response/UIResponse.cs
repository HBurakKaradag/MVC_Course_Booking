using BookingSystem.Core;

namespace BookingSystem.WebUI.Models.Response
{
    public class UIResponse
    {
        public string Message { get; set; }

        public object Data { get; set; }

        public OperationResultType ResultType { get; set; }
    }
}