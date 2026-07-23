using Citas_App.Models;
using Citas_App.Repositories;
using Xunit;

namespace Citas_App.Tests
{
    public class MedicoRepositoryTests : IDisposable
    {
        private readonly string _tempFilePath;
        private readonly MedicoRepository _repository;

        public MedicoRepositoryTests()
        {
            _tempFilePath = Path.Combine(Path.GetTempPath(), $"medicos_test_{Guid.NewGuid()}.json");
            _repository = new MedicoRepository(_tempFilePath);
        }

        [Fact]
        public void ObtenerTodos_CuandoArchivoNoExiste_DevuelveListaVacia()
        {
            // Arrange (repositorio apunta a un archivo temporal que aún no existe)

            // Act
            var resultado = _repository.ObtenerTodos();

            // Assert
            Assert.Empty(resultado);
        }

        [Fact]
        public void Agregar_AsignaIdsIncrementales()
        {
            // Arrange
            var medico1 = new Medico { Nombre = "Carlos", Apellido = "Llanes", Especialidad = "Hematologia", NumeroLicencia = "MED-001" };
            var medico2 = new Medico { Nombre = "Sofia", Apellido = "Rios", Especialidad = "Pediatria", NumeroLicencia = "MED-002" };

            // Act
            _repository.Agregar(medico1);
            _repository.Agregar(medico2);

            // Assert
            Assert.Equal(1, medico1.Id);
            Assert.Equal(2, medico2.Id);
        }

        [Fact]
        public void ObtenerPorId_CuandoNoExiste_DevuelveNull()
        {
            // Arrange
            var medico = new Medico { Nombre = "Carlos", Apellido = "Llanes", Especialidad = "Hematologia", NumeroLicencia = "MED-001" };
            _repository.Agregar(medico);

            // Act
            var resultado = _repository.ObtenerPorId(99);

            // Assert
            Assert.Null(resultado);
        }

        public void Dispose()
        {
            if (File.Exists(_tempFilePath))
                File.Delete(_tempFilePath);
        }
    }
}
