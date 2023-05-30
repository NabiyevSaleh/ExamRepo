using MambaExamAB103Saleh.DAL;
using MambaExamAB103Saleh.Models;
using MambaExamAB103Saleh.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddScoped<LayoutService>();
builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
{
    opt.Password.RequireNonAlphanumeric = false;
}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
     name: "areas",
     pattern: "{area:exists}/{controller=Team}/{action=Index}/{id?}"
    );

app.MapControllerRoute(
    name: "Default",
    pattern: "{controller=home}/{action=index}/{id?}"
    );

app.Run();
