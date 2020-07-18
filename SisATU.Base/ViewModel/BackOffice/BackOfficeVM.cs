using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SisATU.Base.ViewModel
{
   public class BackOfficeVM
    {
        public BackOfficeVM()
        {

        }

        public String NUMDOC { get; set; }
        public Int32 TIPDOC { get; set; }
        public String ESTATEN { get; set; }
        public  String EXP { get; set; }
        public String FECREGI { get; set; }
        public String PRO_TUCS { get; set; }
        public Int32 TIP_PER { get; set; }
        public Int32 TIP_MOLS { get; set; }
        public String FECATEN { get; set; }

        public Int32 IDEXPED { get; set; }
        public Int32  IDDOC { get; set; }
        public Int32 TIPPERS { get; set; }
       
        public Int32 TIP_SOLI { get; set; }
        public Int32 TIP_PROCE { get; set; }
        public String NUMERO_SID { get; set; }
        public String FECHAREG { get; set; }
        public String ESTADO_SOL { get; set; }
        public String TRAMITE { get; set; }
        public String TIP_MODAL { get; set; }
        public String ID_MODA { get; set; }


        public String PERSONA { get; set; }
        public String PARNOM { get; set; }
        public String MODALIDAD_SERVICIO { get; set; }
        public String NOMBRE_PROCEDIMIENTO { get; set; }

        public Int32 ID_EXPEDIENTE { get; set; }

   

        

    }
}
