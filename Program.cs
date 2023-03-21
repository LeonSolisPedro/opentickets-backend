using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using opentickets_backend.Data;

// Add services to the container.
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers()
    .AddJsonOptions(options => { options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; });
builder.Services.AddDbContext<OpenTicketsContext>(opt =>
    opt.UseInMemoryDatabase("opentickets"));
builder.Services.AddCors(options => options.AddPolicy("AllowWebApp", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));


// Configure the app
var app = builder.Build();
using (var scope = app.Services.CreateScope()) { var services = scope.ServiceProvider; Seeder.Initialize(services); }
app.UseCors("AllowWebApp");
app.UseAuthorization();
app.MapControllers();
app.Run();

