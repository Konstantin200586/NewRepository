using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using SMBTools.Web.Api.Responses;

namespace SMBTools.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly ILogger Logger;

        protected BaseController(ILogger logger)
        {
            Logger = logger;
        }

        protected async Task<IActionResult> ProcessRequest<T>(Func<Task<T>> fn)
        {
            try
            {
                var result = await fn();
                return Ok(new ApiResponse<T>(result));
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message);
                return BadRequest(new ApiResponse<T>(e.Message));
            }
        }

        protected async Task<IActionResult> ProcessRequest<T>(Func<Task> fn)
        {
            try
            {
                await fn();
                return Ok(new ApiResponse<T>(default(T)));
            }
            catch (MySqlException e)
            {
                Logger.LogError(e.InnerException?.Message);
                return BadRequest(new ApiResponse<T>(e.Message));
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message);
                return BadRequest(new ApiResponse<T>(e.Message));
            }
        }

        protected IActionResult ProcessRequest<T>(Func<T> fn)
        {
            try
            {
                var result = fn();
                return Ok(new ApiResponse<T>(result));
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message);
                return BadRequest(new ApiResponse<T>(e.Message));
            }
        }
    }
}