using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CitasApp.Web.Controllers
{
    public class MedicoController : Controller
    {
        private readonly IMedicoRepository _repository;

        public MedicoController(IMedicoRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            var medicos = _repository.ObtenerTodos();
            return View(medicos);
        }

        public IActionResult Detalle(int id)
        {
            var medico = _repository.ObtenerPorId(id);
            if (medico == null)
                return NotFound();

            return View(medico);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Medico medico)
        {
            if (ModelState.IsValid)
            {
                _repository.Agregar(medico);
                TempData["SuccessMessage"] = "Médico registrado exitosamente.";
                return RedirectToAction(nameof(Index));
            }
            return View(medico);
        }
    }
}
