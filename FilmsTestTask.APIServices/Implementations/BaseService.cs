using AutoMapper;
using FilmsTestTask.Domain.Entities;
using FilmsTestTask.Repositories.Interfaces;

namespace FilmsTestTask.APIServices.Implementations
{
    public class BaseService<TEntity>
        where TEntity : BaseEntity
    {
        protected readonly IBaseRepository<TEntity> _repo;
        protected readonly IMapper _mapper;

        public BaseService(IBaseRepository<TEntity> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
    }
}
