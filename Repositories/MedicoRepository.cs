using Citas_App.Interfaces;
using Citas_App.Models;

namespace Citas_App.Repositories
{
    public class MedicoRepository : IMedicoRepository
    {
        private static readonly List<Medico> _medicos = new()
        {
            new Medico { Id = 1, Nombre = "Dr. Roberto", Apellido = "Martínez", Especialidad = "Cardiología", NumeroLicencia = "MED-001" },
            new Medico { Id = 2, Nombre = "Dra. Ana", Apellido = "Rodríguez", Especialidad = "Pediatría", NumeroLicencia = "MED-002" },
            new Medico { Id = 3, Nombre = "Dr. Luis", Apellido = "Fernández", Especialidad = "Neurología", NumeroLicencia = "MED-003" }
        };

        public List<Medico> ObtenerTodos() => _medicos;
        public Medico? ObtenerPorId(int id) => _medicos.FirstOrDefault(m => m.Id == id);
    }
}
