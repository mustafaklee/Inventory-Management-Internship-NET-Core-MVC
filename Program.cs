using Microsoft.EntityFrameworkCore;
using LoginEkrani;
using LoginEkrani.Services;
using LoginEkrani.Services.Interfaces;
using LoginEkrani.Services.Implementations;
using Microsoft.AspNetCore.Authentication.Cookies;
using LoginEkrani.Models.Login.Roles;

var builder = WebApplication.CreateBuilder(args);

// LogService'i DI container'a ekleyin
builder.Services.AddScoped<LogService>();

// Servis kayıtları
builder.Services.AddScoped<IStockService, StockService>();
builder.Services.AddScoped<IVoucherService, VoucherService>();
builder.Services.AddScoped<IDefinitionService, DefinitionService>();

// Controller ve View desteğini ekleyin
builder.Services.AddControllersWithViews();

// Session desteğini ekleyin
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20); // Oturum süresi
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Configuration nesnesine erişim
var configuration = builder.Configuration;

// DbContext'i ekleyin ve MySQL bağlantı dizesini kullanın
builder.Services.AddDbContext<Database>(options =>
    options.UseMySql(configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(8, 0, 21))));

// Kimlik doğrulama için Cookie Authentication ekleyin
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login"; // Giriş yapılmadığında yönlendirilecek sayfa
        options.LogoutPath = "/Logout"; // Çıkış yapıldığında yönlendirilecek sayfa
        options.AccessDeniedPath = "/AccessDenied"; // Yetki yoksa yönlendirilecek sayfa
    });

// Authorization yapılandırması
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        // Veritabanı seed işlemi
        SeedData.Initialize(services);
    }
    catch (Exception ex)
    {
        // Hata yönetimi, örneğin logging
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

// Hata ve güvenlik yapılandırması
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Kimlik doğrulama ve yetkilendirme middleware'lerini ekleyin
app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
