using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Base
{
    public class TarjetaPropiedadModelo
    {
        public int ID_TARJETA_PROPIEDAD { get; set; }
        public int ID_VEHICULO { get; set; }
        public string DESDE { get; set; }
        public string HASTA { get; set; }
        public int ID_ESTADO { get; set; }
        public string USUARIO_REG { get; set; }
        public string FECHA_REG { get; set; }
        public string USUARIO_MOD { get; set; }
        public string FECHA_MOD { get; set; }
        public string USUARIO_ELIM { get; set; }
        public string FECHA_ELIM { get; set; }
        public string NRO_TARJETA { get; set; }
    }
}
