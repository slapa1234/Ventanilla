using SisATU.Base;
using SisATU.Base.Enumeradores;
using SisATU.Base.ViewModel;
using SisATU.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SisATU.Servicios;

namespace SisATU.Negocio
{
    public class PersonaBLL
    {
        private PersonaDAL PersonaDAL;
        private Object bdConn;

        public PersonaBLL()
        {
            PersonaDAL = new PersonaDAL(ref bdConn);
        }

        public PersonaVM consultaPersonaSITU(string nroDocumento)
        {
            //consultarPersonaEnSITUS
            var persona = new PersonaDAL(ref bdConn).consultarPersonaEnSITUS(nroDocumento);
            if (persona.NRO_DOCUMENTO != null)
            {
                string texto = nroDocumento.ToString();
                int[] digitos = new int[texto.Length];
                for (int i = 0; i < texto.Length; i++)
                {
                    digitos[i] = int.Parse(texto.Substring(i, 1));
                }
                var digiti1 = digitos[0] * 3;
                var digiti2 = digitos[1] * 2;
                var digiti3 = digitos[2] * 7;
                var digiti4 = digitos[3] * 6;
                var digiti5 = digitos[4] * 5;
                var digiti6 = digitos[5] * 4;
                var digiti7 = digitos[6] * 3;
                var digiti8 = digitos[7] * 2;

                var sumar = digiti1 + digiti2 + digiti3 + digiti4 + digiti5 + digiti6 + digiti7 + digiti8;
                var div = Math.Truncate(Convert.ToDecimal(sumar / 11));
                var multi = div * 11;
                var resta = sumar - multi;
                var resta2 = 11 - resta;
                var sumar2 = Convert.ToInt32(resta2 + 1);

                switch (sumar2)
                {
                    case 1:
                        persona.ULTIMO_DIGITO = 6;

                        break;
                    case 2:
                        persona.ULTIMO_DIGITO = 7;

                        break;
                    case 3:
                        persona.ULTIMO_DIGITO = 8;

                        break;
                    case 4:
                        persona.ULTIMO_DIGITO = 9;

                        break;
                    case 5:
                        persona.ULTIMO_DIGITO = 0;

                        break;
                    case 6:
                        persona.ULTIMO_DIGITO = 1;

                        break;
                    case 7:
                        persona.ULTIMO_DIGITO = 1;

                        break;
                    case 8:
                        persona.ULTIMO_DIGITO = 2;
                        break;
                    case 9:
                        persona.ULTIMO_DIGITO = 3;
                        break;
                    case 10:
                        persona.ULTIMO_DIGITO = 4;
                        break;
                    case 11:
                        persona.ULTIMO_DIGITO = 5;
                        break;
                    default:
                        persona.ULTIMO_DIGITO = 0;
                        break;
                }
            }
            return persona;
        }
        public PersonaVM consultaDatosExt(string nroDocumento, string tipoDocumento)
        {
            var persona = new MigracionesService().ConsultaDatosPersonaExt(nroDocumento, tipoDocumento);
            return persona;
        }
        public PersonaVM consultaDatosReniec(string nroDocumento)
        {
         
            var persona = new ReniecService().ConsultaDNI2(nroDocumento);
            
            return persona;
        }



        public PersonaVM ConsultaPersonaSTD(PersonaVM persona)
        {
            PersonaVM personaResultado = new PersonaVM();
            PersonaVM creaPersonaSTD = new PersonaVM();

            var buscaPersona = new STDDAL().BuscarPersonaSTD(persona.NRO_DOCUMENTO);

            if(buscaPersona.ID_PERSONA == 0)// si no hay persona en STD lo crea
            {
                creaPersonaSTD = new STDDAL().CrearPersonaSTD(new PersonaModelo()
                {
                    APELLIDO_PATERNO = persona.APELLIDO_PATERNO,
                    APELLIDO_MATERNO = persona.APELLIDO_MATERNO,
                    NOMBRES = persona.NOMBRES,
                    NRO_DOCUMENTO = persona.NRO_DOCUMENTO,
                    DIRECCION = persona.DIRECCION,
                });

                buscaPersona = new STDDAL().BuscarPersonaSTD(persona.NRO_DOCUMENTO);

                if(buscaPersona.ID_PERSONA > 0)
                {
                    personaResultado.ID_PERSONA = buscaPersona.ID_PERSONA;
                    personaResultado.CODPAIS = buscaPersona.CODPAIS;
                    personaResultado.CODDPTO = buscaPersona.CODDPTO;
                    personaResultado.CODPROV = buscaPersona.CODPROV;
                    personaResultado.CODDIST = buscaPersona.CODDIST;
                }
            }
            else
            {
                personaResultado.ID_PERSONA = buscaPersona.ID_PERSONA;
                personaResultado.CODPAIS = buscaPersona.CODPAIS;
                personaResultado.CODDPTO = buscaPersona.CODDPTO;
                personaResultado.CODPROV = buscaPersona.CODPROV;
                personaResultado.CODDIST = buscaPersona.CODDIST;
            }
           
            return personaResultado;
        }

