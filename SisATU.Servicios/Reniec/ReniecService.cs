using Newtonsoft.Json;
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
    public class ReniecService
    {
        /// <summary>
        /// Documento Nacional de Identidad
        /// </summary>
        /// <param name="DNI"></param>
        /// <returns></returns>
        public PersonaVM ConsultaDNI(string DNI)
        {
            PersonaVM persona = new PersonaVM();
            try
            {
                //Consulta RENIEC
                ServiceATU.Servicio_ATU servicioReniec = new ServiceATU.Servicio_ATU();
                var resultadoReniec = servicioReniec.ConsultaDNI(new ServiceATU.Usuario() { USULOG = "sissit", USUCON = "p4_tu_l1br0" }, DNI);
                persona.NOMBRES = resultadoReniec.prenombres;
                persona.APELLIDO_PATERNO = resultadoReniec.apPrimer;
                persona.APELLIDO_MATERNO = resultadoReniec.apSegundo;
                persona.FOTO = resultadoReniec.foto;
                persona.DIRECCION = resultadoReniec.direccion;


                /* FUNCION ULTIMO DIGITO */

                string texto = DNI.ToString();
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

                persona.ResultadoProcedimientoVM.CodResultado = 1;
                persona.ResultadoProcedimientoVM.NomResultado = "Cargó Correctamente";
            }
            catch (Exception ex)
            {
                persona.ResultadoProcedimientoVM.CodResultado = 0;
                persona.ResultadoProcedimientoVM.NomResultado = "En estos momentos se presenta problemas de conexion por parte de la PCM, por favor vuelva a intentar en unos minutos.";
                //throw ex;
            }

            return persona;
        }

        public PersonaVM ConsultaDNI2(string DNI)
        {
           
            //var TARGETURL = "https://api.aate.gob.pe/springpide/reniec/" + DNI;
            var TARGETURL = "https://api.aate.gob.pe/api/reniec/" + DNI;
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
                persona.APELLIDO_PATERNO = resultado.apellidoPaterno;
                persona.APELLIDO_MATERNO = resultado.apellidoMaterno;
                persona.FOTO = resultado.foto;
                persona.DIRECCION = resultado.direccion;

                string texto = DNI.ToString();
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
                var resta2 = 0;
                if (resta == 0)
                {
                    resta2 = 0;
                }
                else
                {
                    resta2 = (11 - resta).ValorEntero();
                }
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
                persona.ResultadoProcedimientoVM.CodResultado = 1;
                persona.ResultadoProcedimientoVM.NomResultado = "Cargó Correctamente";
            }
            catch (Exception ex)
            {
                persona.ResultadoProcedimientoVM.CodResultado = 0;
                persona.ResultadoProcedimientoVM.NomResultado = "En estos momentos se presenta problemas de conexion por parte de la PCM, por favor vuelva a intentar en unos minutos.";
            }
            return persona;
        }

    }
}
