using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CitasApp.Domain.Models; 

namespace CitasApp.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Medico> Medicos { get; set; }
        public DbSet<Cita> Citas { get; set; }
    }
}