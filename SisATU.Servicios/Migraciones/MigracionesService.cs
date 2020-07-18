using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SisATU.Base;
using SisATU.Base.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Servicios
{
    public class MigracionesService
    {
        /// <summary>
        /// Carnet de Estrangería
        /// </summary>
        /// <param name="DNI"></param>
        /// <returns></returns>
        /// 
        public PersonaVM ConsultaDatosPersonaExt(string nroDocumento, string tipoDocumento)
        {
            ServiceATU.Servicio_ATU servicioEXTR = new ServiceATU.Servicio_ATU();
            var personaEXTR = servicioEXTR.ConsultaPTP(new ServiceATU.Usuario() { USULOG = "sissit", USUCON = "p4_tu_l1br0" }, nroDocumento, tipoDocumento);
            PersonaVM p = new PersonaVM();
            p.APELLIDO_PATERNO = personaEXTR.APE_PATERNO;
            p.APELLIDO_MATERNO = personaEXTR.APE_MATERNO;
            p.NOMBRES = personaEXTR.NOMBRE;

            return p;
        }
        public PersonaVM ConsultaCE(string DNI)
        {
            PersonaVM persona = new PersonaVM();
            try
            {
                ServiceATU.Servicio_ATU servicioMigraciones = new ServiceATU.Servicio_ATU();

                var resultadoMigraciones = servicioMigraciones.ConsulMigra(new ServiceATU.Usuario() { USULOG = "sissit", USUCON = "p4_tu_l1br0" }, DNI);
                persona.APELLIDO_PATERNO = resultadoMigraciones.strPrimerApellido;
                persona.APELLIDO_MATERNO = resultadoMigraciones.strSegundoApellido;
                persona.NOMBRES = resultadoMigraciones.strNombres;

                //SISTEMAS ATU
                Servicio_STD.Servicio_STD servicioSTD = new Servicio_STD.Servicio_STD();
                var buscarPersona = servicioSTD.BuscarPersona(new Servicio_STD.Usuario() { USULOG = "PTseguro", USUCON = "PTs3gur0" }, new Servicio_STD.Persona() { DNI = DNI });
                persona.ID_PERSONA = buscarPersona.IDPERSON.ValorEntero();
                persona.CODPAIS = buscarPersona.CODPAIS.ValorEntero();
                persona.CODDPTO = buscarPersona.CODDPTO.ValorEntero();
                persona.CODPROV = buscarPersona.CODPROV.ValorEntero();
                persona.CODDIST = buscarPersona.CODDIST.ValorEntero();
                persona.DIRECCION_STD = buscarPersona.DIRECCION;

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


        public PersonaVM ConsultaCE2(string NRODOCUMENTO)
        {
            var TARGETURL = "https://api.aate.gob.pe/springpide/migraciones/" + NRODOCUMENTO;
            PersonaVM persona = new PersonaVM();
            try
            {

                HttpClient client = new HttpClient();
                var byteArray = Encoding.ASCII.GetBytes("PIDE:sisacse2019Aate");
                client.Timeout = TimeSpan.FromSeconds(5);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                //https://stackoverflow.com/questions/22628087/calling-async-method-synchronously/22629216
                HttpResponseMessage response = client.GetAsync(TARGETURL).Result;
                HttpContent content = response.Content;
                string jsonResult = content.ReadAsStringAsync().Result;
                var resultado = JsonConvert.DeserializeObject<PersonaVM>(jsonResult);

                persona.NOMBRES = resultado.nombres;
                persona.APELLIDO_PATERNO = resultado.primerApellido;
                persona.APELLIDO_MATERNO = resultado.segundoApellido;

                persona.ResultadoProcedimientoVM.CodResultado = 1;
                persona.ResultadoProcedimientoVM.NomResultado = "Cargó Correctamente";
                //return persona;
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
