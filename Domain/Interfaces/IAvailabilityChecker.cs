namespace RestaurantBookingSystem.Domain.Interfaces;

public interface IAvailabilityChecker
{
    Task<IEnumerable<Table>> GetAvailableTablesAsync(int restaurantId, DateTime date, TimeSpan time, int guestCount);
    Task<bool> IsTableAvailableAsync(int tableId, DateTime date, TimeSpan time);
}
