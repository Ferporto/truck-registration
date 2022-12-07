namespace TruckRegistration.Trucks.Dtos;

public class TruckOutput
{
    public Guid Id { get; set; }
    public Guid ModelId { get; set; }
    public string ModelName { get; set; }
    public int ModelYear { get; set; }
    public string? LicensePlate { get; set; }
    public int ManufacturingYear { get; set; }

    public TruckOutput()
    {
    }

    public TruckOutput(Truck truck, TruckModel truckModel)
    {
        Id = truck.Id;
        LicensePlate = truck.LicensePlate;
        ManufacturingYear = truck.ManufacturingYear;
        ModelId = truckModel.Id;
        ModelName = truckModel.Type.ToString().ToUpper() + " " + truckModel.Name;
        ModelYear = truckModel.Year;
    }
}