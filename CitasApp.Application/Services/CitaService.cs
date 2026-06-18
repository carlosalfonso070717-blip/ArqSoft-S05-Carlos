using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;
namespace CitasApp.Application.Services
{
    public class CitaService
    {
        private readonly ICitaRepository _repository;

        public CitaService(ICitaRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Cita> ObtenerTodos()
        {
            return _repository.ObtenerTodos();
        }

        public List<Cita> ObtenerPorPaciente(int pacienteId)
        {
            return _repository.ObtenerPorPaciente(pacienteId);
        }
    }
}