using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Base
{
    public class ReciboModelo
    {
       
        public int ID_RECIBO { get; set; }
        public int ID_ENTIDAD_BANCARIA { get; set; }
        public string NUMERO_RECIBO { get; set; }
        public DateTime FECHA_EMISION { get; set; }
        public int ID_ESTADO { get; set; }
        public int ID_EXPEDIENTE { get; set; }
        public int ID_PROCEDIMIENTO { get; set; }
        public string USUARIO_REG { get; set; }
        public string FECHA_REG { get; set; }
        public string USUARIO_MOD { get; set; }
        public string FECHA_MOD { get; set; }
        public string USUARIO_ELIM { get; set; }
        public string FECHA_ELIM { get; set; }
    }
}
