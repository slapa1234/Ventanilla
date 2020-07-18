using SisATU.Base;
using SisATU.Base.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Servicios
{
    public class MtcService
    {
        /// <summary>
        /// Persomiso Temporal de Permanencia
        /// </summary>
        /// <param name="DNI"></param>
        /// <returns></returns>
        public PersonaVM ConsultaPTP(string DNI)
        {
            PersonaVM persona = new PersonaVM();
            try
            {
                var TIPDOC = "14";//Tipo Documento que solicita la web service
                ServiceATU.Servicio_ATU servicioMTC = new ServiceATU.Servicio_ATU();

                var resultado = servicioMTC.ConsultaPTP(new ServiceATU.Usuario() { USULOG = "sissit", USUCON = "p4_tu_l1br0" }, DNI, TIPDOC);
                persona.APELLIDO_PATERNO = resultado.APE_PATERNO;
                persona.APELLIDO_MATERNO = resultado.APE_MATERNO;
                persona.NOMBRES = resultado.NOMBRE;

                Servicio_STD.Servicio_STD servicioSTD = new Servicio_STD.Servicio_STD();
                var buspersona = servicioSTD.BuscarPersona(new Servicio_STD.Usuario() { USULOG = "PTseguro", USUCON = "PTs3gur0" }, new Servicio_STD.Persona() { DNI = DNI });
                persona.ID_PERSONA = buspersona.IDPERSON.ValorEntero();
                persona.CODPAIS = buspersona.CODPAIS.ValorEntero();
                persona.CODDPTO = buspersona.CODDPTO.ValorEntero();
                persona.CODPROV = buspersona.CODPROV.ValorEntero();
                persona.CODDIST = buspersona.CODDIST.ValorEntero();
                persona.DIRECCION_STD = buspersona.DIRECCION;

                if (persona.ID_PERSONA == 0)
                {
                    if (persona.NOMBRES != null)
                    {
                        if (persona.APELLIDO_PATERNO == null)
                            persona.APELLIDO_PATERNO = ".";
                        if (persona.APELLIDO_MATERNO == null)
                            persona.APELLIDO_MATERNO = ".";
                        try
                        {
                            var agregapersona = servicioSTD.AgregarPersona(new Servicio_STD.Usuario() { USULOG = "PTseguro", USUCON = "PTs3gur0" }, new Servicio_STD.Persona()
                            {
                                APELLIDO_PATERNO = persona.APELLIDO_PATERNO,
                                APELLIDO_MATERNO = persona.APELLIDO_MATERNO,
                                NOMBRES = persona.NOMBRES,
                                SEXO = "",
                                DNI = DNI,
                                CODPAIS = 173,
                                CODDPTO = 15,
                                CODPROV = 1,
                                CODDIST = 1,
                                DIRECCION = "S/N"
                            });

                            var busPersona = servicioSTD.BuscarPersona(new Servicio_STD.Usuario() { USULOG = "PTseguro", USUCON = "PTs3gur0" }, new Servicio_STD.Persona() { DNI = DNI });
                            persona.ID_PERSONA = busPersona.IDPERSON.ValorEntero();
                            persona.CODPAIS = busPersona.CODPAIS.ValorEntero();
                            persona.CODDPTO = busPersona.CODDPTO.ValorEntero();
                            persona.CODPROV = busPersona.CODPROV.ValorEntero();
                            persona.CODDIST = busPersona.CODDIST.ValorEntero();
                            persona.DIRECCION_STD = busPersona.DIRECCION;
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }
                persona.NRO_DOCUMENTO = DNI;
                persona.ResultadoProcedimientoVM.CodResultado = 1;
                persona.ResultadoProcedimientoVM.NomResultado = "Cargó Correctamente";
            }

            catch (Exception ex)
            {
                persona.ResultadoProcedimientoVM.CodResultado = 0;
                persona.ResultadoProcedimientoVM.NomResultado = "En estos momentos se presenta problemas de conexión por parte de la PCM, por favor vuelva a intentar en unos minutos.";
            }
            return persona;
        }
    }
}
