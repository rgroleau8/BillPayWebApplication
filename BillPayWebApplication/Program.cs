using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BillPayData.Models;
using BillPayWebApplication.Areas.Identity.Data;
using System.Configuration;



var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<BillPayData.Services.IBillingData, BillPayData.Services.AzureBillingData>();

builder.Services.AddScoped<BillPayData.Services.ILog, BillPayData.Services.Logger>();

IConfiguration configuration = builder.Configuration;

builder.Services.Configure<MySettingsModel>(configuration.GetSection("MySettings"));


builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";
    options.SlidingExpiration = true;
});


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
app.UseAuthentication();;



app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=PatientPortal}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
