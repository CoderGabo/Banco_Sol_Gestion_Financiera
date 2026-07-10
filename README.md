# Banco Sol - Sistema de Gestion Financiera Personal

API REST desarrollada con ASP.NET Core 9 para el registro de ingresos personales y la generacion de reportes financieros consolidados en Bolivianos (BOB) y Dolares (USD).

La solucion implementa autenticacion mediante JWT, documentacion con Swagger y despliegue en Render utilizando PostgreSQL.

---

# Tecnologias utilizadas

- ASP.NET Core 9
- Entity Framework Core
- PostgreSQL
- JWT Authentication
- FluentValidation
- Swagger / OpenAPI
- xUnit
- Docker
- Render

---

# Como ejecutar el proyecto

1. Clonar el repositorio.

```bash
git clone https://github.com/CoderGabo/Banco_Sol_Gestion_Financiera.git
```

2. Configurar el archivo `appsettings.json` o las variables de entorno.

```json
ConnectionStrings:DefaultConnection
Jwt:Key
Jwt:Issuer
Jwt:Audience
ExchangeRate:BaseUrl
```

3. Ejecutar las migraciones.

```bash
dotnet ef database update
```
(Nota: No hace falta ejecutar este paso solo si necesitan localmente tenerlo pero tiene disponiblidad a acceder a la base de datos de Render sin problema.)

4. Ejecutar la API.

```bash
dotnet run
```

La documentacion estara disponible en:

```text
https://localhost:xxxx/swagger
```

---

# API desplegada

API

```text
https://banco-sol-gestion-financiera.onrender.com
```

Swagger (Redireccion realizada)

```text
https://banco-sol-gestion-financiera.onrender.com/swagger
```

Base de datos

- PostgreSQL (Render)

---

# Usuarios de prueba

Todos los usuarios utilizan la misma contraseña:

```text
123456
```

| Usuario | Correo |
|---------|---------|
| Gabriel Diaz | gabdihu@gmail.com |
| Jose Miguel Tordoya | algo@gmail.com |
| Jose Armando Vargas | esteesmicorreo@gmail.com |
| Wanda Sandoval | nada@gmail.com |
| Tatiana Clavijo | yoquienmas@gmail.com |

---

# Autenticacion

Todos los endpoints, excepto los de autenticacion, requieren un JWT.

Flujo recomendado:

1. Ejecutar `POST /api/auth/login`
2. Copiar el token recibido.
3. Presionar **Authorize** en Swagger.
4. Pegar el token con el formato:

```text
Bearer {token}
```

---

# Endpoints

| Metodo | Endpoint | Descripcion |
|---------|----------|-------------|
| POST | `/api/auth/login` | Inicio de sesion |
| POST | `/api/users` | Registro de usuario |
| POST | `/api/incomes` | Registrar ingreso |
| GET | `/api/incomes` | Obtener historial de ingresos |
| GET | `/api/incomes/{id}` | Obtener ingreso especifico |
| GET | `/api/exchange-rate` | Obtener tipo de cambio USD/BOB |
| GET | `/api/reports/balance` | Generar balance consolidado |

---

# Datos de prueba

La base de datos ya contiene informacion para facilitar la evaluacion.

Los ingresos pertenecen a distintos usuarios y contienen registros tanto en:

- BOB
- USD

Esto permite probar inmediatamente:

- Registro de ingresos
- Consulta de historial
- Consulta de un ingreso especifico
- Reporte consolidado
- Conversion entre monedas

Los ingresos siempre se filtran utilizando el usuario autenticado mediante JWT.

---

# Casos de uso implementados

- Registro de ingresos.
- Consulta de historial de ingresos.
- Consulta de un ingreso especifico.
- Consulta del tipo de cambio mediante una API externa.
- Reporte de balance consolidado en BOB o USD.
- Documentacion interactiva con Swagger.
- Validaciones mediante FluentValidation.

Adicionalmente se implementaron:

- Registro de usuarios.
- Autenticacion JWT.
- Manejo centralizado de errores.
- Arquitectura por capas (Controllers, Services, DTOs, Entities y Data).

---

# Pruebas

Se implementaron pruebas automatizadas utilizando xUnit para validar:

- Validadores.
- Servicios.
- Reportes.
- Autenticacion.
- Middleware.

Las pruebas utilizan Entity Framework Core InMemory para aislar cada escenario.

---

# Repositorio

```text
https://github.com/CoderGabo/Banco_Sol_Gestion_Financiera
```