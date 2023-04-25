using SMBTools.Contract.Enums;

namespace SMBTools.Web.DAL.Models;

public class Account : BaseDbModel
{
    public string Email { get; set; }
    public string Password { get; set; }
    public UserRole Role { get; set; }
}