        public PersonaVM ConsultaDNI(string NRO_DOCUMENTO)
        {
            PersonaVM persona = new PersonaVM();
            PersonaVM personaReniec = new PersonaVM();
            PersonaVM buscaPersona = new PersonaVM();
            PersonaVM personaSTD = new PersonaVM();

            personaReniec = PersonaDAL.ConsultarPersona(EnumParametro.DNI.ValorEntero(), NRO_DOCUMENTO);

            if (personaReniec.ResultadoProcedimientoVM.CodResultado == 1)
            {
                persona.NOMBRES = personaReniec.NOMBRES;
                persona.APELLIDO_PATERNO = personaReniec.APELLIDO_PATERNO;
                persona.APELLIDO_MATERNO = personaReniec.APELLIDO_MATERNO;
                persona.FOTO = personaReniec.FOTO;
                persona.DIRECCION = personaReniec.DIRECCION;
                persona.TELEFONO = personaReniec.TELEFONO;
                persona.CORREO = personaReniec.CORREO;
                persona.ID_DEPARTAMENTO = personaReniec.ID_DEPARTAMENTO;
                persona.ID_PROVINCIA = personaReniec.ID_PROVINCIA;
                persona.ID_DISTRITO = personaReniec.ID_DISTRITO;
                persona.DIRECCION_ACTUAL = personaReniec.DIRECCION_ACTUAL;
            }
            else
            {
                personaReniec = PersonaDAL.ConsultaDNI(NRO_DOCUMENTO);
                if (personaReniec.ResultadoProcedimientoVM.CodResultado == 1)
                {
                    persona.NOMBRES = personaReniec.NOMBRES;
                    persona.APELLIDO_PATERNO = personaReniec.APELLIDO_PATERNO;
                    persona.APELLIDO_MATERNO = personaReniec.APELLIDO_MATERNO;
                    persona.FOTO = personaReniec.FOTO;
                    persona.DIRECCION = personaReniec.DIRECCION;
                    persona.ULTIMO_DIGITO = personaReniec.ULTIMO_DIGITO;
                    persona.ResultadoProcedimientoVM.CodResultado = personaReniec.ResultadoProcedimientoVM.CodResultado;
                    persona.ResultadoProcedimientoVM.NomResultado = personaReniec.ResultadoProcedimientoVM.NomResultado;
                }
                else
                {
                    persona.ResultadoProcedimientoVM.CodResultado = personaReniec.ResultadoProcedimientoVM.CodResultado;
                    persona.ResultadoProcedimientoVM.NomResultado = personaReniec.ResultadoProcedimientoVM.NomResultado;
                    return persona;
                }
                
            } 
            

            buscaPersona = new STDDAL().BuscarPersonaSTD(NRO_DOCUMENTO);
            persona.ID_PERSONA = buscaPersona.ID_PERSONA;
            persona.CODPAIS = buscaPersona.CODPAIS;
            persona.CODDPTO = buscaPersona.CODDPTO;
            persona.CODPROV = buscaPersona.CODPROV;
            persona.CODDIST = buscaPersona.CODDIST;

            if (buscaPersona.ID_PERSONA == 0)
            {
                if (personaReniec.NOMBRES != null)
                {
                    try
                    {
                        personaSTD = new STDDAL().CrearPersonaSTD(new PersonaModelo()
                        {
                            APELLIDO_PATERNO = personaReniec.APELLIDO_PATERNO,
                            APELLIDO_MATERNO = personaReniec.APELLIDO_MATERNO,
                            NOMBRES = personaReniec.NOMBRES,
                            NRO_DOCUMENTO = NRO_DOCUMENTO,
                            DIRECCION = personaReniec.DIRECCION,
                        });
                        buscaPersona = new STDDAL().BuscarPersonaSTD(NRO_DOCUMENTO);
                        persona.ID_PERSONA = buscaPersona.ID_PERSONA;
                        persona.CODPAIS = buscaPersona.CODPAIS;
                        persona.CODDPTO = buscaPersona.CODDPTO;
                        persona.CODPROV = buscaPersona.CODPROV;
                        persona.CODDIST = buscaPersona.CODDIST;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            return persona;
        }

         public PersonaVM ConsultarCE(string NRO_DOCUMENTO)
        {
            PersonaVM personaReniec = new PersonaVM();
 
            personaReniec = PersonaDAL.ConsultarPersona(EnumParametro.CE.ValorEntero(), NRO_DOCUMENTO);

            if (personaReniec.ResultadoProcedimientoVM.CodResultado != 1)
            {
                personaReniec = PersonaDAL.ConsultarCE(NRO_DOCUMENTO);
            }

            return personaReniec;
        }

        public PersonaVM ConsultarPTP(string NRO_DOCUMENTO)
        {

            PersonaVM personaReniec = new PersonaVM();
            personaReniec = PersonaDAL.ConsultarPersona(EnumParametro.PTP.ValorEntero(), NRO_DOCUMENTO);

            if (personaReniec.ResultadoProcedimientoVM.CodResultado != 1)
            {
                personaReniec = PersonaDAL.ConsultarPTP(NRO_DOCUMENTO);
            }
            return personaReniec;
        }

        public PersonaVM ConsultarPersona(int ID_TIPO_DOCUMENTO, string NRO_DOCUMENTO)
        {
            return PersonaDAL.ConsultarPersona(ID_TIPO_DOCUMENTO, NRO_DOCUMENTO);
        }

        public ResultadoProcedimientoVM CrearPersona(PersonaModelo persona)
        {
            return PersonaDAL.CrearPersona(persona);
        }

        public ResultadoProcedimientoVM RecuperarContrasena(string nroDocumento, string correo, string contrasenia)
        {
            return PersonaDAL.RecuperarClave(nroDocumento, correo, contrasenia);
        }
    }
}
