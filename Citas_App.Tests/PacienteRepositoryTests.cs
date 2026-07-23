using Citas_App.Models;
using Citas_App.Repositories;
using Xunit;

namespace Citas_App.Tests
{
    public class PacienteRepositoryTests : IDisposable
    {
        private readonly string _tempFilePath;
        private readonly PacienteRepository _repository;

        public PacienteRepositoryTests()
        {
            _tempFilePath = Path.Combine(Path.GetTempPath(), $"pacientes_test_{Guid.NewGuid()}.json");
            _repository = new PacienteRepository(_tempFilePath);
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
        public void Agregar_AsignaIdAutomaticamente()
        {
            // Arrange
            var paciente = new Paciente { Nombre = "Ana", Apellido = "Perez", Email = "ana@test.com", Telefono = "111" };

            // Act
            _repository.Agregar(paciente);
            var todos = _repository.ObtenerTodos();

            // Assert
            Assert.Single(todos);
            Assert.Equal(2, todos[0].Id);
        }

        [Fact]
        public void ObtenerPorId_DevuelveElPacienteCorrecto()
        {
            // Arrange
            var paciente1 = new Paciente { Nombre = "Ana", Apellido = "Perez", Email = "ana@test.com", Telefono = "111" };
            var paciente2 = new Paciente { Nombre = "Luis", Apellido = "Gomez", Email = "luis@test.com", Telefono = "222" };
            _repository.Agregar(paciente1);
            _repository.Agregar(paciente2);

            // Act
            var encontrado = _repository.ObtenerPorId(2);

            // Assert
            Assert.NotNull(encontrado);
            Assert.Equal("Luis", encontrado!.Nombre);
        }

        public void Dispose()
        {
            if (File.Exists(_tempFilePath))
                File.Delete(_tempFilePath);
        }
    }
}
