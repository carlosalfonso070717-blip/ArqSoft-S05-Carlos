using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CitasApp.Domain.Interfaces;
using CitasApp.Infrastructure.Repositories;
using CitasApp.Infrastructure.Notifications;
using CitasApp.Application.Services;
using CitasApp.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. Configurar la conexión a la base de datos SQLite (Mata el error rojo)
builder.Services.AddDbContext<CitasApp.Infrastructure.Data.ApplicationDbContext>(options =>
options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source= ../citasapp.db"));

// 2. Inyectar los repositorios (Mata los errores amarillos)
builder.Services.AddScoped<CitasApp.Domain.Interfaces.IPacienteRepository, CitasApp.Infrastructure.SqlitePacienteRepository>();
builder.Services.AddScoped<CitasApp.Domain.Interfaces.IMedicoRepository, CitasApp.Infrastructure.SqliteMedicoRepository>();
builder.Services.AddScoped<CitasApp.Domain.Interfaces.ICitaRepository, CitasApp.Infrastructure.SqliteCitaRepository>();

// Esto le dice a .NET que este proyecto es una Web API
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTodo", policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

builder.Services.AddIdentityCore<IdentityUser>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// ── 2. Configurar la ruta de datos ──────────────────────────────────────────
var dataPath = Path.Combine(builder.Environment.ContentRootPath, "Data");
if (!Directory.Exists(dataPath))
{
    Directory.CreateDirectory(dataPath);
}

// ── 3. Repositorios JSON/CSV (COMENTADOS POR TRANSICIÓN A BASE DE DATOS) ────
/*
builder.Services.AddScoped<IPacienteRepository>(sp =>
{
    var env = sp.GetRequiredService<IWebHostEnvironment>();
    // El Factory decide si usar JSON o Memoria según el entorno de ejecución
    var repoBase = RepositoryFactory.CrearPacienteRepository(builder.Environment.EnvironmentName, env);
    // El Decorador envuelve al repositorio seleccionado para meter los logs en consola
    return new LoggingPacienteRepository(repoBase);
});

builder.Services.AddScoped<IMedicoRepository>(provider => new MedicoRepository(dataPath));
builder.Services.AddScoped<ICitaRepository>(provider => new CitaRepository(dataPath));
*/

// ── 4. Registrar los Observers independientes (Reto Observer) ────────────────
builder.Services.AddScoped<ICitaObserver, SmsObserver>();
builder.Services.AddScoped<ICitaObserver, EmailObserver>();

// ── 5. Registrar los Servicios de aplicación ──────────────────────────────────
builder.Services.AddScoped<PacienteService>();
builder.Services.AddScoped<MedicoService>();
builder.Services.AddScoped<CitaService>();

var app = builder.Build();

app.UseCors("PermitirTodo");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();