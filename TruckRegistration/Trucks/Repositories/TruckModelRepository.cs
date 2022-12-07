using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TruckRegistration.Models;

namespace TruckRegistration.Trucks.Repositories;

public class TruckModelModelRepository : ITruckModelModelRepository
{
    private readonly Context _context;

    public TruckModelModelRepository(Context context)
    {
        _context = context;
    }
    
    public async Task CreateAsync(TruckModel truckModel)
    {
        _context.Add(truckModel);
        await _context.SaveChangesAsync();
    }

    public IQueryable<TruckModel> GetAll()
    {
        return _context.TruckModels.AsNoTracking();
    }

    public async Task<TruckModel> FirstAsync(Expression<Func<TruckModel, bool>> predicate)
    {
        return await _context.TruckModels.FirstAsync(predicate);
    }
    
    public async Task<TruckModel?> FirstOrDefaultAsync(Expression<Func<TruckModel, bool>> predicate)
    {
        return await _context.TruckModels.FirstOrDefaultAsync(predicate);
    }
    
    public async Task UpdateAsync(TruckModel truckModel)
    {
        _context.Update(truckModel);
        await _context.SaveChangesAsync();
    }
    
    public async Task DeleteAsync(TruckModel truckModel)
    {
        _context.Remove(truckModel);
        await _context.SaveChangesAsync();
    }
}