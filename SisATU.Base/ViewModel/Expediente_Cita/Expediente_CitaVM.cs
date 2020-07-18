using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Base.ViewModel
{
    public class Expediente_CitaVM
    {
        public int ID_EXPEDIENTE { get; set; }
        public string NUMERO_SID { get; set; }
        public string NUMERO_ANIO { get; set; }
        public string FECHA_CITA { get; set; }
        public string ADMINISTRADO { get; set; }
    }
}
