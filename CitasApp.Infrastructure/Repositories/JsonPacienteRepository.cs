using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;

namespace CitasApp.Infrastructure.Repositories
{
    public class JsonPacienteRepository : IPacienteRepository
    {
        private readonly string _filePath;

        public JsonPacienteRepository()
        {
            _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "pacientes.json");

            CrearJsonSiNoExiste();
        }

        public List<Paciente> ObtenerTodos()
        {
            try
            {
                if (!File.Exists(_filePath)) return new List<Paciente>();

                var json = File.ReadAllText(_filePath);
                return JsonSerializer.Deserialize<List<Paciente>>(json) ?? new List<Paciente>();
            }
            catch
            {
                return new List<Paciente>();
            }
        }

        public Paciente ObtenerPorId(int id)
        {
            return ObtenerTodos().FirstOrDefault(p => p.Id == id)!;
        }

        public void Agregar(Paciente paciente)
        {
            var lista = ObtenerTodos();
            lista.Add(paciente);
            File.WriteAllText(_filePath, JsonSerializer.Serialize(lista, new JsonSerializerOptions { WriteIndented = true }));
        }

        private void CrearJsonSiNoExiste()
        {
            if (!File.Exists(_filePath))
            {
                // Nota: Si tus propiedades se llaman diferente (ej. NombrePaciente), cámbialas aquí
                var dePrueba = new List<Paciente>
                {
                    new Paciente { Id = 1, Nombre = "Carlos Alfonso" },
                    new Paciente { Id = 2, Nombre = "Juan Pérez" },
                    new Paciente { Id = 3, Nombre = "María López" },
                    new Paciente { Id = 4, Nombre = "Jorge Torres" }
                };
                File.WriteAllText(_filePath, JsonSerializer.Serialize(dePrueba, new JsonSerializerOptions { WriteIndented = true }));
            }
        }
    }
}