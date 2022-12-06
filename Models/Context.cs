using Microsoft.EntityFrameworkCore;
using TruckRegistration.Trucks;

namespace TruckRegistration.Models;

public class Context : DbContext
{
    public DbSet<Truck> Trucks { get; set; }
    public DbSet<TruckModel> TruckModels { get; set; }

    public Context(DbContextOptions<Context> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Truck>().ToTable("Truck");
        modelBuilder.Entity<TruckModel>().ToTable("TruckModel");
    }
}