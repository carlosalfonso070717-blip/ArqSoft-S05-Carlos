using Citas_App.Interfaces;
using Citas_App.Models;
using System.Text.Json;

namespace Citas_App.Repositories
{
    public class MedicoRepository : IMedicoRepository
    {
        private readonly string _filePath;

        public MedicoRepository()
        {
            _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "medicos.json");
        }

        private List<Medico> LeerArchivo()
        {
            if (!File.Exists(_filePath))
                return new List<Medico>();

            try
            {
                var json = File.ReadAllText(_filePath);
                return JsonSerializer.Deserialize<List<Medico>>(json) ?? new List<Medico>();
            }
            catch (JsonException ex)
            {
                // Log el error y retornar lista vacía en caso de JSON corrupto
                Console.WriteLine($"Error al deserializar medicos.json: {ex.Message}");
                // Opcional: Hacer backup del archivo corrupto
                var backupPath = _filePath + ".corrupto";
                if (File.Exists(_filePath))
                {
                    File.Copy(_filePath, backupPath, true);
                }
                // Crear archivo nuevo con lista vacía
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
    }
}
