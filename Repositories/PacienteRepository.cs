using Citas_App.Interfaces;
using Citas_App.Models;

namespace Citas_App.Repositories
{
    public class PacienteRepository : IPacienteRepository
    {
        private static readonly List<Paciente> _pacientes = new()
        {
            new Paciente { Id = 1, Nombre = "Juan", Apellido = "Pérez", Email = "juan@email.com", Telefono = "555-1234" },
            new Paciente { Id = 2, Nombre = "María", Apellido = "García", Email = "maria@email.com", Telefono = "555-5678" },
            new Paciente { Id = 3, Nombre = "Carlos", Apellido = "López", Email = "carlos@email.com", Telefono = "555-9012" }
        };

        public List<Paciente> ObtenerTodos() => _pacientes;
        public Paciente? ObtenerPorId(int id) => _pacientes.FirstOrDefault(p => p.Id == id);
    }
}
