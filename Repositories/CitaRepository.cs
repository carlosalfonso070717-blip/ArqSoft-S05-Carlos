using Citas_App.Interfaces;
using Citas_App.Models;
using System.Text.Json;

namespace Citas_App.Repositories
{
    public class CitaRepository : ICitaRepository
    {
        private readonly string _filePath;

        public CitaRepository()
        {
            _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "citas.json");
        }

        private List<Cita> LeerArchivo()
        {
            if (!File.Exists(_filePath))
                return new List<Cita>();

            try
            {
                var json = File.ReadAllText(_filePath);
                return JsonSerializer.Deserialize<List<Cita>>(json) ?? new List<Cita>();
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error al deserializar citas.json: {ex.Message}");
                var backupPath = _filePath + ".corrupto";
                if (File.Exists(_filePath))
                {
                    File.Copy(_filePath, backupPath, true);
                }
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

        public List<Cita> ObtenerPorPaciente(int pacienteId) => 
            LeerArchivo().Where(c => c.PacienteId == pacienteId).ToList();

        public void Agregar(Cita cita)
        {
            var citas = LeerArchivo();
            cita.Id = citas.Any() ? citas.Max(c => c.Id) + 1 : 1;
            citas.Add(cita);
            GuardarArchivo(citas);
        }
    }
}
