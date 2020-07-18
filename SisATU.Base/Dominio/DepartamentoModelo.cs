using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Base.Dominio
{
    public class DepartamentoModelo
    {
        public int ID_DEPARTAMENTO { get; set; }
        public string NOMBRE_DEPARTAMENTO { get; set; }
        public int ID_USUARIO_MODIFICA { get; set; }
        public string FECHA_MODIFICA { get; set; }
        public string SITUACION_REGISTRO { get; set; }
        public int ID_PAIS { get; set; }
    }
}
