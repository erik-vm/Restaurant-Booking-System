using RestaurantBookingSystem.Domain.Interfaces;

namespace RestaurantBookingSystem.Domain;

public abstract class BaseEntity : IEntity, IAuditable
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    protected BaseEntity()
    {
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public virtual void UpdateTimestamp()
    {
        UpdatedAt = DateTime.UtcNow;
    }
}
