using System;
using System.Collections.Generic;
using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;

namespace CitasApp.Infrastructure.Repositories
{
    public class LoggingPacienteRepository : IPacienteRepository
    {
        private readonly IPacienteRepository _innerRepository;

        public LoggingPacienteRepository(IPacienteRepository innerRepository)
        {
            _innerRepository = innerRepository;
        }

        public List<Paciente> ObtenerTodos()
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            Console.WriteLine($"[{timestamp}] ObtenerTodos – inicio");

            var lista = _innerRepository.ObtenerTodos();

            Console.WriteLine($"[{timestamp}] ObtenerTodos – {lista.Count} registros");
            return lista;
        }

        public Paciente ObtenerPorId(int id)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            Console.WriteLine($"[{timestamp}] ObtenerPorId({id}) - inicio");

            var paciente = _innerRepository.ObtenerPorId(id);

            if (paciente != null)
            {
                Console.WriteLine($"[{timestamp}] ObtenerPorId({id}) - encontrado"); 
            }
            else
            {
                Console.WriteLine($"[{timestamp}] ObtenerPorId({id}) - no encontrado");
            }

            return paciente!;
        }

        public void Agregar(Paciente paciente)
        {
            _innerRepository.Agregar(paciente);
        }
    }
}