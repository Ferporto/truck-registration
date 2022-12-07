using TruckRegistration.Models;
using TruckRegistration.Trucks.Dtos;

namespace TruckRegistration.Trucks;

public class Truck : Entity
{
    public Guid ModelId { get; set; }
    public string? LicensePlate { get; set; }
    public int ManufacturingYear { get; set; }

    public Truck()
    {
    }

    public Truck(TruckInput truck)
    {
        Id = truck.Id;
        ModelId = truck.ModelId;
        LicensePlate = truck.LicensePlate;
        ManufacturingYear = DateTime.UtcNow.Year;
    }
    
    public void Update(TruckInput truck)
    {
        ModelId = truck.ModelId;
        LicensePlate = truck.LicensePlate;
    }
}