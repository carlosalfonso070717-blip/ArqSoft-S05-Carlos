using Citas_App.Interfaces;
using Citas_App.Models;

namespace Citas_App.Repositories
{
    public class CitaRepository : ICitaRepository
    {
        private static readonly List<Cita> _citas = new()
        {
            new Cita { Id = 1, PacienteId = 1, MedicoId = 1, Fecha = DateOnly.FromDateTime(DateTime.Now.AddDays(1)), Hora = new TimeOnly(10, 0), Motivo = "Consulta general", Estado = "Pendiente" },
            new Cita { Id = 2, PacienteId = 2, MedicoId = 2, Fecha = DateOnly.FromDateTime(DateTime.Now.AddDays(2)), Hora = new TimeOnly(11, 30), Motivo = "Control pediátrico", Estado = "Confirmada" },
            new Cita { Id = 3, PacienteId = 1, MedicoId = 3, Fecha = DateOnly.FromDateTime(DateTime.Now.AddDays(3)), Hora = new TimeOnly(14, 0), Motivo = "Evaluación neurológica", Estado = "Pendiente" }
        };

        public List<Cita> ObtenerTodos() => _citas;
        public List<Cita> ObtenerPorPaciente(int pacienteId) => _citas.Where(c => c.PacienteId == pacienteId).ToList();
    }
}
