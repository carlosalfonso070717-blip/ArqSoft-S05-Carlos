using CitasApp.Domain.Interfaces;
using CitasApp.Infrastructure.Repositories;
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

// ── 2. Registrar tus Repositorios reales ─────────────────────────────────────
builder.Services.AddScoped<IPacienteRepository>(provider => new PacienteRepository(dataPath));
builder.Services.AddScoped<IMedicoRepository>(provider => new MedicoRepository(dataPath));
builder.Services.AddScoped<ICitaRepository>(provider => new CitaRepository(dataPath));

// ── 3. Registrar los Servicios de aplicación ──────────────────────────────────
builder.Services.AddScoped<PacienteService>();
builder.Services.AddScoped<MedicoService>();
builder.Services.AddScoped<CitaService>();

var app = builder.Build();

app.UseCors("PermitirTodo");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
