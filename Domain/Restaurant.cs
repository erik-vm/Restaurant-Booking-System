using System.ComponentModel.DataAnnotations;

namespace RestaurantBookingSystem.Domain;

public class Restaurant : BaseEntity
{
    [Required]
    [MaxLength(128)]
    public string Name { get; set; } = default!;

    [Required]
    [MaxLength(256)]
    public string Location { get; set; } = default!;

    public string? Description { get; set; }

    [Required]
    public int Capacity { get; set; }

    [MaxLength(20)]
    public string? PhoneNumber { get; set; }

    [MaxLength(128)]
    public string? Email { get; set; }

    public ICollection<Table> Tables { get; set; } = new List<Table>();
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
