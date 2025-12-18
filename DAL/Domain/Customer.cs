using System.ComponentModel.DataAnnotations;

namespace DAL.Domain;

public class Customer : BaseEntity
{
    [Required]
    [MaxLength(64)]
    public string FirstName { get; set; } = default!;

    [Required]
    [MaxLength(64)]
    public string LastName { get; set; } = default!;

    [Required]
    [MaxLength(128)]
    public string Email { get; set; } = default!;

    [Required]
    [MaxLength(20)]
    public string PhoneNumber { get; set; } = default!;

    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public string GetFullName()                                                                                                                            
    {
        return $"{FirstName} {LastName}";
    }
}