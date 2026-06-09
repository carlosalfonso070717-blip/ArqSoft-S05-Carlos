using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CitasApp.Web.Controllers
{
    public class PacienteController : Controller
    {
        private readonly IPacienteRepository _repository;

        public PacienteController(IPacienteRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            var pacientes = _repository.ObtenerTodos();
            return View(pacientes);
        }

        public IActionResult Detalle(int id)
        {
            var paciente = _repository.ObtenerPorId(id);
            if (paciente == null)
                return NotFound();

            return View(paciente);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                _repository.Agregar(paciente);
                TempData["SuccessMessage"] = "Paciente registrado exitosamente.";
                return RedirectToAction(nameof(Index));
            }
            return View(paciente);
        }
    }
}
