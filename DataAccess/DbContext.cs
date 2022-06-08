using CleanArchitecture.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.DataAccess
{
    public class DbContext<T> : IDbContext<T> where T : class, IEntity
    {
        private readonly DbSet<T> _data;
        private readonly ApiDbContext _context;

        public DbContext(ApiDbContext context)
        {
            _context = context;
            _data = context.Set<T>();
        }
        public void DeleteById(int id)
        {
            var result = _data.Find(id);
            if (result != null)
            {
                _data.Remove(result);
                _context.SaveChanges();
            }
        }

        public IList<T> GetAll()
        {
            return _data.ToList();
        }

        public T GetById(int id)
        {
            return _data?.FirstOrDefault(q => q.Id.Equals(id));
        }

        public T Save(T entity)
        {
            _data.Add(entity);
            _context.SaveChanges();
            return entity;
        }
    }
}