using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Base.Dominio
{
    public class DistritoModelo
    {
        public int ID_DISTRITO { get; set; }
        public string NOMBRE_DISTRITO { get; set; }
        public int ID_USUARIO_MODIFICA { get; set; }
        public string FECHA_MODIFICA { get; set; }
        public string SITUACION_REGISTRO { get; set; }
        public int ID_PROVINCIA { get; set; }
        public string DISUBI { get; set; }
    }
}
