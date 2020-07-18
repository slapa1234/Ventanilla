using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Base
{
    public class TipoSeguroModelo
    {
        public int ID_TIPO_SEGURO { get; set; }
        public string NOMBRE { get; set; }
        public int ID_ESTADO { get; set; }
        public string USUARIO_REG { get; set; }
        public string FECHA_REG { get; set; }
        public string USUARIO_MOD { get; set; }
        public string FECHA_MOD { get; set; }
        public string USAURIO_ELIM { get; set; }
        public string FECHA_ELIM { get; set; }
    }
}