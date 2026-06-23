using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;

namespace CitasApp.Infrastructure.Repositories
{
    public class JsonCitaRepository : ICitaRepository
    {
        private readonly string _filePath;

        public JsonCitaRepository()
        {
            _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "citas.json");
            CrearJsonSiNoExiste();
        }

        public IEnumerable<Cita> ObtenerTodos()
        {
            try
            {
                if (!File.Exists(_filePath)) return Enumerable.Empty<Cita>();
                var json = File.ReadAllText(_filePath);
                return JsonSerializer.Deserialize<List<Cita>>(json) ?? new List<Cita>();
            }
            catch
            {
                return Enumerable.Empty<Cita>();
            }
        }

        public List<Cita> ObtenerPorPaciente(int pacienteId)
        {
            return ObtenerTodos().Where(c => c.PacienteId == pacienteId).ToList();
        }

        private void CrearJsonSiNoExiste()
        {
            if (!File.Exists(_filePath))
            {
                var dePrueba = new List<Cita>
                {
                    new Cita { Id = 1, PacienteId = 1, MedicoId = 1, Fecha = DateTime.Parse("2026-06-01"), Hora = TimeSpan.Parse("09:00:00"), Motivo = "Consulta general", Estado = "Pendiente" },
                    new Cita { Id = 2, PacienteId = 2, MedicoId = 2, Fecha = DateTime.Parse("2026-06-01"), Hora = TimeSpan.Parse("10:00:00"), Motivo = "Revisión de resultados", Estado = "Pendiente" },
                    new Cita { Id = 3, PacienteId = 3, MedicoId = 1, Fecha = DateTime.Parse("2026-06-02"), Hora = TimeSpan.Parse("11:00:00"), Motivo = "Control mensual", Estado = "Pendiente" },
                    new Cita { Id = 4, PacienteId = 4, MedicoId = 3, Fecha = DateTime.Parse("2026-06-03"), Hora = TimeSpan.Parse("12:00:00"), Motivo = "Cirugía", Estado = "Pendiente" }
                };
                File.WriteAllText(_filePath, JsonSerializer.Serialize(dePrueba, new JsonSerializerOptions { WriteIndented = true }));
            }
        }

        List<Cita> ICitaRepository.ObtenerTodos()
        {
            throw new NotImplementedException();
        }

        public Cita? ObtenerPorId(int id)
        {
            throw new NotImplementedException();
        }

        public void Agregar(Cita cita)
        {
            throw new NotImplementedException();
        }
    }
}