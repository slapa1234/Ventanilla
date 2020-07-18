using SisATU.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SisATU.WebUI.Controllers
{
    public class ConductorController : Controller
    {
        // GET: Conductor
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ConsultaDNI(string DNI)
        {
            var resultado = new PersonaBLL().ConsultaDNI(DNI);
            return Json(new { modelo = resultado });
        }

        public JsonResult ConsultaCE(string DNI)
        {
            try
            {
                var resultado = new PersonaBLL().ConsultarCE(DNI);
                return Json(new { modelo = resultado });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public JsonResult ConsultaPTP(string DNI)
        {
            var resultado = new PersonaBLL().ConsultarPTP(DNI);
            return Json(new { modelo = resultado });
        }

         
    }
}