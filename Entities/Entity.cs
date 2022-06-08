using CleanArchitecture.Abstractions;

namespace CleanArchitecture.Entities
{
    public abstract class Entity : IEntity
    {
        public int Id { get; set; }
    }
}
