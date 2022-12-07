using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TruckRegistration.Models;
using TruckRegistration.Trucks.Dtos;

namespace TruckRegistration.Trucks.Controllers;

[ApiController]
[Route("truck-models")]
public class TruckModelController : ControllerBase
{
    private readonly Context _context;

    public TruckModelController(Context context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TruckModelInput input)
    {
        var truckModelToInsert = new TruckModel(input);

        _context.Add(truckModelToInsert);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetList()
    {
        var truckModels = await _context.TruckModels.AsNoTracking().ToListAsync();
        var output = truckModels.Select(truckModel => new TruckModelOutput(truckModel));

        return Ok(output);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var truckModel = await _context.TruckModels.FirstOrDefaultAsync(truckModel => truckModel.Id == id);
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
        var truckModelToUpdate = await _context.TruckModels.FirstOrDefaultAsync(truckModel => truckModel.Id == id);
        if (truckModelToUpdate == null)
        {
            return NotFound();
        }

        truckModelToUpdate.Update(input);

        _context.Update(truckModelToUpdate);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var truckModelToDelete = await _context.TruckModels.FirstOrDefaultAsync(truckModel => truckModel.Id == id);
        if (truckModelToDelete == null)
        {
            return NotFound();
        }

        _context.Remove(truckModelToDelete);
        await _context.SaveChangesAsync();

        return Ok();
    }
}