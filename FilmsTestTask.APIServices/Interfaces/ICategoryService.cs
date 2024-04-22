using FilmsTestTask.APIServices.Models;

namespace FilmsTestTask.APIServices.Interfaces
{
    public interface ICategoryService
    {
        Task<int> CountLayerAsync(int id);

        Task<CategoryApiModel> UpdateAsync(CategoryApiModel model);

        Task<bool> DeleteAsync(int id);
    }
}
