using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SMBTools.Web.DAL.Infrastructure;
using SMBTools.Web.DAL.Repositories.Interfaces;

namespace SMBTools.Web.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly ILogger<UnitOfWork> _logger;

        public UnitOfWork(AppDbContext context, ILogger<UnitOfWork> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task SaveAsync()
        {
            // Use this until EF Core team will design a way to disable concurrency checks:
            // https://github.com/dotnet/efcore/issues/10443
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                //additionaly think about this
                _logger.LogWarning(ex, $"Concurrency problem {ex.Message}");

                return;
            }

        }
    }
}