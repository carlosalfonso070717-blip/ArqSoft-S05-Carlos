using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;

namespace CitasApp.Infrastructure.Repositories
{
    public class JsonMedicoRepository : IMedicoRepository
    {
        private readonly string _filePath;

        public JsonMedicoRepository()
        {

            _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "medicos.json");

            CrearJsonSiNoExiste();
        }

        public List<Medico> ObtenerTodos()
        {
            try
            {
                if (!File.Exists(_filePath)) return new List<Medico>();

                var json = File.ReadAllText(_filePath);
                return JsonSerializer.Deserialize<List<Medico>>(json) ?? new List<Medico>();
            }
            catch
            {
                return new List<Medico>();
            }
        }

        public Medico ObtenerPorId(int id)
        {
            return ObtenerTodos().FirstOrDefault(m => m.Id == id)!;
        }

        public void Agregar(Medico medico)
        {
            var lista = ObtenerTodos();
            lista.Add(medico);
            File.WriteAllText(_filePath, JsonSerializer.Serialize(lista, new JsonSerializerOptions { WriteIndented = true }));
        }

        private void CrearJsonSiNoExiste()
        {
            if (!File.Exists(_filePath))
            {
                // Nota: Si tus propiedades de Medico se llaman diferente (ej. NombreMedico o Especialidad), adáptalas aquí
                var dePrueba = new List<Medico>
                {
                    new Medico { Id = 1, Nombre = "Dr. Jorge Torres" },
                    new Medico { Id = 2, Nombre = "Dra. Alejandra Inzunza" },
                    new Medico { Id = 3, Nombre = "Dr. Ricardo Ancona" }
                };
                File.WriteAllText(_filePath, JsonSerializer.Serialize(dePrueba, new JsonSerializerOptions { WriteIndented = true }));
            }
        }
    }
}