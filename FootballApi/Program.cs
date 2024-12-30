using FootballApi.Application.Interfaces.Football;
using FootballApi.Application.Interfaces;
using FootballApi.Persistence.Repositories;
using FootballApi.Services;
using Microsoft.EntityFrameworkCore;
using Model.Context;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<ITeamRepository, TeamRepository>();
// Add services to the container.

builder.Services.AddHttpClient<IPlayerService, FootballService>();
builder.Services.AddScoped<IPlayerStatisticService, PlayerStatisticService>();
builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<IStadiumService, StadiumService>();
builder.Services.AddScoped<IStadiumRepository, StadiumRepository>();
builder.Services.AddScoped<ILeagueService, LeagueService>();
builder.Services.AddScoped<ILeagueRepository, LeagueRepository>();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllersWithViews(); // MVC desteði ekleniyor
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ITeamService, TeamService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Statik dosyalar için destek

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); // Varsayýlan rota ekleniyor

app.MapControllerRoute(
    name: "compare",
    pattern: "Transfer/Compare/{player1Id}/{player2Id}",
    defaults: new { controller = "Transfer", action = "Compare" });

app.Run();
