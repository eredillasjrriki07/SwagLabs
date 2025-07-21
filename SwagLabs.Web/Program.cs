using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SwagLabs.Dal.EfStructures;
using SwagLabs.Services.ApiWrapper.Configuration;
using SwagLabs.Services.AuthServices;
using SwagLabs.Services.DataServices;
using SwagLabs.Services.Logging.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureSerilog();
builder.Services.RegisterLoggingInterfaces();

// Add services to the container.
builder.Services.AddRazorPages();

if (builder.Environment.IsDevelopment() || builder.Environment.IsEnvironment("Local"))
{
    builder.Services.AddWebOptimizer(options =>
    {
        options.MinifyCssFiles("SwagLabs.Web.styles.css");
        options.MinifyCssFiles("/css/site.css");
        options.MinifyJsFiles("/js/site.js");

        //options.AddJavaScriptBundle("/js/validationCode.");
    });
}

builder.Services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();

var connectionString = builder.Configuration.GetConnectionString("SwagLabs");

builder.Services.AddDbContextPool<AppDbContext>(
    options => options.UseSqlServer(
        connectionString, sqlOptions => sqlOptions.EnableRetryOnFailure().CommandTimeout(60)
    )
);

builder.Services.AddRepositories();

builder.Services.AddDataServices(builder.Configuration);

builder.Services.AddAuthServices(builder.Configuration);

builder.Services.ConfigureApiServiceWrapper(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseWebOptimizer();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();

app.Run();
