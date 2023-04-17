using ApplicationCore.IServices;
using ApplicationCore.IServices.Generic;
using ApplicationCore.Services;
using Infrastructure.Context;
using Infrastructure.Repositories.Generic;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;


// Add services to the container.
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers()
    .AddJsonOptions(options => { options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; });
builder.Services.AddDbContext<OpenTicketsContext>(opt => opt.UseInMemoryDatabase("opentickets"));
builder.Services.AddCors(options => options.AddPolicy("AllowWebApp", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));
builder.Services.AddScoped<IComputadoraService, ComputadoraService>();
builder.Services.AddScoped<ITicketService, TicketService>();



// Configure the app
var app = builder.Build();
using (var scope = app.Services.CreateScope()) { var services = scope.ServiceProvider; OpenTicketsSeeder.Initialize(services); }
app.UseCors("AllowWebApp");
app.UseAuthorization();
app.MapControllers();
app.Run();
