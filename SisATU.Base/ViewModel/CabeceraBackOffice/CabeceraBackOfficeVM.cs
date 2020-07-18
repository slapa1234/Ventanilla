using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SisATU.Base.ViewModel
{
    public class CabeceraBackOfficeVM
    {
        public CabeceraBackOfficeVM()
        {

        }
        
        public String TRAMITE { get; set; }
        public String NUMERO_SOLICITANTE { get; set; }
        public String SOLICITANTE { get; set; }
        public String NUMERO_RECURRENTE { get; set; }
        public String RECURRENTE { get; set; }

        public String SOLICITUD { get; set; }
        public String NOMBRE_PROCEDIMIENTO { get; set; }
        public String MODALIDAD_SERVICIO { get; set; }
        public String DATOREGISTRO { get; set; }
        public String FECHA_ATENCION { get; set; }


    }
}
