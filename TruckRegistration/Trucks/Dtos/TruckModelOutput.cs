using TruckRegistration.Trucks.Enums;

namespace TruckRegistration.Trucks.Dtos;

public class TruckModelOutput
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public TruckModelType Type { get; set; }
    public int Year { get; set; }

    public TruckModelOutput()
    {
    }

    public TruckModelOutput(TruckModel truckModel)
    {
        Id = truckModel.Id;
        Name = truckModel.Name;
        Type = truckModel.Type;
        Year = truckModel.Year;
    }
}