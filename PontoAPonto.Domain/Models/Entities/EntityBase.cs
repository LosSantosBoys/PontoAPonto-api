using System.ComponentModel.DataAnnotations;

namespace PontoAPonto.Domain.Models.Entities
{
    public abstract class EntityBase
    {
        protected EntityBase()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }

        public Guid Id { get; private set; }
        public DateTime CreatedAt {  get; private set; }
        public DateTime UpdatedAt { get; private set; }
    }
}