using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CitasApp.Domain.Interfaces;
using CitasApp.Infrastructure.Notifications;
using CitasApp.Application.Services;
using CitasApp.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// 1. Configurar la conexión a la base de datos SQLite (misma DB que Api)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=../citasapp.db"));

// 2. Inyectar los repositorios SQLite
builder.Services.AddScoped<IPacienteRepository, CitasApp.Infrastructure.SqlitePacienteRepository>();
builder.Services.AddScoped<IMedicoRepository, CitasApp.Infrastructure.SqliteMedicoRepository>();
builder.Services.AddScoped<ICitaRepository, CitasApp.Infrastructure.SqliteCitaRepository>();

builder.Services.AddIdentityCore<IdentityUser>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// 3. Observers
builder.Services.AddScoped<ICitaObserver, SmsObserver>();
builder.Services.AddScoped<ICitaObserver, EmailObserver>();

// 4. Servicios de aplicación
builder.Services.AddScoped<PacienteService>();
builder.Services.AddScoped<MedicoService>();
builder.Services.AddScoped<CitaService>();

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