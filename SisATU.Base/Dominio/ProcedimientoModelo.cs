using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Base
{
    public class ProcedimientoModelo
    {
        public int ID_PROCEDIMIENTO { get; set; }
        public int TIPO_PROCEDIMIENTO { get; set; }
        public string NOMBRE_PROCEDIMIENTO { get; set; }
        public int VALOR_PROCEDIMIENTO { get; set; }
        public string DETALLE_MODALIDAD { get; set; }
        public int ID_ESTADO { get; set; }
        public string USUARIO_REG { get; set; }
        public string FECHA_REG { get; set; }
        public string USUARIO_MOD { get; set; }
        public string FECHA_MOD { get; set; }
        public string USUARIO_ELIM { get; set; }
        public string FECHA_ELIM { get; set; }
        public int ID_PROCEDIMIENTO_ENUNCIADO { get; set; }
        public string MONTO { get; set; }
        public int FLAG_AUTOMATIZACION { get; set; }
    }
}
