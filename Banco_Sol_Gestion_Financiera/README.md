# Datos de prueba

La base de datos PostgreSQL desplegada contiene datos de ejemplo para facilitar la evaluacion de la API sin necesidad de registrar informacion manualmente.

La API cuenta con autenticacion mediante **JWT**, por lo que antes de utilizar los endpoints protegidos es necesario iniciar sesion y obtener un token de acceso.

---

# Autenticacion

Todos los endpoints de la API se encuentran protegidos mediante JWT, excepto los endpoints relacionados con autenticacion.

Para acceder a las funcionalidades protegidas:

1. Ejecutar el endpoint de login.
2. Ingresar las credenciales de uno de los usuarios de prueba.
3. Copiar el token generado en la respuesta.
4. En Swagger, presionar el boton **Authorize** ubicado en la parte superior derecha.
5. Ingresar el token utilizando el siguiente formato:

```text
Bearer {token}
```

Ejemplo:

```text
Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6...
```

Una vez autorizado, Swagger enviara automaticamente el token en todas las solicitudes posteriores.

En caso de utilizar Postman, agregar el token en los headers:

```http
Authorization: Bearer {token}
```

---

# Usuarios de prueba

Para facilitar las pruebas, se cuenta con usuarios previamente registrados.

Todas las cuentas utilizan la siguiente contraseña:

```text
123456
```

| Usuario             | Correo electronico                                          |
| ------------------- | ----------------------------------------------------------- |
| Gabriel Diaz        | [gabdihu@gmail.com](mailto:gabdihu@gmail.com)               |
| Jose Miguel Tordoya | [algo@gmail.com](mailto:algo@gmail.com)                     |
| Jose Armando Vargas | [esteesmicorreo@gmail.com](mailto:esteesmicorreo@gmail.com) |
| Wanda Sandoval      | [nada@gmail.com](mailto:nada@gmail.com)                     |
| Tatiana Clavijo     | [yoquienmas@gmail.com](mailto:yoquienmas@gmail.com)         |

---

# Informacion disponible

La base de datos desplegada contiene ingresos registrados en:

* **Bolivianos (BOB)**
* **Dolares Americanos (USD)**

Los registros incluyen fechas distribuidas entre noviembre y diciembre de 2025.

Esto permite probar inmediatamente las siguientes funcionalidades:

* Consulta del historial completo de ingresos.
* Consulta de un ingreso especifico.
* Registro de nuevos ingresos.
* Generacion del reporte de balance consolidado en BOB o USD.
* Filtrado de ingresos por rango de fechas.

---

# Manejo de usuarios e ingresos

Los ingresos se encuentran asociados al usuario autenticado mediante JWT.

Por seguridad, los endpoints no reciben el `UserId` directamente desde el cliente. El usuario se obtiene desde la informacion contenida dentro del token.

Ejemplo:

* Si Gabriel inicia sesion, solamente podra consultar y administrar sus propios ingresos.
* Si intenta consultar un ingreso perteneciente a otro usuario, la API respondera indicando que el registro no existe para ese usuario.

Esto evita accesos indebidos mediante la modificacion manual de identificadores.

---

# Flujo recomendado de prueba en Swagger

1. Acceder al endpoint de login.
2. Iniciar sesion utilizando uno de los usuarios de prueba.
3. Copiar el token generado.
4. Presionar **Authorize** en Swagger.
5. Agregar el token con el formato:

```text
Bearer {token}
```

6. Probar los endpoints disponibles:

   * Crear ingresos.
   * Consultar historial.
   * Consultar ingresos especificos.
   * Generar reportes financieros.

---

# Funcionalidades adicionales implementadas

Ademas de los casos de uso principales, se agregaron funcionalidades adicionales:

* Registro de usuarios.
* Autenticacion mediante JWT.
* Proteccion de endpoints mediante autorizacion.
* Validacion de datos de entrada.
* Manejo centralizado de errores.
* Separacion por capas utilizando servicios, DTOs, entidades y mappers.

