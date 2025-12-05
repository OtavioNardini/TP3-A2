using Microsoft.EntityFrameworkCore;
using RestauranteAPP_TP3.Data;
using Microsoft.AspNetCore.Identity;
using RestauranteAPP_TP3.Customs;
using Microsoft.AspNetCore.Identity.UI.Services;
using RestauranteAPP_TP3.Models;
using Microsoft.Extensions.Logging;
using Refit;
using RestauranteAPP_TP3.Integração.Refit;
using RestauranteAPP_TP3.Integração;
using RestauranteAPP_TP3.Integração.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
       options.UseSqlServer(connectionString));
builder.Services.AddRefitClient<IViaCepIntegracaoRefit>().ConfigureHttpClient(c => c.BaseAddress = new Uri("https://viacep.com.br"));
builder.Services.AddScoped<IViaCepIntegracao, ViaCepIntegracao>();
builder.Services.AddRefitClient<IBrasilAPIIntegracaoRefit>().ConfigureHttpClient(c => c.BaseAddress = new Uri("https://brasilapi.com.br/api"));
builder.Services.AddScoped<IBrasilAPIIntegracao, BrasilAPIIntegracao>();

builder.Services.AddIdentity<Usuario, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Ensure login path goes to the Identity UI pages (area "Identity")
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";
    options.LogoutPath = "/Identity/Account/Logout";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
});

builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddRazorPages();

var app = builder.Build();

// --- Run identity seed (creates roles + optional admin) ---
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        await IdentitySeed.SeedAsync(services);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding identity data.");
    }

   
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages();
app.Run();
