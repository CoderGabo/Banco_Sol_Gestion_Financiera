namespace Banco_Sol_Gestion_Financiera.Common
{
    public class ErrorMessages
    {
        public const string USER_NOT_FOUND =
            "USR001 - El usuario no existe.";

        public const string EMAIL_ALREADY_EXISTS =
            "USR002 - El correo ya está registrado.";

        public const string NAME_REQUIRED =
            "USR003 - El nombre es obligatorio.";

        public const string NAME_TOO_LONG =
            "USR004 - El nombre no puede superar los 100 caracteres.";

        public const string EMAIL_REQUIRED =
            "USR005 - El correo es obligatorio.";

        public const string INVALID_EMAIL =
            "USR006 - El formato del correo electrónico no es válido.";

        public const string PASSWORD_REQUIRED =
            "USR007 - La contraseña es obligatoria.";

        public const string PASSWORD_TOO_SHORT =
            "USR008 - La contraseña debe tener al menos 8 caracteres.";

        public const string INVALID_CURRENCY =
            "INC001 - La moneda debe ser 'BOB' o 'USD'.";

        public const string INVALID_AMOUNT =
            "INC002 - El monto debe ser mayor a cero.";

        public const string DESCRIPTION_REQUIRED =
            "INC003 - La descripción es obligatoria.";

        public const string SOURCE_REQUIRED =
            "INC004 - El origen del ingreso es obligatorio.";

        public const string RECEIVED_DATE_REQUIRED =
            "INC005 - La fecha de recepción es obligatoria.";

        public const string DESCRIPTION_TOO_LONG =
            "INC006 - La descripción no puede superar los 255 caracteres.";

        public const string SOURCE_TOO_LONG =
            "INC007 - El origen del ingreso no puede superar los 50 caracteres.";

        public const string RECEIVED_DATE_FUTURE =
            "INC008 - La fecha del ingreso no puede ser posterior a la fecha actual.";

        public const string START_DATE_REQUIRED =
            "REP001 - La fecha de inicio es obligatoria.";

        public const string END_DATE_REQUIRED =
            "REP002 - La fecha de fin es obligatoria.";

        public const string INVALID_DATE_RANGE =
            "REP003 - La fecha de inicio no puede ser mayor que la fecha de fin.";

        public const string FUTURE_DATE_NOT_ALLOWED =
            "REP004 - Las fechas no pueden ser posteriores a la fecha actual.";
    }
}
