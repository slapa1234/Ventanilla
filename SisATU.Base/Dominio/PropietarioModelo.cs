using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Base
{
    public class PropietarioModelo
    {
        public int ID_PROPIETARIO { get; set; }
        public string NRO_DOCUMENTO { get; set; }
        public int ID_TIPO_CONTRIBUYENTE { get; set; }
        public int ID_TIPO_DOCUMENTO { get; set; }
        public int ID_TARJETA_PROPIEDAD { get; set; }
        public string OBSERVACION { get; set; }
        public int ID_ESTADO { get; set; }
        public string USUARIO_REG { get; set; }
        public string FECHA_REG { get; set; }
        public string USUARIO_MOD { get; set; }
        public string FECHA_MOD { get; set; }
        public string USUARIO_ELIM { get; set; }
        public string FECHA_ELIM { get; set; }
        public string NOMBRE_PROPIETARIO { get; set; }
    }
}
