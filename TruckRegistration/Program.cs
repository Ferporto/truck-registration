using Microsoft.EntityFrameworkCore;
using TruckRegistration.Models;
using TruckRegistration.Trucks.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<ITruckRepository, TruckRepository>();
builder.Services.AddScoped<ITruckModelModelRepository, TruckModelModelRepository>();

builder.Services.AddDbContext<Context>(options => options
    .UseSqlServer("Data Source=.;Initial Catalog=Truck_Registration;Integrated Security=True;"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<Context>();    
    context.Database.Migrate();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapControllers();
app.MapFallbackToFile("index.html");
app.Run();