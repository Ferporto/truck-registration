using System.Linq.Expressions;

namespace TruckRegistration.Trucks.Repositories;

public interface ITruckModelModelRepository
{
    Task CreateAsync(TruckModel truckModel);
    IQueryable<TruckModel> GetAll();
    Task<TruckModel> FirstAsync(Expression<Func<TruckModel, bool>> predicate);
    Task<TruckModel?> FirstOrDefaultAsync(Expression<Func<TruckModel, bool>> predicate);
    Task UpdateAsync(TruckModel truckModel);
    Task DeleteAsync(TruckModel truckModel);
}