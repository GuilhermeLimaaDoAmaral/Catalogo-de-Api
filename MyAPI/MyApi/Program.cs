
using ApiCatalog.Core.DTOs.Automapper;
using ApiCatalog.Core.Entities;
using ApiCatalog.Core.Interfaces.Repository;
using ApiCatalog.Core.Interfaces.Services;
using ApiCatalog.Core.Services;
using ApiCatalog.Extensions;
using ApiCatalog.Fillters;
using ApiCatalog.Persistence.Context;
using ApiCatalog.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

//Inject

//Repository
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//Service
builder.Services.AddScoped<IProductService, ProductService>();


// Add services to the container.

//Filtro de exeção não tratado
builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(ApiExceptionFilter));
})
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
}).AddNewtonsoftJson();
    

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//DB
builder.Services.AddDbContext<ApplicationDbContext>(option =>
    option.UseSqlite(builder.Configuration.GetConnectionString("TaskListDefaultConnection")));

builder.Services.AddAutoMapper(typeof(ProductDTOAutoMappingExtensionscs));

var app = builder.Build();
 
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ConfigureExceptionHandler(); // Middleweare personalizado
}

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.Run();
