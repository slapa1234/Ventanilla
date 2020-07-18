using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Base
{
    public class VehiculoCITVModelo
    {
        public int ID_VEHICULO_CITV { get; set; }
        public int ID_VEHICULO { get; set; }
        public string CERTIFICADORA_CITV { get; set; }
        public string NRO_CERTIFICADO { get; set; }
        public string FECHA_CERTIFICADO { get; set; }
        public string FECHA_VENCIMIENTO { get; set; }
        public string RESULTADO { get; set; }
        public string ESTADO_CITV { get; set; }
        public int ID_ESTADO { get; set; }
        public string USUARIO_REG { get; set; }
        public string FECHA_REG { get; set; }
        public string USUARIO_MOD { get; set; }
        public string FECHA_MOD { get; set; }
        public string USUARIO_ELIM { get; set; }
        public string FECHA_ELIM { get; set; }
    }
}
