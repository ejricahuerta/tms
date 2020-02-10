namespace Web.Models
{
    public class ErrorViewModel
    {
        public ErrorCode Code { get; set; } = ErrorCode.BadRequest;

        public string Error { get; set; }
        public string Resolution { get; set; }
    }


    public enum ErrorCode
    {
        BadRequest = 400,
        Unauthorized = 401,
        Forbidden = 403,
        NotFound = 404,
        BadGateway = 502,
        InternalError = 500
    }
}
