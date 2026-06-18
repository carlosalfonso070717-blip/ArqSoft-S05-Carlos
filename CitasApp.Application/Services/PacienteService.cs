using CitasApp.Domain.Interfaces;
// Nota: Si la clase 'Paciente' te marca error, presiona Ctrl + . para agregar el 'using' de tu Dominio

namespace CitasApp.Application.Services
{
    public class PacienteService
    {
        private readonly IPacienteRepository _repository;

        // Inyección de dependencias del repositorio a través de la interfaz (Puerto)
        public PacienteService(IPacienteRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<dynamic> ObtenerTodos()
        {
            return _repository.ObtenerTodos();
        }

        public dynamic ObtenerPorId(int id)
        {
            return _repository.ObtenerPorId(id);
        }
    }
}
