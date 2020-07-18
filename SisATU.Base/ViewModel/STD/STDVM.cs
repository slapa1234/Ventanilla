using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Base.ViewModel
{
    public class STDVM
    {
        public int IDDOC { get; set; }
        public string NUMERO_SID { get; set; }
        public string NUMERO_ANIO { get; set; }
        public int TIPO_EXPEDIENTE { get; set; }
        public int IDUNIDAD_STD { get; set; }
        public int CODPAIS { get; set; }
        public int CODDPTO { get; set; }
        public int CODPROV { get; set; }
        public int CODDIST { get; set; }
        public string DIRECCION_STD { get; set; }
        public int? ID_PERSONA { get; set; }
        public int? ID_PROVEEDOR { get; set; }
        public int ID_PROCEDIMIENTO { get; set; }
        public string NOMBRE { get; set; }
        public string OBSERVACION { get; set; }
        public int? IDFLUJO { get; set; }
        public int IDDOC_PADRE { get; set; }
        public int IDDOC_HIJO { get; set; }
        public string ASUNTO { get; set; }
        public string PARA { get; set; }
        public int IDCRTLNUM { get; set; }
        //public int ESTADO { get; set; }
    }
}
