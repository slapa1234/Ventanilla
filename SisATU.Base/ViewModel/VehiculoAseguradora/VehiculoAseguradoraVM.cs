using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Base.ViewModel
{
    public class VehiculoAseguradoraVM
    {
        public int ID_VEHICULO_ASEGURADORA { get; set; }
        public int ID_ASEGURADORA { get; set; }
        public int ID_VEHICULO { get; set; }
        public int ID_ESTADO { get; set; }
        public string POLIZA { get; set; }
        public string FEC_INI_VIGENCIA { get; set; }
        public string FEC_FIN_VIGENCIA { get; set; }
        public string USUARIO_REG { get; set; }
        public string FECHA_REG { get; set; }
        public string USUARIO_MOD { get; set; }
        public string FECHA_MOD { get; set; }
        public string USUARIO_ELIM { get; set; }
        public string FECHA_ELIM { get; set; }
    }
}
