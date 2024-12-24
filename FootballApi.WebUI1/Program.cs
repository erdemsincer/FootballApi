using FootballApi.Application.Interfaces;
using FootballApi.Application.Interfaces.Football;
using FootballApi.Persistence.Repositories;
using FootballApi.Services;
using Microsoft.EntityFrameworkCore;
using Model.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Servislerin DI (Dependency Injection) konteynerine eklenmesi
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient<IPlayerService, FootballService>();
builder.Services.AddScoped<IPlayerStatisticService, PlayerStatisticService>();
builder.Services.AddScoped<ITeamRepository, TeamRepository>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddHttpClient();
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
