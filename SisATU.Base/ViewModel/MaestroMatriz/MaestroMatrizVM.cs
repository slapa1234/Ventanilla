using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Base.ViewModel
{
    public class MaestroMatrizVM
    {
        public int ID_TABLA_VIGENCIA { get; set; }
        public int ID_MODALIDAD_SERVICIO { get; set; }
        public int ANIO_FABRICACION_INI { get; set; }
        public int ANIO_FABRICACION_FIN { get; set; }
        public string FECHA_SALIDA { get; set; }
        public int PERIODO { get; set; }
        public int ID_TIPO_PERSONA { get; set; }
        public int ID_ESTADO { get; set; }
        public string USUARIO_REG { get; set; }
        public string FECHA_REG { get; set; }
        public string USUARIO_MOD { get; set; }
        public string FECHA_MOD { get; set; }
        public string USUARIO_ELIM { get; set; }
        public string FECHA_ELIM { get; set; }
        public int ANIOS { get; set; }
    }
}
