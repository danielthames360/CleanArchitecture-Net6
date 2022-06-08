namespace CleanArchitecture.Abstractions
{
    public interface ICrud<T>
    {
        T Save(T entity);
        IList<T> GetAll();
        T GetById(int id);
        void DeleteById(int id);
    }
}