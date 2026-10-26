using Demo_ASPMVC_05__Auth.BLL.Helpers;
using Demo_ASPMVC_05__Auth.BLL.Interfaces;
using Demo_ASPMVC_05__Auth.BLL.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configuration des services
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IHasherHelper, HasherHelper>();
builder.Services.AddSingleton<HasherHelper.Config>(_ =>
{
    return new HasherHelper.Config()
    {
        // https://learn.microsoft.com/fr-fr/dotnet/core/extensions/configuration
        Pepper = builder.Configuration["PwdHash:Pepper"] ?? " 😱 Backup Papper 😱"
    };
});


// Configuration de l'authentification
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login";
        options.AccessDeniedPath = "/Auth/Nope";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
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
app.UseRouting();

// Ajout du middleware d'authentification (toujours avant celui d'authorization)
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();


