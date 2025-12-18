using DAL.Domain.Interfaces;

namespace DAL.Domain;

public abstract class BaseEntity : IAuditable, IEntity
{
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
    public int Id { get; set; }
}