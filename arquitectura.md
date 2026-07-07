# Diagramas del Sistema - CitasApp

A continuación se presentan los tres diagramas solicitados para ilustrar el funcionamiento y la estructura de la aplicación.

## 1. Diagrama de Clases (Entidades)
**¿Qué entidades hay y cómo se relacionan?**
Este diagrama se enfoca exclusivamente en las entidades de dominio principales: `Paciente`, `Medico` y `Cita`. Muestra cómo la cita actúa como el puente que relaciona a un paciente con su médico.

```mermaid
classDiagram
    class Paciente {
        +int Id
        +string Nombre
        +int Edad
    }
    
    class Medico {
        +int Id
        +string Nombre
    }
    
    class Cita {
        +int Id
        +int PacienteId
        +int MedicoId
        +DateTime Fecha
        +TimeSpan Hora
        +string Motivo
        +string Estado
    }

    %% Relaciones
    Paciente "1" <-- "*" Cita : Tiene
    Medico "1" <-- "*" Cita : Atiende
