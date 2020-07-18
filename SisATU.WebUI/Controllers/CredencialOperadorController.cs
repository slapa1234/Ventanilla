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
    public class CredencialOperadorController : Controller
    {
        // GET: CredencialOperador
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CrearCredencialOperador(string NRO_DOCUMENTO = "", int ID_PROCEDIMIENTO = 0)
        {
            ExpedienteVM viewModelo = new ExpedienteVM();
            List<ComboDepartamentoVM> comboDepartamento = new List<ComboDepartamentoVM>();
            List<ComboProvinciaVM> comboProvincia = new List<ComboProvinciaVM>();
            List<ComboDistritoVM> comboDistrito = new List<ComboDistritoVM>();

            viewModelo.ID_MODALIDAD_SERVICIO_OPERADOR = Session["ID_MODALIDAD_SERVICIO"].ValorEntero();

            var comboTipoModalidad = new ModalidadServicioBLL().ComboModalidadServicio();
            var comboTipoDocumento = new ParametroBLL().ConsultaParametro(EnumParametroTipo.TipoDocumento.ValorEntero());
            var comboTipoOperador = new ParametroBLL().ConsultaParametro(EnumParametroTipo.TipoOperador.ValorEntero());
            var comboSexo = new ParametroBLL().ConsultaParametro(EnumParametroTipo.TipoSexo.ValorEntero());

            comboTipoModalidad.Add(new ComboModalidadServicioVM() { ID_MODALIDAD_SERVICIO = 0, NOMBRE = ".:Seleccione Modalidad:." });
            comboTipoDocumento.Add(new ParametroModelo() { PARSEC = 0, PARNOM = ".:Tipo de Documento:." });
            comboTipoOperador.Add(new ParametroModelo() { PARSEC = 0, PARNOM = ".:Tipo de Operador:." });
            comboSexo.Add(new ParametroModelo() { PARSEC = 0, PARNOM = ".:Tipo de Sexo:." });

            if (viewModelo.ID_MODALIDAD_SERVICIO_OPERADOR != EnumModalidadServicio.TransporteRegularPersona.ValorEntero())
            {
                comboTipoOperador.RemoveAll(x => x.PARSEC == EnumParametroSecuenciaTipoOperador.COBRADOR.ValorEntero() || x.PARSEC == EnumParametroSecuenciaTipoOperador.CONDUCTORYCOBRADOR.ValorEntero());
                viewModelo.ID_TIPO_OPERADOR = EnumParametroSecuenciaTipoOperador.CONDUCTOR.ValorEntero();
            }

            viewModelo.SelectTipoModalidadOperador = comboTipoModalidad.OrderBy(x => x.ID_MODALIDAD_SERVICIO)
               .Select(j => new SelectListItem
               {
                   Value = j.ID_MODALIDAD_SERVICIO.ToString(),
                   Text = j.NOMBRE,
               }).ToList();

            viewModelo.SelectTipoDocumentoOperador = comboTipoDocumento.OrderBy(x => x.PARCOD)
               .Select(j => new SelectListItem
               {
                   Value = j.PARSEC.ToString(),
                   Text = j.PARNOM,
               }).ToList();

            viewModelo.SelectTipoOperador = comboTipoOperador.OrderBy(x => x.PARSEC)
               .Select(j => new SelectListItem
               {
                   Value = j.PARSEC.ToString(),
                   Text = j.PARNOM,
               }).ToList();

            if (Session["ID_TIPO_PERSONA"].ValorEntero() == EnumParametro.PersonaNatural.ValorEntero())
            {
                viewModelo.NRO_DOCUMENTO_OPERADOR = NRO_DOCUMENTO;
                viewModelo.ID_TIPO_DOCUMENTO_OPERADOR = Session["ID_TIPO_DOCUMENTO"].ValorEntero();
                var buscarOperador = new OperadorBLL().BuscarOperador("", viewModelo.ID_TIPO_DOCUMENTO_OPERADOR, NRO_DOCUMENTO, viewModelo.ID_MODALIDAD_SERVICIO_OPERADOR, Session["ID_TIPO_PERSONA"].ValorEntero(), ID_PROCEDIMIENTO);


                comboDepartamento = new DepartamentoBLL().ComboDepartamento(0);
                comboProvincia = new ProvinciaBLL().ComboProvincia(buscarOperador.ID_DEPARTAMENTO_OPERADOR);
                comboDistrito = new DistritoBLL().ComboDistrito(buscarOperador.ID_PROVINCIA_OPERADOR);

                comboDepartamento.Add(new ComboDepartamentoVM() { ID_DEPARTAMENTO = 0, NOMBRE_DEPARTAMENTO = ".:Seleccione Departamento:." });
                comboProvincia.Add(new ComboProvinciaVM() { ID_PROVINCIA = 0, NOMBRE_PROVINCIA = ".:Seleccione Provincia:." });
                comboDistrito.Add(new ComboDistritoVM() { ID_DISTRITO = 0, NOMBRE_DISTRITO = ".:Seleccione Distrito:." });

                viewModelo.SelectDepartamento = comboDepartamento.OrderBy(x => x.ID_DEPARTAMENTO)
                .Select(j => new SelectListItem
                {
                    Value = j.ID_DEPARTAMENTO.ToString(),
                    Text = j.NOMBRE_DEPARTAMENTO,
                }).ToList();

                viewModelo.SelectProvincia = comboProvincia.OrderBy(x => x.ID_PROVINCIA)
                    .Select(j => new SelectListItem
                    {
                        Value = j.ID_PROVINCIA.ToString(),
                        Text = j.NOMBRE_PROVINCIA,
                    }).ToList();

                viewModelo.SelectDistrito = comboDistrito.OrderBy(x => x.ID_DISTRITO)
                    .Select(j => new SelectListItem
                    {
                        Value = j.ID_DISTRITO.ToString(),
                        Text = j.NOMBRE_DISTRITO,
                    }).ToList();

                if (buscarOperador.ResultadoProcedimientoVM.CodResultado == 1 && buscarOperador.NOMBRES != null && buscarOperador.ID_OPERADOR != 0 )
                {
                    viewModelo.ID_OPERADOR = buscarOperador.ID_OPERADOR;
                    viewModelo.APELLIDO_PATERNO_OPERADOR = buscarOperador.APELLIDO_PATERNO;
                    viewModelo.APELLIDO_MATERNO_OPERADOR = buscarOperador.APELLIDO_MATERNO;
                    viewModelo.NOMBRE_OPERADOR = buscarOperador.NOMBRES;
                    viewModelo.FECHA_NACIMIENTO_OPERADOR = buscarOperador.FECHA_NACIMIENTO;
                    viewModelo.DIRECCION_OPERADOR = buscarOperador.DIRECCION;
                    viewModelo.TELEFONO_CEL_OPERADOR = buscarOperador.TELEFONO_CEL;
                    viewModelo.TELEFONO_CASA_OPERADOR = buscarOperador.TELEFONO_CASA;
                    viewModelo.CORREO_OPERADOR = buscarOperador.CORREO;
                    viewModelo.NRO_LICENCIA_OPERADOR = buscarOperador.NRO_LICENCIA;
                    viewModelo.FECHA_EXPEDICION_OPERADOR = buscarOperador.FECHA_EXPEDICION;
                    viewModelo.FECHA_REVALIDACION_OPERADOR = buscarOperador.FECHA_REVALIDACION;
                    viewModelo.CATEGORIA_OPERADOR = buscarOperador.CATEGORIA;
                    if (buscarOperador.ID_TIPO_OPERADOR == 0)
                    {
                        if (Session["ID_MODALIDAD_SERVICIO"].ValorEntero() != EnumModalidadServicio.TransporteRegularPersona.ValorEntero())
                        {
                            viewModelo.ID_TIPO_OPERADOR = EnumParametroTipoOperador.CONDUCTOR.ValorEntero();
                        }
                        else
                        {
                            viewModelo.ID_TIPO_OPERADOR = 0;
                        }
                       
                    }
                    else
                    {
                        viewModelo.ID_TIPO_OPERADOR = buscarOperador.ID_TIPO_OPERADOR;
                    }
                   
                    viewModelo.FECHA_VENCIMIENTO_CREDENCIAL = buscarOperador.FECHA_VENCIMIENTO_CREDENCIAL;

                    if (buscarOperador.NOMBRE_FOTO != null)
                    {
                        viewModelo.TIENE_FOTO = true;
                        viewModelo.FOTO_BASE64 = Url.Content("~/Adjunto/foto_operador/" + buscarOperador.NOMBRE_FOTO);
                        viewModelo.FOTO_OPERADOR = buscarOperador.NOMBRE_FOTO;
                    }
                     
                    viewModelo.ID_SEXO = buscarOperador.ID_SEXO;
                    viewModelo.ID_DEPARTAMENTO_OPERADOR = buscarOperador.ID_DEPARTAMENTO_OPERADOR;
                    viewModelo.ID_PROVINCIA_OPERADOR = buscarOperador.ID_PROVINCIA_OPERADOR;
                    viewModelo.ID_DISTRITO_OPERADOR = buscarOperador.ID_DISTRITO_OPERADOR;
                    if (buscarOperador.TieneCredencial == 1)
                    {
                        viewModelo.ObtencionRenovacion = "Renovacion";
                    }
                    else
                    {
                        viewModelo.ObtencionRenovacion = "Obtencion";
                    }
                    
                    viewModelo.PUNTOS_FIRME = buscarOperador.PUNTOS_FIRME.ValorEntero();
                    viewModelo.GRAVE = buscarOperador.GRAVE;
                    viewModelo.MUY_GRAVE = buscarOperador.MUY_GRAVE;
                    viewModelo.ESTADO_LICENCIA = buscarOperador.ESTADO_LICENCIA;
                    viewModelo.ID_TIPO_CREDENCIAL = EnumTipoCredencial.RENOVACION.ValorEntero();
                    //viewModelo.ID_TIPO_OPERADOR = buscarOperador.TIPO_OPERADOR;
                }
                else
                {
                    string tipoDocumento = "";
                    if (viewModelo.ID_TIPO_DOCUMENTO_OPERADOR == EnumParametro.DNI.ValorEntero())
                    {
                        tipoDocumento = "2";
                    }
                    else if (viewModelo.ID_TIPO_DOCUMENTO_OPERADOR == EnumParametro.CE.ValorEntero())
                    {
                        tipoDocumento = "4";
                    }
                    else if (viewModelo.ID_TIPO_DOCUMENTO_OPERADOR == EnumParametro.PTP.ValorEntero())
                    {
                        tipoDocumento = "14";
                    }

                    var resultadoReniec = new OperadorBLL().consultaDatosPersonalesYLic(tipoDocumento, NRO_DOCUMENTO, Session["ID_MODALIDAD_SERVICIO"].ValorEntero(), Session["ID_TIPO_PERSONA"].ValorEntero(),ID_PROCEDIMIENTO);

                    if (buscarOperador.NOMBRES != null)
                    {
                        viewModelo.NOMBRE_OPERADOR = resultadoReniec.NOMBRES;
                        viewModelo.APELLIDO_PATERNO_OPERADOR = resultadoReniec.APELLIDO_PATERNO;
                        viewModelo.APELLIDO_MATERNO_OPERADOR = resultadoReniec.APELLIDO_MATERNO;
                        viewModelo.DIRECCION_OPERADOR = resultadoReniec.DIRECCION;
                        viewModelo.NRO_LICENCIA_OPERADOR = resultadoReniec.NRO_LICENCIA;
                        viewModelo.FECHA_EXPEDICION_OPERADOR = resultadoReniec.FECHA_EXPEDICION;
                        viewModelo.FECHA_REVALIDACION_OPERADOR = resultadoReniec.FECHA_REVALIDACION;
                        viewModelo.CATEGORIA_OPERADOR = resultadoReniec.CATEGORIA;
                        viewModelo.PUNTOS_FIRME = resultadoReniec.PUNTOS_FIRME.ValorEntero();
                        viewModelo.GRAVE = resultadoReniec.GRAVE;
                        viewModelo.MUY_GRAVE = resultadoReniec.MUY_GRAVE;
                        viewModelo.ESTADO_LICENCIA = resultadoReniec.ESTADO_LICENCIA;
                        viewModelo.ID_TIPO_CREDENCIAL = EnumTipoCredencial.OBTENCION.ValorEntero();
                        viewModelo.FECHA_VENCIMIENTO_CREDENCIAL = resultadoReniec.FECHA_VENCIMIENTO_CREDENCIAL;
                        viewModelo.TieneCredencial = resultadoReniec.TieneCredencial;
                        if (resultadoReniec.FOTO_BASE64 != null)
                        {
                            viewModelo.TIENE_FOTO = true;
                            viewModelo.FOTO_BASE64 = "data:image/png; base64, " + resultadoReniec.FOTO_BASE64;
                            viewModelo.FOTO_OPERADOR = resultadoReniec.FOTO_BASE64;
                        }

                        if (resultadoReniec.TieneCredencial == 1)
                        {
                            viewModelo.ObtencionRenovacion = "Renovacion";
                        }
                        else
                        {
                            viewModelo.ObtencionRenovacion = "Obtencion";
                        }
                       
                    }

                }
            }
            else
            {
                comboDepartamento = new DepartamentoBLL().ComboDepartamento(0);
                comboProvincia = new ProvinciaBLL().ComboProvincia(0);
                comboDistrito = new DistritoBLL().ComboDistrito(0);

                comboDepartamento.Add(new ComboDepartamentoVM() { ID_DEPARTAMENTO = 0, NOMBRE_DEPARTAMENTO = ".:Seleccione Departamento:." });
                comboProvincia.Add(new ComboProvinciaVM() { ID_PROVINCIA = 0, NOMBRE_PROVINCIA = ".:Seleccione Provincia:." });
                comboDistrito.Add(new ComboDistritoVM() { ID_DISTRITO = 0, NOMBRE_DISTRITO = ".:Seleccione Distrito:." });

                viewModelo.SelectDepartamento = comboDepartamento.OrderBy(x => x.ID_DEPARTAMENTO)
                .Select(j => new SelectListItem
                {
                    Value = j.ID_DEPARTAMENTO.ToString(),
                    Text = j.NOMBRE_DEPARTAMENTO,
                }).ToList();

                viewModelo.SelectProvincia = comboProvincia.OrderBy(x => x.ID_PROVINCIA)
                    .Select(j => new SelectListItem
                    {
                        Value = j.ID_PROVINCIA.ToString(),
                        Text = j.NOMBRE_PROVINCIA,
                    }).ToList();

                viewModelo.SelectDistrito = comboDistrito.OrderBy(x => x.ID_DISTRITO)
                    .Select(j => new SelectListItem
                    {
                        Value = j.ID_DISTRITO.ToString(),
                        Text = j.NOMBRE_DISTRITO,
                    }).ToList();
                viewModelo.ID_TIPO_CREDENCIAL = EnumTipoCredencial.RENOVACION.ValorEntero();
                viewModelo.ObtencionRenovacion = "Renovacion";
                //viewModelo.FECHA_VENCIMIENTO_CREDENCIAL = 
            }
            viewModelo.SelectSexo = comboSexo.OrderBy(x => x.PARCOD)
               .Select(j => new SelectListItem
               {
                   Value = j.PARSEC.ToString(),
                   Text = j.PARNOM,
               }).ToList();


            viewModelo.ID_MODALIDAD_SERVICIO_OPERADOR = Session["ID_MODALIDAD_SERVICIO"].ValorEntero();
            viewModelo.ID_PROCEDIMIENTO = ID_PROCEDIMIENTO;
            return PartialView(viewModelo);
        }

        public ActionResult BuscarOperador(string RUC, int ID_TIPO_DOCUMENTO, string NRO_DOCUMENTO, int ID_TIPO_MODALIDAD, int ID_TIPO_PERSONA, int ID_PROCEDIMIENTO)
        {
            OperadorVM operador = new OperadorVM();
            operador = new OperadorBLL().BuscarOperador(RUC, ID_TIPO_DOCUMENTO, NRO_DOCUMENTO, ID_TIPO_MODALIDAD, ID_TIPO_PERSONA, ID_PROCEDIMIENTO);
            return Json(new { modelo = operador }, JsonRequestBehavior.AllowGet);
        }
    }
}