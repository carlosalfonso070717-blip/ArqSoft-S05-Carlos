using Citas_App.Models;
using Citas_App.Repositories;
using Xunit;

namespace Citas_App.Tests
{
    public class CitaRepositoryTests : IDisposable
    {
        private readonly string _tempFilePath;
        private readonly CitaRepository _repository;

        public CitaRepositoryTests()
        {
            _tempFilePath = Path.Combine(Path.GetTempPath(), $"citas_test_{Guid.NewGuid()}.json");
            _repository = new CitaRepository(_tempFilePath);
        }

        [Fact]
        public void ObtenerTodos_CuandoNoHayCitas_DevuelveListaVacia()
        {
            // Arrange (repositorio apunta a un archivo temporal que aún no existe)

            // Act
            var resultado = _repository.ObtenerTodos();

            // Assert
            Assert.Empty(resultado);
        }

        [Fact]
        public void Agregar_GuardaLaCitaCorrectamente()
        {
            // Arrange
            var cita = new Cita
            {
                PacienteId = 1,
                MedicoId = 1,
                Fecha = new DateOnly(2026, 7, 17),
                Hora = new TimeOnly(14, 30),
                Motivo = "Control",
                Estado = "Pendiente"
            };

            // Act
            _repository.Agregar(cita);
            var todas = _repository.ObtenerTodos();

            // Assert
            Assert.Single(todas);
            Assert.Equal("Control", todas[0].Motivo);
        }

        [Fact]
        public void ObtenerPorPaciente_FiltraSoloLasCitasDeEsePaciente()
        {
            // Arrange
            _repository.Agregar(new Cita { PacienteId = 1, MedicoId = 1, Fecha = new DateOnly(2026, 7, 17), Hora = new TimeOnly(9, 0), Motivo = "A" });
            _repository.Agregar(new Cita { PacienteId = 2, MedicoId = 1, Fecha = new DateOnly(2026, 7, 18), Hora = new TimeOnly(10, 0), Motivo = "B" });
            _repository.Agregar(new Cita { PacienteId = 1, MedicoId = 2, Fecha = new DateOnly(2026, 7, 19), Hora = new TimeOnly(11, 0), Motivo = "C" });

            // Act
            var resultado = _repository.ObtenerPorPaciente(1);

            // Assert
            Assert.Equal(2, resultado.Count);
            Assert.All(resultado, c => Assert.Equal(1, c.PacienteId));
        }

        public void Dispose()
        {
            if (File.Exists(_tempFilePath))
                File.Delete(_tempFilePath);
        }
    }
}
