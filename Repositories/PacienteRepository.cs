using Citas_App.Interfaces;
using Citas_App.Models;
using System.Text.Json;

namespace Citas_App.Repositories
{
    public class PacienteRepository : IPacienteRepository
    {
        private readonly string _filePath;

        public PacienteRepository()
        {
            _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "pacientes.json");
        }

        private List<Paciente> LeerArchivo()
        {
            if (!File.Exists(_filePath))
                return new List<Paciente>();

            try
            {
                var json = File.ReadAllText(_filePath);
                return JsonSerializer.Deserialize<List<Paciente>>(json) ?? new List<Paciente>();
            }
            catch (JsonException ex)
            {
                // Log el error y retornar lista vacía en caso de JSON corrupto
                Console.WriteLine($"Error al deserializar pacientes.json: {ex.Message}");
                // Opcional: Hacer backup del archivo corrupto
                var backupPath = _filePath + ".corrupto";
                if (File.Exists(_filePath))
                {
                    File.Copy(_filePath, backupPath, true);
                }
                // Crear archivo nuevo con lista vacía
                GuardarArchivo(new List<Paciente>());
                return new List<Paciente>();
            }
        }

        private void GuardarArchivo(List<Paciente> pacientes)
        {
            var json = JsonSerializer.Serialize(pacientes, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }

        public List<Paciente> ObtenerTodos() => LeerArchivo();

        public Paciente? ObtenerPorId(int id) => LeerArchivo().FirstOrDefault(p => p.Id == id);
    }
}
