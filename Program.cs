using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using opentickets_backend.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options => { options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; });
builder.Services.AddDbContext<OpenTicketsContext>(opt =>
    opt.UseInMemoryDatabase("opentickets"));
builder.Services.AddCors(options => options.AddPolicy("AllowWebApp", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

var app = builder.Build();
app.UseCors("AllowWebApp");

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();

