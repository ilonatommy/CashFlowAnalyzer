using Microsoft.EntityFrameworkCore;
using CashFlowAnalyzer.Server.Data;
using CashFlowAnalyzer.Shared.Models;
using CashFlowAnalyzer.Components;
using CashFlowAnalyzer.Client.Services;
using CashFlowAnalyzer.Server.Services;

var builder = WebApplication.CreateBuilder(args);

// because of prerendering, we have to re-register Client's services on the server:
// see: https://github.com/dotnet/aspnetcore/issues/51432
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<SpreadsheetReader>();
builder.Services.AddScoped<IFinancialDataService, FinancialDataService>();
builder.Services.AddScoped<DatabaseService>();

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddIdentityServer()
    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.Lax;
    options.ExpireTimeSpan = TimeSpan.FromDays(14);
    options.SlidingExpiration = true;
    options.LoginPath = "/Login";
    options.LogoutPath = "/Logout";
});

builder.Services.AddControllers();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapControllers();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(CashFlowAnalyzer.Client._Imports).Assembly);

app.Run();
