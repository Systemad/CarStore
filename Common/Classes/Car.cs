using Common.Enums;

namespace Common.Classes;

public class Car : Vehicle
{
    public Car(string regNo, string manufacturer, int odometer, VehicleType vehicleType, double costPerKm, double costPerDay, VehicleStatus vehicleStatus)
    {
        RegNo = regNo;
        Manufacturer = manufacturer;
        Odometer = odometer;
        VehicleType = vehicleType;
        CostPerKm = costPerKm;
        CostPerDay = costPerDay;
        VehicleStatus = vehicleStatus;
    }
}