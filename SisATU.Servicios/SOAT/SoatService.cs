using SisATU.Base.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Servicios
{
    public class SoatService
    {
        public AseguradoraVM ConsultaSOAT(string nroPlaca)
        {
            AseguradoraVM seguradora = new AseguradoraVM();
            try
            {
                ServiceATU.Servicio_ATU servicioSoat = new ServiceATU.Servicio_ATU();

                var SOAT = servicioSoat.ConsultaSOAT(new ServiceATU.Usuario() { USULOG = "sissit", USUCON = "p4_tu_l1br0" }, nroPlaca);

                if (SOAT.nroplaca != null)
                {
                    seguradora.NOMBRE = SOAT.compania;
                    seguradora.FEC_INI_VIGENCIA = SOAT.inivigen;
                    seguradora.FEC_FIN_VIGENCIA = SOAT.finvigen;
                    seguradora.POLIZA = SOAT.polcerti;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return seguradora;
        }
    }
}
