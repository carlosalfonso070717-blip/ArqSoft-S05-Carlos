using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;
using System.Text.Json;

namespace CitasApp.Infrastructure.Repositories
{
    public class MedicoRepository : IMedicoRepository
    {
        private readonly string _filePath;

        public MedicoRepository(string dataPath)
        {
            _filePath = Path.Combine(dataPath, "medicos.json");
        }

        private List<Medico> LeerArchivo()
        {
            if (!File.Exists(_filePath)) return new List<Medico>();
            try
            {
                var json = File.ReadAllText(_filePath);
                return JsonSerializer.Deserialize<List<Medico>>(json) ?? new List<Medico>();
            }
            catch (JsonException)
            {
                GuardarArchivo(new List<Medico>());
                return new List<Medico>();
            }
        }

        private void GuardarArchivo(List<Medico> medicos)
        {
            var json = JsonSerializer.Serialize(medicos, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }

        public List<Medico> ObtenerTodos() => LeerArchivo();
        public Medico? ObtenerPorId(int id) => LeerArchivo().FirstOrDefault(m => m.Id == id);
        public void Agregar(Medico medico)
        {
            var medicos = LeerArchivo();
            medico.Id = medicos.Any() ? medicos.Max(m => m.Id) + 1 : 1;
            medicos.Add(medico);
            GuardarArchivo(medicos);
        }
    }
}