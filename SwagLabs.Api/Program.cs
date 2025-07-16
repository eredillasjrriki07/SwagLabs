using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SwagLabs.Api.ApiVersionSupport;
using SwagLabs.Dal.EfStructures;
using SwagLabs.Services.DataServices;
using SwagLabs.Services.Logging.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Configure logging
builder.ConfigureSerilog();
builder.Services.RegisterLoggingInterfaces();

//Add services to controller
builder.Services.AddControllers()
    .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = null;
            options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            options.JsonSerializerOptions.WriteIndented = true;
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        }
    ).ConfigureApiBehaviorOptions(options =>
        {
            // suppress automatic model binding error
            options.SuppressModelStateInvalidFilter = true;
            //Don't create a problem details error object if set to true
            options.SuppressMapClientErrors = false;
            options.ClientErrorMapping[StatusCodes.Status404NotFound].Link = "https://httpstatuses.com/404";
            options.ClientErrorMapping[StatusCodes.Status404NotFound].Title = "Invalid location";
        }
    );

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin();
    });
});

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagLabsApiVersionConfiguration(new ApiVersion(1, 0));
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("SwagLabs");

builder.Services.AddDbContextPool<AppDbContext>(
    options => options.UseSqlServer(
        connectionString, sqlOptions => sqlOptions.EnableRetryOnFailure().CommandTimeout(60)
    )
);

builder.Services.AddRepositories();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

//Add CORS Policy
app.UseCors("AllowAll");

app.UseAuthorization();
app.MapControllers();

app.Run();
