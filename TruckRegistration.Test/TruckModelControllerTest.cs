using System.Net;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TruckRegistration.Trucks;
using TruckRegistration.Trucks.Controllers;
using TruckRegistration.Trucks.Dtos;
using TruckRegistration.Trucks.Enums;
using TruckRegistration.Trucks.Repositories;

namespace TruckRegistration.Test;

public class TruckModelControllerTest
{
    [Fact]
    public async Task GetList()
    {
        // Arrange  
        var mockedTruckModelModelRepository = new Mock<ITruckModelModelRepository>();

        var modelId = Guid.Parse("349ACCF3-B94F-4C7C-B2A1-3137D30A97E8");
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
        
        mockedTruckModelModelRepository.Setup(repository => repository.GetAll())
            .Returns(expectedTruckModels.AsQueryable());

        var controller = new TruckModelController(mockedTruckModelModelRepository.Object);

        // Act
        var output = await controller.GetList();

        // Assert
        var expectedTruckModel = expectedTruckModels[0];
        var expectedOutput = new TruckModelOutput
        {
            Id = expectedTruckModel.Id,
            Name = expectedTruckModel.Name,
            Type = expectedTruckModel.Type,
            Year = expectedTruckModel.Year,
        };

        var okResult = output as OkObjectResult;
        Assert.NotNull(okResult);
        Assert.True(okResult.StatusCode == (int)HttpStatusCode.OK);

        var okResultValue = okResult.Value as List<TruckModelOutput>;
        Assert.True(okResultValue.Count == 1);

        var okResultValue1 = okResultValue[0];
        Assert.True(okResultValue1.Id == expectedOutput.Id);
        Assert.True(okResultValue1.Name == expectedOutput.Name);
        Assert.True(okResultValue1.Type == expectedOutput.Type);
        Assert.True(okResultValue1.Year == expectedOutput.Year);

        mockedTruckModelModelRepository.Verify(repository => repository.GetAll(), Times.Once);
    }

    [Fact]
    public async Task Get()
    {
        // Arrange  
        var mockedTruckModelModelRepository = new Mock<ITruckModelModelRepository>();

        var modelId = Guid.Parse("349ACCF3-B94F-4C7C-B2A1-3137D30A97E8");
        var expectedTruckModel = new TruckModel
        {
            Id = modelId,
            Name = "Model 01",
            Type = TruckModelType.Fh,
            Year = 2022
        };

        mockedTruckModelModelRepository.Setup(repository =>
                repository.FirstOrDefaultAsync(truckModel => truckModel.Id == modelId))
            .Returns(Task.FromResult(expectedTruckModel)!);

        var controller = new TruckModelController(mockedTruckModelModelRepository.Object);

        // Act
        var output = await controller.Get(modelId);

        // Assert
        var expectedOutput = new TruckModelOutput
        {
            Id = expectedTruckModel.Id,
            Name = expectedTruckModel.Name,
            Type = expectedTruckModel.Type,
            Year = expectedTruckModel.Year,
        };

        var okResult = output as OkObjectResult;
        Assert.NotNull(okResult);
        Assert.True(okResult.StatusCode == (int)HttpStatusCode.OK);

        var okResultValue = okResult.Value as TruckModelOutput;
        Assert.True(okResultValue.Id == expectedOutput.Id);
        Assert.True(okResultValue.Name == expectedOutput.Name);
        Assert.True(okResultValue.Type == expectedOutput.Type);
        Assert.True(okResultValue.Year == expectedOutput.Year);

        mockedTruckModelModelRepository.Verify(
            repository => repository.FirstOrDefaultAsync(truckModel => truckModel.Id == modelId), Times.Once);
    }

    [Fact]
    public async Task GetNotFound()
    {
        // Arrange  
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

        var controller = new TruckModelController(mockedTruckModelModelRepository.Object);

        // Act
        var output = await controller.Get(truckId);

        // Assert
        var notFoundResult = output as NotFoundResult;
        Assert.NotNull(notFoundResult);
        Assert.True(notFoundResult.StatusCode == (int)HttpStatusCode.NotFound);
        
        mockedTruckModelModelRepository.Verify(
            repository => repository.FirstAsync(truckModel => truckModel.Id == expectedTruck.ModelId), Times.Never);
    }

    [Fact]
    public async Task Create()
    {
        // Arrange  
        var mockedTruckModelModelRepository = new Mock<ITruckModelModelRepository>();

        var modelId = Guid.Parse("349ACCF3-B94F-4C7C-B2A1-3137D30A97E8");
        var input = new TruckModelInput
        {
            Id = modelId,
            Name = "Model 01",
            Type = TruckModelType.Fm,
            Year = 2023,
        };

        var controller = new TruckModelController(mockedTruckModelModelRepository.Object);

        // Act
        var output = await controller.Create(input);

        // Assert
        var okResult = output as OkResult;
        Assert.NotNull(okResult);
        Assert.True(okResult.StatusCode == (int)HttpStatusCode.OK);

        mockedTruckModelModelRepository.Verify(repository => repository.CreateAsync(
            It.Is<TruckModel>(truckModel => truckModel.Id == input.Id && truckModel.Name == input.Name &&
                                  truckModel.Type == input.Type && truckModel.Year == input.Year)
        ), Times.Once);
    }

