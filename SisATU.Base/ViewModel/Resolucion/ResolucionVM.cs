using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Base.ViewModel
{
    public class ResolucionVM
    {
        public int ID_RESOLUCION { get; set; }
        public int ID_TIPO_AUTORIZACION { get; set; }
        public string FECHA_AUTORIZACION { get; set; }
        public string FECHA_VIGENCIA { get; set; }
        public int ID_TIPO_RESOLUCION { get; set; }
        public string FECHA_NOTIFICACION { get; set; }
        public string ASUNTO { get; set; }
        public int ID_ESTADO { get; set; }
        public string USUARIO_REG { get; set; }
        public string FECHA_REG { get; set; }
        public string USUARIO_MOD { get; set; }
        public string FECHA_MOD { get; set; }
        public string USUARIO_ELIM { get; set; }
        public string FECHA_ELIM { get; set; }
    }
}
