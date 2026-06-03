using Citas_App.Interfaces;
using Citas_App.Models;
using Microsoft.AspNetCore.Mvc;

namespace Citas_App.Controllers
{
    public class CitaController : Controller
    {
        private readonly ICitaRepository _citaRepo;
        private readonly IPacienteRepository _pacienteRepo;
        private readonly IMedicoRepository _medicoRepo;

        public CitaController(ICitaRepository citaRepo,
                              IPacienteRepository pacienteRepo,
                              IMedicoRepository medicoRepo)
        {
            _citaRepo = citaRepo;
            _pacienteRepo = pacienteRepo;
            _medicoRepo = medicoRepo;
        }

        public IActionResult Index()
        {
            ViewBag.Pacientes = _pacienteRepo.ObtenerTodos();
            ViewBag.Medicos = _medicoRepo.ObtenerTodos();
            return View(_citaRepo.ObtenerTodos());
        }

        public IActionResult PorPaciente(int pacienteId)
        {
            ViewBag.Pacientes = _pacienteRepo.ObtenerTodos();
            ViewBag.Medicos = _medicoRepo.ObtenerTodos();
            return View(_citaRepo.ObtenerPorPaciente(pacienteId));
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Pacientes = _pacienteRepo.ObtenerTodos();
            ViewBag.Medicos = _medicoRepo.ObtenerTodos();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Cita cita)
        {
            if (ModelState.IsValid)
            {
                _citaRepo.Agregar(cita);
                TempData["SuccessMessage"] = "Cita agendada exitosamente.";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Pacientes = _pacienteRepo.ObtenerTodos();
            ViewBag.Medicos = _medicoRepo.ObtenerTodos();
            return View(cita);
        }
    }
}
