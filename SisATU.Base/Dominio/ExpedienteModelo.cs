using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Base
{
    public class ExpedienteModelo
    {
        public int ID_EXPEDIENTE { get; set; }
        public int IDDOC { get; set; }
        public string NUMERO_SID { get; set; }
        public string NUMERO_ANIO { get; set; }
        public int ID_PROCEDIMIENTO { get; set; }
        public int ID_MODALIDAD_SERVICIO { get; set; }
        public int ID_SOLICITUD { get; set; }
        public string NUMERO_SOLICITANTE { get; set; }
        public string NUMERO_RECURRENTE { get; set; }
        public int ID_ESTADO { get; set; }
        public string USUARIO_REG { get; set; }
        public string FECHA_REG { get; set; }
        public string USUARIO_MOD { get; set; }
        public string FECHA_MOD { get; set; }
        public string USUARIO_ELIM { get; set; }
        public string FECHA_ELIM { get; set; }
        public int IS_PADRE_EXPEDIENTE { get; set; }
        public int ID_EXPEDIENTE_PADRE { get; set; }
        public string ASUNTO_NO_TUPA { get; set; }
        public int ID_VEHICULO { get; set; }
        public int IDFLUJO { get; set; }
    }
}
