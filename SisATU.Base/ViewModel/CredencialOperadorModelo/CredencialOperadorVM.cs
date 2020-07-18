using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SisATU.Base.ViewModel
{
    public class CredencialOperadorVM
    {
        public int ID_CREDENCIAL_OPERADOR { get; set; }
        public int ID_OPERADOR { get; set; }
        public int ID_EXPEDIENTE { get; set; }
        public string FECHA_IMPRESION { get; set; }
        public string FECHA_VENCIMIENTO { get; set; }
        public string FECHA_ENTREGA { get; set; }
        public int ANIO_CREDENCIAL { get; set; }
        public int ID_TIPO_DOCUMENTO_OPERADOR { get; set; }
        public List<SelectListItem> SelectTipoDocumento { get; set; }
        public string NUMERO_DOCUMENTO_OPERADOR { get; set; }
        public string FECHA_INICIO { get; set; }
        public int ID_ESTADO { get; set; }
        public string USUARIO_REG { get; set; }
        public string FECHA_REG { get; set; }
        public string USUARIO_MOD { get; set; }
        public string FECHA_MOD { get; set; }
        public string USUARIO_ELIM { get; set; }
        public string FECHA_ELIM { get; set; }
    }
}
