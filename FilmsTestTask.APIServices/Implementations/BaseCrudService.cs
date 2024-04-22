using AutoMapper;
using FilmsTestTask.APIServices.Interfaces;
using FilmsTestTask.APIServices.Models;
using FilmsTestTask.Domain.Entities;
using FilmsTestTask.Repositories.Interfaces;

namespace FilmsTestTask.APIServices.Implementations
{
    public class BaseCrudService<TEntity, TModel> : BaseService<TEntity>, IBaseCrudService<TModel>
        where TModel : BaseApiModel
        where TEntity : BaseEntity
    {
        public BaseCrudService(IBaseRepository<TEntity> repo, IMapper mapper)
            : base(repo, mapper)
        {
        }

        public async Task<int> AddAsync(TModel model)
        {
            return await _repo.AddAsync(_mapper.Map<TEntity>(model));
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repo.DeleteAsync(id);
        }

        public async Task<IList<TModel>> GetAllAsync()
        {
            var entities = await _repo.GetAllAsync();
            return _mapper.Map<List<TModel>>(entities);
        }

        public async Task<TModel> GetAsync(int id)
        {
            var entity = await _repo.GetAsync(id);
            return _mapper.Map<TModel>(entity);
        }

        public async Task<TModel> UpdateAsync(TModel model)
        {
            var entity = await _repo.UpdateAsync(_mapper.Map<TEntity>(model));
            return _mapper.Map<TModel>(entity);
        }
    }
}
