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
    //yetkisiz kullanıcıyı yönlendirdiğimiz sayfa
    config.LoginPath = new PathString("/Admin/Auth/Login");
    config.LogoutPath = new PathString("Admin/Auth/Logout");
    config.Cookie = new CookieBuilder
    {
        Name = "BlogApp",
        HttpOnly = true,
        SameSite = SameSiteMode.Strict,
        SecurePolicy = CookieSecurePolicy.SameAsRequest //canlıya çıkınca always yapmalıyız https olması için her zaman
    };
    config.SlidingExpiration = true; //kimlik doğrulama belirtecinin geçerlilik süresini belirler.  değeri true olarak ayarlanırsa, belirtecin son kullanımından itibaren geçerlilik süresi içinde herhangi bir istek alındığında, belirtecin süresi sıfırlanır ve tekrar belirlenen süre kadar geçerli olur. Yani, kullanıcı aktifken belirtecin süresi sürekli olarak yenilenir. Bu şekilde, kullanıcı aktif olduğu sürece oturumunun süresi uzatılmış olur.
    config.ExpireTimeSpan = TimeSpan.FromDays(7);
    config.AccessDeniedPath = new PathString("/Admin/Auth/Accesdenied");
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