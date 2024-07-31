using System.Text.Json.Serialization;
using System.Threading.RateLimiting;
using Asp.Versioning;
using Core.Repositories;
using Core.Services;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;


// Add services to the container.
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
  options.AddPolicy("CorsPolicy", builder =>
  builder.AllowAnyOrigin()
  .AllowAnyMethod()
  .AllowAnyHeader()
  );
});
builder.Services.AddRateLimiter(_ => _
.AddFixedWindowLimiter(policyName: "fixed", options =>
{
  options.PermitLimit = 250;
  options.Window = TimeSpan.FromMinutes(1);
  options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
  options.QueueLimit = 50;
}));
builder.Services.AddControllers(options => {
  options.RespectBrowserAcceptHeader = true;
  options.ReturnHttpNotAcceptable = true;
}).AddJsonOptions(options => {
  options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1);
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ApiVersionReader = ApiVersionReader.Combine(
        new UrlSegmentApiVersionReader());
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});
// .AddJsonOptions(options => { options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; });
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("opentickets"));
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<ComputadoraService>();
builder.Services.AddScoped<EmpleadoService>();
builder.Services.AddScoped<TicketService>();
builder.Services.AddScoped<IComputadoraRepository, ComputadoraRepository>();


// Configure the app
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
  app.UseDeveloperExceptionPage();
  app.UseMigrationsEndPoint();
}
using (var scope = app.Services.CreateScope())
{
  var services = scope.ServiceProvider;
  var context = services.GetRequiredService<AppDbContext>();
  await AppDbSeeder.SeedAsync(context);
}
app.UseCors("CorsPolicy");
app.UseRateLimiter();
app.UseAuthorization();
app.MapControllers().RequireRateLimiting("fixed");
app.Run();
