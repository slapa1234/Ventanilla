using SisATU.Base;
using SisATU.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SisATU.WebUI.Controllers
{
    public class EmpresaController : Controller
    {
        // GET: Empresa
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ConsultaRUC(string RUC = "", string NRO_DOCUMENTO_REPRESENTANTE_LEGAL = "", int ID_TIPO_DOCUMENTO_REPRESENTANTE_LEGAL = 0, bool Representante = false)
        {
            var resultado = new EmpresaBLL().ConsultaRuc(RUC);
            UsuarioModelo representante = new UsuarioModelo();
            if (Representante == true)
            {
                representante = new UsuarioBLL().BuscarRepresentante(RUC, NRO_DOCUMENTO_REPRESENTANTE_LEGAL, ID_TIPO_DOCUMENTO_REPRESENTANTE_LEGAL);
            }

            return Json(new { modelo = resultado, representante = representante.ResultadoUsuarioVM.Validacion});
        }
    }
}