using System.Collections.Generic;
using System.Linq;
using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;
using CitasApp.Infrastructure.Data;

namespace CitasApp.Infrastructure
{
    public class SqliteMedicoRepository : IMedicoRepository
    {
        private readonly ApplicationDbContext _context;

        public SqliteMedicoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Medico> ObtenerTodos()
        {
            return _context.Medicos.ToList();
        }

        public Medico? ObtenerPorId(int id)
        {
            return _context.Medicos.Find(id);
        }

        public void Agregar(Medico medico)
        {
            _context.Medicos.Add(medico);
            _context.SaveChanges();
        }
    }
}