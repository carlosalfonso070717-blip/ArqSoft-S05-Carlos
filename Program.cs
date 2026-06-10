using CitasApp.Domain.Interfaces;
using CitasApp.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// ── 1. Configurar las rutas de los archivos ──────────────────────────────────
var dataPath = Path.Combine(builder.Environment.ContentRootPath, "Data");
if (!Directory.Exists(dataPath))
{
    Directory.CreateDirectory(dataPath);
}

// Rutas exactas para cada tipo de archivo
var csvPacientes = Path.Combine(dataPath, "pacientes.csv");
var csvMedicos = Path.Combine(dataPath, "medicos.csv");
var csvCitas = Path.Combine(dataPath, "citas.csv");
var sqlitePath = Path.Combine(dataPath, "citasapp.db");


// ── 2. EL INTERRUPTOR DE ARQUITECTURA HEXAGONAL ──────────────────────────────
// Para tu video: Comenta el bloque que NO usas y descomenta el que SÍ usas.

// ▶ BLOQUE A — JSON 
/*
builder.Services.AddScoped<IPacienteRepository>(provider => new PacienteRepository(dataPath));
builder.Services.AddScoped<IMedicoRepository>(provider => new MedicoRepository(dataPath));
builder.Services.AddScoped<ICitaRepository>(provider => new CitaRepository(dataPath));
*/

// ▶ BLOQUE B — CSV
builder.Services.AddScoped<IPacienteRepository>(_ => new CsvPacienteRepository(csvPacientes));
builder.Services.AddScoped<IMedicoRepository>(_ => new CsvMedicoRepository(csvMedicos));
builder.Services.AddScoped<ICitaRepository>(_ => new CsvCitaRepository(csvCitas));


// ▶ BLOQUE C — SQLite
/*
builder.Services.AddScoped<IPacienteRepository>(_ => new SqlitePacienteRepository(sqlitePath));
builder.Services.AddScoped<IMedicoRepository>(_ => new SqliteMedicoRepository(sqlitePath));
builder.Services.AddScoped<ICitaRepository>(_ => new SqliteCitaRepository(sqlitePath));
*/


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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