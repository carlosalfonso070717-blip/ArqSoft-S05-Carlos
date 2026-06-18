using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;
using System.Text.Json;

namespace CitasApp.Infrastructure.Repositories
{
    public class PacienteRepository : IPacienteRepository
    {
        private readonly string _filePath;

        public PacienteRepository(string dataPath)
        {
            _filePath = Path.Combine(dataPath, "pacientes.json");
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
                Console.WriteLine($"Error al deserializar pacientes.json: {ex.Message}");
                var backupPath = _filePath + ".corrupto";
                if (File.Exists(_filePath))
                {
                    File.Copy(_filePath, backupPath, true);
                }
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

        public void Agregar(Paciente paciente)
        {
            var pacientes = LeerArchivo();
            paciente.Id = pacientes.Any() ? pacientes.Max(p => p.Id) + 1 : 1;
            pacientes.Add(paciente);
            GuardarArchivo(pacientes);
        }
    }
}
