using Microsoft.EntityFrameworkCore;
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Erro");
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
