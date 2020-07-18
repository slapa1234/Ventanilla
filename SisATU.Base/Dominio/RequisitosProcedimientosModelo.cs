using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Base
{
    public class RequisitosProcedimientosModelo
    {
        public int ID_REQUISITOS_PROCEDIMIENTOS { get; set; }
        public int ID_PROCEDIMIENTO { get; set; }
        public string DESCRIPCION_REQUISITOS { get; set; }
        public int ID_ESTADO { get; set; }
        public string USUARIO_REG { get; set; }
        public string FECHA_REG { get; set; }
        public string USUARIO_MOD { get; set; }
        public string FECHA_MOD { get; set; }
        public string USUARIO_ELIM { get; set; }
        public string FECHA_ELIM { get; set; }
    }
}
