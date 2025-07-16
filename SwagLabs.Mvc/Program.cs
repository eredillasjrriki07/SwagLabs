using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SwagLabs.Dal.EfStructures;
using SwagLabs.Services.ApiWrapper.Configuration;
using SwagLabs.Services.DataServices;
using SwagLabs.Services.Logging.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureSerilog();
builder.Services.RegisterLoggingInterfaces();

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("SwagLabs");

builder.Services.AddDbContextPool<AppDbContext>(
    options => options.UseSqlServer(
        connectionString, sqlOptions => sqlOptions.EnableRetryOnFailure().CommandTimeout(60)
    )
);

builder.Services.AddRepositories();

builder.Services.AddDataServices(builder.Configuration);

builder.Services.ConfigureApiServiceWrapper(builder.Configuration);

builder.Services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddWebOptimizer(false, false);

    /*
    builder.Services.AddWebOptimizer(options =>
    {   
        options.MinifyCssFiles("SwagLabs.Mvc.styles.css");
        options.MinifyCssFiles("css/site.css");
        options.MinifyJsFiles("js/site.js");
    });
    */
}
else
{
    builder.Services.AddWebOptimizer(options =>
    {
        options.MinifyCssFiles("SwagLabs.Mvc.styles.css");
        options.MinifyCssFiles("css/site.css");
        options.MinifyJsFiles("js/site.js");
    });
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseWebOptimizer();
app.UseRouting();

app.UseAuthorization();

app.MapControllers();


app.Run();
