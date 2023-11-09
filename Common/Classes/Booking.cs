using Common.Enums;
using Common.Extensions;
using Common.Interfaces;

namespace Common.Classes;

public class Booking : IBooking
{
    public int Id { get; set; }
    public double Cost { get; set; }
    public int Days { get; set; }
    public int KmRented { get; set; }
    public int KmReturned { get; set; }
    public DateTime DateRented { get; set; }
    public DateTime DateReturned { get; set; }
    public BookingStatus Status { get; set; }
    public Vehicle Vehicle { get; set; }
    public IPerson Customer { get; set; }

    public void ReturnVehicle(double distance, DateTime returned)
    {
        DateReturned = returned;
        KmReturned = KmRented + (int)distance;
        Days = DateRented.Duration(returned);
        Cost = (distance * Vehicle.CostPerKm) + (Days * Vehicle.CostPerDay);
        Status = BookingStatus.Closed;
    }
    public Booking(int id, int kmRented, Vehicle vehicle, IPerson customer, DateTime dateRented, BookingStatus status)
    {
        Id = id;
        KmRented = kmRented;
        Vehicle = vehicle;
        Customer = customer;
        DateRented = dateRented;
        Status = status;
    }
}