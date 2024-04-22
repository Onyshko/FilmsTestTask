using AutoMapper;
using FilmsTestTask.APIServices.Interfaces;
using FilmsTestTask.APIServices.Models;
using FilmsTestTask.Domain.Entities;
using FilmsTestTask.Repositories.Interfaces;

namespace FilmsTestTask.APIServices.Implementations
{
    public class CategoryService : BaseService<Category>, ICategoryService
    {
        public CategoryService(IBaseRepository<Category> repo, IMapper mapper) : base(repo, mapper)
        {
        }

        public async Task<int> CountLayerAsync(int id)
        {
            var existingEntity = await _repo.GetAsync(id);
            var layer = 0;

            while (existingEntity.ParentCategoryId is not null)
            {
                layer++;
                existingEntity = await _repo.GetAsync(existingEntity.ParentCategoryId.Value);
            }

            return layer;
        }

        public async Task<CategoryApiModel> UpdateAsync(CategoryApiModel model)
        {
            try
            {
                if (model.ParentCategoryId is not null)
                {
                    var children = (await _repo.GetAllAsync()).Where(x => x.ParentCategoryId == model.Id).Select(x => x.Id).ToList();

                    foreach (var childId in children)
                    {
                        await IsValidParentId(childId, model.ParentCategoryId.Value);
                    }
                }

                var entity = await _repo.UpdateAsync(_mapper.Map<Category>(model));
                return _mapper.Map<CategoryApiModel>(entity);
            }
            catch (InvalidDataException)
            {
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var children = (await _repo.GetAllAsync()).Where(x => x.ParentCategoryId == id).Select(x => x.Id).ToList();

            foreach (var childId in children)
            {
                await DeleteAsync(childId);
            }

            var entity = await _repo.DeleteAsync(id);
            return _mapper.Map<bool>(entity);
        }

        private async Task IsValidParentId(int id, int parentId)
        {
            if (id != parentId)
            {
                foreach(var childId in (await _repo.GetAllAsync()).Where(x => x.ParentCategoryId == parentId).Select(x => x.Id))
                {
                    await IsValidParentId(childId, parentId);
                }
            }
            else
            {
                throw new InvalidDataException("You can't use child Id for parent Id!");
            }
        }
    }
}
