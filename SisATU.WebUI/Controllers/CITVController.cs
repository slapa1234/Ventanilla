using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SisATU.WebUI.Controllers
{
    public class CITVController : Controller
    {
        // GET: CITV
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CrearCITV()
        {
            return PartialView();
        }
    }
}