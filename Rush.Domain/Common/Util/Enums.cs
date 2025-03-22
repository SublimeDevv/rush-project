namespace Rush.Domain.Common.Util
{
    public class Enums
    {
        public enum Sexo
        {
            NO_ESPECIFICADO,
            MASCULINO,
            FEMENINO,
            OTRO
            
        }
        
        public enum Roles
        {
            Recursos_Humanos,
            Supervisor,
            Empleado,
            Admin,
            Sistemas,
            Gerente,
        }

        public enum AuditLogLevel
        {
            INFO,
            WARNING,
            ERROR,
            SUCCESS
        }

        public enum HttpMethodLog
        {
            GET,
            POST,
            PUT,
            DELETE,
            PATCH
        }


        public enum StatusProject
        {
            /*Fase del proyecto en la que se empieza la reclutacion y la definicion de este*/DEFINING,    
            /*Fase de planificacion*/PLANNING,
            /*Fase de investigacion*/ANALYSIS,
            /*Fase de diseño*/DESIGN,
            /*Fase de desarrollo */DEVELOPMENT,
            /*Paro del proyecto */ON_HOLD,
            /*Cancelado*/CANCELED,
            /*Pues completado*/COMPLETED,

        }
        public enum StatusActivity
        {
            /*Fase de desarrollo */BEGIN,
            /*Paro del proyecto */ON_HOLD,
            /*Cancelado*/CANCELED,
            /*Pues completado*/COMPLETED,

        }
    }
}
