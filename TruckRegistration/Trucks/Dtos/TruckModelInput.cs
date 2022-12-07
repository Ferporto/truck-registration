using TruckRegistration.Trucks.Enums;

namespace TruckRegistration.Trucks.Dtos;

public class TruckModelInput
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public TruckModelType Type { get; set; }
    public int Year { get; set; }
}