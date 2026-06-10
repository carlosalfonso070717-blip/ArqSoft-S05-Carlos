# 🏥 MediApp - Sistema de Gestión de Citas Médicas

## 📋 Descripción del Proyecto

**MediApp** es un sistema integral de gestión de citas médicas desarrollado con ASP.NET Core MVC. La aplicación permite administrar eficientemente citas médicas, pacientes y personal médico en un entorno hospitalario o clínico.

El sistema cuenta con una interfaz moderna y intuitiva con diseño glassmorphism, búsqueda en tiempo real, filtros avanzados y gestión completa de datos mediante archivos JSON.

### Características Principales

-  **Gestión de Pacientes**: Registro completo de pacientes con información de contacto
-  **Gestión de Médicos**: Administración del personal médico y sus especialidades
-  **Gestión de Citas**: Agendamiento y control de citas médicas con estados
-  **Búsqueda en Tiempo Real**: Filtra pacientes, médicos y citas instantáneamente
-  **Filtros Avanzados**: Filtra citas por estado (Pendiente/Confirmada/Cancelada) y fecha
-  **Interfaz Moderna**: Diseño glassmorphism con gradientes y animaciones suaves
-  **Estadísticas en Tiempo Real**: Visualiza métricas importantes del sistema
-  **Persistencia de Datos**: Almacenamiento en archivos JSON
-  **Manejo de Errores**: Sistema robusto con validación y recuperación de datos corruptos
-  **Responsive Design**: Compatible con dispositivos móviles, tablets y escritorio

---

## Tecnologías Utilizadas

### Backend
- **Framework**: ASP.NET Core MVC 10.0
- **Lenguaje**: C# 12.0
- **Arquitectura**: Patrón Repositorio con Inyección de Dependencias
- **Persistencia**: System.Text.Json (Archivos JSON)

### Frontend
- **Motor de Vistas**: Razor Views
- **CSS Framework**: Bootstrap 5.3
- **CSS Personalizado**: Glassmorphism, Gradientes, Animaciones CSS3
- **JavaScript**: Vanilla JS para búsqueda y filtros en tiempo real
- **Tipografía**: Google Fonts (Inter)
- **Iconos**: Emoji Unicode

### Patrones y Prácticas
-  **Patrón Repositorio**: Separación de lógica de acceso a datos
-  **Inyección de Dependencias**: Constructor injection con ASP.NET Core DI
-  **MVC Pattern**: Separación clara de responsabilidades
-  **Manejo de Excepciones**: Try-catch con backup automático de datos corruptos
-  **Validación de Datos**: Data Annotations y validación del lado del servidor
---

## 📸 Capturas de Pantalla

<img width="1919" height="1199" alt="image" src="https://github.com/user-attachments/assets/7c1b217a-1a8c-4033-8c5f-a5f7d6be536a" />


<img width="1845" height="1059" alt="image" src="https://github.com/user-attachments/assets/240524d5-da3f-4494-b104-db10b4b34487" />


<img width="1796" height="1012" alt="image" src="https://github.com/user-attachments/assets/8c335a9e-b99e-4727-b8bb-ecb70f040ae8" />


<img width="1822" height="989" alt="image" src="https://github.com/user-attachments/assets/d686947a-f951-4216-8c7f-71f00711e2be" />




---

##  Funcionalidades Detalladas

###  Módulo de Pacientes
- Registro de pacientes con nombre, apellido, email y teléfono
- Visualización en cards con avatares
- Búsqueda en tiempo real por cualquier campo
- Vista de detalles completos
- Generación automática de IDs

###  Módulo de Médicos
- Registro de médicos con especialidad y número de licencia
- Visualización en cards con información destacada
- Búsqueda por nombre, especialidad o licencia
- Contador de especialidades únicas
- Vista de perfil médico

###  Módulo de Citas
- Agendamiento de citas con paciente, médico, fecha y hora
- Estados: Pendiente, Confirmada, Cancelada 
- Filtros combinados: búsqueda + estado + fecha
- Vista de calendario visual con día y mes
- Detalles completos de cada cita
- Validación de datos del lado del servidor

###  Dashboard
- Estadísticas en tiempo real
- Accesos rápidos a todas las funcionalidades
- Cards interactivas con efectos hover
- Guía de inicio rápido
- Consejos del sistema

---

## 🔧 Configuración

### Archivos JSON
Los datos se almacenan en archivos JSON
