using SisATU.Base;
using SisATU.Base.ViewModel;
using SisATU.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Datos
{
    public class STDDAL
    {
        public STDVM CrearExpedienteSTD(STDVM std)
        {
            STDService obj = new STDService();
            return obj.CrearExpedienteSTD(std);
        }
        public STDVM CerrarExpedienteSTD(STDVM std)
        {
            STDService obj = new STDService();
            return obj.CerrarExpedienteSTD(std);
        }

        public STDVM AcumuladorSTD(STDVM std)
        {
            STDService obj = new STDService();
            return obj.AcumuladorSTD(std);
        }

        public STDVM GenerarResolucionSTD(STDVM std)
        {
            STDService obj = new STDService();
            return obj.GenerarResolucionSTD(std);
        }

        public PersonaVM BuscarPersonaSTD(string DNI)
        {
            STDService obj = new STDService();
            return obj.BuscarPersonaSTD(DNI);
        }

        public PersonaVM CrearPersonaSTD(PersonaModelo persona)
        {
            STDService obj = new STDService();
            return obj.CrearPersonaSTD(persona);
        }
       
    }
}
