using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Globalization;
using SalesWebMvc.Data;
using SalesWebMvc.Services;
using System.Configuration;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Identity;


var builder = WebApplication.CreateBuilder(args);

// Configuração de banco de dados
builder.Services.AddDbContext<SalesWebMvcContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("SalesWebMvcContext"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("SalesWebMvcContext")),
        mySqlOptions => mySqlOptions.MigrationsAssembly("SalesWebMvc")));

// Configuração de identidade
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<SalesWebMvcContext>()
    .AddDefaultTokenProviders();

// Configuração de cookies de autenticação
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Login"; // Caminho para a página de login
    options.AccessDeniedPath = "/Home/AccessDenied"; // Caminho em caso de acesso negado
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Duração do cookie
    options.SlidingExpiration = true; // Renova o cookie a cada requisição
});

// Injeção de dependência dos seus serviços
builder.Services.AddScoped<SeedingService>();
builder.Services.AddScoped<VendedorService>();
builder.Services.AddScoped<DepartamentoService>();
builder.Services.AddScoped<VendaService>();
builder.Services.AddScoped<LoginService>();

var enUS = new CultureInfo("pt-BR");
var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(enUS),
    SupportedCultures = new List<CultureInfo> { enUS },
    SupportedUICultures = new List<CultureInfo> { enUS },
};

// Adicionar serviços de controle e visão
builder.Services.AddControllersWithViews();

var app = builder.Build();
app.UseRequestLocalization(localizationOptions);

// Configuração do pipeline de requisições
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    using (var scope = app.Services.CreateScope())
    {
        var seedingService = scope.ServiceProvider.GetRequiredService<SeedingService>();
        seedingService.Seed();
    }
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Certifique-se de que o middleware de autenticação e autorização está no pipeline
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();