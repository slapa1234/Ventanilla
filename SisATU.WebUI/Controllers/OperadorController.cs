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


namespace SisATU.WebUI.Controllers
{
    public class OperadorController : Controller
    {
        // GET: Operador
        //consulta datos Personales y Licencia
        public ActionResult CrearOperador(string nroRUC, int ID_TIPO_PERSONA, int ID_MODALIDAD_SERVICIO)
        {
            ExpedienteVM viewModelo = new ExpedienteVM();
            var comboTipoDocumento = new ParametroBLL().ConsultaParametro(EnumParametroTipo.TipoDocumento.ValorEntero());
            var comboTipoModalidad = new ModalidadServicioBLL().ComboModalidadServicio();
            var listaOperadoresByRuc = new OperadorBLL().consultarListaOperador(nroRUC);
            var comboTipoOperador = new ParametroBLL().ConsultaParametro(EnumParametroTipo.TipoOperador.ValorEntero());

            var comboDepartamento = new DepartamentoBLL().ComboDepartamento(0);
            var comboProvincia = new ProvinciaBLL().ComboProvincia(Session["ID_DEPARTAMENTO"].ValorEntero());
            var comboDistrito = new DistritoBLL().ComboDistrito(Session["ID_PROVINCIA"].ValorEntero());

            comboTipoDocumento.Add(new ParametroModelo() { PARCOD = 0, PARNOM = ".:Tipo de Documento:." });
            comboTipoModalidad.Add(new ComboModalidadServicioVM() { ID_MODALIDAD_SERVICIO = 0, NOMBRE = ".:Seleccione Modalidad:." });
            comboTipoOperador.Add(new ParametroModelo() { PARSEC = 0, PARNOM = ".:Tipo de Operador:." });

            comboDepartamento.Add(new ComboDepartamentoVM() { ID_DEPARTAMENTO = 0, NOMBRE_DEPARTAMENTO = ".:Seleccione Departamento:." });
            comboProvincia.Add(new ComboProvinciaVM() { ID_PROVINCIA = 0, NOMBRE_PROVINCIA = ".:Seleccione Provincia:." });
            comboDistrito.Add(new ComboDistritoVM() { ID_DISTRITO = 0, NOMBRE_DISTRITO = ".:Seleccione Distrito:." });



            if (ID_TIPO_PERSONA == EnumParametroTipoPersona.PersonaJuridica.ValorEntero())
            {
                if (ID_MODALIDAD_SERVICIO != EnumModalidadServicio.TransporteRegularPersona.ValorEntero())
                {
                    comboTipoOperador.RemoveAll(x => x.PARSEC == EnumParametroSecuenciaTipoOperador.COBRADOR.ValorEntero() || x.PARSEC == EnumParametroSecuenciaTipoOperador.CONDUCTORYCOBRADOR.ValorEntero());
                }
            }



            viewModelo.SelectTipoDocumento = comboTipoDocumento.OrderBy(x => x.PARCOD)
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

            viewModelo.SelectTipoOperador = comboTipoOperador.OrderBy(x => x.PARSEC)
               .Select(j => new SelectListItem
               {
                   Value = j.PARSEC.ToString(),
                   Text = j.PARNOM,
               }).ToList();

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

            viewModelo.OperadorVM = listaOperadoresByRuc;
            viewModelo.ID_MODALIDAD_SERVICIO_OPERADOR = Session["ID_MODALIDAD_SERVICIO"].ValorEntero();
            return PartialView(viewModelo);
        }

        public ActionResult DetalleOperador()
        {
            return PartialView();
        }


        public JsonResult consultaDatosPersonaYLic(string tipoDocumento, string nroDocumento, int ID_TIPO_PERSONA,int ID_PROCEDIMIENTO)
        {
            var resultado = new OperadorBLL().consultaDatosPersonalesYLic(tipoDocumento, nroDocumento, Session["ID_MODALIDAD_SERVICIO"].ValorEntero(), ID_TIPO_PERSONA, ID_PROCEDIMIENTO);
            return Json(new { modelo = resultado });
        }
        //persona = new PersonaBLL().ConsultaDNI(acceso.NroDocumento);

