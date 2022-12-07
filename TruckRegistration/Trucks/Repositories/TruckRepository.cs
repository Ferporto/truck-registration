using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TruckRegistration.Models;

namespace TruckRegistration.Trucks.Repositories;

public class TruckRepository : ITruckRepository
{
    private readonly Context _context;

    public TruckRepository(Context context)
    {
        _context = context;
    }
    
    public async Task CreateAsync(Truck truck)
    {
        _context.Add(truck);
        await _context.SaveChangesAsync();
    }

    public IQueryable<Truck> GetAll()
    {
        return _context.Trucks.AsNoTracking();
    }

    public async Task<Truck> FirstAsync(Expression<Func<Truck, bool>> predicate)
    {
        return await _context.Trucks.FirstAsync(predicate);
    }
    
    public async Task<Truck?> FirstOrDefaultAsync(Expression<Func<Truck, bool>> predicate)
    {
        return await _context.Trucks.FirstOrDefaultAsync(predicate);
    }
    
    public async Task UpdateAsync(Truck truck)
    {
        _context.Update(truck);
        await _context.SaveChangesAsync();
    }
    
    public async Task DeleteAsync(Truck truck)
    {
        _context.Remove(truck);
        await _context.SaveChangesAsync();
    }
}