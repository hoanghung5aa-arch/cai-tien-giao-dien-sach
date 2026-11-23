using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using _31241027221_NguyenHoangHung.Data;
using _31241027221_NguyenHoangHung.Models;
using _31241027221_NguyenHoangHung.Services;
using _31241027221_NguyenHoangHung.Validation;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrWhiteSpace(connectionString) || connectionString.Contains("YOUR_", StringComparison.OrdinalIgnoreCase))
{
    throw new InvalidOperationException("Configure ConnectionStrings:DefaultConnection before starting the application.");
}
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(100);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("Email"));
builder.Services.AddTransient<IEmailSender, IdentityEmailSender>();
builder.Services.AddScoped<IMovieImageStorage, MovieImageStorage>();
builder.Services.AddScoped<IOrderEmailService, OrderEmailService>();
builder.Services.AddControllersWithViews(options =>
    options.ModelBinderProviders.Insert(0, new InvariantNumericModelBinderProvider()))
    .AddNewtonsoftJson();

var app = builder.Build();
var viCulture = CultureInfo.GetCultureInfo("vi-VN");
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(viCulture),
    SupportedCultures = [viCulture],
    SupportedUICultures = [viCulture]
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Movies}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
