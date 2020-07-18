using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Base
{
    public class STDModelo
    {
        public string NUMERO_DOC { get; set; }
        public int IDCLASE { get; set; }
        public int FOLIOS_INI { get; set; }
        public int FOLIOS_FIN { get; set; }
        public int TIPOEXPEDIENTE { get; set; }
        public int IDUNIDAD { get; set; }
        public int IDUSER_CREA { get; set; }
        public int CODPAIS { get; set; }
        public int CODDPTO { get; set; }
        public int CODPROV { get; set; }
        public int CODDIST { get; set; }
        public string DIRECCION { get; set; }
        public int ESTADO { get; set; }
        public string INSTITUCION_NO_REG { get; set; }
    }
}
