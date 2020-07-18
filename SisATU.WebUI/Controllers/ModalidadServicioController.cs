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

namespace SisATU.WebUI.Controllers
{
    public class ModalidadServicioController : Controller
    {
        // GET: ModalidadServicio
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult getModalidades(int idTipoPersona)
        {
            var modalidades = new ModalidadServicioBLL().getModalidadByTipoPersona(idTipoPersona); //lista todos
            return Json(new { resultado = modalidades });
        }

        public JsonResult getTramiteByModalidad(int idModalidad)
        {
            var tramites = new TramiteBLL().getListaTramiteByTipo(idModalidad); //lista todos
            return Json(new { resultado = tramites });
        }
    }
}