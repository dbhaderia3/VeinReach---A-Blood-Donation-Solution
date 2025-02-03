using Microsoft.EntityFrameworkCore;
using Blood_Donation.Controllers;
using Blood_Donation.Models;
using Blood_Donation.Data;

var builder = WebApplication.CreateBuilder(args);
// Add Database services for the application
builder.Services.AddScoped<DatabaseService>(); // Add DatabaseService

// Register your DbContext with dependency injection (DI)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); // Use the connection string from appsettings.json

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
