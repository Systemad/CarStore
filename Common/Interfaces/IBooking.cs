using Common.Classes;
using Common.Enums;

namespace Common.Interfaces;

public interface IBooking
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
    void ReturnVehicle(double distance, DateTime returned);
}