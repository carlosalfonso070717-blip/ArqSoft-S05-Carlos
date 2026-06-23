using Microsoft.AspNetCore.Hosting;
using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;
using System.IO;

namespace CitasApp.Infrastructure.Repositories
{
    public static class RepositoryFactory
    {
        public static IPacienteRepository CrearPacienteRepository(string entorno, IWebHostEnvironment env)
        {
            if (entorno == "Production")
            {
                // En producción usamos tu repositorio original que lee de la carpeta Data
                var dataPath = Path.Combine(env.ContentRootPath, "Data");
                return new PacienteRepository(dataPath);

                // NOTA: Si prefieres usar SQLite en producción, cambia la línea de arriba por:
                // return new SqlitePacienteRepository(dataPath);
            }

            // Cualquier otro entorno (Development) -> Retorna tu nuevo repositorio JSON
            // Como tu constructor es vacío, no necesita que le pasemos el dataPath
            return new JsonPacienteRepository();
        }
    }
}