using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Base
{
    public class ResultadoProcedimientoVM
    {
        public int CodResultado { get; set; }
        public string NomResultado { get; set; }
        public int CodAuxiliar { get; set; }
        public int IDDOC_PADRE { get; set; }
        public int IDDOC_HIJO { get; set; }
        public int COD_OPERADOR { get; set; }
        public string CLAVE_NUEVO { get; set; }
    }
}
