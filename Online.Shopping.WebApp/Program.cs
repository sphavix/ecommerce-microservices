using Online.Shopping.WebApp.Services;
using Online.Shopping.WebApp.Services.Contracts;
using Online.Shopping.WebApp.Utilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Http Clients
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
builder.Services.AddHttpClient<ICouponService, CouponService>();
builder.Services.AddHttpClient<IAuthService, AuthService>();

ServiceDependency.CouponApiBaseUrl = builder.Configuration["ServiceUrls:CouponApi"]!;
ServiceDependency.CouponApiBaseUrl = builder.Configuration["ServiceUrls:AuthApi"]!;

builder.Services.AddScoped<ICouponService, CouponService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IBaseService, BaseService>();

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
