using CleanArchitecture.Abstractions;
using CleanArchitecture.Repository;

namespace CleanArchitecture.Application
{
    public class Application<T> : IApplication<T> where T : IEntity
    {
        private readonly IRepository<T> _repository;

        public Application(IRepository<T> repository)
        {
            _repository = repository;
        }
        public void DeleteById(int id)
        {
            _repository.DeleteById(id);
        }

        public IList<T> GetAll()
        {
            return _repository.GetAll();
        }

        public T GetById(int id)
        {
            return _repository.GetById(id);
        }

        public T Save(T entity)
        {
            return _repository.Save(entity);
        }
    }
}
