using SisATU.Base;
using SisATU.Base.ViewModel;
using SisATU.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Negocio
{
    public class STDBLL
    {
        public STDVM CrearSTD(STDVM std)
        {
            STDDAL obj = new STDDAL();  
            return obj.CrearExpedienteSTD(std);
        }
    }
}
