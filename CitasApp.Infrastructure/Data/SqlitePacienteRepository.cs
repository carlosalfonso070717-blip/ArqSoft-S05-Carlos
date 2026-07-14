using System.Collections.Generic;
using System.Linq;
using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;
using CitasApp.Infrastructure.Data;

namespace CitasApp.Infrastructure
{
    public class SqlitePacienteRepository : IPacienteRepository
    {
        private readonly ApplicationDbContext _context;

        public SqlitePacienteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Paciente> ObtenerTodos()
        {
            return _context.Pacientes.ToList();
        }

        public Paciente? ObtenerPorId(int id)
        {
            return _context.Pacientes.Find(id);
        }

        public void Agregar(Paciente paciente)
        {
            _context.Pacientes.Add(paciente);
            _context.SaveChanges();
        }
    }
}