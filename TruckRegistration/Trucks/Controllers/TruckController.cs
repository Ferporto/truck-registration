using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TruckRegistration.Trucks.Dtos;
using TruckRegistration.Trucks.Repositories;

namespace TruckRegistration.Trucks.Controllers;

[ApiController]
[Route("trucks")]
public class TruckController : ControllerBase
{
    private readonly ITruckRepository _truckRepository;
    private readonly ITruckModelModelRepository _truckModelModelRepository;

    public TruckController(ITruckRepository truckRepository, ITruckModelModelRepository truckModelModelRepository)
    {
        _truckRepository = truckRepository;
        _truckModelModelRepository = truckModelModelRepository;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TruckInput input)
    {
        var truckToInsert = new Truck(input);
        await _truckRepository.CreateAsync(truckToInsert);

        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetList()
    {
        var output = await _truckRepository.GetAll()
            .Join(_truckModelModelRepository.GetAll(),
                truck => truck.ModelId, truckModel => truckModel.Id,
                (truck, truckModel) => new TruckOutput(truck, truckModel)).ToListAsync();

        return Ok(output);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var truck = await _truckRepository.FirstOrDefaultAsync(truck => truck.Id == id);
        if (truck == null)
        {
            return NotFound();
        }
        
        var truckModel = await _truckModelModelRepository.FirstAsync(truckModel => truckModel.Id == truck.ModelId);
        var output = new TruckOutput(truck, truckModel);

        return Ok(output);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] TruckInput input)
    {
        var truckToUpdate = await _truckRepository.FirstOrDefaultAsync(truck => truck.Id == id);
        if (truckToUpdate == null)
        {
            return NotFound();
        }

        truckToUpdate.Update(input);
        await _truckRepository.UpdateAsync(truckToUpdate);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var truckToDelete = await _truckRepository.FirstOrDefaultAsync(truck => truck.Id == id);
        if (truckToDelete == null)
        {
            return NotFound();
        }

        await _truckRepository.DeleteAsync(truckToDelete);

        return Ok();
    }
}