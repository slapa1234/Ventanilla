using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SisATU.Base;
using SisATU.Base.ViewModel;
using SisATU.Base.ViewModel.Operador;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Servicios
{
    public class OperadorService
    {
        /// <summary>
        /// Obtener datos personales y Licencia de conducir
        /// </summary>
        /// <param name="TIPO DOCUMENTO"></param>
        /// <param name="NRO DOCUMENTO"></param>
        /// <returns></returns>
        /// 
        public PersonaVM consultaDatosReniec(string nroDocumento)
        {
            var persona = new ReniecService().ConsultaDNI2(nroDocumento);

            return persona;
        }
        public OperadorVM consultaDatosPersonalesYLic(string tipoDocumento, string nroDocumento)
        {
            OperadorVM operador = new OperadorVM();
            var TARGETURL = "https://api.aate.gob.pe/rest/consultaUltimaLicencia/" + tipoDocumento + "/" + nroDocumento;

            if (tipoDocumento == "2")
            {
                var persona = new ReniecService().ConsultaDNI2(nroDocumento);
                operador.APELLIDO_PATERNO = persona.APELLIDO_PATERNO;
                operador.APELLIDO_MATERNO = persona.APELLIDO_MATERNO;
                operador.NOMBRES = persona.NOMBRES;
                operador.FOTO_BASE64 = persona.FOTO;
                operador.DIRECCION = persona.DIRECCION;

                try
                {
                    HttpClient client = new HttpClient();
                    var byteArray = Encoding.ASCII.GetBytes("PIDE:sisacse2019Aate");
                    client.Timeout = TimeSpan.FromSeconds(5);
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                    HttpResponseMessage response = client.GetAsync(TARGETURL).Result;
                    HttpContent content = response.Content;

                    string jsonResult = content.ReadAsStringAsync().Result;
                    var resultado = JsonConvert.DeserializeObject<ResultadoMTC>(jsonResult);

                    var Licencia = resultado.GetDatosUltimaLicenciaMTCResponse.GetDatosUltimaLicenciaMTCResult.diffgram.NewDataSet.Table;
                    var papeleta = ConsultaPapeleta(tipoDocumento, nroDocumento);

                    operador.NRO_LICENCIA = Licencia.NUM_LICENCIA;
                    operador.CATEGORIA = Licencia.CATEGORIA;
                    operador.FECHA_EXPEDICION = Licencia.FECEXP;
                    operador.FECHA_REVALIDACION = Licencia.FECREV;
                    operador.ESTADO_LICENCIA = Licencia.ESTADO;
                    operador.RESTRICCION = Licencia.RESTRICCION;
                    operador.PUNTOS_FIRME = papeleta.PUNTOS_FIRME;
                    operador.GRAVE = papeleta.GRAVE;
                    operador.MUY_GRAVE = papeleta.MUY_GRAVE;
                }
                catch (Exception ex)
                {
                    //throw;
                }
            }
            else
            {
                ServiceATU.Servicio_ATU servicioEXTR = new ServiceATU.Servicio_ATU();
                var personaEXTR = servicioEXTR.ConsultaPTP(new ServiceATU.Usuario() { USULOG = "sissit", USUCON = "p4_tu_l1br0" }, nroDocumento, tipoDocumento);
                var papeleta = ConsultaPapeleta(nroDocumento, tipoDocumento);

                operador.APELLIDO_PATERNO = personaEXTR.APE_PATERNO;
                operador.APELLIDO_MATERNO = personaEXTR.APE_MATERNO;
                operador.NOMBRES = personaEXTR.NOMBRE;
                operador.NRO_LICENCIA = personaEXTR.NUM_LICENCIA;
                operador.CATEGORIA = personaEXTR.CATEGORIA;

                operador.FECHA_EXPEDICION = personaEXTR.FECEXP;
                operador.FECHA_REVALIDACION = personaEXTR.FECREV;
                operador.RESTRICCION = personaEXTR.RESTRICCION;
            }
            return operador;
        }

        //public async Task<String> consultaDatosPersonalesYLic(string tipoDocumento, string nroDocumento)
        //{
        //    var TARGETURL = "https://api.aate.gob.pe/rest/consultaLicencia";
        //    //var TARGETURL = "https://api.aate.gob.pe/api/sunarp/getRegistroPlacaVehicular?zona=01&oficina=01&placa=AZR646";
        //    string result = "";
        //    try
        //    {
        //        HttpClient client = new HttpClient();
        //        var byteArray = Encoding.ASCII.GetBytes("PIDE:sisacse2019Aate");
        //        StringContent queryString = new StringContent("{\"tipoDocumento\": \" " + tipoDocumento + " \", \"nroDocumento\": \" " + nroDocumento + " \"}", Encoding.UTF8, "application/json");
        //        client.Timeout = TimeSpan.FromSeconds(5);

        //        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
        //        HttpResponseMessage response = await client.PostAsync(new Uri(TARGETURL), queryString);
        //        HttpContent content = response.Content;
        //        result = await content.ReadAsStringAsync();
        //        //var x = 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        //throw;
        //    }

        //    return result;
        //}




        //public OperadorVM consultaDatosPersonalesYLic(string tipoDocumento, string nroDocumento)
        //{
        //    OperadorVM operador = new OperadorVM();

        //    if (tipoDocumento == "2")
        //    {

        //        var persona = new ReniecService().ConsultaDNI2(nroDocumento);
        //        operador.APELLIDO_PATERNO = persona.APELLIDO_PATERNO;
        //        operador.APELLIDO_MATERNO = persona.APELLIDO_MATERNO;
        //        operador.NOMBRES = persona.NOMBRES;
        //        operador.FOTO_BASE64 = persona.FOTO;
        //        operador.DIRECCION = persona.DIRECCION;

        //        try
        //        {
        //            ServiceATU.Servicio_ATU servicio = new ServiceATU.Servicio_ATU();
        //            var licencia = servicio.ConsultaLicencia(new ServiceATU.Usuario() { USULOG = "sissit", USUCON = "p4_tu_l1br0" }, nroDocumento, tipoDocumento);
        //            var papeleta = ConsultaPapeleta(nroDocumento, tipoDocumento);

        //            operador.NRO_LICENCIA = licencia.NUM_LICENCIA;
        //            operador.CATEGORIA = licencia.CATEGORIA;
        //            operador.FECHA_EXPEDICION = licencia.FECEXP;
        //            operador.FECHA_REVALIDACION = licencia.FECREV;
        //            operador.RESTRICCION = licencia.RESTRICCION;
        //            operador.PUNTOS_FIRME = papeleta.PUNTOS_FIRME;
        //            operador.ESTADO_LICENCIA = licencia.ESTADO;
        //            operador.GRAVE = papeleta.GRAVE;
        //            operador.MUY_GRAVE = papeleta.MUY_GRAVE;
        //        }
        //        catch (Exception)
        //        {

        //        }
        //    }
        //    else
        //    {
        //        ServiceATU.Servicio_ATU servicioEXTR = new ServiceATU.Servicio_ATU();
        //        var personaEXTR = servicioEXTR.ConsultaPTP(new ServiceATU.Usuario() { USULOG = "sissit", USUCON = "p4_tu_l1br0" }, nroDocumento, tipoDocumento);
        //        var papeleta = ConsultaPapeleta(nroDocumento, tipoDocumento);

        //        operador.APELLIDO_PATERNO = personaEXTR.APE_PATERNO;
        //        operador.APELLIDO_MATERNO = personaEXTR.APE_MATERNO;
        //        operador.NOMBRES = personaEXTR.NOMBRE;
        //        operador.NRO_LICENCIA = personaEXTR.NUM_LICENCIA;
        //        operador.CATEGORIA = personaEXTR.CATEGORIA;

        //        operador.FECHA_EXPEDICION = personaEXTR.FECEXP;
        //        operador.FECHA_REVALIDACION = personaEXTR.FECREV;
        //        operador.RESTRICCION = personaEXTR.RESTRICCION;
        //    }

        //    return operador;
        //}

        //public OperadorVM ConsultaPapeleta(string nroDocumento, string tipoDocumento)
        //{
        //    OperadorVM operador = new OperadorVM();
        //    ServiceATU.Servicio_ATU servicio = new ServiceATU.Servicio_ATU();
        //    var papeleta = servicio.ConsultaPapeleta(new ServiceATU.Usuario() { USULOG = "sissit", USUCON = "p4_tu_l1br0" }, nroDocumento, tipoDocumento);

        //    int cantidadGrave = 0;
        //    int cantidadMuyGrave = 0;
        //    foreach (var item in papeleta)
        //    {
        //        var falta = item.FALTA.Substring(0, 1);

        //        if (falta == "G")
        //        {
        //            cantidadGrave++;
        //            operador.GRAVE = cantidadGrave;

        //        }
        //        else if (falta == "M")
        //        {
        //            cantidadMuyGrave++;
        //            operador.MUY_GRAVE = cantidadMuyGrave;
        //        }
        //        operador.PUNTOS_FIRME += item.PUNTOS_FIRME;
        //    }



        //    return operador;
        //}

        public OperadorVM ConsultaPapeleta(string tipoDocumento, string nroDocumento)
        {
            OperadorVM operador = new OperadorVM();
            var TARGETURL = "https://api.aate.gob.pe/rest/consultaPapeletas/" + tipoDocumento + "/" + nroDocumento;
            try
            {
                HttpClient client = new HttpClient();
                var byteArray = Encoding.ASCII.GetBytes("PIDE:sisacse2019Aate");
                client.Timeout = TimeSpan.FromSeconds(5);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                HttpResponseMessage response = client.GetAsync(TARGETURL).Result;
                HttpContent content = response.Content;

                string jsonResult = content.ReadAsStringAsync().Result;
                JObject jsonconvetido = JsonConvert.DeserializeObject<JObject>(jsonResult);

                var jsonString = jsonconvetido;
                var ExisteData = jsonString["GetDatosPapeletasMTCResponse"]["GetDatosPapeletasMTCResult"]["diffgram"]["NewDataSet"];

                if (ExisteData != null)
                {
                    var jsonResponse = jsonconvetido["GetDatosPapeletasMTCResponse"]["GetDatosPapeletasMTCResult"]["diffgram"]["NewDataSet"]["Table"];

                    int cantidadGrave = 0;
                    int cantidadMuyGrave = 0;

                    foreach (JToken x in jsonResponse)
                    {
                        PapeletaVM obj = new PapeletaVM();
                        if (x["TIPOPIT"].ToString() == "G")
                        {
                            cantidadGrave++;
                            operador.GRAVE = cantidadGrave;
                        }
                        else if (x["TIPOPIT"].ToString() == "M")
                        {
                            cantidadMuyGrave++;
                            operador.MUY_GRAVE = cantidadMuyGrave;
                        }
                        operador.PUNTOS_FIRME += x["PUNTOS_x0020_FIRMES"].ValorEntero();
                    }
                }

                //var sampledataJson = JObject.Parse(jsonconvetido["GetDatosPapeletasMTCResponse"]["GetDatosPapeletasMTCResult"]["diffgram"].co);

                //if (((JObject)jsonconvetido["GetDatosPapeletasMTCResponse"]["GetDatosPapeletasMTCResult"]["diffgram"]).Count > 0)
                //{
                
                //}

            }
            catch (Exception ex)
            {
                throw;
            }
            //OperadorVM operador = new OperadorVM();
            //ServiceATU.Servicio_ATU servicio = new ServiceATU.Servicio_ATU();
            //var papeleta = servicio.ConsultaPapeleta(new ServiceATU.Usuario() { USULOG = "sissit", USUCON = "p4_tu_l1br0" }, nroDocumento, tipoDocumento);

            //int cantidadGrave = 0;
            //int cantidadMuyGrave = 0;
            //foreach (var item in papeleta)
            //{
            //    var falta = item.FALTA.Substring(0, 1);

            //    if (falta == "G")
            //    {
            //        cantidadGrave++;
            //        operador.GRAVE = cantidadGrave;

            //    }
            //    else if (falta == "M")
            //    {
            //        cantidadMuyGrave++;
            //        operador.MUY_GRAVE = cantidadMuyGrave;
            //    }
            //    operador.PUNTOS_FIRME += item.PUNTOS_FIRME;
            //}

            return operador;
        }
    }
}
