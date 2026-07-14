using System.Collections.Generic;
using System.Linq;
using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;
using CitasApp.Infrastructure.Data;

namespace CitasApp.Infrastructure
{
    public class SqliteCitaRepository : ICitaRepository
    {
        private readonly ApplicationDbContext _context;

        public SqliteCitaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Cita> ObtenerTodos()
        {
            return _context.Citas.ToList();
        }

        public Cita? ObtenerPorId(int id)
        {
            return _context.Citas.Find(id);
        }

        public void Agregar(Cita cita)
        {
            _context.Citas.Add(cita);
            _context.SaveChanges();
        }

        public List<Cita> ObtenerPorPaciente(int pacienteId)
        {
            return _context.Citas
                .Where(c => c.PacienteId == pacienteId)
                .ToList();
        }
    }
}