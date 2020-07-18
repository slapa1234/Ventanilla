using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SisATU.Base
{
    public class DatosAccesoAdministrado
    {
        public List<SelectListItem> SelectSolicitud { get; set; }
        public List<SelectListItem> SelectTipoModalidad { get; set; }
        public int ID_MODALIDAD_SERVICIO { get; set; }
        public List<SelectListItem> SelectTipoDocumento { get; set; }
        public int ID_TIPO_DOCUMENTO { get; set; }
        public int ID_TIPO_PERSONA { get; set; }
        public string passwordAdministrado { get; set; }
        public string NroDocumento { get; set; }
        public string Asunto { get; set; }
        public string NroPlaca { get; set; }
        public int ID_SOLICITUD { get; set; }
        //clave//
        public string CLAVE_NUEVO { get; set; }
        //
    }
}
