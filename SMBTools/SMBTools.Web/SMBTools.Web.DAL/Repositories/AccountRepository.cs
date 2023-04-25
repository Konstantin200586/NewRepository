using AutoMapper;
using Microsoft.Extensions.Logging;
using SMBTools.Contract.Filters;
using SMBTools.Web.DAL.Infrastructure;
using SMBTools.Web.DAL.Models;
using SMBTools.Web.DAL.Repositories.Interfaces;

namespace SMBTools.Web.DAL.Repositories;

public class AccountRepository : BaseDbContextRepository<Account, AccountFilter>, IAccountRepository
{
    public AccountRepository(AppDbContext context,
        ILogger<Account> logger,
        IMapper mapper)
        : base(context, logger, mapper)
    {
    }

    protected override IQueryable<Account> AddFilterConditions(IQueryable<Account> items, AccountFilter filter)
    {
        if (!string.IsNullOrEmpty(filter.Email))
        {
            items = items.Where(i => i.Email == filter.Email);
        }

        return items;
    }
}