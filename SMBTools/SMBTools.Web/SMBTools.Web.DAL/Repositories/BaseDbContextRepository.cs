using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SMBTools.Contract.Filters;
using SMBTools.Web.DAL.DataModels;
using SMBTools.Web.DAL.Infrastructure;
using SMBTools.Web.DAL.Models;
using SMBTools.Web.DAL.Repositories.Interfaces;

namespace SMBTools.Web.DAL.Repositories
{
    public abstract class BaseDbContextRepository<TDb, TDbFilter> : IBaseRepository<TDb, TDbFilter>
        where TDb : BaseDbModel
        where TDbFilter : BaseFilter
    {
        protected readonly AppDbContext _context;
        protected readonly ILogger<TDb> _logger;
        protected readonly IMapper _mapper;

        protected BaseDbContextRepository(AppDbContext context, ILogger<TDb> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<T> GetByIdAsync<T>(Guid id) where T : BaseDataModel
        {
            return await _mapper.ProjectTo<T>(_context.Set<TDb>()).FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<List<T>> GetByFilterAsync<T>(TDbFilter filter) where T : BaseDataModel
        {
            var builder = ConstructFilter(filter);

            return _mapper.ProjectTo<T>(builder).ToListAsync();
        }

        public virtual void Create<T>(T item) where T : BaseDataModel
        {
            var mappedItem = _mapper.Map<TDb>(item);
            _context.Add(mappedItem);
        }

        public virtual async Task UpdateAsync<T>(T item) where T : BaseDataModel
        {
            var dbItem = await _context.Set<TDb>().FirstOrDefaultAsync(i => i.Id == item.Id);

            if (dbItem == null)
            {
                return;
            }

            var itemForSave = _mapper.Map<TDb>(item);
            RestoreDefaultProperties(dbItem, itemForSave);

            _mapper.Map(itemForSave, dbItem);
        }

        //soft delete by default
        public virtual async Task DeleteAsync(Guid id)
        {
            var item = await _context.Set<TDb>().FirstOrDefaultAsync(i => i.Id == id);

            item.IsDeleted = true;
        }

        //use to prevent some urgent fields overriding
        protected virtual void RestoreDefaultProperties(TDb beforeSave, TDb forSave)
        {
            forSave.Id = beforeSave.Id;
            forSave.IsDeleted = beforeSave.IsDeleted;
        }

        public async Task<T> GetOneByFilterAsync<T>(TDbFilter filter) where T : BaseDataModel
        {
            var builder = ConstructFilter(filter);

            var result = await _mapper.ProjectTo<T>(builder).FirstOrDefaultAsync();

            return result;
        }

        protected abstract IQueryable<TDb> AddFilterConditions(IQueryable<TDb> items, TDbFilter filter);

        private IQueryable<TDb> ConstructFilter(TDbFilter filter)
        {
            IQueryable<TDb> builder = _context.Set<TDb>().AsNoTracking();

            if (filter.Id.HasValue)
            {
                builder = builder.Where(x => x.Id == filter.Id.Value);
            }

            if (filter.Ids != null && filter.Ids.Any())
            {
                builder = builder.Where(x => filter.Ids.Contains(x.Id));
            }

            if (filter.IsDeleted.HasValue)
            {
                builder = builder.Where(x => x.IsDeleted == filter.IsDeleted.Value);
            }

            builder = AddFilterConditions(builder, filter);

            return builder;
        }
    }
}
