using FilmsTestTask.APIServices.Models;

namespace FilmsTestTask.APIServices.Interfaces
{
    public interface IBaseCrudService<TModel>
        where TModel : BaseApiModel
    {
        Task<IList<TModel>> GetAllAsync();

        Task<TModel> GetAsync(int id);

        Task<int> AddAsync(TModel model);

        Task<TModel> UpdateAsync(TModel model);

        Task<bool> DeleteAsync(int id);
    }
}
