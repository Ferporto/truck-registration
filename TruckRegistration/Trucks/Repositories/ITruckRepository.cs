using System.Linq.Expressions;

namespace TruckRegistration.Trucks.Repositories;

public interface ITruckRepository
{
    Task CreateAsync(Truck truck);
    IQueryable<Truck> GetAll();
    Task<Truck> FirstAsync(Expression<Func<Truck, bool>> predicate);
    Task<Truck?> FirstOrDefaultAsync(Expression<Func<Truck, bool>> predicate);
    Task UpdateAsync(Truck truck);
    Task DeleteAsync(Truck truck);
}