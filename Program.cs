using CitasApp.Domain.Interfaces;
using CitasApp.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configurar la ruta base de datos
var dataPath = Path.Combine(builder.Environment.ContentRootPath, "Data");
if (!Directory.Exists(dataPath))
{
    Directory.CreateDirectory(dataPath);
}

// Registrar los repositorios con inyección de dependencias pasando la ruta
builder.Services.AddScoped<IPacienteRepository>(provider => 
    new PacienteRepository(dataPath));
builder.Services.AddScoped<IMedicoRepository>(provider => 
    new MedicoRepository(dataPath));
builder.Services.AddScoped<ICitaRepository>(provider => 
    new CitaRepository(dataPath));

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
