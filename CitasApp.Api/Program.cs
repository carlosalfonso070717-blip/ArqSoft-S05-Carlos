using Microsoft.AspNetCore.Hosting;
using CitasApp.Domain.Interfaces;
using CitasApp.Infrastructure.Repositories;
using CitasApp.Infrastructure.Notifications; // ── NUEVO: Namespace para tus Observers de infraestructura
using CitasApp.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Esto le dice a .NET que este proyecto es una Web API
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTodo", policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

// ── 1. Configurar la ruta de datos ──────────────────────────────────────────
var dataPath = Path.Combine(builder.Environment.ContentRootPath, "Data");
if (!Directory.Exists(dataPath))
{
    Directory.CreateDirectory(dataPath);
}

// ── 2. Registrar tus Repositorios (Con Factory y Decorator para Paciente) ────
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

// ── 3. Registrar los Observers independientes (Reto Observer) ────────────────
builder.Services.AddScoped<ICitaObserver, SmsObserver>();
builder.Services.AddScoped<ICitaObserver, EmailObserver>();

// ── 4. Registrar los Servicios de aplicación ──────────────────────────────────
builder.Services.AddScoped<PacienteService>();
builder.Services.AddScoped<MedicoService>();
builder.Services.AddScoped<CitaService>(); // .NET le pasará automáticamente el repositorio y los Observers creados arriba

var app = builder.Build();

app.UseCors("PermitirTodo");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();