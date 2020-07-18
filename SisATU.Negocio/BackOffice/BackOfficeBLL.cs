using CrystalDecisions.CrystalReports.Engine;
using SisATU.Base.ViewModel;
using SisATU.Datos;
using SisATU.Negocio.Reportes;
using SisATU.Negocio.Reportes.Credenciales;
using SisATU.Negocio.Reportes.Credenciales.Constancia;
using SisATU.Negocio.Reportes.Resoluciones.Credenciales;
//using SisATU.Negocio.Reportes.Resoluciones.Credenciales;
using SisATU.Negocio.Reportes.Resoluciones.resolucion_taxi_independiente;
using SISCRE.WEB.ADM.Documentos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SisATU.Negocio
{
    public class BackOfficeBLL

    {
        BackOfficeDAL backofficeDAL = new BackOfficeDAL();


        public async Task<DTListaExpedienteVM> BuscarPag(string expediente, string NroDocumento, string persona, int id_modalidad_servicio, string fechaRegistro, string orden, int pagina = 1, int registros = 10)
        {
            var resultado = await backofficeDAL.BuscarPag(expediente, NroDocumento, persona, id_modalidad_servicio, fechaRegistro, orden, pagina, registros);
            return resultado;
        }

        public CabeceraBackOfficeVM BuscarCabecera(int id_expediente)
        {
            BackOfficeDAL obj = new BackOfficeDAL();
            return obj.ConsultaCabecera(id_expediente);
        }
    }
}
