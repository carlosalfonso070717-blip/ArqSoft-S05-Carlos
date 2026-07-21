using System;
using System.Collections.Generic;
using System.Linq;
using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;

namespace CitasApp.Application.Services
{
    public class CitaService
    {
        private readonly ICitaRepository _repository;
        private readonly IEnumerable<ICitaObserver> _observers;

        // El costructor recibe el repositorio de citas y TODOS los observadores (Sms, Email) inyectados desde Program.cs
        public CitaService(ICitaRepository repository, IEnumerable<ICitaObserver> observers)
        {
            _repository = repository;
            _observers = observers;
        }

        public IEnumerable<Cita> ObtenerTodos()
        {
            return _repository.ObtenerTodos();
        }

        public List<Cita> ObtenerPorPaciente(int pacienteId)
        {
            return _repository.ObtenerPorPaciente(pacienteId);
        }

        // ── El método clave del patrón Observer ────────────────────────────────
        public Cita ConfirmarCita(int citaId)
        {
            var cita = _repository.ObtenerTodos().FirstOrDefault(c => c.Id == citaId);

            if (cita != null)
            {
                RegistrarLogConfirmacion(cita);

                cita.Estado = "Confirmada";

                NotificarObservadores(cita);
            }

            return cita;
        }
        private void RegistrarLogConfirmacion(Cita cita)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            Console.WriteLine($"[{timestamp}] Cita {cita.Id} confirmada");
        }

        private void NotificarObservadores(Cita cita)
        {
            foreach (var observer in _observers)
            {
                observer.Notificar(cita);
            }
        }
    }
}