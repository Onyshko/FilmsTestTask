using FilmsTestTask.Services.Models;

namespace FilmsTestTask.Services.Interfaces
{
    public interface IBaseCrudService<Tmodel>
        where Tmodel : BaseMvcModel
    {
        Task<IList<Tmodel>> GetAllAsync();

        Task<Tmodel> GetAsync(int id);

        Task<HttpResponseMessage> AddAsync(Tmodel model);

        Task<HttpResponseMessage> UpdateAsync(Tmodel model);

        Task<HttpResponseMessage> DeleteAsync(int id);
    }
}
