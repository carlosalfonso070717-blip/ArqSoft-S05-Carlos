using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;
using System.Text.Json;

namespace CitasApp.Infrastructure.Repositories
{
    public class CitaRepository : ICitaRepository
    {
        private readonly string _filePath;

        public CitaRepository(string dataPath)
        {
            _filePath = Path.Combine(dataPath, "citas.json");
        }

        private List<Cita> LeerArchivo()
        {
            if (!File.Exists(_filePath)) return new List<Cita>();
            try
            {
                var json = File.ReadAllText(_filePath);
                return JsonSerializer.Deserialize<List<Cita>>(json) ?? new List<Cita>();
            }
            catch (JsonException)
            {
                GuardarArchivo(new List<Cita>());
                return new List<Cita>();
            }
        }

        private void GuardarArchivo(List<Cita> citas)
        {
            var json = JsonSerializer.Serialize(citas, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }

        public List<Cita> ObtenerTodos() => LeerArchivo();
        public Cita? ObtenerPorId(int id) => LeerArchivo().FirstOrDefault(c => c.Id == id);
        public void Agregar(Cita cita)
        {
            var citas = LeerArchivo();
            cita.Id = citas.Any() ? citas.Max(c => c.Id) + 1 : 1;
            citas.Add(cita);
            GuardarArchivo(citas);
        }

        public List<Cita> ObtenerPorPaciente(int pacienteId) => LeerArchivo().Where(c => c.PacienteId == pacienteId).ToList();
    }
}