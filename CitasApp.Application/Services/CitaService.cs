using System.Collections.Generic;
using System.Linq; // 🧠 Nuevo: Para buscar la cita por ID dentro del IEnumerable
using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models; // Mantenemos tus modelos tal como los tenías

namespace CitasApp.Application.Services
{
    public class CitaService
    {
        private readonly ICitaRepository _repository;
        private readonly IEnumerable<ICitaObserver> _observers; // ── NUEVO: Lista de observadores desacoplados

        // MODIFICADO: El constructor ahora recibe el repositorio y TODOS los observadores registrados en Program.cs
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

        // ── NUEVO: Método requerido para resolver el Reto Observer ──────────────
        public void ConfirmarCita(int citaId)
        {
            // 1. Buscamos la cita usando de forma segura tu método ObtenerTodos()
            var cita = _repository.ObtenerTodos().FirstOrDefault(c => c.Id == citaId);

            if (cita != null)
            {
                // 2. Ejecutas la actualización de estado si tu modelo Cita maneja la propiedad
                // cita.Confirmada = true;

                // Si tu interfaz ICitaRepository cuenta con método para persistir el cambio, lo llamas aquí:
                // _repository.Actualizar(cita);

                // 3. NOTIFICACIÓN REACTIVA: Avisamos a cada observador registrado (SMS, Email, etc.)
                // Recuerda que aquí se ejecutan sin que Application conozca las clases de Infraestructura.
                foreach (var observer in _observers)
                {
                    observer.Notificar(cita);
                }
            }
        }
    }
}