using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Base
{
    public class EmpresaModelo
    {
        public string RUC { get; set; }
        public string RAZON_SOCIAL { get; set; }
        public string DIRECCION_LEGAL { get; set; }
        public string TELEFONO1 { get; set; }
        public string TELEFONO2 { get; set; }
        public string MAIL { get; set; }
        public int CAPITAL_SOCIAL { get; set; }
        public DateTime FECHA_INSCRIPCION_EMPRESA { get; set; }
        public string OBSERVACION { get; set; }
        public string COLOR_UNIFORME { get; set; }
        public int ID_TIPO_PERSONA { get; set; }
        public int ID_TIPO_CONTRIBUYENTE { get; set; }
        public int ID_ESTADO { get; set; }
        public string USUARIO_REG { get; set; }
        public string FECHA_REG { get; set; }
        public string USUARIO_MOD { get; set; }
        public string FECHA_MOD { get; set; }

        //public int? ID_PROVEEDOR { get; set; }

    }
}
