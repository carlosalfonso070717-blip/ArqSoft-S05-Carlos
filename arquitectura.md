# Arquitectura y Patrones de Diseño - CitasApp

A continuación se presenta el diagrama de clases del estado actual de este proyecto, destacando la implementación de los patrones **Factory**, **Decorator** y **Observer** respetando la Arquitectura Hexagonal.

```mermaid
classDiagram
    %% Capa de Dominio (Entidades e Interfaces)
    namespace Domain {
        class Cita {
            +int Id
            +int PacienteId
            +int MedicoId
            +DateTime Fecha
            +TimeSpan Hora
            +string Motivo
            +string Estado
        }
        
        class ICitaObserver {
            <<interface>>
            +Notificar(Cita cita)
        }
        
        class IPacienteRepository {
            <<interface>>
            +ObtenerTodos() List~Paciente~
            +ObtenerPorId(int id) Paciente
            +Agregar(Paciente paciente)
        }
    }

    %% Capa de Aplicación (Casos de uso)
    namespace Application {
        class CitaService {
            -ICitaRepository _repository
            -IEnumerable~ICitaObserver~ _observers
            +ConfirmarCita(int citaId) Cita
        }
    }

    %% Capa de Infraestructura (Implementaciones)
    namespace Infrastructure {
        class SmsObserver {
            +Notificar(Cita cita)
        }
        class EmailObserver {
            +Notificar(Cita cita)
        }
        
        class LoggingPacienteRepository {
            -IPacienteRepository _innerRepository
            +ObtenerTodos() List~Paciente~
            +ObtenerPorId(int id) Paciente
        }
        
        class RepositoryFactory {
            <<static>>
            +CrearPacienteRepository(string entorno, IWebHostEnvironment env) IPacienteRepository
        }
        
        class JsonPacienteRepository {
            +ObtenerTodos() List~Paciente~
            +ObtenerPorId(int id) Paciente
        }
    }

    %% Relaciones del Patrón Observer
    ICitaObserver <|.. SmsObserver : Implementa
    ICitaObserver <|.. EmailObserver : Implementa
    CitaService o-- ICitaObserver : Contiene / Notifica

    %% Relaciones del Patrón Decorator
    IPacienteRepository <|.. LoggingPacienteRepository : Implementa
    LoggingPacienteRepository o-- IPacienteRepository : Envuelve (Wraps)

    %% Relaciones del Patrón Factory
    RepositoryFactory ..> IPacienteRepository : Crea
    IPacienteRepository <|.. JsonPacienteRepository : Implementa
