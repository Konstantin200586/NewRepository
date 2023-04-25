namespace SMBTools.Web.Api.Responses
{
    public class ApiResponse<T>
    {
        public T Payload { get; set; }
        public string ErrMsg { get; set; }
        public int ErrCode { get; set; }

        public ApiResponse(T payload)
        {
            Payload = payload;
            ErrMsg = string.Empty;
            ErrCode = 0;
        }

        public ApiResponse(string errMsg) : this(errMsg, 601)
        {
        }

        public ApiResponse(string errMsg, int errCode)
        {
            Payload = default;
            ErrMsg = errMsg;
            ErrCode = errCode;
        }
    }
}
