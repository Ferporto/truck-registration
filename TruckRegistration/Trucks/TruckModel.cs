using TruckRegistration.Models;
using TruckRegistration.Trucks.Dtos;
using TruckRegistration.Trucks.Enums;

namespace TruckRegistration.Trucks;

public class TruckModel : Entity
{
    public string Name { get; set; }
    public TruckModelType Type { get; set; }
    public int Year { get; set; }

    public TruckModel()
    {
    }

    public TruckModel(TruckModelInput truckModel)
    {
        Id = truckModel.Id;
        Name = truckModel.Name;
        Type = truckModel.Type;
        Year = truckModel.Year;
    }

    public void Update(TruckModelInput truckModel)
    {
        Name = truckModel.Name;
        Type = truckModel.Type;
        Year = truckModel.Year;
    }
}