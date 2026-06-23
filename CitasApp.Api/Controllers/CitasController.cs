using CitasApp.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace CitasApp.Api.Controllers
{
    [ApiController]
    [Route("api/citas")]
    public class CitasController : ControllerBase
    {
        private readonly CitaService _citaService;
        private readonly PacienteService _pacienteService;
        private readonly MedicoService _medicoService;

        public CitasController(CitaService citaService,
                               PacienteService pacienteService,
                               MedicoService medicoService)
        {
            _citaService = citaService;
            _pacienteService = pacienteService;
            _medicoService = medicoService;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_citaService.ObtenerTodos());

        [HttpGet("porpaciente/{pacienteId}")]
        public IActionResult PorPaciente(int pacienteId)
        {
            var citas = _citaService.ObtenerPorPaciente(pacienteId);
            return citas.Count == 0 ? NotFound() : Ok(citas);
        }

        // ── NUEVO: El endpoint POST para cumplir con el Reto ──────────────────
        [HttpPost("confirmar/{id}")]
        public IActionResult ConfirmarCita(int id)
        {
            // 1. Llama a tu servicio que dispara los observadores (Sms y Email)
            _citaService.ConfirmarCita(id);

            // 2. Retorna la respuesta en formato JSON que espera el profesor
            return Ok(new
            {
                mensaje = "Cita confirmada",
                id = id
            });
        }
    }
}