using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using KhumaloCraftPOE.Data;
using KhumaloCraftPOE.Areas.Identity.Data;
using KhumaloCraftPOE.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("KhumaloCraftPOEDbContextConnection") ?? throw new InvalidOperationException("Connection string 'KhumaloCraftPOEDbContextConnection' not found.");

builder.Services.AddDbContext<KhumaloCraftPOEDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<KhumaloCraftPOEDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IProductRepository, ProductRepository>(); // Register IProductRepository with ProductRepository implementation (assuming ProductRepository class exists)
builder.Services.AddScoped<IOrderRepository, OrderRepository>(); // Register IOrderRepository with OrderRepository implementation (assuming OrderRepository class exists)





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

app.MapRazorPages();
app.Run();
