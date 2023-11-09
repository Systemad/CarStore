using Common.Classes;
using Common.Enums;
using Common.Extensions;
using Common.Interfaces;
using Data;

namespace Business;

public class BookingProcessor
{
    private readonly IData _db;
    public BookingProcessor(IData db) => _db = db;

    public IEnumerable<Customer> GetCustomers()
    {
        var persons = _db.Get<Customer>(null);
        return persons;
    }

    public IPerson? GetPerson(string ssn)
    {
        var person = _db.Single<Customer>(x => x.SSN == ssn);
        return person;
    }

    public IEnumerable<IBooking> GetBookings()
    {
        var bookings = _db.Get<Booking>(null);
        return bookings;
    }

    public IBooking? GetBooking(int vehicleId)
    {
        var booking = _db.Single<Booking>(x => x.Vehicle.Id == vehicleId);
        return booking;
    }

    public IEnumerable<Vehicle> GetVehicles(VehicleStatus status = default)
    {
        if (status != default)
            return _db.Get<Vehicle>(x => x.VehicleStatus == status);
        return _db.Get<Vehicle>(null);
    }

    public Vehicle? GetVehicle(int vehicleId)
    {
        var vehicle = _db.Single<Vehicle>(x => x.Id == vehicleId);
        return vehicle;
    }

    public Vehicle? GetVehicle(string regNo)
    {
        var vehicle = _db.Single<Vehicle>(x => x.RegNo == regNo);
        return vehicle;
    }

    public async Task<IBooking> RentVehicle(int vehicleId, int customerId)
    {
        var delay = HelperExtensions.GetRandomNumber();
        await Task.Delay(TimeSpan.FromSeconds(delay));
        var booking = _db.RentVehicle(vehicleId, customerId);
        return booking;
    }

    public IBooking ReturnVehicle(int vehicleId, double distance)
    {
        var booking = _db.ReturnVehicle(vehicleId, distance);
        return booking;
    }

    public void AddVehicle(VehicleType vehicleType, string manufacturer, string regNo, int odometer, double costPerKm,
        double costPerDay)
    {
        switch (vehicleType)
        {
            case VehicleType.Sedan:
            case VehicleType.Combi:
            case VehicleType.Can:
                var car = new Car(regNo: regNo, manufacturer: manufacturer, odometer: odometer, vehicleType: vehicleType,
                    costPerKm: costPerKm, costPerDay: costPerDay, vehicleStatus: VehicleStatus.Available);
                _db.Add<Vehicle>(car);
                break;
            case VehicleType.Motorcycle:
                var motorcycle = new Motorcycle(regNo: regNo, manufacturer: manufacturer, odometer: odometer,
                    vehicleType: vehicleType,
                    costPerKm: costPerKm, costPerDay: costPerDay, vehicleStatus: VehicleStatus.Available);
                _db.Add<Vehicle>(motorcycle);
                break;
        }
    }

    public void AddCustomer(string ssn, string firstName, string lastName)
    {
        var newCustomer = new Customer(firstName, lastName, ssn);
        _db.Add<Customer>(newCustomer);
    }

    public string[] VehicleStatusNames => _db.VehicleStatusNames;
    public string[] VehicleTypeNames => _db.VehicleTypeNames;
    public VehicleType GetVehicleType(string name) => _db.GetVehicleType(name);
}