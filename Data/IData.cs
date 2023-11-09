using System.Collections;
using System.Linq.Expressions;
using Common.Enums;
using Common.Interfaces;

namespace Data;

public interface IData
{
    List<T> Get<T>(Expression<Func<T, bool>>? expression);
    T? Single<T>(Expression<Func<T, bool>>? expression);
    public void Add<T>(T item);
    
    int NextVehicleId { get; }
    int NextPersonId { get; }
    int NextBookingId { get; }

    public IBooking ReturnVehicle(int vehicleId, double distance);
    public IBooking RentVehicle(int vehicleId, int customerId);
    
    public string[] VehicleStatusNames => Enum.GetNames(typeof(VehicleStatus));
    public string[] VehicleTypeNames => Enum.GetNames(typeof(VehicleType));
    public VehicleType GetVehicleType(string name) => (VehicleType)Enum.Parse(typeof(VehicleType), name);
}