using SisATU.Base.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Servicios
{
    public class CitvService
    {
        public VehiculoCITVVM ConsultaCITV(string nroPlaca)
        {
            VehiculoCITVVM revicionTecnica = new VehiculoCITVVM();
            try
            {
                ServiceATU.Servicio_ATU servicioCITV = new ServiceATU.Servicio_ATU();

                var CITV = servicioCITV.ConsultaCITV(new ServiceATU.Usuario() { USULOG = "sissit", USUCON = "p4_tu_l1br0" }, nroPlaca);

                if (CITV.result != null)
                {
                    revicionTecnica.CERTIFICADORA_CITV = CITV.result.Empresa_certificadora;
                    revicionTecnica.FECHA_CERTIFICADO = CITV.result.Vigente_desde;
                    revicionTecnica.FECHA_VENCIMIENTO = CITV.result.Vigente_Hasta;
                    revicionTecnica.NRO_CERTIFICADO = CITV.result.Nro_certificado;
                }
            }
            catch (Exception)
            {

            }
            return revicionTecnica;
        }
    }
}
