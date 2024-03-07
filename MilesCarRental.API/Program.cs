using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;
using Vehicle.Persistence.Database;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Configuration;
using Serilog;
using Vehicle.Service.Queries.ServiceQueries;
using Vehicle.Service.Queries.Repository.Interface.IVehicles;



var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(opts =>
    opts.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
    b => b.MigrationsAssembly("MilesCarRental.API")),
    ServiceLifetime.Scoped
);

//builder.Services.AddMediatR(Assembly.Load("Vehicle.Service.EventHandlers"));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.Load("Vehicle.Service.EventHandlers")));
//builder.Services.AddMediatR(cfg => cfg.AsScoped().FromAssemblyName("Vehicle.Service.EventHandlers"));



//Inyección de interfaces y servicios para entidad Vehicles
builder.Services.AddTransient<IReadVehicles, VehicleQueryService>();



builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MilesCarRenta.API", Version = "v1" });

    // Configurar la ruta al archivo XML de documentación
    //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    //c.IncludeXmlComments(xmlPath);
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCorsPolicy", builder =>
    {
        builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
    });
});

//var loggerConfiguration = new LoggerConfiguration()
//    .WriteTo.Syslog(
//        host: configuration.GetValue<string>("Papertrail:host"),
//        port: configuration.GetValue<int>("Papertrail:port") // Asumiendo que también necesitas el puerto
//    );
//Log.Logger = loggerConfiguration.CreateLogger();

//builder.Host.UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MilesCarRenta.API v1");
});

app.UseAuthorization();

app.MapControllers();

app.Run();

