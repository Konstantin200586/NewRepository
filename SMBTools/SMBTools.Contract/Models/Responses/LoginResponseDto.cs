using SMBTools.Contract.Enums;

namespace SMBTools.Contract.Models.Responses;

public class LoginResponseDto
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public UserRole RoleType { get; set; }
}