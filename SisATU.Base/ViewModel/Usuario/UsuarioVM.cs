using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SisATU.Base.ViewModel
{
    public class UsuarioVM
    {
        public int ID_USARIO { get; set; }
        public int ID_PERSONA { get; set; }
        public string NOMBRE_USUARIO { get; set; }
        public string CLAVE { get; set; }
        public int ID_ESTADO { get; set; }
        public string USU_REG { get; set; }
        public string FECHA_REG { get; set; }
        public string USU_MODIF { get; set; }
        public string FECHA_MODIF { get; set; }
        public string USU_ANULA { get; set; }
        public string FECHA_ANULA { get; set; }
        public List<SelectListItem> SelectTipoPersona { get; set; }
        public int ID_TIPO_PERSONA { get; set; }
        public List<SelectListItem> SelectTipoDocumento { get; set; }
        public int ID_TIPO_DOCUMENTO { get; set; }
        public int ID_TIPO_DOCUMENTO_REPRESENTANTE_LEGAL { get; set; }
        public string NRO_DOCUMENTO { get; set; }
        public string NRO_DOCUMENTO_REPRESENTANTE_LOCAL { get; set; }
        public string DNI { get; set; }
        public int DIGITO_VERIFICADOR { get; set; }
        public string RAZON_SOCIAL { get; set; }
        public string TELEFONO { get; set; }
        public string CORREO { get; set; }
        public string DIRECCION { get; set; }
        public string NOMBRES { get; set; }
        public string APEPAT { get; set; }
        public string APEMAT { get; set; }

    }
}
