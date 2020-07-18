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
    public class SunatService
    {
        public EmpresaVM ConsultaRUC(string RUC)
        {
            EmpresaVM empresa = new EmpresaVM();
            try
            {
                try
                {
                    ServiceATU.Servicio_ATU servicio = new ServiceATU.Servicio_ATU();
                    var consultaRuc = servicio.ConsultaRUC(new ServiceATU.Usuario() { USULOG = "sissit", USUCON = "p4_tu_l1br0" }, RUC);
                    empresa.RUC = consultaRuc.ddp_numruc;
                    empresa.RAZON_SOCIAL = consultaRuc.ddp_nombre;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return empresa;
        }


        public EmpresaVM ConsultaRUC2(string RUC)
        {
            var TARGETURL = "https://api.aate.gob.pe/sunat/getDatosPrincipales/" + RUC;
            EmpresaVM empresa = new EmpresaVM();

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
                var resultado = JsonConvert.DeserializeObject<EmpresaVM>(jsonResult);

                empresa.RUC = resultado.ddp_numruc;
                empresa.RAZON_SOCIAL = resultado.ddp_nombre;
            }
            catch (Exception ex)
            {

            }
            return empresa;
        }




        public EmpresaVM BuscaEmpresaSTD(string RUC)
        {
            EmpresaVM modelo = new EmpresaVM();
            Servicio_STD.Servicio_STD servicio = new Servicio_STD.Servicio_STD();
            try
            {
                var resultadoSUNAT = servicio.BuscarProveedor(new Servicio_STD.Proveedor() { RUC = RUC });
                modelo.ID_EMPRESA = resultadoSUNAT.IDPROVEE.ValorEntero();
                modelo.CODPAIS = resultadoSUNAT.CODPAIS.ValorEntero();
                modelo.CODDPTO = resultadoSUNAT.CODDPTO.ValorEntero();
                modelo.CODPROV = resultadoSUNAT.CODPROV.ValorEntero();
                modelo.CODDIST = resultadoSUNAT.CODDIST.ValorEntero();
                modelo.DIRECCION_STD = resultadoSUNAT.DIRECCION;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return modelo;
        }

        public EmpresaVM CrearEmpresaSTD(EmpresaVM empresa)
        {
            EmpresaVM modelo = new EmpresaVM();
            Servicio_STD.Servicio_STD servicio = new Servicio_STD.Servicio_STD();
            try
            {
                var resultadoSUNAT = servicio.AgregarProveedor(new Servicio_STD.Usuario() { USULOG = "sissit", USUCON = "p4_tu_l1br0" }, new Servicio_STD.Proveedor()
                {
                    RAZON_SOCIAL = empresa.RAZON_SOCIAL,
                    RUC = empresa.RUC,
                    CODPAIS = 173,
                    CODDPTO = 15,
                    CODPROV = 1,
                    CODDIST = 1,
                    DIRECCION = "S/N",
                    IDPERTIP = 1
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return modelo;
        }




    }
}
