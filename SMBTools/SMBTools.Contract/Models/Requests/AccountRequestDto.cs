using SMBTools.Contract.Enums;

namespace SMBTools.Contract.Models.Requests;

public class AccountRequestDto
{
    public string Email { get; set; }
    public string Password { get; set; }
    public UserRole Role { get; set; }
}