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
    public class VehiculoController : Controller
    {
        // GET: Vehiculo
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ObtenerDatosVehiculo(string nroPlaca)
        {
            ConsultarVehiculoVM vehiculo = new VehiculoBLL().ConsultarDatosVehiculo(nroPlaca);
            return Json(new { modelo = vehiculo }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PerteneceSolicitante(string nroPlaca, string nroSolicitante)
        {
            int resultado = new VehiculoBLL().ConsultaPerteneceSolicitante(nroPlaca, nroSolicitante);
            return Json(new { modelo = resultado }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ObtieneModelo(int ID_MARCA)
        {
            var comboModelo = new ModeloBLL().ComboModelo(ID_MARCA);
            comboModelo.Add(new ComboModeloVM() { ID_MODELO = 0, NOMBRE = ".:Modelo:." });
            return Json(new { resultado = comboModelo.OrderBy(x => x.ID_MODELO) });
        }
        public ActionResult CrearVehiculo(int ID_PROCEDIMIENTO)
        {
            ExpedienteVM viewModelo = new ExpedienteVM();

            var comboMarca = new MarcaBLL().ComboMarca();
            var ComboModelo = new ModeloBLL().ComboModelo(comboMarca[0].ID_MARCA);
            var ComboTipoCombustible = new ParametroBLL().ConsultaParametro(EnumParametroTipo.TipoCombustible.ValorEntero());
            var comboTipoModalidad = new ModalidadServicioBLL().ComboModalidadServicio();
            var comboClaseVehiculo = new ClaseVehiculoBLL().ComboClaseVehiculo();
            var comboCategoriaVehiculo = new CategoriaVehiculoBLL().ComboCategoriaVehiculo();

            comboMarca.Add(new ComboMarcaVM { ID_MARCA = 0, NOMBRE = ".:Marca:." });
            ComboModelo.Add(new ComboModeloVM() { ID_MODELO = 0, NOMBRE = ".:Modelo:." });
            ComboTipoCombustible.Add(new ParametroModelo { PARSEC = 0, PARNOM = ".:Tipo de Combustible:." });
            comboTipoModalidad.Add(new ComboModalidadServicioVM() { ID_MODALIDAD_SERVICIO = 0, NOMBRE = ".:Tipo de Modalidad:." });
            comboClaseVehiculo.Add(new ComboClaseVehiculoVM() { ID_CLASE_VEHICULO = 0, NOMBRE = ".:Clase Vehículo:." });
            comboCategoriaVehiculo.Add(new ComboCategoriaVehiculoVM() { ID_CATEGORIA_VEHICULO = 0, NOMBRE = ".:Categoria Vehículo:." });

            viewModelo.SelectMarca = comboMarca.OrderBy(x => x.ID_MARCA)
            .Select(j => new SelectListItem
            {
                Value = j.ID_MARCA.ToString(),
                Text = j.NOMBRE,
            }).ToList();

            viewModelo.SelectModelo = ComboModelo.OrderBy(x => x.ID_MODELO)
               .Select(j => new SelectListItem
               {
                   Value = j.ID_MODELO.ToString(),
                   Text = j.NOMBRE,
               }).ToList();

            viewModelo.SelectTipoCombustible = ComboTipoCombustible.OrderBy(x => x.PARSEC)
                .Select(j => new SelectListItem
                {
                    Value = j.PARSEC.ToString(),
                    Text = j.PARNOM,
                }).ToList();

            viewModelo.SelectTipoModalidad = comboTipoModalidad.OrderBy(x => x.ID_MODALIDAD_SERVICIO)
                .Select(j => new SelectListItem
                {
                    Value = j.ID_MODALIDAD_SERVICIO.ToString(),
                    Text = j.NOMBRE,
                }).ToList();

            viewModelo.SelectClaseVehiculo = comboClaseVehiculo.OrderBy(x => x.ID_CLASE_VEHICULO)
                .Select(j => new SelectListItem
                {
                    Value = j.ID_CLASE_VEHICULO.ToString(),
                    Text = j.NOMBRE,
                }).ToList();

            viewModelo.SelectCategoriaVehiculo = comboCategoriaVehiculo.OrderBy(x => x.ID_CATEGORIA_VEHICULO)
                .Select(j => new SelectListItem
                {
                    Value = j.ID_CATEGORIA_VEHICULO.ToString(),
                    Text = j.NOMBRE,
                }).ToList();

            //viewModelo.NroPlaca = "C5Q673";
            viewModelo.ID_PROCEDIMIENTO = ID_PROCEDIMIENTO;
            viewModelo.ID_MODALIDAD_SERVICIO = Session["ID_MODALIDAD_SERVICIO"].ValorEntero();
            return PartialView(viewModelo);
        }

        public ActionResult MaestroMatriz(int ID_TIPO_PERSONA, int ANIO_PERIODO, int ID_MODALIDAD_SERVICIO, string ANIO_FABRICACION)
        {
            var resultado = new MaestroMatrizBLL().ConsultaMaestroMatriz(ID_TIPO_PERSONA, ANIO_PERIODO, ID_MODALIDAD_SERVICIO, ANIO_FABRICACION);
            return Json(new { resultado = resultado });
        }

    }
}