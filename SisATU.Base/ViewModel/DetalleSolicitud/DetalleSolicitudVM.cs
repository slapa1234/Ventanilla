using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Base.ViewModel
{
    public class DetalleSolicitudVM
    {
        public int ID_DET_SOLICITUD { get; set; }
        public int ID_EXPEDIENTE { get; set; }
        public string DATO_REGISTRO { get; set; }
        public int ID_ENTIDAD_BANCARIA { get; set; }
        public string NUMERO_RECIBO { get; set; }
        public string DESCRIPCION { get; set; }
        public int ID_ESTADO { get; set; }
        public string USU_REG { get; set; }
        public string FECHA_REG { get; set; }
        public string USU_MODIF { get; set; }
        public string FECHA_MODIF { get; set; }
        public string USU_ANULA { get; set; }
        public string FECHA_ANULA { get; set; }
        public decimal PorcentajeDocumento { get; set; }
    }
}
