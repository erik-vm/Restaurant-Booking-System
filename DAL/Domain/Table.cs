using System.ComponentModel.DataAnnotations;

namespace DAL.Domain;

public class Table : BaseEntity
{
    [Required]
    [MaxLength(20)]
    public string TableNumber { get; set; } = default!;

    [Required]
    public int SeatingCapacity { get; set; }

    public bool IsActive { get; set; } = true;

    [Required]
    public int RestaurantId { get; set; }

    public Restaurant Restaurant { get; set; } = default!;

    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}