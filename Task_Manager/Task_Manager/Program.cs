using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Task_Manager.Data;
using Task_Manager.Models;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext and Identity services to the container
builder.Services.AddDbContext<TaskDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<TaskDbContext>()
    .AddDefaultTokenProviders();

// Add controllers or Razor Pages
builder.Services.AddControllersWithViews(); // Or builder.Services.AddRazorPages(); for Razor Pages

var app = builder.Build();

// Configure the HTTP request pipeline
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

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Set up the default routing
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
