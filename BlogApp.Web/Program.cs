using System.Reflection;
using BlogApp.Data.Context;
using BlogApp.Data.Extensions;
using BlogApp.Entity.Entities;
using BlogApp.Services.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.LoadDataLayerExtension(builder.Configuration);
builder.Services.ServiceLayerExtension();

builder.Services.AddControllersWithViews();

builder.Services.AddSession(); //İdentity için eklendi

//identity
builder.Services.AddIdentity<AppUser,AppRole>(opt=>
{
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireUppercase = false;
})
.AddRoleManager<RoleManager<AppRole>>().AddEntityFrameworkStores<AppDbContext>()
 .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(config =>
{
    config.LoginPath = new PathString("/Admin/Auth/Login");
    config.LogoutPath = new PathString("/Admin/Auth/Logout");
    config.Cookie = new CookieBuilder
    {
        Name = "YoutubeBlog",
        HttpOnly = true,
        SameSite = SameSiteMode.Strict,
        SecurePolicy = CookieSecurePolicy.SameAsRequest //Always 
    };
    config.SlidingExpiration = true;
    config.ExpireTimeSpan = TimeSpan.FromDays(7);
    config.AccessDeniedPath = new PathString("/Admin/Auth/AccessDenied");
});
// builder.Services.AddDbContext<AppDbContext>(options =>
//     options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

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

//cookie base için
app.UseSession();

app.UseRouting();
//identity , sıralama önemli ilk başta authentication olmalı. birisinin login olup olmadığını bilmeden authorization işlemi uygulayamayız.
app.UseAuthentication();
app.UseAuthorization();

// app.MapControllerRoute(
//     name: "default",
//     pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseEndpoints(endpoints =>
{
    endpoints.MapAreaControllerRoute(
        name: "Admin",
        areaName: "Admin",
        pattern: "Admin/{controller=Home}/{action=Index}/{id?}"
        ); 
    endpoints.MapDefaultControllerRoute();
});

app.Run();