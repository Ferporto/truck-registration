using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TruckRegistration.Models;
using TruckRegistration.Trucks.Dtos;

namespace TruckRegistration.Trucks.Controllers;

[ApiController]
[Route("trucks")]
public class TruckController : ControllerBase
{
    private readonly Context _context;

    public TruckController(Context context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TruckInput input)
    {
        var truckToInsert = new Truck(input);

        _context.Add(truckToInsert);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetList()
    {
        var output = await _context.Trucks.AsNoTracking()
            .Join(_context.TruckModels.AsNoTracking(),
                truck => truck.ModelId, truckModel => truckModel.Id,
                (truck, truckModel) => new TruckOutput(truck, truckModel)).ToListAsync();

        return Ok(output);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var truck = await _context.Trucks.FirstOrDefaultAsync(truck => truck.Id == id);
        if (truck == null)
        {
            return NotFound();
        }
        
        var truckModel = await _context.TruckModels.FirstAsync(truckModel => truckModel.Id == truck.ModelId);
        var output = new TruckOutput(truck, truckModel);

        return Ok(output);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] TruckInput input)
    {
        var truckToUpdate = await _context.Trucks.FirstOrDefaultAsync(truck => truck.Id == id);
        if (truckToUpdate == null)
        {
            return NotFound();
        }

        truckToUpdate.Update(input);

        _context.Update(truckToUpdate);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var truckToDelete = await _context.Trucks.FirstOrDefaultAsync(truck => truck.Id == id);
        if (truckToDelete == null)
        {
            return NotFound();
        }

        _context.Remove(truckToDelete);
        await _context.SaveChangesAsync();

        return Ok();
    }
}