# Endpoints disponibles

La API expone los siguientes endpoints principales:

| Metodo | Endpoint               | Descripcion                                                                                     |
| ------ | ---------------------- | ----------------------------------------------------------------------------------------------- |
| POST   | `/api/auth/login`      | Autentica un usuario y devuelve un token JWT.                                                   |
| POST   | `/api/users`           | Registra un nuevo usuario en el sistema.                                                        |
| POST   | `/api/incomes`         | Registra un nuevo ingreso para el usuario autenticado.                                          |
| GET    | `/api/incomes`         | Obtiene el historial completo de ingresos del usuario autenticado.                              |
| GET    | `/api/incomes/{id}`    | Obtiene un ingreso especifico perteneciente al usuario autenticado.                             |
| GET    | `/api/exchange-rate`   | Consulta el tipo de cambio vigente entre USD y BOB desde la API externa HexaRate.               |
| GET    | `/api/reports/balance` | Genera un reporte de balance consolidado para un periodo determinado en la moneda seleccionada. |

---

## Ejemplos de uso

### Obtener el tipo de cambio

**Solicitud**

```http
GET /api/exchange-rate
```

**Respuesta**

```json
{
  "baseCurrency": "USD",
  "targetCurrency": "BOB",
  "exchangeRate": 9.925,
  "unit": 1,
  "updatedAt": "2026-07-09T00:07:48.934Z"
}
```

---

### Obtener balance consolidado

**Solicitud**

```http
GET /api/reports/balance?startDate=2026-01-01&endDate=2026-07-08&currency=BOB
```

**Respuesta**

```json
{
  "startDate": "2026-01-01T00:00:00",
  "endDate": "2026-07-08T00:00:00",
  "currency": "BOB",
  "total": 2369.65
}
```

> **Nota:** Todos los endpoints, excepto los relacionados con autenticacion, requieren un token JWT valido enviado en el encabezado `Authorization` con el formato:
>
> ```text
> Bearer {token}
> ```

La documentacion interactiva de todos los endpoints, incluyendo parametros, modelos de solicitud, respuestas y la posibilidad de realizar pruebas directamente desde el navegador, se encuentra disponible en:

```text
/swagger
```

# Pruebas automatizadas

La solucion incluye pruebas automatizadas utilizando xUnit para verificar el correcto funcionamiento de las principales reglas de negocio y servicios de la aplicacion.

Las pruebas implementadas cubren:

## Validadores

Se verifican las reglas de validacion de entrada mediante FluentValidation:

- Registro de ingresos:
  - Rechazo de montos menores o iguales a cero.
  - Rechazo de monedas no permitidas.
  - Validacion de campos obligatorios.

- Registro de usuarios:
  - Validacion de nombre.
  - Validacion de correo electronico.
  - Validacion de contraseña.

- Reportes:
  - Validacion de rangos de fechas.
  - Validacion de fechas futuras.
  - Validacion de moneda.

## Servicios

Se incluyen pruebas para validar la logica principal de negocio:

- IncomeService:
  - Creacion correcta de ingresos.
  - Validacion de monedas invalidas.
  - Consulta de ingresos asociados al usuario.

- UserService:
  - Creacion de usuarios.
  - Validacion de correos duplicados.
  - Consulta de usuarios.

- ReportService:
  - Calculo de balances en BOB.
  - Conversion USD a BOB utilizando tipo de cambio.
  - Calculo de balances en USD.
  - Conversion BOB a USD.

- AuthService:
  - Inicio de sesion correcto.
  - Validacion de credenciales incorrectas.

## Middleware

Se verifico el manejo centralizado de errores:

- Excepciones de validacion retornan HTTP 400.
- Recursos inexistentes retornan HTTP 404.
- Errores inesperados retornan HTTP 500.

Las pruebas utilizan una base de datos en memoria mediante Entity Framework Core InMemory para aislar los escenarios y evitar dependencia de datos externos.

