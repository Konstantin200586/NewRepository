using SMBTools.Contract.Filters;
using SMBTools.Web.BLL.Models;

namespace SMBTools.Web.BLL.Services.Interfaces;

public interface IAccountService
{
    Task CreateAsync(AccountModel accountModel);
    Task DeleteAsync(Guid id);
    Task<AccountModel> GetByIdAsync(Guid id);
    Task<List<AccountModel>> GetByFilterAsync(AccountFilter filter);
}