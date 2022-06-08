using CleanArchitecture.Abstractions;

namespace CleanArchitecture.Repository
{
    public class Repository<T> : IRepository<T> where T : IEntity
    {
        private readonly IDbContext<T> _context;

        public Repository(IDbContext<T> context)
        {
            _context = context;
        }
        public void DeleteById(int id)
        {
            _context.DeleteById(id);
        }

        public IList<T> GetAll()
        {
            return _context.GetAll();
        }

        public T GetById(int id)
        {
            return _context.GetById(id);
        }

        public T Save(T entity)
        {
            if (entity.Id.Equals(0))
            {
                _context.Save(entity);
            }
            return entity;
        }
    }
}