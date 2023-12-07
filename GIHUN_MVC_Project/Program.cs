using GIHUN_MVC_Project.Core.Interfaces;
using GIHUN_MVC_Project.Core.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IUserRepository, UserService>();  
builder.Services.AddTransient<IReservationHotelRepository, ReservationHotelService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(opt =>
{
    opt.SlidingExpiration = true;
    opt.Cookie.HttpOnly = true;
    opt.LoginPath = "/User/Login";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

CookiePolicyOptions cookiePolicy = new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Lax
};

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCookiePolicy(cookiePolicy);
app.UseAuthentication();
app.UseAuthorization();

app.MapDefaultControllerRoute();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