        public ActionResult _DetalleOperador(int NumeroRegistro = 0, string NRO_DOCUMENTO = "", string APELLIDO_PATERNO = "", string APELLIDO_MATERNO = "", string NOMBRE = "",
                                             int TIPO_DOCUMENTO = 0, string DIRECCION = "", string TELEFONO_CEL = "", string TELEFONO_CASA = "", string CORREO = "",
                                             int TIPO_OPERADOR = 0, string NRO_LICENCIA = "", string CATEGORIA = "", string FECHA_EXPEDICION = "", string FECHA_REVALIDACION = "",
                                             string NOMBRE_TIPO_DOCUMENTO = "", string NOMBRE_TIPO_OPERADOR = "", String FOTO = "", int ID_SEXO = 0, string FECHA_NACIMIENTO = "",
                                             int ID_DEPARTAMENTO_OPERADOR = 0, int ID_PROVINCIA_OPERADOR = 0, int ID_DISTRITO_OPERADOR = 0, bool REGISTRO_AGREGADO = false)
        {
            OperadorVM modelo = new OperadorVM();
            modelo.NRO_ORDEN = NumeroRegistro + 1;
            modelo.NRO_DOCUMENTO = NRO_DOCUMENTO;
            modelo.APELLIDO_PATERNO = APELLIDO_PATERNO;
            modelo.APELLIDO_MATERNO = APELLIDO_MATERNO;
            modelo.NOMBRES = NOMBRE;
            modelo.ID_TIPO_DOCUMENTO = TIPO_DOCUMENTO;
            modelo.DIRECCION = DIRECCION;
            modelo.TELEFONO_CEL = TELEFONO_CEL;
            modelo.TELEFONO_CASA = TELEFONO_CASA;
            modelo.CORREO = CORREO;
            modelo.ID_TIPO_OPERADOR = TIPO_OPERADOR;
            modelo.NRO_LICENCIA = NRO_LICENCIA;
            modelo.CATEGORIA = CATEGORIA;
            modelo.FECHA_EXPEDICION = FECHA_EXPEDICION;
            modelo.FECHA_REVALIDACION = FECHA_REVALIDACION;
            modelo.NOMBRE_TIPO_DOCUMENTO = NOMBRE_TIPO_DOCUMENTO;
            modelo.NOMBRE_TIPO_OPERADOR = NOMBRE_TIPO_OPERADOR;
            modelo.FOTO_BASE64 = FOTO;
            modelo.ID_SEXO = ID_SEXO;

            modelo.FECHA_NACIMIENTO = FECHA_NACIMIENTO;
            modelo.ID_DEPARTAMENTO_OPERADOR = ID_DEPARTAMENTO_OPERADOR;
            modelo.ID_PROVINCIA_OPERADOR = ID_PROVINCIA_OPERADOR;
            modelo.ID_DISTRITO_OPERADOR = ID_DISTRITO_OPERADOR;
            modelo.REGISTRO_AGREGADO = REGISTRO_AGREGADO;

            return PartialView(modelo);
        }
        public ActionResult CredencialOperador()
        {
            ExpedienteVM viewModelo = new ExpedienteVM();
            var comboTipoDocumento = new ParametroBLL().ConsultaParametro(EnumParametroTipo.TipoDocumento.ValorEntero());
            comboTipoDocumento.Add(new ParametroModelo() { PARCOD = 0, PARNOM = ".:Tipo de Documento:." });
            viewModelo.SelectTipoDocumento = comboTipoDocumento.OrderBy(x => x.PARCOD)
            .Select(j => new SelectListItem
            {
                Value = j.PARSEC.ToString(),
                Text = j.PARNOM,
            }).ToList();
            return PartialView(viewModelo);
        }
    }

}