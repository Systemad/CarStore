using Common.Enums;

namespace Common.Classes;

public class Vehicle
{
    public int Id { get; set; }
    public string RegNo { get; set; }
    public string Manufacturer { get; set; }
    public int Odometer { get; set; }
    public VehicleType VehicleType { get; set; }
    public double CostPerKm { get; set; }
    public double CostPerDay { get; set; }
    public VehicleStatus VehicleStatus { get; set; }

    public void Rent() => VehicleStatus = VehicleStatus.Booked;
    public void Return() => VehicleStatus = VehicleStatus.Available;
}