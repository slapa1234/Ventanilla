using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Base.ViewModel
{
    public class TramiteSimpleVM
    {
        public int TIPO_PERSONA { get; set; }
        public int IDTRAMITE { get; set; }
        public int IDMODALIDAD { get; set; }
        public int IDPROCEDIMIENTO { get; set; }
        public int IDTIPODOCUMENTO { get; set; }
        public string NRODOCUMENTO { get; set; }
        public string NOMBRES { get; set; }
        public string APEPAT { get; set; }
        public string APEMAT { get; set; }
        public string NRORECIBOPAGO { get; set; }
        public string CORREOELECTRONICO { get; set; }
        public string FECHACREACION { get; set; }
        public string RUC { get; set; }

        public int ID_TIPO_PERSONA { get; set; }
        public string ID_SSI_EXP { get; set; }
        public int AUTORIZA_EMAIL { get; set; }
        public string NRO_TELEF { get; set; }
        public string DIRECCION { get; set; }
        public string PLACA { get; set; }

        public int IDBANCO { get; set; }
        public string FECHA_PAGO { get; set; }
    }
}
