using System.Net;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TruckRegistration.Trucks;
using TruckRegistration.Trucks.Controllers;
using TruckRegistration.Trucks.Dtos;
using TruckRegistration.Trucks.Enums;
using TruckRegistration.Trucks.Repositories;

namespace TruckRegistration.Test;

public class TruckControllerTest
{
    [Fact]
    public async Task GetList()
    {
        // Arrange  
        var mockedTruckRepository = new Mock<ITruckRepository>();
        var mockedTruckModelModelRepository = new Mock<ITruckModelModelRepository>();

        var truckId = Guid.Parse("D974E229-92C7-4981-8DD0-EE6274309CAA");
        var modelId = Guid.Parse("349ACCF3-B94F-4C7C-B2A1-3137D30A97E8");

        var expectedTrucks = new List<Truck>
        {
            new()
            {
                Id = truckId,
                LicensePlate = "Plate 01",
                ManufacturingYear = 2022,
                ModelId = modelId
            }
        };

        var expectedTruckModels = new List<TruckModel>
        {
            new()
            {
                Id = modelId,
                Name = "Model 01",
                Type = TruckModelType.Fh,
                Year = 2022
            }
        };

        mockedTruckRepository.Setup(repository => repository.GetAll())
            .Returns(expectedTrucks.AsQueryable());

        mockedTruckModelModelRepository.Setup(repository => repository.GetAll())
            .Returns(expectedTruckModels.AsQueryable());

        var controller = new TruckController(mockedTruckRepository.Object, mockedTruckModelModelRepository.Object);

        // Act
        var output = await controller.GetList();

        // Assert
        var expectedTruck = expectedTrucks[0];
        var expectedTruckModel = expectedTruckModels[0];
        var expectedOutput = new TruckOutput
        {
            Id = expectedTruck.Id,
            LicensePlate = expectedTruck.LicensePlate,
            ManufacturingYear = expectedTruck.ManufacturingYear,
            ModelId = expectedTruckModel.Id,
            ModelName = expectedTruckModel.Type.ToString().ToUpper() + " " + expectedTruckModel.Name,
            ModelYear = expectedTruckModel.Year
        };

        var okResult = output as OkObjectResult;
        Assert.NotNull(okResult);
        Assert.True(okResult.StatusCode == (int)HttpStatusCode.OK);

        var okResultValue = okResult.Value as List<TruckOutput>;
        Assert.True(okResultValue.Count == 1);

        var okResultValue1 = okResultValue[0];
        Assert.True(okResultValue1.Id == expectedOutput.Id);
        Assert.True(okResultValue1.LicensePlate == expectedOutput.LicensePlate);
        Assert.True(okResultValue1.ManufacturingYear == expectedOutput.ManufacturingYear);
        Assert.True(okResultValue1.ModelId == expectedOutput.ModelId);
        Assert.True(okResultValue1.ModelName == expectedOutput.ModelName);
        Assert.True(okResultValue1.ModelYear == expectedOutput.ModelYear);

        mockedTruckRepository.Verify(repository => repository.GetAll(), Times.Once);
        mockedTruckModelModelRepository.Verify(repository => repository.GetAll(), Times.Once);
    }

