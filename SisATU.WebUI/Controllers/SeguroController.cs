using SisATU.Base.ViewModel;
using SisATU.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SisATU.WebUI.Controllers
{
    public class SeguroController : Controller
    {
        // GET: Seguro
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CrearSeguro()
        {
            ExpedienteVM viewModelo = new ExpedienteVM();
            var ComboTipoSeguro = new TipoSeguroBLL().ComboTipoSeguro();
            var ComboAfocat = new AfocatBLL().ConsultaAfocat();

            ComboTipoSeguro.Add(new ComboTipoSeguroVM() { ID_TIPO_SEGURO = 0, NOMBRE = ".:Tipo de Seguro:." });
            ComboAfocat.Add(new ComboAfocatVM() { ID_AFOCAT = 0, NOMBRE = ".:Aseguradora:." });

            viewModelo.SelectTipoSeguro = ComboTipoSeguro.OrderBy(x => x.ID_TIPO_SEGURO)
              .Select(j => new SelectListItem
              {
                  Value = j.ID_TIPO_SEGURO.ToString(),
                  Text = j.NOMBRE,
              }).ToList();

            viewModelo.SelectAfocat = ComboAfocat.OrderBy(x => x.ID_AFOCAT)
              .Select(j => new SelectListItem
              {
                  Value = j.ID_AFOCAT.ToString(),
                  Text = j.NOMBRE,
              }).ToList();

            return PartialView(viewModelo);
        }
    }
}