using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using CitasApp.Domain.Models;

namespace CitasApp.Infrastructure.Repositories
{
    // EXTRACT CLASS: Esta clase se encarga ÚNICAMENTE de leer y escribir en el disco duro.
    public class JsonStorageManager
    {
        private readonly string _filePath;

        public JsonStorageManager(string dataPath)
        {
            _filePath = Path.Combine(dataPath, "citas.json");
        }

        public List<Cita> Leer()
        {
            if (!File.Exists(_filePath)) return new List<Cita>();
            try
            {
                var json = File.ReadAllText(_filePath);
                return JsonSerializer.Deserialize<List<Cita>>(json) ?? new List<Cita>();
            }
            catch (JsonException)
            {
                Guardar(new List<Cita>());
                return new List<Cita>();
            }
        }

        public void Guardar(List<Cita> citas)
        {
            var json = JsonSerializer.Serialize(citas, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }
    }
}