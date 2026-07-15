using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;
using System.Text.Json;

namespace CitasApp.Infrastructure.Repositories
{
    public class CitaRepository : ICitaRepository
    {
        private readonly JsonStorageManager _storage;

        public CitaRepository(JsonStorageManager storage)
        {
            _storage = storage;
        }

        public List<Cita> ObtenerTodos() => _storage.Leer();
        public Cita? ObtenerPorId(int id) => _storage.Leer().FirstOrDefault(c => c.Id == id);
        public void Agregar(Cita cita)
        {
            var citas = _storage.Leer();
            cita.Id = citas.Any() ? citas.Max(c => c.Id) + 1 : 1;
            citas.Add(cita);
            _storage.Guardar(citas);
        }

        public List<Cita> ObtenerPorPaciente(int pacienteId) => _storage.Leer().Where(c => c.PacienteId == pacienteId).ToList();
    }
}