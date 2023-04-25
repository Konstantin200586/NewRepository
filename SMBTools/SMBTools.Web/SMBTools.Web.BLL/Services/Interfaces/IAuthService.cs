using SMBTools.Contract.Models.Responses;
using SMBTools.Web.BLL.Models;

namespace SMBTools.Web.BLL.Services.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseDto> GetUserTokenAsync(LoginModel loginModel);
        LoginResponseDto RefreshToken(string refreshToken, string oldAccessToken);
    }
}
