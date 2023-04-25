using SMBTools.Contract.Enums;

namespace SMBTools.Web.BLL.Models;

public class AccountModel : BaseModel
{
    public string Email { get; set; }
    public string Password { get; set; }
    public UserRole Role { get; set; }
}