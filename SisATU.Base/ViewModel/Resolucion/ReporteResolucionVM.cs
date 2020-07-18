using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Base.ViewModel
{
    public class ReporteResolucionVM
    {
        public int ID_EXPEDIENTE_PADRE { get; set; }
        public int ID_EXPEDIENTE_HIJO { get; set; }
        public string FECHA_REG { get; set; }
        public string PROPIETARIO { get; set; }
        public string NUMERO_RECURRENTE { get; set; }
        public string PLACA { get; set; }
        public string NUMERO_RESOLUCION { get; set; }
    }
}
