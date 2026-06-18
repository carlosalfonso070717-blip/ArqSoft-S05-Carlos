using CitasApp.Domain.Interfaces;
// Nota: Si la clase 'Medico' te marca error, presiona Ctrl + . para agregar el 'using' de tu Dominio

namespace CitasApp.Application.Services
{
    public class MedicoService
    {
        private readonly IMedicoRepository _repository;

        public MedicoService(IMedicoRepository repository)
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
