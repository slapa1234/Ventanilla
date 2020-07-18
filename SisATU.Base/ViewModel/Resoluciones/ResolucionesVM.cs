using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Base.ViewModel
{
    public class ResolucionesVM
    {
        public ResolucionesVM()
        {
            
        }
        public int ID_EXPEDIENTE_PADRE { get; set; }
        public int ID_EXPEDIENTE_HIJO { get; set; }
        public string FECHA_REGISTRO { get; set; }
        public string PROPIETARIO { get; set; }
        public string NUMERO_DOC_SOLICITANTE { get; set; }
        public string PLACA { get; set; }
        public string ANIO { get; set; }
        public string NUMERO_RESOLUCION { get; set; }
    }
}
