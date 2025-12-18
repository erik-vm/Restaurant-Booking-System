namespace RestaurantBookingSystem.Domain.Interfaces;

public interface IBookingService
{
    Task<Booking?> CreateBookingAsync(Booking booking);
    Task<bool> CancelBookingAsync(int bookingId);
    Task<IEnumerable<Booking>> GetUpcomingBookingsAsync(int customerId);
    Task<(bool IsValid, string? ErrorMessage)> ValidateBookingAsync(Booking booking);
}
