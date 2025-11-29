using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PlataformaB2B_A2_TP3.Data;
using PlataformaB2B_A2_TP3.Domain.Entities;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PlataformaB2B_A2_TP3Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PlataformaB2B_A2_TP3Context")
        ?? throw new InvalidOperationException("Connection string not found.")));

// Identity (usando int como chave)
builder.Services.AddIdentity<Usuario, Role>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<PlataformaB2B_A2_TP3Context>()
.AddDefaultTokenProviders();

// JWT config (coloque chaves em appsettings.json)
var jwtSection = builder.Configuration.GetSection("Jwt");
var jwtKey = jwtSection.GetValue<string>("Key") ?? "change_this_secret_to_something_very_secure_and_long";
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSection.GetValue<string>("Issuer") ?? "yourIssuer",
        ValidAudience = jwtSection.GetValue<string>("Audience") ?? "yourAudience",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
        RoleClaimType = System.Security.Claims.ClaimTypes.Role
    };
});

// Authorization policies (opcional)
// Assuming Roles constants exist or we define them. Let's use strings for now to be safe if Roles class is missing.
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Administrador", policy => policy.RequireRole("Administrador"));
    options.AddPolicy("Comprador", policy => policy.RequireRole("Comprador"));
    options.AddPolicy("Aprovador", policy => policy.RequireRole("Aprovador"));
    options.AddPolicy("Fornecedor", policy => policy.RequireRole("Fornecedor"));
});

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Seed roles + optional admin user (ver passo 6)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var ctx = services.GetRequiredService<PlataformaB2B_A2_TP3Context>();
        ctx.Database.Migrate();

        var roleManager = services.GetRequiredService<RoleManager<Role>>();
        var userManager = services.GetRequiredService<UserManager<Usuario>>();

        var roles = new[] { "Administrador", "Comprador", "Aprovador", "Fornecedor" };
        foreach (var r in roles)
        {
            if (!roleManager.RoleExistsAsync(r).GetAwaiter().GetResult())
            {
                roleManager.CreateAsync(new Role(r)).GetAwaiter().GetResult();
            }
        }

        // create default admin (configurar credenciais em appsettings)
        var adminEmail = builder.Configuration["AdminUser:Email"] ?? "admin@local";
        var adminPwd = builder.Configuration["AdminUser:Password"] ?? "Admin123!";
        var admin = userManager.FindByEmailAsync(adminEmail).GetAwaiter().GetResult();
        if (admin == null)
        {
            admin = new Usuario { UserName = adminEmail, Email = adminEmail, Nome = "Administrator" };
            var r = userManager.CreateAsync(admin, adminPwd).GetAwaiter().GetResult();
            if (r.Succeeded) userManager.AddToRoleAsync(admin, "Administrador").GetAwaiter().GetResult();
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Error seeding identity data");
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
