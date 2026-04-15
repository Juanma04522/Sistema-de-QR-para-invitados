using Domain.Interfaces;
using Infrastructure.Repositories;
using ElectronNET.API;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.WebHost.UseElectron(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));     

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IInvitadoRespoitory, InvitadoRespository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<CheckInServices>();
builder.Services.AddScoped<InvitadoService>();
builder.Services.AddScoped<EstadisticaService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (HybridSupport.IsElectronActive)
{
    Task.Run(async () => await Electron.WindowManager.CreateWindowAsync());
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
