using SisATU.Base;
using SisATU.Base.Enumeradores;
using SisATU.Base.ViewModel;
using SisATU.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SisATU.WebUI.Controllers
{
    public class PropietarioController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            return View();
        } 

        public ActionResult CrearTarjetaPropiedad()
        {
            ExpedienteVM viewModelo = new ExpedienteVM();
            var comboTipoDocumento = new ParametroBLL().ConsultaParametro(EnumParametroTipo.TipoDocumento.ValorEntero());

            comboTipoDocumento.Add(new ParametroModelo() { PARCOD = 0, PARNOM = ".:Tipo de Documento:." });

            viewModelo.SelectTipoDocumentoPropietario = comboTipoDocumento.OrderBy(x => x.PARCOD)
                .Select(j => new SelectListItem
                {
                    Value = j.PARSEC.ToString(),
                    Text = j.PARNOM,
                }).ToList();
            return PartialView(viewModelo);
        }
    }
}