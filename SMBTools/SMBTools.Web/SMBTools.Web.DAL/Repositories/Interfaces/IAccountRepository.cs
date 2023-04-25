using SMBTools.Contract.Filters;
using SMBTools.Web.DAL.Models;

namespace SMBTools.Web.DAL.Repositories.Interfaces;

public interface IAccountRepository : IBaseRepository<Account, AccountFilter>
{
}