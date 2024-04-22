using FilmsTestTask.Services.Models;

namespace FilmsTestTask.Services.Interfaces
{
    public interface ICategoryService : IBaseCrudService<CategoryMvcModel>
    {
        Task<int> GetLayerAsync(int id);
    }
}
