namespace TruckRegistration.Trucks.Dtos;

public class TruckInput
{
    public Guid Id { get; set; }
    public Guid ModelId { get; set; }
    public string LicensePlate { get; set; }
}