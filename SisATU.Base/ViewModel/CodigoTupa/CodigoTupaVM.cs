using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Base.ViewModel
{
    public class CodigoTupaVM
    {
        public int ID_REG { get; set; }
        public int PARCOD { get; set; }
        public string DESCRIP { get; set; }
        public string USUREG { get; set; }
        public DateTime FECREG { get; set; }
        public string USUMOD { get; set; }
        public DateTime FECMOD { get; set; }
        public int ESTREG { get; set; }
        public string PARSIG { get; set; }
        public int VAL { get; set; }
        public string DER_TRA { get; set; }
        public string TIPO { get; set; }
        public string CODIGO { get; set; }
        public int IDMAT { get; set; }
    }
}
