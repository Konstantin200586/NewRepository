using SMBTools.Contract.Filters;
using SMBTools.Web.DAL.DataModels;
using SMBTools.Web.DAL.Models;

namespace SMBTools.Web.DAL.Repositories.Interfaces
{
    public interface IBaseRepository<TDb, TFilter> where TDb : BaseDbModel where TFilter : BaseFilter
    {
        Task<List<T>> GetByFilterAsync<T>(TFilter filter) where T : BaseDataModel;
        Task<T> GetOneByFilterAsync<T>(TFilter filter) where T : BaseDataModel;
        Task<T> GetByIdAsync<T>(Guid id) where T : BaseDataModel;
        Task DeleteAsync(Guid id);
        Task UpdateAsync<T>(T item) where T : BaseDataModel;
        void Create<T>(T item) where T : BaseDataModel;
    }
}
