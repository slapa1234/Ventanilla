using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Base.ViewModel
{
    public class ComboProcedimientoVM
    {
        public int ID_PROCEDIMIENTO { get; set; }
        public string NOMBRE_PROCEDIMIENTO { get; set; }
        public string MONTO { get; set; }
        public string DOCUMENTACION_EVALUACION { get; set; }
        public int PLATAFORMA { get; set; }
    }
}
