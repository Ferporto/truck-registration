using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TruckRegistration.Models;
using TruckRegistration.Trucks.Dtos;
using TruckRegistration.Trucks.Repositories;

namespace TruckRegistration.Trucks.Controllers;

[ApiController]
[Route("truck-models")]
public class TruckModelController : ControllerBase
{
    private readonly ITruckModelModelRepository _truckModelModelRepository;

    public TruckModelController(ITruckModelModelRepository truckModelModelRepository)
    {
        _truckModelModelRepository = truckModelModelRepository;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TruckModelInput input)
    {
        var truckModelToInsert = new TruckModel(input);
        await _truckModelModelRepository.CreateAsync(truckModelToInsert);

        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetList()
    {
        var truckModels = await _truckModelModelRepository.GetAll().ToListAsync();
        var output = truckModels.Select(truckModel => new TruckModelOutput(truckModel));

        return Ok(output);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var truckModel = await _truckModelModelRepository.FirstOrDefaultAsync(truckModel => truckModel.Id == id);
        if (truckModel == null)
        {
            return NotFound();
        }

        var output = new TruckModelOutput(truckModel);

        return Ok(output);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] TruckModelInput input)
    {
        var truckModelToUpdate = await _truckModelModelRepository.FirstOrDefaultAsync(truckModel => truckModel.Id == id);
        if (truckModelToUpdate == null)
        {
            return NotFound();
        }

        truckModelToUpdate.Update(input);
        await _truckModelModelRepository.UpdateAsync(truckModelToUpdate);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var truckModelToDelete = await _truckModelModelRepository.FirstOrDefaultAsync(truckModel => truckModel.Id == id);
        if (truckModelToDelete == null)
        {
            return NotFound();
        }

        await _truckModelModelRepository.DeleteAsync(truckModelToDelete);

        return Ok();
    }
}