using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Base.ViewModel
{
    public class PapeletaVM
    {
        public int COD_ADMINISTRADO { get; set; }
        public int NUM_INFRACCION { get; set; }
        public int COD_ENTIDAD { get; set; }
        public string ENTIDAD { get; set; }
        public int PAPELETA { get; set; }
        public string FALTA { get; set; }
        public string FEC_INFRACCION { get; set; }
        public int PUNTOS_x0020_FIRMES { get; set; }
        public int P_x0020_PROCESO { get; set; }
        public string ESTADO { get; set; }
        public string TIPOPIT { get; set; }
        public string DAT_FECHA_FIRME { get; set; }
        public string FEC_FIRME { get; set; }
    }
     
}
