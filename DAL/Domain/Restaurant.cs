using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DAL.Domain;

public class Restaurant : BaseEntity
{
    [Required] [MaxLength(128)] public string Name { get; set; } = default!;
    [Required] [MaxLength(256)] public string Location { get; set; } = default!;
    [Required] [MaxLength(1000)] public string? Description { get; set; }
    [Required] int Capacity { get; set; }
    [MaxLength(20)] string? PhoneNumber { get; set; }
    [MaxLength(128)] string? Email { get; set; }

    public ICollection<Table> Tables { get; set; } = new List<Table>();
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}