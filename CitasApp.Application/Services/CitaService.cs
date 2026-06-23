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

        // El constructor recibe el repositorio de citas y TODOS los observadores (Sms, Email) inyectados desde Program.cs
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
            // 1. Buscamos la cita por su ID utilizando el repositorio
            var cita = _repository.ObtenerTodos().FirstOrDefault(c => c.Id == citaId);

            if (cita != null)
            {
                // 2. Pintamos en la consola el log principal de confirmación con la hora del sistema
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                Console.WriteLine($"[{timestamp}] Cita {cita.Id} confirmada");

                // 3. Actualizamos el estado de la cita usando la propiedad real de tu Cita.cs
                cita.Estado = "Confirmada";

                // 4. NOTIFICACIÓN REACTIVA: Avisamos a cada observador registrado (SMS y Email)
                foreach (var observer in _observers)
                {
                    observer.Notificar(cita);
                }
            }

            // Retornamos el objeto cita (con su estado actualizado) al controlador
            return cita;
        }
    }
}