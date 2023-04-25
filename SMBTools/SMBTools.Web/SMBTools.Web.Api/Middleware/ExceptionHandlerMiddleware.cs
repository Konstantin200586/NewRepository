using SMBTools.Web.Api.Responses;

namespace SMBTools.Web.Api.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                using (var writer = new StreamWriter(context.Response.Body))
                {
                    var response = new ApiResponse<object>(e.Message, Errors.ErrorCodes[e.GetType()]);
                }
            }
        }
    }
}