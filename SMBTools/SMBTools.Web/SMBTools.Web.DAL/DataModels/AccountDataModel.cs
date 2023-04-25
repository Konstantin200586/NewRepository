using SMBTools.Contract.Enums;

namespace SMBTools.Web.DAL.DataModels;

public class AccountDataModel : BaseDataModel
{
    public string Email { get; set; }
    public string Password { get; set; }
    public UserRole Role { get; set; }
}