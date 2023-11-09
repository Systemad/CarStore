using System.Linq.Expressions;
using Common.Classes;
using Common.Enums;
using Common.Interfaces;

namespace Data;

public class CollectionData : IData
{
    private readonly List<IPerson> _persons = new();
    private readonly List<Vehicle> _vehicles = new();
    private readonly List<IBooking> _bookings = new();

    public CollectionData() => GetSeedData();

    public int NextVehicleId => _vehicles.Count.Equals(0) ? 1 : _vehicles.Max(b => b.Id) + 1;
    public int NextPersonId => _persons.Count.Equals(0) ? 1 : _persons.Max(b => b.Id) + 1;
    public int NextBookingId => _bookings.Count.Equals(0) ? 1 : _bookings.Max(b => b.Id) + 1;

    public IBooking ReturnVehicle(int vehicleId, double distance)
    {
        var booking = _bookings.Single(x => x.Vehicle.Id == vehicleId && x.Status == BookingStatus.Open && x.Vehicle.VehicleStatus == VehicleStatus.Booked);
        var vehicle = _vehicles.Single(x => x.Id == vehicleId);
        if (booking is null || vehicle is null)
            throw new InvalidOperationException("vehicle or booking doesn't exist");
        vehicle.Return();
        booking.ReturnVehicle(distance, DateTime.Now);
        return booking;
    }

    public IBooking RentVehicle(int vehicleId, int customerId)
    {
        var vehicle = _vehicles.Single(x => x.Id == vehicleId);
        if (vehicle is null)
            throw new InvalidOperationException("vehicle doesn't exist");

        var customer = _persons.Single(x => x.Id == customerId);
        if (customer is null)
            throw new InvalidOperationException("customer doesn't exist");

        vehicle.Rent();
        var newBooking = new Booking(id: NextBookingId, kmRented: vehicle.Odometer,
            vehicle: vehicle,
            customer: customer, dateRented: DateTime.Now, status: BookingStatus.Open);
        Add<Booking>(newBooking);
        return newBooking;
    }


    public List<T> Get<T>(Expression<Func<T, bool>>? expression)
    {
        var type = typeof(T);
        if (type == typeof(Customer))
        {
            return expression is not null
                ? _persons.OfType<T>().Where(expression.Compile()).ToList()
                : _persons.OfType<T>().ToList();
        }

        if (type == typeof(Vehicle))
        {
            return expression is not null
                ? _vehicles.OfType<T>().Where(expression.Compile()).ToList()
                : _vehicles.OfType<T>().ToList();
        }

        if (type == typeof(Booking))
        {
            return expression is not null
                ? _bookings.OfType<T>().Where(expression.Compile()).ToList()
                : _bookings.OfType<T>().ToList();
        }
        return new List<T>();
    }

    public T? Single<T>(Expression<Func<T, bool>>? expression)
    {
        if (expression == null)
            throw new ArgumentException("expression is null");
    
        var type = typeof(T);
        if (type == typeof(Customer))
        {
            return _persons.OfType<T>().SingleOrDefault(expression.Compile());
        }

        if (type == typeof(Vehicle))
        {
            return _vehicles.OfType<T>().SingleOrDefault(expression.Compile());
        }

        if (type == typeof(Booking))
        {
            return _bookings.OfType<T>().SingleOrDefault(expression.Compile());
        }

        return default;
    }

    public void Add<T>(T item)
    {
        var type = typeof(T);
        if (type == typeof(Customer))
        {
            var person = item as IPerson;
            person.Id = NextPersonId;
            _persons.Add(person);
        }
        else if (type == typeof(Vehicle))
        {
            var vehicle = item as Vehicle;
            vehicle.Id = NextVehicleId;
            _vehicles.Add(vehicle);
        }
        else if (type == typeof(Booking))
        {
            var booking = item as IBooking;
            booking.Id = NextBookingId;
            _bookings.Add(booking);
        }
        else
        {
            throw new ArgumentException($"unknown type {type}");
        }
    }

    void GetSeedData()
    {
        var customers = new List<IPerson>
        {
            new Customer(name: "Jane", lastname: "Doe", ssn: "98765")
            {
                Id = 1
            },
            new Customer(name: "John", lastname: "Doe", ssn: "12345")
            {
                Id = 2
            }
        };

        var vehicles = new List<Vehicle>
        {
            new Motorcycle("ABC123", manufacturer: "Volvo", odometer: 1000, vehicleType: VehicleType.Combi,
                costPerKm: 1, costPerDay: 200, vehicleStatus: VehicleStatus.Available) { Id = 1 },

            new Motorcycle(regNo: "DEF456", manufacturer: "Saab", odometer: 20000,
                vehicleType: VehicleType.Sedan,
                costPerKm: 1, costPerDay: 200, vehicleStatus: VehicleStatus.Available) { Id = 2 },

            new Car(regNo: "GHI789", manufacturer: "Tesla", odometer: 1000, vehicleType: VehicleType.Sedan,
                costPerKm: 3, costPerDay: 100, vehicleStatus: VehicleStatus.Available) { Id = 3 },

            new Car(regNo: "JKL012", manufacturer: "Yahama", odometer: 30000,
                vehicleType: VehicleType.Motorcycle,
                costPerKm: 0.5, costPerDay: 300, vehicleStatus: VehicleStatus.Available) { Id = 4 }
        };

        _persons.AddRange(customers);
        _vehicles.AddRange(vehicles);
    }
}