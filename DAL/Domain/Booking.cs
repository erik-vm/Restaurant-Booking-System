using System.ComponentModel.DataAnnotations;

namespace DAL.Domain;

public class Booking : BaseEntity
{
    [Required]
    public DateTime BookingDate { get; set; }

    [Required]
    public TimeSpan BookingTime { get; set; }

    [Required]
    public int NumberOfGuests { get; set; }

    [Required]
    public BookingStatus Status { get; set; } = BookingStatus.Pending;

    [MaxLength(500)]
    public string? SpecialRequests { get; set; }

    [Required]
    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = default!;

    [Required]
    public int RestaurantId { get; set; }
    public Restaurant Restaurant { get; set; } = default!;

    public int? TableId { get; set; }
    public Table? Table { get; set; }

    public bool IsUpcoming()                                                                                                                               
    {
        var bookingDateTime = BookingDate.Date + BookingTime;
        return bookingDateTime > DateTime.UtcNow;
    }

    public bool CanBeCancelled()                                                                                                                           
    {
        return Status == BookingStatus.Pending || Status == BookingStatus.Confirmed;
    }
}