    [Fact]
    public async Task Get()
    {
        // Arrange  
        var mockedTruckRepository = new Mock<ITruckRepository>();
        var mockedTruckModelModelRepository = new Mock<ITruckModelModelRepository>();

        var truckId = Guid.Parse("D974E229-92C7-4981-8DD0-EE6274309CAA");
        var modelId = Guid.Parse("349ACCF3-B94F-4C7C-B2A1-3137D30A97E8");

        var expectedTruck = new Truck
        {
            Id = truckId,
            LicensePlate = "Plate 01",
            ManufacturingYear = 2022,
            ModelId = modelId
        };

        var expectedTruckModel = new TruckModel
        {
            Id = modelId,
            Name = "Model 01",
            Type = TruckModelType.Fh,
            Year = 2022
        };

        mockedTruckRepository.Setup(repository => repository.FirstOrDefaultAsync(truck => truck.Id == truckId))
            .Returns(Task.FromResult(expectedTruck)!);

        mockedTruckModelModelRepository.Setup(repository =>
                repository.FirstAsync(truckModel => truckModel.Id == expectedTruck.ModelId))
            .Returns(Task.FromResult(expectedTruckModel));

        var controller = new TruckController(mockedTruckRepository.Object, mockedTruckModelModelRepository.Object);

        // Act
        var output = await controller.Get(truckId);

        // Assert
        var expectedOutput = new TruckOutput
        {
            Id = expectedTruck.Id,
            LicensePlate = expectedTruck.LicensePlate,
            ManufacturingYear = expectedTruck.ManufacturingYear,
            ModelId = expectedTruckModel.Id,
            ModelName = expectedTruckModel.Type.ToString().ToUpper() + " " + expectedTruckModel.Name,
            ModelYear = expectedTruckModel.Year
        };

        var okResult = output as OkObjectResult;
        Assert.NotNull(okResult);
        Assert.True(okResult.StatusCode == (int)HttpStatusCode.OK);

        var okResultValue = okResult.Value as TruckOutput;
        Assert.True(okResultValue.Id == expectedOutput.Id);
        Assert.True(okResultValue.LicensePlate == expectedOutput.LicensePlate);
        Assert.True(okResultValue.ManufacturingYear == expectedOutput.ManufacturingYear);
        Assert.True(okResultValue.ModelId == expectedOutput.ModelId);
        Assert.True(okResultValue.ModelName == expectedOutput.ModelName);
        Assert.True(okResultValue.ModelYear == expectedOutput.ModelYear);

        mockedTruckRepository.Verify(repository => repository.FirstOrDefaultAsync(truck => truck.Id == truckId),
            Times.Once);
        mockedTruckModelModelRepository.Verify(
            repository => repository.FirstAsync(truck => truck.Id == expectedTruck.ModelId), Times.Once);
    }

    [Fact]
    public async Task GetNotFound()
    {
        // Arrange  
        var mockedTruckRepository = new Mock<ITruckRepository>();
        var mockedTruckModelModelRepository = new Mock<ITruckModelModelRepository>();

        var truckId = Guid.Parse("D974E229-92C7-4981-8DD0-EE6274309CAA");
        var modelId = Guid.Parse("349ACCF3-B94F-4C7C-B2A1-3137D30A97E8");

        var expectedTruck = new Truck
        {
            Id = truckId,
            LicensePlate = "Plate 01",
            ManufacturingYear = 2022,
            ModelId = modelId
        };

        var controller = new TruckController(mockedTruckRepository.Object, mockedTruckModelModelRepository.Object);

        // Act
        var output = await controller.Get(truckId);

        // Assert
        var notFoundResult = output as NotFoundResult;
        Assert.NotNull(notFoundResult);
        Assert.True(notFoundResult.StatusCode == (int)HttpStatusCode.NotFound);

        mockedTruckRepository.Verify(repository => repository.FirstOrDefaultAsync(truck => truck.Id == truckId),
            Times.Once);
        mockedTruckModelModelRepository.Verify(
            repository => repository.FirstAsync(truck => truck.Id == expectedTruck.ModelId), Times.Never);
    }

    [Fact]
    public async Task Create()
    {
        // Arrange  
        var mockedTruckRepository = new Mock<ITruckRepository>();
        var mockedTruckModelModelRepository = new Mock<ITruckModelModelRepository>();

        var truckId = Guid.Parse("D974E229-92C7-4981-8DD0-EE6274309CAA");
        var modelId = Guid.Parse("349ACCF3-B94F-4C7C-B2A1-3137D30A97E8");

        var input = new TruckInput
        {
            Id = truckId,
            LicensePlate = "Plate 01",
            ModelId = modelId
        };

        var controller = new TruckController(mockedTruckRepository.Object, mockedTruckModelModelRepository.Object);

        // Act
        var output = await controller.Create(input);

        // Assert
        var okResult = output as OkResult;
        Assert.NotNull(okResult);
        Assert.True(okResult.StatusCode == (int)HttpStatusCode.OK);

        mockedTruckRepository.Verify(repository => repository.CreateAsync(
            It.Is<Truck>(truck => truck.Id == input.Id && truck.LicensePlate == input.LicensePlate &&
                                  truck.ModelId == input.ModelId)
        ), Times.Once);
    }

