using SisATU.Base;
using SisATU.Base.Enumeradores;
using SisATU.Base.ViewModel;
using SisATU.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SisATU.WebUI.Util;
using System.Net.Http;
using System.IO;
using System.Threading.Tasks;

namespace SisATU.WebUI.Controllers
{
    public class BackOfficeController : Controller
    {
        #region Index
        public ActionResult Index()
        {
            BackOfficeVM viewModelo = new BackOfficeVM();
            var comboModalidades = new ModalidadServicioBLL().getModalidadByTipoPersona(0);
            ViewBag.SelectModalidad = new SelectList(comboModalidades.OrderBy(x => x.ID_MODALIDAD_SERVICIO), "ID_MODALIDAD_SERVICIO", "NOMBRE");
            return View(viewModelo);
        }
        #endregion


        public async Task<ActionResult> Buscar_Pag(DTParameters parametros, string expediente, string NroDocumento, string persona, int id_modalidad_servicio, string fechaRegistro)
        {
            try
            {
                BackOfficeBLL obj = new BackOfficeBLL();

                var resultado = await obj.BuscarPag(expediente, NroDocumento, persona, id_modalidad_servicio, fechaRegistro, parametros.SortOrder, parametros.Page.page + 1, parametros.Length);

                return Json(new
                {
                    draw = parametros.Draw,
                    data = resultado.ListaExpediente,
                    recordsFiltered = resultado.TotalRegistro,
                    recordsTotal = resultado.TotalRegistro
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //GENERACION PDF
        public ActionResult ImprimirTUC(int IDDOC, int tipoModalidad)
        {
            ViewBag.IDDOC = IDDOC;
            ViewBag.tipoModalidad = tipoModalidad;


            return PartialView();
        }
        public JsonResult Imprimir_TUC(int idexpediente, int tipoModalidad)
        {
         
            int tipo = 0;
            string mensaje = "";
            string urlArchivos = "~/Downloads/";
            Archivo.EliminarArchivos(urlArchivos);
            string resultado = new ReportesBLL().getDatosTarjetaUnicaCirculacion(idexpediente,Server.MapPath(urlArchivos) , tipoModalidad);
            //if (tipo == 1)
            //{
            resultado = urlArchivos.Substring(2) + resultado.Substring(2);
            //}
            return Json(new { modelo = resultado, tipo = tipo, mensaje = mensaje });
        }

        public ActionResult ImprimirResolucion(int IDDOC)
        {
            ViewBag.IDDOC = IDDOC;
            return PartialView();
        }

        public JsonResult Imprimir_ReporteResolucion(int IDDOC)
        {

            int tipo = 0;
            string mensaje = "";
            string urlArchivos = "~/Downloads/";
            Archivo.EliminarArchivos(urlArchivos);
            string resultado = new ReportesBLL().ReporteResolucion(IDDOC, Server.MapPath(urlArchivos));
            //if (tipo == 1)
            //{
            resultado = urlArchivos.Substring(2) + resultado.Substring(2);
            //}
            return Json(new { modelo = resultado, tipo = tipo, mensaje = mensaje });
        }
        public ActionResult ImpReportePadron(int ID_EXPEDIENTE, int ID_MODALIDAD_SERVICIO,string PERSONA, string MODALIDAD_SERVICIO, string FECHA_REG,string TRAMITE)
        {
            ViewBag.ID_EXPEDIENTE = ID_EXPEDIENTE;
            ViewBag.ID_MODALIDAD_SERVICIO = ID_MODALIDAD_SERVICIO;
          
            
            return PartialView();
        }

        public JsonResult Imprimir_padron(int ID_EXPEDIENTE, int ID_MODALIDAD_SERVICIO)
        {

            int tipo = 0;
            string mensaje = "";
            string urlArchivos = "~/Downloads/";
            Archivo.EliminarArchivos(urlArchivos);
            string resultado = new ReportesBLL().genera_PADRON(ID_EXPEDIENTE, Server.MapPath(urlArchivos), ID_MODALIDAD_SERVICIO);
            //if (tipo == 1)
            //{
            resultado = urlArchivos.Substring(2) + resultado.Substring(2);
            //}
            return Json(new { modelo = resultado, tipo = tipo, mensaje = mensaje });
        }

        public ActionResult ImprimirCredencial(int ID_EXPEDIENTE, int tipoModalidad)
        {
            ViewBag.ID_EXPEDIENTE = ID_EXPEDIENTE;
            ViewBag.tipoModalidad = tipoModalidad;
            return PartialView();
        }


        public JsonResult Imprimir_Credencial(int idexpediente, int tipoModalidad)
        {

            int tipo = 0;
            string mensaje = "";
            string urlArchivos = "~/Downloads/";
            Archivo.EliminarArchivos(urlArchivos);
            string resultado = new ReportesBLL().genera_pdf_Credencial(idexpediente, Server.MapPath(urlArchivos), tipoModalidad, "", "", "");
            //if (tipo == 1)
            //{
            resultado = urlArchivos.Substring(2) + resultado.Substring(2);
            //}
            return Json(new { modelo = resultado, tipo = tipo, mensaje = mensaje });
        }

        public ActionResult ImprimirCredencialTaxi(int ID_EXPEDIENTE, int tipoModalidad)
        {
            ViewBag.ID_EXPEDIENTE = ID_EXPEDIENTE;
            ViewBag.tipoModalidad = tipoModalidad;
            return PartialView();
        }

        public JsonResult Imprimir_Credencial_Taxi(int idexpediente, int tipoModalidad)
        {

            int tipo = 0;
            string mensaje = "";
            string urlArchivos = "~/Downloads/";
            Archivo.EliminarArchivos(urlArchivos);
            string resultado = new ReportesBLL().genera_pdf_Credencial_taxi(idexpediente, Server.MapPath(urlArchivos), tipoModalidad, "", "", "");
            //if (tipo == 1)
            //{
            resultado = urlArchivos.Substring(2) + resultado.Substring(2);
            //}
            return Json(new { modelo = resultado, tipo = tipo, mensaje = mensaje });
        }

        public ActionResult Imprimir_TarjetaTaxi(int ID_EXPEDIENTE, int tipoModalidad)
        {
            ViewBag.ID_EXPEDIENTE = ID_EXPEDIENTE;
            ViewBag.tipoModalidad = tipoModalidad;
            return PartialView();
        }


        public JsonResult Imprimir_tarj_taxi(int idexpediente, int tipoModalidad)
        {

            int tipo = 0;
            string mensaje = "";
            string urlArchivos = "~/Downloads/";
           
            var url_foto = "~/Adjunto/foto_operador/";
            var pathArchivo = Server.MapPath(urlArchivos);
            var pathFoto = Server.MapPath(url_foto);
            Archivo.EliminarArchivos(urlArchivos);
            string resultado = new ReportesBLL().genera_pdf_Tarje_Crendencial_taxi(idexpediente, Server.MapPath(urlArchivos),pathFoto,tipoModalidad,"","","");
            //if (tipo == 1)
            //{
            resultado = urlArchivos.Substring(2) + resultado.Substring(2);
            //}
            return Json(new { modelo = resultado, tipo = tipo, mensaje = mensaje });
        }


        public ActionResult Imprimir_Tarjeta(int ID_EXPEDIENTE, int tipoModalidad)
        {
            ViewBag.ID_EXPEDIENTE = ID_EXPEDIENTE;
            ViewBag.tipoModalidad = tipoModalidad;


            return PartialView();
        }


        public JsonResult Imprimir_tarj(int idexpediente, int tipoModalidad)
        {

            int tipo = 0;
            string mensaje = "";
            string urlArchivos = "~/Downloads/";

            var url_foto = "~/Adjunto/foto_operador/";
            var pathArchivo = Server.MapPath(urlArchivos);
            var pathFoto = Server.MapPath(url_foto);
            Archivo.EliminarArchivos(urlArchivos);
            string resultado = new ReportesBLL().genera_pdf_Tarje_Crendencial(idexpediente, Server.MapPath(urlArchivos), pathFoto, tipoModalidad, "", "", "");
            //if (tipo == 1)
            //{
            resultado = urlArchivos.Substring(2) + resultado.Substring(2);
            //}
            return Json(new { modelo = resultado, tipo = tipo, mensaje = mensaje });
        }

        //OBTENER CABECERA
        public ActionResult ConsultarDetalleCabecera(int idexpediente)
        {
            BackOfficeBLL obj = new BackOfficeBLL();
            CabeceraBackOfficeVM resultado = obj.BuscarCabecera(idexpediente);
            return PartialView(resultado);
        }
    }
}