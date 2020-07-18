using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Base.ViewModel
{
    public class TramiteSATVM
    {
        public int ID_TRAMITE_SAT { get; set; }
        public int ID_TRAMITE { get; set; }
        public string NRO_ACTA_CONTROL { get; set; }
        public string FECHA_ACTA { get; set; }
        public int DIAS_HABILES_ACTA { get; set; }
        public string ID_MULTA { get; set; }
        public string PLACA { get; set; }
        public string NRO_RESOL_SANC { get; set; }
        public double MONTO_IN_RESOL { get; set; }
        public string FECHA_RESOLUCION { get; set; }
        public int FLAG_PRESENTO_RECURSO { get; set; }
        public int ID_BANCO { get; set; }
        public string FECHA_PAGO { get; set; }
        public string NRO_RECIBO { get; set; }
        public double MONTO_CANCELADO { get; set; }
        public string NOM_ARCHIVO_VOUCH { get; set; }
        public int ID_ESTADO { get; set; }
        public string FECHA_REGISTRO { get; set; }
        public string FECHA_HORA_REG { get; set; }
        public int NRO_CUOTAS { get; set; }
    }
}