    [Fact]
    public async Task Update()
    {
        // Arrange  
        var mockedTruckRepository = new Mock<ITruckRepository>();
        var mockedTruckModelModelRepository = new Mock<ITruckModelModelRepository>();

        var truckId = Guid.Parse("D974E229-92C7-4981-8DD0-EE6274309CAA");
        var modelId = Guid.Parse("349ACCF3-B94F-4C7C-B2A1-3137D30A97E8");

        var input = new TruckInput
        {
            Id = truckId,
            LicensePlate = "Plate 01",
            ModelId = modelId
        };

        var expectedTruck = new Truck
        {
            Id = truckId,
            LicensePlate = "Plate 01",
            ManufacturingYear = 2022,
            ModelId = modelId
        };

        mockedTruckRepository.Setup(repository => repository.FirstOrDefaultAsync(truck => truck.Id == truckId))
            .Returns(Task.FromResult(expectedTruck)!);

        var controller = new TruckController(mockedTruckRepository.Object, mockedTruckModelModelRepository.Object);

        // Act
        var output = await controller.Update(truckId, input);

        // Assert
        var okResult = output as OkResult;
        Assert.NotNull(okResult);
        Assert.True(okResult.StatusCode == (int)HttpStatusCode.OK);

        mockedTruckRepository.Verify(repository => repository.UpdateAsync(
            It.Is<Truck>(truck => truck.Id == input.Id && truck.LicensePlate == input.LicensePlate &&
                                  truck.ModelId == input.ModelId)
        ), Times.Once);
    }

    [Fact]
    public async Task UpdateNotFound()
    {
        // Arrange  
        var mockedTruckRepository = new Mock<ITruckRepository>();
        var mockedTruckModelModelRepository = new Mock<ITruckModelModelRepository>();

        var truckId = Guid.Parse("D974E229-92C7-4981-8DD0-EE6274309CAA");
        var modelId = Guid.Parse("349ACCF3-B94F-4C7C-B2A1-3137D30A97E8");

        var input = new TruckInput
        {
            Id = truckId,
            LicensePlate = "Plate 01",
            ModelId = modelId
        };

        var controller = new TruckController(mockedTruckRepository.Object, mockedTruckModelModelRepository.Object);

        // Act
        var output = await controller.Update(truckId, input);

        // Assert
        var notFoundResult = output as NotFoundResult;
        Assert.NotNull(notFoundResult);
        Assert.True(notFoundResult.StatusCode == (int)HttpStatusCode.NotFound);

        mockedTruckRepository.Verify(repository => repository.UpdateAsync(
            It.Is<Truck>(truck => truck.Id == input.Id && truck.LicensePlate == input.LicensePlate &&
                                  truck.ModelId == input.ModelId)
        ), Times.Never);
    }

    [Fact]
    public async Task Delete()
    {
        // Arrange  
        var mockedTruckRepository = new Mock<ITruckRepository>();
        var mockedTruckModelModelRepository = new Mock<ITruckModelModelRepository>();

        var truckId = Guid.Parse("D974E229-92C7-4981-8DD0-EE6274309CAA");
        var modelId = Guid.Parse("349ACCF3-B94F-4C7C-B2A1-3137D30A97E8");

        var expectedTruck = new Truck
        {
            Id = truckId,
            LicensePlate = "Plate 01",
            ManufacturingYear = 2022,
            ModelId = modelId
        };

        mockedTruckRepository.Setup(repository => repository.FirstOrDefaultAsync(truck => truck.Id == truckId))
            .Returns(Task.FromResult(expectedTruck)!);

        var controller = new TruckController(mockedTruckRepository.Object, mockedTruckModelModelRepository.Object);

        // Act
        var output = await controller.Delete(truckId);

        // Assert
        var okResult = output as OkResult;
        Assert.NotNull(okResult);
        Assert.True(okResult.StatusCode == (int)HttpStatusCode.OK);

        mockedTruckRepository.Verify(repository => repository.DeleteAsync(
            It.Is<Truck>(truck => truck.Id == expectedTruck.Id && truck.LicensePlate == expectedTruck.LicensePlate &&
                                  truck.ModelId == expectedTruck.ModelId)
        ), Times.Once);
    }

    [Fact]
    public async Task DeleteNotFound()
    {
        // Arrange  
        var mockedTruckRepository = new Mock<ITruckRepository>();
        var mockedTruckModelModelRepository = new Mock<ITruckModelModelRepository>();

        var truckId = Guid.Parse("D974E229-92C7-4981-8DD0-EE6274309CAA");

        var controller = new TruckController(mockedTruckRepository.Object, mockedTruckModelModelRepository.Object);

        // Act
        var output = await controller.Delete(truckId);

        // Assert
        var notFoundResult = output as NotFoundResult;
        Assert.NotNull(notFoundResult);
        Assert.True(notFoundResult.StatusCode == (int)HttpStatusCode.NotFound);

        mockedTruckRepository.Verify(repository => repository.DeleteAsync(
            It.Is<Truck>(truck => truck.Id == truckId)
        ), Times.Never);
    }
}