    [Fact]
    public async Task Update()
    {
        // Arrange  
        var mockedTruckModelModelRepository = new Mock<ITruckModelModelRepository>();

        var modelId = Guid.Parse("349ACCF3-B94F-4C7C-B2A1-3137D30A97E8");
        var input = new TruckModelInput
        {
            Id = modelId,
            Name = "Model 01",
            Type = TruckModelType.Fm,
            Year = 2023,
        };

        var expectedTruckModel = new TruckModel
        {
            Id = modelId,
            Name = "Model 01",
            Type = TruckModelType.Fm,
            Year = 2023,
        };

        mockedTruckModelModelRepository.Setup(repository => repository.FirstOrDefaultAsync(truckModel => truckModel.Id == modelId))
            .Returns(Task.FromResult(expectedTruckModel)!);

        var controller = new TruckModelController(mockedTruckModelModelRepository.Object);

        // Act
        var output = await controller.Update(modelId, input);

        // Assert
        var okResult = output as OkResult;
        Assert.NotNull(okResult);
        Assert.True(okResult.StatusCode == (int)HttpStatusCode.OK);

        mockedTruckModelModelRepository.Verify(repository => repository.UpdateAsync(
            It.Is<TruckModel>(truckModel => truckModel.Id == input.Id && truckModel.Name == input.Name &&
                                            truckModel.Type == input.Type && truckModel.Year == input.Year)
        ), Times.Once);
    }

    [Fact]
    public async Task UpdateNotFound()
    {
        // Arrange  
        var mockedTruckModelModelRepository = new Mock<ITruckModelModelRepository>();

        var modelId = Guid.Parse("349ACCF3-B94F-4C7C-B2A1-3137D30A97E8");
        var input = new TruckModelInput
        {
            Id = modelId,
            Name = "Model 01",
            Type = TruckModelType.Fm,
            Year = 2023,
        };

        var controller = new TruckModelController(mockedTruckModelModelRepository.Object);

        // Act
        var output = await controller.Update(modelId, input);

        // Assert
        var notFoundResult = output as NotFoundResult;
        Assert.NotNull(notFoundResult);
        Assert.True(notFoundResult.StatusCode == (int)HttpStatusCode.NotFound);

        mockedTruckModelModelRepository.Verify(repository => repository.UpdateAsync(
            It.Is<TruckModel>(truckModel => truckModel.Id == input.Id && truckModel.Name == input.Name &&
                                            truckModel.Type == input.Type && truckModel.Year == input.Year)
        ), Times.Never);
    }

    [Fact]
    public async Task Delete()
    {
        // Arrange  
        var mockedTruckModelModelRepository = new Mock<ITruckModelModelRepository>();

        var modelId = Guid.Parse("349ACCF3-B94F-4C7C-B2A1-3137D30A97E8");
        var expectedTruckModel = new TruckModel
        {
            Id = modelId,
            Name = "Model 01",
            Type = TruckModelType.Fm,
            Year = 2023,
        };

        mockedTruckModelModelRepository.Setup(repository => repository.FirstOrDefaultAsync(truckModel => truckModel.Id == modelId))
            .Returns(Task.FromResult(expectedTruckModel)!);

        var controller = new TruckModelController(mockedTruckModelModelRepository.Object);

        // Act
        var output = await controller.Delete(modelId);

        // Assert
        var okResult = output as OkResult;
        Assert.NotNull(okResult);
        Assert.True(okResult.StatusCode == (int)HttpStatusCode.OK);

        mockedTruckModelModelRepository.Verify(repository => repository.DeleteAsync(
            It.Is<TruckModel>(truckModel => truckModel.Id == expectedTruckModel.Id && truckModel.Name == expectedTruckModel.Name &&
                                            truckModel.Type == expectedTruckModel.Type && truckModel.Year == expectedTruckModel.Year)
        ), Times.Once);
    }

    [Fact]
    public async Task DeleteNotFound()
    {
        // Arrange  
        var mockedTruckModelModelRepository = new Mock<ITruckModelModelRepository>();

        var modelId = Guid.Parse("349ACCF3-B94F-4C7C-B2A1-3137D30A97E8");

        var controller = new TruckModelController(mockedTruckModelModelRepository.Object);

        // Act
        var output = await controller.Delete(modelId);

        // Assert
        var notFoundResult = output as NotFoundResult;
        Assert.NotNull(notFoundResult);
        Assert.True(notFoundResult.StatusCode == (int)HttpStatusCode.NotFound);

        mockedTruckModelModelRepository.Verify(repository => repository.DeleteAsync(
            It.Is<TruckModel>(truckModel => truckModel.Id == modelId)
        ), Times.Never);
    }
}