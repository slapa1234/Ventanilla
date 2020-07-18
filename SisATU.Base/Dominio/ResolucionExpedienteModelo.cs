using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Base
{
    public class ResolucionExpedienteModelo
    {
        public int ID_RESOLUCION_EXPEDIENTE { get; set; }
        public int ID_RESOLUCION { get; set; }
        public int ID_EXPEDIENTE { get; set; }
        public string DESDE_FECHA { get; set; }
        public string HASTA_FECHA { get; set; }
        public int ID_ESTADO { get; set; }
        public string USUARIO_REG { get; set; }
        public string FECHA_REG { get; set; }
        public string USUARIO_MOD { get; set; }
        public string FECHA_MOD { get; set; }
        public string USUARIO_ELIM { get; set; }
        public string FECHA_ELIM { get; set; }
        public string NUMERO_RESOLUCION { get; set; }
        public string RUC { get; set; }
        public int ID_TIPO_PERSONA { get; set; }
        public int ID_PROCEDIMIENTO { get; set; }
    }
}
