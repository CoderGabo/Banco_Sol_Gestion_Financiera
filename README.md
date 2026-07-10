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

2. Crear una base de datos PostgreSQL.

3. Ejecutar el script `database/schema.sql` para crear las tablas.

4. (Opcional) Ejecutar `database/seed.sql` para cargar datos de prueba.

5. Configurar el archivo `appsettings.json` o las variables de entorno.

```json
ConnectionStrings:DefaultConnection
Jwt:Key
Jwt:Issuer
Jwt:Audience
ExchangeRate:BaseUrl
```

6. Ejecutar la API.

```bash
dotnet run
```

La documentacion estara disponible en:

```text
https://localhost:xxxx/swagger

```
> Si prefieren utilizar la base de datos desplegada en Render, solo es necesario configurar la cadena de conexion correspondiente y omitir los pasos 2, 3 y 4.
---

# API desplegada

API

```text
https://banco-sol-gestion-financiera.onrender.com
```

Swagger

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

# Base de datos

Dentro de la carpeta `database` se incluyen los scripts necesarios para crear la estructura de la base de datos y cargar informacion de ejemplo.

```text
database/
├── schema.sql
└── seed.sql
```

- `schema.sql` crea las tablas utilizadas por la aplicacion.
- `seed.sql` inserta usuarios e ingresos de prueba para facilitar la evaluacion.

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

# Generacion de hash

La API incluye un endpoint auxiliar para generar un hash BCrypt compatible con la autenticacion utilizada por el sistema.

```text
GET /api/auth/hash
```

Este endpoint devuelve el hash correspondiente a la contraseña:

```text
123456
```

Puede utilizarse para generar rapidamente contraseñas al insertar nuevos usuarios manualmente en la base de datos.

# Tipo de cambio

La API consulta el tipo de cambio USD/BOB mediante HexaRate.

Como mecanismo de resiliencia, el sistema mantiene el ultimo tipo de cambio exitoso obtenido. En caso de indisponibilidad temporal del proveedor externo, se utiliza el ultimo valor disponible.

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
