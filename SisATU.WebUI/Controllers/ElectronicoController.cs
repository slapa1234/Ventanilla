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
using SisATU.Base.Constante;

namespace SisATU.WebUI.Controllers
{
    public class ElectronicoController : Controller
    {
        // GET: Electronico
        #region INDEX
        public ActionResult Index()
        {
            ExpedienteVM viewModelo = new ExpedienteVM();
            var comboTipoPersona = new ParametroBLL().ConsultaParametro(EnumParametroTipo.TipoPersona.ValorEntero());
            var comboTipoDocumento = new ParametroBLL().ConsultaParametro(EnumParametroTipo.TipoDocumento.ValorEntero());
            var comboTipoSolicitud = new ParametroBLL().ConsultaParametro(EnumParametroTipo.TipoSolicitud.ValorEntero());
            var comboTramites = new TramiteBLL().getListaTramiteByTipo(0);
            var comboTipoProcedimiento = new ModalidadServicioBLL().getProcedimientosByFiltro(Session["ID_TIPO_PERSONA"].ValorEntero(), Session["ID_MODALIDAD_SERVICIO"].ValorEntero(), Session["ID_TIPO_TRAMITE"].ValorEntero());
            var ComboEntidadBancaria = new EntidadBancariaBLL().ConsultaComboEntidadBancaria();
            var comboTipoModalidad = new ModalidadServicioBLL().ComboModalidadServicio();
            var comboTipoOperador = new ParametroBLL().ConsultaParametro(EnumParametroTipo.TipoOperador.ValorEntero());
            var comboDepartamento = new DepartamentoBLL().ComboDepartamento(0);
            var comboProvincia = new ProvinciaBLL().ComboProvincia(Session["ID_DEPARTAMENTO"].ValorEntero());
            var comboDistrito = new DistritoBLL().ComboDistrito(Session["ID_PROVINCIA"].ValorEntero());

            comboTipoPersona.Add(new ParametroModelo() { PARCOD = 0, PARNOM = ".:Tipo de Persona:." });
            comboTipoDocumento.Add(new ParametroModelo() { PARCOD = 0, PARNOM = ".:Tipo de Documento:." });
            comboTipoSolicitud.Add(new ParametroModelo() { PARCOD = 0, PARNOM = ".:Tipo de Procedimiento:." });
            comboTipoProcedimiento.Add(new ComboProcedimientoVM() { ID_PROCEDIMIENTO = 0, NOMBRE_PROCEDIMIENTO = ".:Seleccione Procedimiento:." });
            ComboEntidadBancaria.Add(new EntidadBancariaVM() { ID_ENTIDAD_BANCARIA = 0, NOMBRE = ".:Entidad Bancaria:." });
            comboTipoModalidad.Add(new ComboModalidadServicioVM() { ID_MODALIDAD_SERVICIO = 0, NOMBRE = ".:Seleccione Modalidad:." });
            comboTipoOperador.Add(new ParametroModelo() { PARCOD = 0, PARNOM = ".:Tipo Operador:." });
            comboDepartamento.Add(new ComboDepartamentoVM() { ID_DEPARTAMENTO = 0, NOMBRE_DEPARTAMENTO = ".:Seleccione Departamento:." });
            comboProvincia.Add(new ComboProvinciaVM() { ID_PROVINCIA = 0, NOMBRE_PROVINCIA = ".:Seleccione Provincia:." });
            comboDistrito.Add(new ComboDistritoVM() { ID_DISTRITO = 0, NOMBRE_DISTRITO = ".:Seleccione Distrito:." });

            viewModelo.SelectTipoPersona = comboTipoPersona.OrderBy(x => x.PARSEC)
                .Select(j => new SelectListItem
                {
                    Value = j.PARSEC.ToString(),
                    Text = j.PARNOM,
                    Selected = j.PARSEC == EnumParametro.PersonaJuridica.ValorEntero(),
                }).ToList();

            viewModelo.SelectTipoDocumento = comboTipoDocumento.OrderBy(x => x.PARSEC)
                .Select(j => new SelectListItem
                {
                    Value = j.PARSEC.ToString(),
                    Text = j.PARNOM,
                }).ToList();

            viewModelo.SelectTipoSolicitud = comboTipoSolicitud.OrderBy(x => x.PARCOD)
                .Select(j => new SelectListItem
                {
                    Value = j.PARCOD.ToString(),
                    Text = j.PARNOM,
                }).ToList();

            viewModelo.SelectTipoProcedimiento = comboTipoProcedimiento.OrderBy(x => x.ID_PROCEDIMIENTO)
                .Select(j => new SelectListItem
                {
                    Value = j.ID_PROCEDIMIENTO.ToString(),
                    Text = j.NOMBRE_PROCEDIMIENTO,
                }).ToList();

            viewModelo.SelectEntidadBancaria = ComboEntidadBancaria.OrderBy(x => x.ID_ENTIDAD_BANCARIA)
                .Select(j => new SelectListItem
                {
                    Value = j.ID_ENTIDAD_BANCARIA.ToString(),
                    Text = j.NOMBRE,
                }).ToList();

            viewModelo.SelectTipoModalidad = comboTipoModalidad.OrderBy(x => x.ID_MODALIDAD_SERVICIO)
                .Select(j => new SelectListItem
                {
                    Value = j.ID_MODALIDAD_SERVICIO.ToString(),
                    Text = j.NOMBRE,
                }).ToList();

            viewModelo.SelectTipoOperador = comboTipoOperador.OrderBy(x => x.PARCOD)
                .Select(j => new SelectListItem
                {
                    Value = j.PARCOD.ToString(),
                    Text = j.PARNOM,
                }).ToList();


            viewModelo.SelectTramite = comboTramites
            .Select(j => new SelectListItem
            {
                Value = j.ID_TIPO_TRAMITE.ToString(),
                Text = j.NOMBRE_TRAMITE,
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

            viewModelo.ID_TIPO_PERSONA = Session["ID_TIPO_PERSONA"].ValorEntero();

            switch (viewModelo.ID_TIPO_PERSONA)
            {
                case 1: //JURIDICA
                    viewModelo.RAZON_SOCIAL = Session["USUARIO"].ValorCadena();
                    viewModelo.RUC = Session["RUC"].ValorCadena();
                    viewModelo.ID_EMPRESA = Session["ID_EMPRESA"].ValorEntero();
                    viewModelo.NRO_DOCUMENTO = Session["NRO_DOCUMENTO"].ValorCadena();
                    viewModelo.NOMBRES = Session["NOMBRES"].ValorCadena();
                    viewModelo.APELLIDO_PATERNO = Session["APELLIDO_PATERNO"].ValorCadena();
                    viewModelo.APELLIDO_MATERNO = Session["APELLIDO_MATERNO"].ValorCadena();
                    viewModelo.DIRECCION = Session["DIRECCION"].ValorCadena();
                    viewModelo.DIRECCION_ACTUAL = Session["DIRECCION_ACTUAL"].ValorCadena();
                    viewModelo.NRO_DOCUMENTO = Session["NRO_DOCUMENTO"].ValorCadena();
                    viewModelo.TELEFONO = Session["TELEFONO"].ValorCadena();
                    viewModelo.CORREO = Session["CORREO"].ValorCadena();
                    viewModelo.ID_DEPARTAMENTO = Session["ID_DEPARTAMENTO"].ValorEntero();
                    viewModelo.ID_PROVINCIA = Session["ID_PROVINCIA"].ValorEntero();
                    viewModelo.ID_DISTRITO = Session["ID_DISTRITO"].ValorEntero();
                    break;
                case 2: //NATURAL
                    viewModelo.NOMBRES = Session["USUARIO"].ValorCadena();
                    viewModelo.APELLIDO_PATERNO = Session["APELLIDO_PATERNO"].ValorCadena();
                    viewModelo.APELLIDO_MATERNO = Session["APELLIDO_MATERNO"].ValorCadena();
                    viewModelo.DIRECCION = Session["DIRECCION"].ValorCadena();
                    viewModelo.DIRECCION_ACTUAL = Session["DIRECCION_ACTUAL"].ValorCadena();
                    viewModelo.NRO_DOCUMENTO = Session["NRO_DOCUMENTO"].ValorCadena();
                    viewModelo.TELEFONO = Session["TELEFONO"].ValorCadena();
                    viewModelo.CORREO = Session["CORREO"].ValorCadena();
                    viewModelo.ID_DEPARTAMENTO = Session["ID_DEPARTAMENTO"].ValorEntero();
                    viewModelo.ID_PROVINCIA = Session["ID_PROVINCIA"].ValorEntero();
                    viewModelo.ID_DISTRITO = Session["ID_DISTRITO"].ValorEntero();
                    break;
                default:
                    break;
            }

            viewModelo.ID_PERSONA = Session["ID_PERSONA"].ValorEntero();
            viewModelo.CODPAIS = Session["CODPAIS"].ValorEntero();
            viewModelo.CODDIST = Session["CODDIST"].ValorEntero();
            viewModelo.CODDPTO = Session["CODDPTO"].ValorEntero();
            viewModelo.CODPROV = Session["CODPROV"].ValorEntero();
            viewModelo.DIRECCION_STD = Session["DIRECCION_STD"].ValorCadena();
            viewModelo.ID_TIPO_DOCUMENTO = Session["ID_TIPO_DOCUMENTO"].ValorEntero();
            viewModelo.ID_MODALIDAD_SERVICIO = Session["ID_MODALIDAD_SERVICIO"].ValorEntero();
            viewModelo.ID_TIPO_TRAMITE = Session["ID_TIPO_TRAMITE"].ValorEntero();
            viewModelo.FECHA_VENCIMIENTO_EXPEDIENTE = Session["FECHA_VENCIMIENTO_EXPEDIENTE"].ValorCadena();
            viewModelo.IDUNIDAD_STD = 7;
            return View(viewModelo);
        }
        #endregion

        #region CERRAR CESSION
        public ActionResult cerrarSesion()
        {
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("Inicio", "Acceso");
        }
        #endregion

        public ActionResult ObtieneTipoProcedimiento(int ID_MODALIDAD_SERVICIO)
        {
            var comboProcedimiento = new ProcedimientoBLL().ComboProcedimientoXModalidad(ID_MODALIDAD_SERVICIO);
            comboProcedimiento.Add(new ComboProcedimientoVM() { ID_PROCEDIMIENTO = 0, NOMBRE_PROCEDIMIENTO = ".:Seleccione Procedimiento:." });
            return Json(new { resultado = comboProcedimiento.OrderBy(x => x.ID_PROCEDIMIENTO) });
        }

        public ActionResult ProcedimientoNoTUPA(string r)
        {
            ProcedimientoNOTUPAVM viewModel = new ProcedimientoNOTUPAVM();
            viewModel.NRO_RUC = r;
            return View(viewModel);
        }

        public ActionResult _ObtenerRequisitos(int ID_PROCEDIMIENTO)
        {
            var resultado = new RequisitosProcedimientosBLL().ComboRequisitosProcedimiento(ID_PROCEDIMIENTO);
            return PartialView(resultado);
        }

        public ActionResult _DetallePagos(int ID_PROCEDIMIENTO)
        {
            var resultado = new ProcedimientoBLL().ConsultarDatosProcedimientoHijo(ID_PROCEDIMIENTO, Session["ID_TIPO_PERSONA"].ValorEntero());
            var ComboEntidadBancaria = new EntidadBancariaBLL().ConsultaComboEntidadBancaria();
            ComboEntidadBancaria.Add(new EntidadBancariaVM() { ID_ENTIDAD_BANCARIA = 0, NOMBRE = ".:Entidad Bancaria:." });

            List<ReciboVM> modelo = new List<ReciboVM>();
            foreach (var item in resultado)
            {
                var REGISTRO = new ReciboVM();
                REGISTRO.ID_PROCEDIMIENTO = item.ID_PROCEDIMIENTO;
                REGISTRO.NOMBREPAGO = item.PROCEDIMIENTO_PROCEDENCIA;
                REGISTRO.SelectEntidadBancaria = ComboEntidadBancaria.OrderBy(x => x.ID_ENTIDAD_BANCARIA)
                .Select(j => new SelectListItem
                {
                    Value = j.ID_ENTIDAD_BANCARIA.ToString(),
                    Text = j.NOMBRE,
                }).ToList();
                modelo.Add(REGISTRO);
            }
            return PartialView(modelo);
        }

        public ActionResult _DetalleRequisitos(string numeroDocumento = "", string strRequisitos = "", string nroVoucher = "", int totalRequisito = 0, int totalRequisitoEntregado = 0, int idEntidadBancaria = 0)
        {
            DetalleSolicitudVM modelo = new DetalleSolicitudVM();
            modelo.DATO_REGISTRO = numeroDocumento;
            modelo.DESCRIPCION = strRequisitos;
            modelo.NUMERO_RECIBO = nroVoucher;
            modelo.PorcentajeDocumento = (totalRequisitoEntregado * 100) / totalRequisito;
            modelo.ID_ENTIDAD_BANCARIA = idEntidadBancaria;
            return PartialView(modelo);
        }

        [HttpPost]
        public ActionResult GuardarExpediente(ExpedienteVM modelo)
        {
            ResultadoProcedimientoVM resultado = new ResultadoProcedimientoVM();
            if (ModelState.IsValid)
            {
                // VALIDACIONES BASICAS

                if (Session["TIPO_EXPEDIENTE"].ValorEntero() == EnumTipoExpediente.Tupa.ValorEntero())
                {
                    if (modelo.ID_PROCEDIMIENTO == 51 || modelo.ID_PROCEDIMIENTO == 55 || modelo.ID_PROCEDIMIENTO == 29 || modelo.ID_PROCEDIMIENTO == 28)//renovacion TUC
                    {
                        if (modelo.ID_TIPO_PERSONA == EnumParametro.PersonaJuridica.ValorEntero())
                        {
                            resultado = GuardarExpediente2(modelo);
                            if (modelo.FLG_CORREO == true)
                            {
                                var rptaenvio = enviarCorreoReno_Auto(modelo.CORREO, resultado.CodAuxiliar, modelo.ID_MODALIDAD_SERVICIO, modelo.ID_TIPO_PERSONA, resultado.IDDOC_HIJO, resultado.IDDOC_PADRE);
                            }
                        }
                        else
                        {
                            resultado = CrearTarjetaUnicaCirculacion(modelo);
                            if (resultado.CodResultado == 1)
                            {
                                if (modelo.FLG_CORREO == true)
                                {
                                    var rptaenvio = enviarCorreoResolucion(modelo.CORREO, modelo.NOMBRES + ", " + modelo.APELLIDO_PATERNO + " " + modelo.APELLIDO_MATERNO, modelo.NRO_DOCUMENTO, resultado.IDDOC_HIJO, resultado.IDDOC_PADRE); //prueba envio correo
                                }
                            }
                        }
                    }
                    else if (modelo.ID_PROCEDIMIENTO == 26) // procedimiento 7.2
                    {
                        resultado = GuardarExpediente2(modelo);
                        if (resultado.CodResultado == 1)
                        {
                            if (modelo.FLG_CORREO == true)
                            {
                                var rptaenvio = EnviarCorreoConfirmacionRegistroOper(modelo.CORREO, modelo.OperadorVM, Session["USUARIO"].ToString(), "TRANSPORTE REGULAR DE PERSONAS", DateTime.Now.ValorFechaCorta(), resultado.CodAuxiliar.ToString());
                            }
                        }
                        else
                        {
                            return Json(new { success = false, mensaje = resultado.NomResultado });
                        }

                    }
                    else if (modelo.ID_PROCEDIMIENTO == 28 || modelo.ID_PROCEDIMIENTO == 34 || modelo.ID_PROCEDIMIENTO == 47 || modelo.ID_PROCEDIMIENTO == 52)//RA
                    {
                        resultado = GuardarExpediente2(modelo);

                        if (resultado.CodResultado == 1)
                        {
                            if (modelo.FLG_CORREO == true)

                            {
                                if (modelo.ID_TIPO_PERSONA == 1)
                                {
                                    var rptaenvio = enviarCorreoReno_Auto(modelo.CORREO, resultado.CodAuxiliar, modelo.ID_MODALIDAD_SERVICIO, modelo.ID_TIPO_PERSONA, resultado.IDDOC_HIJO, resultado.IDDOC_PADRE);
                                }
                                else
                                {
                                    var rptaenvio = enviarCorreoReno_Auto(modelo.CORREO, resultado.CodAuxiliar, modelo.ID_MODALIDAD_SERVICIO, modelo.ID_TIPO_PERSONA, resultado.IDDOC_HIJO, resultado.IDDOC_PADRE);
                                }

                            }
                        }
                    }
                    else if ((Procedimiento.CREDOPE.BuscaValorArray(modelo.ID_PROCEDIMIENTO)) || (Procedimiento.DUPOPE.BuscaValorArray(modelo.ID_PROCEDIMIENTO)))  //procedimiento emitir credencial
                    {
                        resultado = GuardarExpediente2(modelo);
                        if (resultado.CodResultado == 1)
                        {
                            if (modelo.FLG_CORREO == true)
                            {
                                var rptaenvio = EnviarCorreoCredencial(modelo.CORREO, modelo.OperadorVM, modelo.ID_MODALIDAD_SERVICIO_OPERADOR, modelo.ID_TIPO_OPERADOR, Session["USUARIO"].ToString(), "TRANSPORTE REGULAR DE PERSONAS", DateTime.Now.ValorFechaCorta(), resultado.CodAuxiliar.ToString(), modelo.ID_TIPO_PERSONA);

                            }
                        }
                        else
                        {
                            return Json(new { success = false, mensaje = resultado.NomResultado });
                        }
                    }
                    else if (modelo.ID_PROCEDIMIENTO == 30 || modelo.ID_PROCEDIMIENTO == 37 || modelo.ID_PROCEDIMIENTO == 40 || modelo.ID_PROCEDIMIENTO == 50 || modelo.ID_PROCEDIMIENTO == 57)
                    {
                        resultado = CrearDuplicadoTUC(modelo);
                        if (resultado.CodResultado == 1)
                        {
                            if (modelo.FLG_CORREO == true)
                            {
                                var rptaenvio = enviarCorreoDuplicado(modelo.CORREO, modelo.NOMBRES + ", " + modelo.APELLIDO_PATERNO + " " + modelo.APELLIDO_MATERNO, modelo.NRO_DOCUMENTO, resultado.IDDOC_HIJO); //prueba envio correo
                            }
                        }
                        else
                        {
                            return Json(new { success = false, mensaje = resultado.NomResultado });
                        }
                    }
                    else
                    {
                        resultado = CrearRequisitos(modelo);
                    }
                }
                else
                {
                    resultado = CrearExpedienteCita(modelo);
                    if (resultado.CodResultado == 1)
                    {
                        if (modelo.FLG_CORREO == true)
                        {
                            var rptaenvio = EnviarCorreoBase(modelo.CORREO, resultado.CodAuxiliar.ValorCadena());
                        }
                    }
                }



                if (resultado.CodResultado == 0)
                {
                    return Json(new { success = false, mensaje = resultado.NomResultado });
                }

                return Json(new { success = true, mensaje = resultado.NomResultado });
            }
            else
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y => y.Count > 0)
                           .ToList();
            }
            return Json(new { success = true, mensaje = resultado.NomResultado });
        }

        #region GUARDAR TARJETA UNICA DE CIRCULACION (TUC)
        public ResultadoProcedimientoVM CrearTarjetaUnicaCirculacion(ExpedienteVM modelo)
        {
            ResultadoProcedimientoVM resultado = new ResultadoProcedimientoVM();

            #region Expediente
            ExpedienteModelo expediente = new ExpedienteModelo();
            expediente.ID_PROCEDIMIENTO = modelo.ID_PROCEDIMIENTO;
            expediente.ID_MODALIDAD_SERVICIO = Session["ID_MODALIDAD_SERVICIO"].ValorEntero();
            expediente.ID_SOLICITUD = EnumParametro.ProcedimientoTupa.ValorEntero();
            expediente.NUMERO_SOLICITANTE = modelo.NUMERO_SOLICITANTE;
            expediente.NUMERO_RECURRENTE = modelo.NUMERO_RECURRENTE;
            expediente.ID_ESTADO = modelo.ID_ESTADO;
            expediente.USUARIO_REG = Session["USUARIO"].ValorCadena();
            #endregion

            #region Persona
            PersonaModelo persona = new PersonaModelo();
            persona.ID_PERSONA = modelo.ID_PERSONA;
            persona.NRO_DOCUMENTO = modelo.NRO_DOCUMENTO;
            persona.ID_TIPO_PERSONA = Session["ID_TIPO_PERSONA"].ValorEntero();
            persona.APELLIDO_PATERNO = modelo.APELLIDO_PATERNO;
            persona.APELLIDO_MATERNO = modelo.APELLIDO_MATERNO;
            persona.NOMBRES = modelo.NOMBRES;

            persona.ID_TIPO_DOCUMENTO = Session["ID_TIPO_DOCUMENTO"].ValorEntero();  //modelo.ID_TIPO_DOCUMENTO,
            persona.RAZON_SOCIAL = modelo.RAZON_SOCIAL;
            persona.ID_DEPARTAMENTO = modelo.ID_DEPARTAMENTO;
            persona.ID_PROVINCIA = modelo.ID_PROVINCIA;
            persona.ID_DISTRITO = modelo.ID_DISTRITO;

            if (modelo.DIRECCION_ACTUAL == null)
            {
                resultado.CodResultado = 0;
                resultado.NomResultado = "Ingresar Dirección del Solicitante";
                return resultado;
            }
            persona.DIRECCION = modelo.DIRECCION;
            persona.DIRECCION_ACTUAL = modelo.DIRECCION_ACTUAL;
            persona.TELEFONO = modelo.TELEFONO;
            if (modelo.CORREO == null)
            {
                resultado.CodResultado = 0;
                resultado.NomResultado = "Ingresar Correo del Solicitante";
                return resultado;
            }
            persona.CORREO = modelo.CORREO;
            persona.USUARIO_REG = Session["USUARIO"].ValorCadena();
            #endregion

            #region Recibo
            List<ReciboModelo> detalleRecibo = new List<ReciboModelo>();

            foreach (var item in modelo.ReciboVM)
            {
                var resultadoComprobante = new ReciboBLL().BuscarRecibo(item.NUMERO_RECIBO.ValorCadena());

                if (item.ID_ENTIDAD_BANCARIA == 0)
                {
                    resultado.CodResultado = 0;
                    resultado.NomResultado = "Elegir una Entidad bancaria.";
                    return resultado;
                }

                if (String.IsNullOrEmpty(item.NUMERO_RECIBO))
                {
                    resultado.CodResultado = 0;
                    resultado.NomResultado = "Ingresar Nro de Comprobante.";
                    return resultado;
                }

                if (resultadoComprobante.NUMERO_RECIBO != null)
                {
                    resultado.CodResultado = 0;
                    resultado.NomResultado = "El Voucher " + resultadoComprobante.NUMERO_RECIBO + "ya fue registrado el " + resultadoComprobante.FECHA_EMISION.ValorFechaCorta();
                    return resultado;
                }

                var registro = new ReciboModelo();
                registro.ID_ENTIDAD_BANCARIA = item.ID_ENTIDAD_BANCARIA;
                registro.NUMERO_RECIBO = item.NUMERO_RECIBO;
                registro.FECHA_EMISION = Convert.ToDateTime(item.FECHA_EMISION);
                registro.USUARIO_REG = Session["USUARIO"].ValorCadena();
                detalleRecibo.Add(registro);
            }
            #endregion

            #region STD
            STDVM STD = new STDVM();

            STD.IDUNIDAD_STD = modelo.IDUNIDAD_STD;
            STD.CODPAIS = modelo.CODPAIS;
            STD.CODDPTO = modelo.CODDPTO;
            STD.CODPROV = modelo.CODPROV;
            STD.CODDIST = modelo.CODDIST;
            STD.DIRECCION_STD = modelo.DIRECCION_ACTUAL;
            STD.NOMBRE = modelo.NOMBRES;
            STD.ID_PROCEDIMIENTO = modelo.ID_PROCEDIMIENTO;
            STD.ID_PROVEEDOR = modelo.ID_EMPRESA;
            //Proceso TUPA STD
            STD.TIPO_EXPEDIENTE = Session["TIPO_EXPEDIENTE"].ValorEntero();
            STD.OBSERVACION = Session["OBSERVACION"].ValorCadena();

            #endregion

            #region Empresa
            EmpresaModelo empresa = new EmpresaModelo()
            {
                RUC = modelo.RUC,
                RAZON_SOCIAL = modelo.RAZON_SOCIAL,
            };
            #endregion

            #region Vehiculo
            VehiculoModelo vehiculo = new VehiculoModelo();
            {
                vehiculo.ID_VEHICULO = modelo.ID_VEHICULO;
                if (String.IsNullOrEmpty(modelo.NroPlaca))
                {
                    resultado.CodResultado = 0;
                    resultado.NomResultado = "Ingresar una Placa.";
                    return resultado;
                }
                vehiculo.PLACA = modelo.NroPlaca;
                vehiculo.ID_MODALIDAD_SERVICIO = modelo.ID_MODALIDAD_SERVICIO;
                if (modelo.ID_CLASE_VEHICULO == 0)
                {
                    resultado.CodResultado = 0;
                    resultado.NomResultado = "Ingresar una Clase del Vehiculo.";
                    return resultado;
                }
                vehiculo.ID_CLASE_VEHICULO = modelo.ID_CLASE_VEHICULO;
                if (modelo.ID_MODELO == 0)
                {
                    resultado.CodResultado = 0;
                    resultado.NomResultado = "Ingresar una Modelo del Vehiculo.";
                    return resultado;
                }
                vehiculo.ID_MODELO = modelo.ID_MODELO;
                if (modelo.ID_TIPO_COMBUSTIBLE == 0)
                {
                    resultado.CodResultado = 0;
                    resultado.NomResultado = "Ingresar el Tipo de Combustible.";
                    return resultado;
                }
                vehiculo.ID_TIPO_COMBUSTIBLE = modelo.ID_TIPO_COMBUSTIBLE;
                if (modelo.ID_CATEGORIA_VEHICULO == 0)
                {
                    resultado.CodResultado = 0;
                    resultado.NomResultado = "Ingresar el Categoria del Vehiculo.";
                    return resultado;
                }

                vehiculo.ID_CATEGORIA_VEHICULO = modelo.ID_CATEGORIA_VEHICULO;
                vehiculo.ANIO_FABRICACION = modelo.ANIO_FABRICACION;
                vehiculo.SERIE = modelo.SERIE;
                vehiculo.SERIE_MOTOR = modelo.SERIE_MOTOR;
                vehiculo.PESO_SECO = modelo.PESO_SECO.ValorEntero();
                vehiculo.PESO_BRUTO = modelo.PESO_BRUTO.ValorEntero();
                vehiculo.LONGITUD = modelo.LONGITUD.ValorEntero();
                vehiculo.ALTURA = modelo.ALTURA.ValorEntero();
                vehiculo.ANCHO = modelo.ANCHO.ValorEntero();
                vehiculo.CARGA_UTIL = modelo.CARGA_UTIL.ValorEntero();
                vehiculo.CAPACIDAD_PASAJERO = modelo.CAPACIDAD_PASAJERO.ValorEntero();
                vehiculo.NUMERO_ASIENTOS = modelo.NUMERO_ASIENTOS.ValorEntero();
                vehiculo.NUMERO_RUEDA = modelo.NUMERO_RUEDA.ValorEntero();
                vehiculo.NUMERO_EJE = modelo.NUMERO_EJE.ValorEntero();
                vehiculo.NUMERO_PUERTA = modelo.NUMERO_PUERTA.ValorEntero();
                vehiculo.FECHA_INSCRIPCION = "";
                vehiculo.CILINDRADA = modelo.CILINDRADA;
                vehiculo.OBSERVACION = modelo.Observacion;
                if (modelo.ID_MARCA == 0)
                {
                    resultado.CodResultado = 0;
                    resultado.NomResultado = "Ingresar una Marca del Vehiculo";
                    return resultado;
                }
                vehiculo.ID_MARCA = modelo.ID_MARCA;
                vehiculo.USUARIO_REG = Session["USUARIO"].ValorCadena();
            };
            #endregion

            #region Tarjeta Propiedad
            TarjetaPropiedadModelo tarjetaPropietario = new TarjetaPropiedadModelo()
            {
                DESDE = modelo.FECHA_INICIO_PROPIETARIO,
                HASTA = modelo.FECHA_FIN_PROPIETARIO,
                NRO_TARJETA = modelo.NRO_TARJETA_PROPIETARIO,
                USUARIO_REG = Session["USUARIO"].ValorCadena()
            };
            #endregion

            #region Propietario
            PropietarioModelo propietario = new PropietarioModelo()
            {
                NRO_DOCUMENTO = modelo.NUMERO_DOCUMENTO_PROPIEDAD,
                ID_TIPO_DOCUMENTO = modelo.ID_TIPO_DOCUMENTO_PROPIETARIO,
                ID_TIPO_CONTRIBUYENTE = 1,
                NOMBRE_PROPIETARIO = modelo.NOMBRE_PROPIETARIO,
                USUARIO_REG = Session["USUARIO"].ValorCadena(),
            };
            #endregion

            TarjetaCirculacionModelo tarjetaCirculacion = new TarjetaCirculacionModelo()
            {
                FECHA_IMPRESION = DateTime.Now.ValorFechaCorta(),
                ANIO = DateTime.Now.Year,
                //FECHA_VENCIMIENTO_DOCUMENTO = (DateTime.Now.AddYears(5)).ValorFechaCorta(),
                USUARIO_REG = Session["USUARIO"].ValorCadena(),
            };

            VehiculoAseguradoraModelo vehiculoAseguradora = new VehiculoAseguradoraModelo();
            vehiculoAseguradora.NOMBRE_ASEGURADORA = modelo.NOMBRE_ASEGURADORA;
            if (modelo.ID_TIPO_SEGURO == EnumTipoSeguro.SOAT.ValorEntero())
            {
                if (String.IsNullOrEmpty(modelo.NOMBRE_ASEGURADORA))
                {

                    resultado.CodResultado = 0;
                    resultado.NomResultado = "Ingresar Nombre de la Aseguradora";
                    return resultado;
                }

                if (String.IsNullOrEmpty(modelo.POLIZA))
                {
                    resultado.CodResultado = 0;
                    resultado.NomResultado = "Ingresar Nro. Poliza de la Aseguradora";
                    return resultado;
                }
            }
            vehiculoAseguradora.ID_TIPO_SEGURO = modelo.ID_TIPO_SEGURO;
            vehiculoAseguradora.POLIZA = modelo.POLIZA;
            vehiculoAseguradora.FEC_INI_VIGENCIA = modelo.SeguroFechaInicio;

            if (Convert.ToDateTime(modelo.SeguroFechaFin) <= Convert.ToDateTime(DateTime.Now.ValorFechaCorta()))
            {
                resultado.CodResultado = 0;
                resultado.NomResultado = "Seguro se encuentra vencido";
                return resultado;
            }
            vehiculoAseguradora.FEC_FIN_VIGENCIA = modelo.SeguroFechaFin;
            vehiculoAseguradora.USUARIO_REG = Session["USUARIO"].ValorCadena();

            VehiculoCITVModelo vehiculoCITV = new VehiculoCITVModelo();
            vehiculoCITV.CERTIFICADORA_CITV = modelo.CERTIFICADORA_CITV;
            vehiculoCITV.NRO_CERTIFICADO = modelo.NRO_CERTIFICADO;
            vehiculoCITV.FECHA_CERTIFICADO = modelo.FECHA_INICIO_CITV;
            if (Convert.ToDateTime(modelo.FECHA_FIN_CITV) <= Convert.ToDateTime(DateTime.Now.ValorFechaCorta()))
            {
                resultado.CodResultado = 0;
                resultado.NomResultado = "CITV se encuentra vencido";
                return resultado;
            }
            vehiculoCITV.FECHA_VENCIMIENTO = modelo.FECHA_FIN_CITV;
            vehiculoCITV.RESULTADO = "";
            vehiculoCITV.ESTADO_CITV = "1";
            vehiculoCITV.USUARIO_REG = Session["USUARIO"].ValorCadena();

            ResolucionModelo resolucion = new ResolucionModelo();

            resolucion.ID_TIPO_AUTORIZACION = 1;
            resolucion.FECHA_AUTORIZACION = DateTime.Now.ValorFechaCorta();
            resolucion.FECHA_VIGENCIA = (DateTime.Parse("31/12/" + modelo.ANIO_FABRICACION).AddYears(modelo.ANIO_RENOVACION + 1).ValorFechaCorta()).ValorCadena();
            resolucion.ID_TIPO_RESOLUCION = 1;
            resolucion.FECHA_NOTIFICACION = DateTime.Now.ValorFechaCorta();
            resolucion.ASUNTO = "SOLICITO RENOVACION.";
            resolucion.USUARIO_REG = Session["USUARIO"].ValorCadena();


            ResolucionExpedienteModelo resolucionExpediente = new ResolucionExpedienteModelo();
            resolucionExpediente.DESDE_FECHA = DateTime.Now.ValorFechaCorta();
            //resolucionExpediente.HASTA_FECHA = DateTime.Parse(modelo.ANIO_FABRICACION + modelo.ANIO_RENOVACION).ValorCadena();
            resolucionExpediente.USUARIO_REG = Session["USUARIO"].ValorCadena();

            List<DetalleSolicitudModelo> detalleSolicitud = new List<DetalleSolicitudModelo>();

            foreach (var item in modelo.DetalleSolicitudVM)
            {
                var registro = new DetalleSolicitudModelo();
                registro.NUMERO_RECIBO = item.NUMERO_RECIBO;
                registro.DATO_REGISTRO = item.DATO_REGISTRO;
                registro.DESCRIPCION = item.DESCRIPCION;
                registro.ID_ENTIDAD_BANCARIA = item.ID_ENTIDAD_BANCARIA;
                registro.USU_REG = Session["USUARIO"].ValorCadena();
                detalleSolicitud.Add(registro);
            }

            resultado = new ExpedienteBLL().CrearExpedienteTUC(expediente, empresa, persona, detalleRecibo, STD, vehiculo, tarjetaPropietario, propietario, tarjetaCirculacion, resolucion, resolucionExpediente, vehiculoAseguradora, vehiculoCITV);

            return resultado;

        }
        #endregion

        #region Setear Valores
        public ResultadoProcedimientoVM GuardarExpediente2(ExpedienteVM modelo)
        {
            ResultadoProcedimientoVM resultadoExpediente = new ResultadoProcedimientoVM();

            #region EXPEDIENTE
            ExpedienteModelo expediente = new ExpedienteModelo()
            {
                ID_PROCEDIMIENTO = modelo.ID_PROCEDIMIENTO,
                ID_MODALIDAD_SERVICIO = modelo.ID_MODALIDAD_SERVICIO,
                ID_SOLICITUD = EnumParametro.ProcedimientoTupa.ValorEntero(),
                NUMERO_SOLICITANTE = modelo.NUMERO_SOLICITANTE,
                NUMERO_RECURRENTE = modelo.NUMERO_RECURRENTE,
                ID_ESTADO = modelo.ID_ESTADO,
                USUARIO_REG = Session["USUARIO"].ValorCadena()
            };
            #endregion

            #region PERSONA
            PersonaModelo persona = new PersonaModelo()
            {
                ID_PERSONA = modelo.ID_PERSONA,
                NRO_DOCUMENTO = modelo.NRO_DOCUMENTO,
                ID_TIPO_PERSONA = modelo.ID_TIPO_PERSONA,
                APELLIDO_PATERNO = modelo.APELLIDO_PATERNO,
                APELLIDO_MATERNO = modelo.APELLIDO_MATERNO,
                NOMBRES = modelo.NOMBRES,
                ID_TIPO_DOCUMENTO = Session["ID_TIPO_DOCUMENTO"].ValorEntero(),
                RAZON_SOCIAL = modelo.RAZON_SOCIAL,
                DIRECCION = modelo.DIRECCION,
                DIRECCION_ACTUAL = modelo.DIRECCION_ACTUAL,
                TELEFONO = modelo.TELEFONO,
                CORREO = modelo.CORREO,
                USUARIO_REG = Session["USUARIO"].ValorCadena(),
                ID_DEPARTAMENTO = modelo.ID_DEPARTAMENTO,
                ID_PROVINCIA = modelo.ID_PROVINCIA,
                ID_DISTRITO = modelo.ID_DISTRITO,
            };

            #endregion

            #region EMPRESA
            EmpresaModelo empresa = new EmpresaModelo()
            {
                RUC = modelo.RUC,
                RAZON_SOCIAL = modelo.RAZON_SOCIAL,
            };
            #endregion

            #region OPERADOR
            OperadorModelo operador = new OperadorModelo()
            {
                NRO_DOCUMENTO = modelo.NRO_DOCUMENTO_OPERADOR,
                ID_TIPO_PERSONA = modelo.ID_TIPO_PERSONA,
                APELLIDO_PATERNO = modelo.APELLIDO_PATERNO_OPERADOR,
                APELLIDO_MATERNO = modelo.APELLIDO_MATERNO_OPERADOR,
                NOMBRE = modelo.NOMBRE_OPERADOR,
                ID_TIPO_DOCUMENTO = modelo.ID_TIPO_DOCUMENTO_OPERADOR,
                DIRECCION = modelo.DIRECCION_OPERADOR,
                TELEFONO_CEL = modelo.TELEFONO_CEL_OPERADOR,
                TELEFONO_CASA = modelo.TELEFONO_CASA_OPERADOR,
                CORREO = modelo.CORREO_OPERADOR,
                ID_TIPO_OPERADOR = modelo.ID_TIPO_OPERADOR,
                ID_MODALIDAD_SERVICIO = modelo.ID_MODALIDAD_SERVICIO,
                FECHA_INSCRIPCION = DateTime.Now.ValorFechaCorta(),
                AÑO = DateTime.Now.Year,
                NRO_LICENCIA = modelo.NRO_LICENCIA_OPERADOR,
                CATEGORIA = modelo.CATEGORIA_OPERADOR,
                FECHA_EXPEDICION = modelo.FECHA_EXPEDICION_OPERADOR,
                FECHA_REVALIDACION = modelo.FECHA_REVALIDACION_OPERADOR,
                FEC_NAC = modelo.FECHA_NACIMIENTO_OPERADOR,
                FOTO_OPERADOR = modelo.FOTO_OPERADOR,
                FOTO_BASE64 = modelo.FOTO_OPERADOR,
                ID_SEXO = modelo.ID_SEXO,
                ID_DEPARTAMENTO_OPERADOR = modelo.ID_DEPARTAMENTO_OPERADOR,
                ID_PROVINCIA_OPERADOR = modelo.ID_PROVINCIA_OPERADOR,
                ID_DISTRITO_OPERADOR = modelo.ID_DISTRITO_OPERADOR,
            };
            #endregion

            #region DETALLE RECIBO

            List<ReciboModelo> detalleRecibo = new List<ReciboModelo>();
            foreach (var item in modelo.ReciboVM)
            {
                var registro = new ReciboModelo();
                registro.ID_ENTIDAD_BANCARIA = item.ID_ENTIDAD_BANCARIA;
                registro.NUMERO_RECIBO = item.NUMERO_RECIBO;
                registro.FECHA_EMISION = Convert.ToDateTime(item.FECHA_EMISION);
                registro.USUARIO_REG = Session["USUARIO"].ValorCadena();
                registro.ID_EXPEDIENTE = item.ID_EXPEDIENTE;
                registro.ID_PROCEDIMIENTO = item.ID_PROCEDIMIENTO;
                detalleRecibo.Add(registro);
            }
            #endregion

            #region STD
            STDVM STD = new STDVM()
            {
                IDUNIDAD_STD = modelo.IDUNIDAD_STD,
                CODPAIS = modelo.CODPAIS,
                CODDPTO = modelo.CODDPTO,
                CODPROV = modelo.CODPROV,
                CODDIST = modelo.CODDIST,
                DIRECCION_STD = modelo.DIRECCION_ACTUAL,
                NOMBRE = modelo.NOMBRES,
                ID_PROCEDIMIENTO = modelo.ID_PROCEDIMIENTO,
                ID_PROVEEDOR = modelo.ID_EMPRESA,
                //Proceso TUPA STD
                TIPO_EXPEDIENTE = 3,
                OBSERVACION = "PROCEDIMIENTO TUPA",
            };
            #endregion

            #region  DETALLE SOLICITUD
            List<DetalleSolicitudModelo> detalleSolicitud = new List<DetalleSolicitudModelo>();
            foreach (var item in modelo.DetalleSolicitudVM)
            {
                var registro = new DetalleSolicitudModelo();
                registro.NUMERO_RECIBO = item.NUMERO_RECIBO;
                registro.DATO_REGISTRO = item.DATO_REGISTRO;
                registro.DESCRIPCION = item.DESCRIPCION;
                registro.ID_ENTIDAD_BANCARIA = item.ID_ENTIDAD_BANCARIA;
                registro.USU_REG = Session["USUARIO"].ValorCadena();
                detalleSolicitud.Add(registro);
            }
            #endregion

            #region CREDENCIAL OPERADOR
            CredencialOperadorModelo credencialOperador = new CredencialOperadorModelo();
            {
                credencialOperador.FECHA_IMPRESION = DateTime.Now.ValorFechaCorta();
                credencialOperador.FECHA_ENTREGA = DateTime.Now.ValorFechaCorta();
                credencialOperador.ANIO_CREDENCIAL = DateTime.Now.Year;
                credencialOperador.ID_TIPO_DOCUMENTO_OPERADOR = modelo.ID_TIPO_DOCUMENTO_OPERADOR;
                credencialOperador.NUMERO_DOCUMENTO_OPERADOR = modelo.NRO_DOCUMENTO_OPERADOR;
                credencialOperador.FECHA_INICIO = DateTime.Now.ValorFechaCorta();
                credencialOperador.USUARIO_REG = Session["USUARIO"].ValorCadena();
                if (Procedimiento.DUPOPE.BuscaValorArray(modelo.ID_PROCEDIMIENTO))
                {
                    credencialOperador.ID_TIPO_CREDENCIAL = EnumTipoCredencial.DUPLICADO.ValorEntero();
                }
                else
                {
                    credencialOperador.ID_TIPO_CREDENCIAL = modelo.ID_TIPO_CREDENCIAL;
                }

            };

            #endregion

            #region DETALLE OPERADOR

            List<OperadorModelo> detalleOperador = new List<OperadorModelo>();
            foreach (var item in modelo.OperadorVM)
            {
                if ((item.ID_OPERADOR == 0) && (item.REGISTRO_AGREGADO == true))
                {
                    var registro = new OperadorModelo();
                    registro.NRO_DOCUMENTO = item.NRO_DOCUMENTO;
                    registro.ID_TIPO_PERSONA = modelo.ID_TIPO_PERSONA;
                    registro.APELLIDO_PATERNO = item.APELLIDO_PATERNO;
                    registro.APELLIDO_MATERNO = item.APELLIDO_MATERNO;
                    registro.NOMBRE = item.NOMBRES;
                    registro.ID_TIPO_DOCUMENTO = item.ID_TIPO_DOCUMENTO;
                    registro.DIRECCION = item.DIRECCION;
                    registro.TELEFONO_CEL = item.TELEFONO_CEL;
                    registro.TELEFONO_CASA = item.TELEFONO_CASA;
                    registro.CORREO = item.CORREO;
                    registro.ID_TIPO_OPERADOR = item.ID_TIPO_OPERADOR;
                    registro.ID_MODALIDAD_SERVICIO = modelo.ID_MODALIDAD_SERVICIO;
                    registro.FECHA_INSCRIPCION = DateTime.Now.ValorFechaCorta();
                    registro.AÑO = DateTime.Now.Year;
                    registro.NRO_LICENCIA = item.NRO_LICENCIA;
                    registro.CATEGORIA = item.CATEGORIA;
                    registro.FECHA_EXPEDICION = item.FECHA_EXPEDICION;
                    registro.FECHA_REVALIDACION = item.FECHA_REVALIDACION;
                    registro.RESTRICCION = item.RESTRICCION;
                    registro.FOTO_OPERADOR = item.FOTO_BASE64;
                    registro.ESTADO_LICENCIA = item.ESTADO_LICENCIA;
                    registro.ID_SEXO = item.ID_SEXO;
                    registro.FEC_NAC = item.FECHA_NACIMIENTO;
                    registro.FOTO_BASE64 = item.FOTO_BASE64;
                    registro.USUARIO_REG = Session["USUARIO"].ValorCadena();
                    registro.ID_DEPARTAMENTO_OPERADOR = item.ID_DEPARTAMENTO_OPERADOR;
                    registro.ID_DISTRITO_OPERADOR = item.ID_DISTRITO_OPERADOR;
                    registro.ID_PROVINCIA_OPERADOR = item.ID_PROVINCIA_OPERADOR;

                    if (registro.ID_TIPO_OPERADOR != 2)
                    {
                        if (Convert.ToDateTime(item.FECHA_REVALIDACION) < Convert.ToDateTime(DateTime.Now.ValorFechaCorta()))
                        {
                            resultadoExpediente.CodResultado = 0;
                            resultadoExpediente.NomResultado = "La Fecha Revalidacion esta Vencida.";
                            return resultadoExpediente;
                        }
                    }
                    detalleOperador.Add(registro);
                }
            }
            #endregion

            #region RESOLUCION EXPEDIENTE
            ResolucionExpedienteModelo resolucionExpediente = new ResolucionExpedienteModelo()
            {
                DESDE_FECHA = DateTime.Now.ValorFechaCorta(),
                //HASTA_FECHA = DateTime.Now.AddYears(5).ValorFechaCorta(),
                USUARIO_REG = Session["USUARIO"].ValorCadena(),
            };
            #endregion

            #region RESOLUCION
            ResolucionModelo resolucion = new ResolucionModelo()
            {
                ID_TIPO_AUTORIZACION = 1,
                FECHA_AUTORIZACION = DateTime.Now.ValorFechaCorta(),
                //FECHA_VIGENCIA = DateTime.Now.AddYears(5).ValorFechaCorta(),
                ID_TIPO_RESOLUCION = 1,
                FECHA_NOTIFICACION = DateTime.Now.ValorFechaCorta(),
                ASUNTO = "SOLICITO RENOVACION.",
                USUARIO_REG = Session["USUARIO"].ValorCadena(),
            };
            #endregion
            resultadoExpediente = new ExpedienteBLL().CrearExpediente2(expediente, empresa, persona, detalleRecibo, STD, detalleSolicitud, resolucion, resolucionExpediente, detalleOperador, operador, credencialOperador);

            return resultadoExpediente;
        }
        #endregion

        #region Guardar Requisitos
        public ResultadoProcedimientoVM CrearRequisitos(ExpedienteVM modelo)
        {
            ResultadoProcedimientoVM resultado = new ResultadoProcedimientoVM();

            #region Expediente
            ExpedienteModelo expediente = new ExpedienteModelo();
            expediente.ID_PROCEDIMIENTO = modelo.ID_PROCEDIMIENTO;
            expediente.ID_MODALIDAD_SERVICIO = Session["ID_MODALIDAD_SERVICIO"].ValorEntero();
            expediente.ID_SOLICITUD = EnumParametro.ProcedimientoTupa.ValorEntero();
            expediente.NUMERO_SOLICITANTE = modelo.NUMERO_SOLICITANTE;
            expediente.NUMERO_RECURRENTE = modelo.NUMERO_RECURRENTE;
            expediente.ID_ESTADO = modelo.ID_ESTADO;
            expediente.USUARIO_REG = Session["USUARIO"].ValorCadena();
            #endregion

            #region Persona
            PersonaModelo persona = new PersonaModelo();
            persona.ID_PERSONA = modelo.ID_PERSONA;
            persona.NRO_DOCUMENTO = modelo.NRO_DOCUMENTO;
            persona.ID_TIPO_PERSONA = Session["ID_TIPO_PERSONA"].ValorEntero();
            persona.APELLIDO_PATERNO = modelo.APELLIDO_PATERNO;
            persona.APELLIDO_MATERNO = modelo.APELLIDO_MATERNO;
            persona.NOMBRES = modelo.NOMBRES;
            persona.ID_TIPO_DOCUMENTO = Session["ID_TIPO_DOCUMENTO"].ValorEntero();  //modelo.ID_TIPO_DOCUMENTO,
            persona.RAZON_SOCIAL = modelo.RAZON_SOCIAL;
            if (modelo.DIRECCION_ACTUAL == null)
            {
                resultado.CodResultado = 0;
                resultado.NomResultado = "Ingresar Dirección del Solicitante";
                return resultado;
            }
            persona.DIRECCION = modelo.DIRECCION_ACTUAL;
            persona.TELEFONO = modelo.TELEFONO;
            if (modelo.CORREO == null)
            {
                resultado.CodResultado = 0;
                resultado.NomResultado = "Ingresar Correo del Solicitante";
                return resultado;
            }
            persona.CORREO = modelo.CORREO;
            persona.USUARIO_REG = Session["USUARIO"].ValorCadena();
            persona.ID_DEPARTAMENTO = modelo.ID_DEPARTAMENTO;
            persona.ID_PROVINCIA = modelo.ID_PROVINCIA;
            persona.ID_DISTRITO = modelo.ID_DISTRITO;
            #endregion

            #region Recibo
            List<ReciboModelo> detalleRecibo = new List<ReciboModelo>();

            foreach (var item in modelo.ReciboVM)
            {
                var resultadoComprobante = new ReciboBLL().BuscarRecibo(item.NUMERO_RECIBO.ValorCadena());

                if (item.ID_ENTIDAD_BANCARIA == 0)
                {
                    resultado.CodResultado = 0;
                    resultado.NomResultado = "Elegir una Entidad bancaria.";
                    return resultado;
                }

                if (String.IsNullOrEmpty(item.NUMERO_RECIBO))
                {
                    resultado.CodResultado = 0;
                    resultado.NomResultado = "Ingresar Nro de Comprobante.";
                    return resultado;
                }

                if (resultadoComprobante.NUMERO_RECIBO != null)
                {
                    resultado.CodResultado = 0;
                    resultado.NomResultado = "El Voucher " + resultadoComprobante.NUMERO_RECIBO + "ya fue registrado el " + resultadoComprobante.FECHA_EMISION.ValorFechaCorta();
                    return resultado;
                }

                var registro = new ReciboModelo();
                registro.ID_ENTIDAD_BANCARIA = item.ID_ENTIDAD_BANCARIA;
                registro.NUMERO_RECIBO = item.NUMERO_RECIBO;
                registro.FECHA_EMISION = Convert.ToDateTime(item.FECHA_EMISION);
                registro.USUARIO_REG = Session["USUARIO"].ValorCadena();
                detalleRecibo.Add(registro);
            }
            #endregion

            #region STD
            STDVM STD = new STDVM();

            STD.IDUNIDAD_STD = modelo.IDUNIDAD_STD;
            STD.CODPAIS = modelo.CODPAIS;
            STD.CODDPTO = modelo.CODDPTO;
            STD.CODPROV = modelo.CODPROV;
            STD.CODDIST = modelo.CODDIST;
            STD.DIRECCION_STD = modelo.DIRECCION_ACTUAL;
            STD.NOMBRE = modelo.NOMBRES;
            STD.ID_PROCEDIMIENTO = modelo.ID_PROCEDIMIENTO;
            STD.ID_PROVEEDOR = modelo.ID_EMPRESA;
            //Proceso TUPA STD
            STD.TIPO_EXPEDIENTE = Session["TIPO_EXPEDIENTE"].ValorEntero();
            STD.OBSERVACION = Session["OBSERVACION"].ValorCadena();

            #endregion

            #region Empresa
            EmpresaModelo empresa = new EmpresaModelo()
            {
                RUC = modelo.RUC,
                RAZON_SOCIAL = modelo.RAZON_SOCIAL,
            };
            #endregion
            List<DetalleSolicitudModelo> detalleSolicitud = new List<DetalleSolicitudModelo>();

            foreach (var item in modelo.DetalleSolicitudVM)
            {
                var registro = new DetalleSolicitudModelo();
                registro.NUMERO_RECIBO = item.NUMERO_RECIBO;
                registro.DATO_REGISTRO = item.DATO_REGISTRO;
                registro.DESCRIPCION = item.DESCRIPCION;
                registro.ID_ENTIDAD_BANCARIA = item.ID_ENTIDAD_BANCARIA;
                registro.USU_REG = Session["USUARIO"].ValorCadena();
                detalleSolicitud.Add(registro);
            }
            resultado = new ExpedienteBLL().CrearRequisitos(expediente, empresa, persona, detalleRecibo, STD, detalleSolicitud);

            //resultado = new ExpedienteBLL().CrearExpediente2(expediente, empresa, persona, detalleRecibo, STD, detalleSolicitud,null,null);
            return resultado;

        }
        #endregion

        #region Guardar Duplicado TUC
        public ResultadoProcedimientoVM CrearDuplicadoTUC(ExpedienteVM modelo)
        {
            ResultadoProcedimientoVM resultado = new ResultadoProcedimientoVM();

            #region Expediente
            ExpedienteModelo expediente = new ExpedienteModelo();
            expediente.ID_PROCEDIMIENTO = modelo.ID_PROCEDIMIENTO;
            expediente.ID_MODALIDAD_SERVICIO = Session["ID_MODALIDAD_SERVICIO"].ValorEntero();
            expediente.ID_SOLICITUD = EnumParametro.ProcedimientoTupa.ValorEntero();
            expediente.NUMERO_SOLICITANTE = modelo.NUMERO_SOLICITANTE;
            expediente.NUMERO_RECURRENTE = modelo.NUMERO_RECURRENTE;
            expediente.ID_ESTADO = modelo.ID_ESTADO;
            expediente.USUARIO_REG = Session["USUARIO"].ValorCadena();
            #endregion

            #region Persona
            PersonaModelo persona = new PersonaModelo();
            persona.ID_PERSONA = modelo.ID_PERSONA;
            persona.NRO_DOCUMENTO = modelo.NRO_DOCUMENTO;
            persona.ID_TIPO_PERSONA = Session["ID_TIPO_PERSONA"].ValorEntero();
            persona.APELLIDO_PATERNO = modelo.APELLIDO_PATERNO;
            persona.APELLIDO_MATERNO = modelo.APELLIDO_MATERNO;
            persona.NOMBRES = modelo.NOMBRES;
            persona.ID_TIPO_DOCUMENTO = Session["ID_TIPO_DOCUMENTO"].ValorEntero();  //modelo.ID_TIPO_DOCUMENTO,
            persona.RAZON_SOCIAL = modelo.RAZON_SOCIAL;
            persona.ID_DEPARTAMENTO = modelo.ID_DEPARTAMENTO;
            persona.ID_PROVINCIA = modelo.ID_PROVINCIA;
            persona.ID_DISTRITO = modelo.ID_DISTRITO;

            if (modelo.DIRECCION_ACTUAL == null)
            {
                resultado.CodResultado = 0;
                resultado.NomResultado = "Ingresar Dirección del Solicitante";
                return resultado;
            }
            persona.DIRECCION = modelo.DIRECCION_ACTUAL;
            persona.TELEFONO = modelo.TELEFONO;
            if (modelo.CORREO == null)
            {
                resultado.CodResultado = 0;
                resultado.NomResultado = "Ingresar Correo del Solicitante";
                return resultado;
            }
            persona.CORREO = modelo.CORREO;
            persona.USUARIO_REG = Session["USUARIO"].ValorCadena();
            #endregion

            #region Recibo
            List<ReciboModelo> detalleRecibo = new List<ReciboModelo>();

            foreach (var item in modelo.ReciboVM)
            {
                var resultadoComprobante = new ReciboBLL().BuscarRecibo(item.NUMERO_RECIBO.ValorCadena());

                if (item.ID_ENTIDAD_BANCARIA == 0)
                {
                    resultado.CodResultado = 0;
                    resultado.NomResultado = "Elegir una Entidad bancaria.";
                    return resultado;
                }

                if (String.IsNullOrEmpty(item.NUMERO_RECIBO))
                {
                    resultado.CodResultado = 0;
                    resultado.NomResultado = "Ingresar Nro de Comprobante.";
                    return resultado;
                }

                if (resultadoComprobante.NUMERO_RECIBO != null)
                {
                    resultado.CodResultado = 0;
                    resultado.NomResultado = "El Voucher " + resultadoComprobante.NUMERO_RECIBO + "ya fue registrado el " + resultadoComprobante.FECHA_EMISION.ValorFechaCorta();
                    return resultado;
                }

                //if (item.VALIDACION == false)
                //{
                //resultado.CodResultado = 0;
                //    resultado.NomResultado = "El voucher " + item.NUMERO_RECIBO + " ya fue registrado en un proceso anterior.";
                //    return resultado;
                //}
                var registro = new ReciboModelo();
                registro.ID_ENTIDAD_BANCARIA = item.ID_ENTIDAD_BANCARIA;
                registro.NUMERO_RECIBO = item.NUMERO_RECIBO;
                registro.FECHA_EMISION = Convert.ToDateTime(item.FECHA_EMISION);
                registro.USUARIO_REG = Session["USUARIO"].ValorCadena();
                detalleRecibo.Add(registro);
            }
            #endregion

            #region STD
            STDVM STD = new STDVM();

            STD.IDUNIDAD_STD = modelo.IDUNIDAD_STD;
            STD.CODPAIS = modelo.CODPAIS;
            STD.CODDPTO = modelo.CODDPTO;
            STD.CODPROV = modelo.CODPROV;
            STD.CODDIST = modelo.CODDIST;
            STD.DIRECCION_STD = modelo.DIRECCION_ACTUAL;
            STD.NOMBRE = modelo.NOMBRES;
            STD.ID_PROCEDIMIENTO = modelo.ID_PROCEDIMIENTO;
            STD.ID_PROVEEDOR = modelo.ID_EMPRESA;
            //Proceso TUPA STD
            STD.TIPO_EXPEDIENTE = Session["TIPO_EXPEDIENTE"].ValorEntero();
            STD.OBSERVACION = Session["OBSERVACION"].ValorCadena();

            #endregion

            #region Empresa
            EmpresaModelo empresa = new EmpresaModelo()
            {
                RUC = modelo.RUC,
                RAZON_SOCIAL = modelo.RAZON_SOCIAL,
            };
            #endregion

            #region Vehiculo
            VehiculoModelo vehiculo = new VehiculoModelo();
            {
                vehiculo.ID_VEHICULO = modelo.ID_VEHICULO;
                if (String.IsNullOrEmpty(modelo.NroPlaca))
                {
                    resultado.CodResultado = 0;
                    resultado.NomResultado = "Ingresar una Placa.";
                    return resultado;
                }
                vehiculo.PLACA = modelo.NroPlaca;
                vehiculo.ID_MODALIDAD_SERVICIO = modelo.ID_MODALIDAD_SERVICIO;
                if (modelo.ID_CLASE_VEHICULO == 0)
                {
                    resultado.CodResultado = 0;
                    resultado.NomResultado = "Ingresar una Clase del Vehiculo.";
                    return resultado;
                }
                vehiculo.ID_CLASE_VEHICULO = modelo.ID_CLASE_VEHICULO;
                if (modelo.ID_MODELO == 0)
                {
                    resultado.CodResultado = 0;
                    resultado.NomResultado = "Ingresar una Modelo del Vehiculo.";
                    return resultado;
                }
                vehiculo.ID_MODELO = modelo.ID_MODELO;
                if (modelo.ID_TIPO_COMBUSTIBLE == 0)
                {
                    resultado.CodResultado = 0;
                    resultado.NomResultado = "Ingresar el Tipo de Combustible.";
                    return resultado;
                }
                vehiculo.ID_TIPO_COMBUSTIBLE = modelo.ID_TIPO_COMBUSTIBLE;
                if (modelo.ID_CATEGORIA_VEHICULO == 0)
                {
                    resultado.CodResultado = 0;
                    resultado.NomResultado = "Ingresar el Categoria del Vehiculo.";
                    return resultado;
                }
                vehiculo.ID_CATEGORIA_VEHICULO = modelo.ID_CATEGORIA_VEHICULO;
                vehiculo.ANIO_FABRICACION = modelo.ANIO_FABRICACION;
                vehiculo.SERIE = modelo.SERIE;
                vehiculo.SERIE_MOTOR = modelo.SERIE_MOTOR;
                vehiculo.PESO_SECO = modelo.PESO_SECO.ValorEntero();
                vehiculo.PESO_BRUTO = modelo.PESO_BRUTO.ValorEntero();
                vehiculo.LONGITUD = modelo.LONGITUD.ValorEntero();
                vehiculo.ALTURA = modelo.ALTURA.ValorEntero();
                vehiculo.ANCHO = modelo.ANCHO.ValorEntero();
                vehiculo.CARGA_UTIL = modelo.CARGA_UTIL.ValorEntero();
                vehiculo.CAPACIDAD_PASAJERO = modelo.CAPACIDAD_PASAJERO.ValorEntero();
                vehiculo.NUMERO_ASIENTOS = modelo.NUMERO_ASIENTOS.ValorEntero();
                vehiculo.NUMERO_RUEDA = modelo.NUMERO_RUEDA.ValorEntero();
                vehiculo.NUMERO_EJE = modelo.NUMERO_EJE.ValorEntero();
                vehiculo.NUMERO_PUERTA = modelo.NUMERO_PUERTA.ValorEntero();
                vehiculo.FECHA_INSCRIPCION = "";
                vehiculo.CILINDRADA = modelo.CILINDRADA;
                vehiculo.OBSERVACION = modelo.Observacion;
                if (modelo.ID_MARCA == 0)
                {
                    resultado.CodResultado = 0;
                    resultado.NomResultado = "Ingresar una Marca del Vehiculo";
                    return resultado;
                }
                vehiculo.ID_MARCA = modelo.ID_MARCA;
                vehiculo.USUARIO_REG = Session["USUARIO"].ValorCadena();
            };
            #endregion

            #region Tarjeta Propiedad
            TarjetaPropiedadModelo tarjetaPropietario = new TarjetaPropiedadModelo()
            {
                DESDE = modelo.FECHA_INICIO_PROPIETARIO,
                HASTA = modelo.FECHA_FIN_PROPIETARIO,
                NRO_TARJETA = modelo.NRO_TARJETA_PROPIETARIO,
                USUARIO_REG = Session["USUARIO"].ValorCadena()
            };
            #endregion

            #region Propietario
            PropietarioModelo propietario = new PropietarioModelo()
            {
                NRO_DOCUMENTO = modelo.NUMERO_DOCUMENTO_PROPIEDAD,
                ID_TIPO_DOCUMENTO = modelo.ID_TIPO_DOCUMENTO_PROPIETARIO,
                ID_TIPO_CONTRIBUYENTE = 1,
                NOMBRE_PROPIETARIO = modelo.NOMBRE_PROPIETARIO,
                USUARIO_REG = Session["USUARIO"].ValorCadena(),
            };
            #endregion

            TarjetaCirculacionModelo tarjetaCirculacion = new TarjetaCirculacionModelo()
            {
                FECHA_IMPRESION = DateTime.Now.ValorFechaCorta(),
                USUARIO_REG = Session["USUARIO"].ValorCadena(),
            };

            List<DetalleSolicitudModelo> detalleSolicitud = new List<DetalleSolicitudModelo>();

            foreach (var item in modelo.DetalleSolicitudVM)
            {
                var registro = new DetalleSolicitudModelo();
                registro.NUMERO_RECIBO = item.NUMERO_RECIBO;
                registro.DATO_REGISTRO = item.DATO_REGISTRO;
                registro.DESCRIPCION = item.DESCRIPCION;
                registro.ID_ENTIDAD_BANCARIA = item.ID_ENTIDAD_BANCARIA;
                registro.USU_REG = Session["USUARIO"].ValorCadena();
                detalleSolicitud.Add(registro);
            }

            resultado = new ExpedienteBLL().CrearDuplicadoTUC(expediente, empresa, persona, detalleRecibo, STD, vehiculo, tarjetaPropietario, propietario, tarjetaCirculacion);

            return resultado;

        }
        #endregion

        #region GUARDAR CREDENCIAL OPERADOR
        //public ResultadoProcedimientoVM CrearCredencialOperador(ExpedienteVM modelo)
        //{
        //ResultadoProcedimientoVM resultado = new ResultadoProcedimientoVM();
        //ResultadoProcedimientoVM resultadoOperadorEmpresa = new ResultadoProcedimientoVM();
        //ResultadoProcedimientoVM resultadoCreaOperador = new ResultadoProcedimientoVM();
        //ResultadoProcedimientoVM resultadoActualizaFoto = new ResultadoProcedimientoVM();

        //ExpedienteModelo expediente = new ExpedienteModelo()
        //{
        //    ID_PROCEDIMIENTO = modelo.ID_PROCEDIMIENTO,
        //    ID_MODALIDAD_SERVICIO = Session["ID_MODALIDAD_SERVICIO"].ValorEntero(),
        //    ID_SOLICITUD = EnumParametro.ProcedimientoTupa.ValorEntero(),
        //    //TUPA
        //    NUMERO_SOLICITANTE = modelo.NUMERO_SOLICITANTE,
        //    NUMERO_RECURRENTE = modelo.NUMERO_RECURRENTE,
        //    ID_ESTADO = modelo.ID_ESTADO,

        //    USUARIO_REG = Session["USUARIO"].ValorCadena()
        //};



        //var operador = new OperadorModelo();

        //operador.NRO_DOCUMENTO = modelo.NRO_DOCUMENTO_OPERADOR;
        //operador.ID_TIPO_PERSONA = Session["ID_TIPO_PERSONA"].ValorEntero();
        //operador.APELLIDO_PATERNO = modelo.APELLIDO_PATERNO_OPERADOR;
        //operador.APELLIDO_MATERNO = modelo.APELLIDO_MATERNO_OPERADOR;
        //operador.NOMBRE = modelo.NOMBRE_OPERADOR;
        //operador.ID_TIPO_DOCUMENTO = modelo.ID_TIPO_DOCUMENTO_OPERADOR;
        //operador.DIRECCION = modelo.DIRECCION_OPERADOR;
        //operador.TELEFONO_CEL = modelo.TELEFONO_CEL_OPERADOR;
        //operador.TELEFONO_CASA = modelo.TELEFONO_CASA_OPERADOR;
        //operador.CORREO = modelo.CORREO_OPERADOR;
        //operador.ID_TIPO_OPERADOR = modelo.ID_TIPO_OPERADOR;
        //operador.ID_MODALIDAD_SERVICIO = Session["ID_MODALIDAD_SERVICIO"].ValorEntero();
        //operador.FECHA_INSCRIPCION = DateTime.Now.ValorFechaCorta();
        //operador.AÑO = DateTime.Now.Year;
        //operador.NRO_LICENCIA = modelo.NRO_LICENCIA_OPERADOR;
        //operador.CATEGORIA = modelo.CATEGORIA_OPERADOR;
        //operador.FECHA_EXPEDICION = modelo.FECHA_EXPEDICION_OPERADOR;
        //operador.FECHA_REVALIDACION = modelo.FECHA_REVALIDACION_OPERADOR;
        //operador.FEC_NAC = modelo.FECHA_NACIMIENTO_OPERADOR;
        //operador.FOTO_OPERADOR = modelo.FOTO_OPERADOR;
        //operador.FOTO_BASE64 = modelo.FOTO_OPERADOR;
        //operador.ID_SEXO = modelo.ID_SEXO;


        //PersonaModelo persona = new PersonaModelo()
        //{
        //    ID_PERSONA = modelo.ID_PERSONA,
        //    NRO_DOCUMENTO = modelo.NRO_DOCUMENTO,
        //    ID_TIPO_PERSONA = Session["ID_TIPO_PERSONA"].ValorEntero(),
        //    APELLIDO_PATERNO = modelo.APELLIDO_PATERNO,
        //    APELLIDO_MATERNO = modelo.APELLIDO_MATERNO,
        //    NOMBRES = modelo.NOMBRES,
        //    ID_TIPO_DOCUMENTO = Session["ID_TIPO_DOCUMENTO"].ValorEntero(),  //modelo.ID_TIPO_DOCUMENTO,
        //    RAZON_SOCIAL = modelo.RAZON_SOCIAL,
        //    DIRECCION = modelo.DIRECCION,
        //    DIRECCION_ACTUAL = modelo.DIRECCION_ACTUAL,
        //    TELEFONO = modelo.TELEFONO,
        //    CORREO = modelo.CORREO,
        //    USUARIO_REG = Session["USUARIO"].ValorCadena(),
        //    ID_DEPARTAMENTO = modelo.ID_DEPARTAMENTO,
        //    ID_PROVINCIA = modelo.ID_PROVINCIA,
        //    ID_DISTRITO = modelo.ID_DISTRITO,
        //};

        //List<ReciboModelo> detalleRecibo = new List<ReciboModelo>();
        //foreach (var item in modelo.ReciboVM)
        //{
        //    var registro = new ReciboModelo();
        //    registro.ID_ENTIDAD_BANCARIA = item.ID_ENTIDAD_BANCARIA;
        //    registro.NUMERO_RECIBO = item.NUMERO_RECIBO;
        //    registro.FECHA_EMISION = Convert.ToDateTime(item.FECHA_EMISION);
        //    registro.USUARIO_REG = Session["USUARIO"].ValorCadena();
        //    registro.ID_EXPEDIENTE = item.ID_EXPEDIENTE;
        //    registro.ID_PROCEDIMIENTO = item.ID_PROCEDIMIENTO;
        //    detalleRecibo.Add(registro);
        //}

        //STDVM STD = new STDVM()
        //{
        //    IDUNIDAD_STD = modelo.IDUNIDAD_STD,
        //    CODPAIS = modelo.CODPAIS,
        //    CODDPTO = modelo.CODDPTO,
        //    CODPROV = modelo.CODPROV,
        //    CODDIST = modelo.CODDIST,
        //    DIRECCION_STD = modelo.DIRECCION_ACTUAL,
        //    NOMBRE = modelo.NOMBRES,
        //    ID_PROCEDIMIENTO = modelo.ID_PROCEDIMIENTO,
        //    ID_PROVEEDOR = modelo.ID_EMPRESA,
        //    //Proceso TUPA STD
        //    TIPO_EXPEDIENTE = 3,
        //    OBSERVACION = "PROCEDIMIENTO TUPA",
        //};


        //EmpresaModelo empresa = new EmpresaModelo()
        //{
        //    RUC = modelo.RUC,
        //    RAZON_SOCIAL = modelo.RAZON_SOCIAL,
        //};

        //List<DetalleSolicitudModelo> detalleSolicitud = new List<DetalleSolicitudModelo>();

        //foreach (var item in modelo.DetalleSolicitudVM)
        //{
        //    var registro = new DetalleSolicitudModelo();
        //    registro.NUMERO_RECIBO = item.NUMERO_RECIBO;
        //    registro.DATO_REGISTRO = item.DATO_REGISTRO;
        //    registro.DESCRIPCION = item.DESCRIPCION;
        //    registro.ID_ENTIDAD_BANCARIA = item.ID_ENTIDAD_BANCARIA;
        //    registro.USU_REG = Session["USUARIO"].ValorCadena();
        //    detalleSolicitud.Add(registro);
        //}

        //CredencialOperadorModelo credencialOperador = new CredencialOperadorModelo();
        //credencialOperador.FECHA_IMPRESION = DateTime.Now.ValorFechaCorta();
        //credencialOperador.FECHA_ENTREGA = DateTime.Now.ValorFechaCorta();
        //credencialOperador.ANIO_CREDENCIAL = DateTime.Now.Year;
        //credencialOperador.ID_TIPO_DOCUMENTO_OPERADOR = modelo.ID_TIPO_DOCUMENTO_OPERADOR;
        //credencialOperador.NUMERO_DOCUMENTO_OPERADOR = modelo.NRO_DOCUMENTO_OPERADOR;
        //credencialOperador.FECHA_INICIO = DateTime.Now.ValorFechaCorta();
        //credencialOperador.USUARIO_REG = Session["USUARIO"].ValorCadena();



        //var resultadoExpediente = new ExpedienteBLL().CrearExpediente2(expediente, empresa, persona, detalleRecibo, STD, detalleSolicitud, null, null, null, operador, credencialOperador);


        //return resultadoExpediente;
        //}
        #endregion

        #region crear expediente cita
        public ResultadoProcedimientoVM CrearExpedienteCita(ExpedienteVM modelo)
        {
            ResultadoProcedimientoVM resultado = new ResultadoProcedimientoVM();

            ExpedienteModelo expediente = new ExpedienteModelo()
            {
                ID_PROCEDIMIENTO = modelo.ID_PROCEDIMIENTO,
                ID_MODALIDAD_SERVICIO = Session["ID_MODALIDAD_SERVICIO"].ValorEntero(),
                ID_SOLICITUD = EnumParametro.ProcedimientoTupa.ValorEntero(),
                NUMERO_SOLICITANTE = modelo.NUMERO_SOLICITANTE,
                NUMERO_RECURRENTE = modelo.NUMERO_RECURRENTE,
                ID_ESTADO = modelo.ID_ESTADO,
                USUARIO_REG = Session["USUARIO"].ValorCadena(),
                ASUNTO_NO_TUPA = modelo.ASUNTO_NO_TUPA,
            };

            PersonaModelo persona = new PersonaModelo()
            {
                ID_PERSONA = modelo.ID_PERSONA,
                NRO_DOCUMENTO = modelo.NRO_DOCUMENTO,
                ID_TIPO_PERSONA = Session["ID_TIPO_PERSONA"].ValorEntero(),
                APELLIDO_PATERNO = modelo.APELLIDO_PATERNO,
                APELLIDO_MATERNO = modelo.APELLIDO_MATERNO,
                NOMBRES = modelo.NOMBRES,
                ID_TIPO_DOCUMENTO = Session["ID_TIPO_DOCUMENTO"].ValorEntero(),
                RAZON_SOCIAL = modelo.RAZON_SOCIAL,
                DIRECCION = modelo.DIRECCION,
                TELEFONO = modelo.TELEFONO,
                CORREO = modelo.CORREO,
                USUARIO_REG = Session["USUARIO"].ValorCadena(),
                ID_DEPARTAMENTO = modelo.ID_DEPARTAMENTO,
                ID_PROVINCIA = modelo.ID_PROVINCIA,
                ID_DISTRITO = modelo.ID_DISTRITO,
            };

            STDVM STD = new STDVM()
            {
                IDUNIDAD_STD = modelo.IDUNIDAD_STD,
                CODPAIS = modelo.CODPAIS,
                CODDPTO = modelo.CODDPTO,
                CODPROV = modelo.CODPROV,
                CODDIST = modelo.CODDIST,
                DIRECCION_STD = modelo.DIRECCION_ACTUAL,
                NOMBRE = modelo.NOMBRES,
                ID_PROCEDIMIENTO = modelo.ID_PROCEDIMIENTO,
                ID_PROVEEDOR = modelo.ID_EMPRESA,
                //Proceso TUPA STD
                TIPO_EXPEDIENTE = EnumTipoExpediente.NoTupa.ValorEntero(),
                OBSERVACION = Session["OBSERVACION"].ValorCadena(),

            };

            EmpresaModelo empresa = new EmpresaModelo()
            {
                RUC = modelo.RUC,
                RAZON_SOCIAL = modelo.RAZON_SOCIAL,
            };

            var resultadoExpediente = new ExpedienteBLL().CrearExpedienteCita(expediente, empresa, persona, STD);

            return resultadoExpediente;
        }
        #endregion

        public JsonResult ConsultarProcesoTUC(int ID_PROCEDIMIENTO, int ID_TIPO_PERSONA)
        {
            ProcedimientoBLL obj = new ProcedimientoBLL();
            ProcedimientoVM resultado = obj.ConsultarDatosProcedimiento(ID_PROCEDIMIENTO, ID_TIPO_PERSONA);
            return Json(new { modelo = resultado });
        }
        
     
        public JsonResult enviarCorreoResolucion(string correo, string Persona, string DNI, int IDDOC_HIJO, int IDDOC_PADRE)
        {
            var rptaEnvioCorreo = "";
            var codResultado = 0;
            string strPathArchivoGenerado_TUC = "";
            string strPathArchivoGenerado_RESOLUCION = "";
            String[] arrResult;
            String[] arrResultRESOL = new String[0];
            try
            {
                var urlArchivos = "~/Adjunto/resoluciones/";
                var pathArchivo = Server.MapPath(urlArchivos);
                if (IDDOC_HIJO == 0)
                {
                    strPathArchivoGenerado_TUC = new ReportesBLL().getDatosTarjetaUnicaCirculacion(IDDOC_PADRE, pathArchivo, 1);
                    //strPathArchivoGenerado_RESOLUCION = new ReportesBLL().ReporteResolucion(IDDOC_PADRE, pathArchivo);
                }
                else
                {
                    strPathArchivoGenerado_TUC = new ReportesBLL().getDatosTarjetaUnicaCirculacion(IDDOC_HIJO, pathArchivo, 1);
                    strPathArchivoGenerado_RESOLUCION = new ReportesBLL().ReporteResolucion(IDDOC_PADRE, pathArchivo);

                }




                arrResult = strPathArchivoGenerado_TUC.Split('|');
                if (IDDOC_HIJO != 0)
                {
                    arrResultRESOL = strPathArchivoGenerado_RESOLUCION.Split('|');
                }

                var codResultadoPDF = int.Parse(arrResult[0]);
                var urlAdjuntoPDF = "";
                if (codResultadoPDF == 0)
                {
                    return Json(new
                    {
                        codresultado = 0,
                        resultado = arrResult[1]
                    });
                }
                //
                Notificacion confirmacion = new Notificacion();

                String[] arregloRutasArchivo = new String[2];
                confirmacion.Body = "Estimado Administrado (a),<br><br>" +
                                    "Por medio de la presente le comunicamos que el Registro de su Trámite en la ATU se realizó satisfactoriamente.<br><br><br>" +
                                    "Atentamente," +
                                    "<h2>Comunicaciones ATU</h2><br>" +
                                    "<hr>" +
                                    "Este mensaje de correo electrónico y / o el material adjunto puede contener información confidencial o legalmente protegida por la Ley N°  " +
                                    "29733 - Ley de Protección de Datos Personales, y es de uso exclusivo de la(s) persona(s) a quién(es)se dirige.Si no es usted el destinatario " +
                                    "indicado, queda notificado de que la lectura, uitilización, divulgación y / o copia puede estar prohibida en virtud de la legislación vigente, si " +
                                    "usted recibe este mensaje por error por favor elimine toda la información." +
                                    " <br>" +
                                    "* Este buzón es de envió automático, por favor no responder * ";
                //confirmacion.ArchivosMail;
                //confirmacion.ArchivosMail(archivo);
                urlAdjuntoPDF += pathArchivo + arrResult[1];
                //confirmacion.rutaArchivoAdjunto = urlAdjuntoPDF;

                //adjuntando resolucion_de autorización
                arregloRutasArchivo[0] = urlAdjuntoPDF;
                if (IDDOC_HIJO != 0)
                {
                    arregloRutasArchivo[1] = pathArchivo + arrResultRESOL[1];
                }
                confirmacion.arrArchivosRuta = arregloRutasArchivo;
                confirmacion.To.Add(correo);
                confirmacion.asunto = "Registro de Trámite ATU";
                confirmacion.enviar();

                rptaEnvioCorreo = "CORRECTO";
                codResultado = 1;
            }
            catch (Exception ex)
            {
                rptaEnvioCorreo = "error-->" + ex.Message;
                codResultado = 0;
            }

            return Json(new
            {
                codresultado = codResultado,
                resultado = rptaEnvioCorreo
            });
        }

        public JsonResult enviarCorreoDuplicado(string correo, string Persona, string DNI, int expediente)
        {
            var rptaEnvioCorreo = "";
            var codResultado = 0;
            try
            {
                var urlArchivos = "~/Adjunto/resoluciones/";
                var pathArchivo = Server.MapPath(urlArchivos);
                var strPathArchivoGenerado_TUC = new ReportesBLL().getDatosTarjetaUnicaCirculacion(expediente, pathArchivo, 1);

                String[] arrResult = strPathArchivoGenerado_TUC.Split('|');

                var codResultadoPDF = int.Parse(arrResult[0]);
                var urlAdjuntoPDF = "";
                if (codResultadoPDF == 0)
                {
                    return Json(new
                    {
                        codresultado = 0,
                        resultado = arrResult[1]
                    });
                }
                //
                Notificacion confirmacion = new Notificacion();

                String[] arregloRutasArchivo = new String[1];
                confirmacion.Body = "Estimado Administrado (a),<br><br>" +
                                    "Por medio de la presente le comunicamos que el Registro de su Trámite en la ATU se realizó satisfactoriamente.<br><br><br>" +
                                    "Atentamente," +
                                    "<h2>Comunicaciones ATU</h2><br>" +
                                    "<hr>" +
                                    "Este mensaje de correo electrónico y / o el material adjunto puede contener información confidencial o legalmente protegida por la Ley N°  " +
                                    "29733 - Ley de Protección de Datos Personales, y es de uso exclusivo de la(s) persona(s) a quién(es)se dirige.Si no es usted el destinatario " +
                                    "indicado, queda notificado de que la lectura, uitilización, divulgación y / o copia puede estar prohibida en virtud de la legislación vigente, si " +
                                    "usted recibe este mensaje por error por favor elimine toda la información." +
                                    " <br>" +
                                    "* Este buzón es de envió automático, por favor no responder * ";
                //confirmacion.ArchivosMail;
                //confirmacion.ArchivosMail(archivo);
                urlAdjuntoPDF += pathArchivo + arrResult[1];
                //confirmacion.rutaArchivoAdjunto = urlAdjuntoPDF;

                //adjuntando resolucion_de autorización
                arregloRutasArchivo[0] = urlAdjuntoPDF;
                confirmacion.arrArchivosRuta = arregloRutasArchivo;
                confirmacion.To.Add(correo);
                confirmacion.asunto = "Registro de Trámite ATU";
                confirmacion.enviar();

                rptaEnvioCorreo = "CORRECTO";
                codResultado = 1;
            }
            catch (Exception ex)
            {
                rptaEnvioCorreo = "error-->" + ex.Message;
                codResultado = 0;
            }

            return Json(new
            {
                codresultado = codResultado,
                resultado = rptaEnvioCorreo
            });
        }


        public JsonResult EnviarCorreoConstancia(string correo)
        {
            var rptaEnvioCorreo = "";
            var codResultado = 0;
            try
            {
                var urlArchivos = "~/Adjunto/contancia_credencial/Constancia_credencia_1.pdf";
                var pathArchivo = Server.MapPath(urlArchivos);
                // pathArchivo += nombreArchivoRegistro;
                //var strPathArchivoGenerado = pathArchivo;
                // var strPathArchivoGenerado_RESOLUCION = new ReportesBLL().genera_pdf_CONST(0, pathArchivo,1);
                //var strPathArchivoGenerado_RESOL = new ProcedimientoBLL().genera_pdf_res_taxi_independ("ACR-789", pathArchivo, 1, Persona, DNI);

                //
                //String[] arrResultRESOL = strPathArchivoGenerado_RESOLUCION.Split('|');
                //String[] arrResultRESOL;
                //
                //var urlAdjuntoPDF = "";
                //
                Notificacion confirmacion = new Notificacion();
                String[] arregloRutasArchivo = new String[1];

                confirmacion.Body = "Estimado Administrado (a),<br><br>" +
                                    "Por medio de la presente le comunicamos que el Registro de su Trámite en la ATU se realizó satisfactoriamente.<br><br><br>" +
                                    "Atentamente," +
                                    "<h2>Comunicaciones ATU</h2><br>" +
                                    "<hr>" +
                                    "Este mensaje de correo electrónico y / o el material adjunto puede contener información confidencial o legalmente protegida por la Ley N°  " +
                                    "29733 - Ley de Protección de Datos Personales, y es de uso exclusivo de la(s) persona(s) a quién(es)se dirige.Si no es usted el destinatario " +
                                    "indicado, queda notificado de que la lectura, uitilización, divulgación y / o copia puede estar prohibida en virtud de la legislación vigente, si " +
                                    "usted recibe este mensaje por error por favor elimine toda la información." +
                                    " <br>" +
                                    "* Este buzón es de envió automático, por favor no responder * ";
                //confirmacion.ArchivosMail;
                //confirmacion.ArchivosMail(archivo);
                //urlAdjuntoPDF += pathArchivo + arrResult[1];
                //confirmacion.rutaArchivoAdjunto = urlAdjuntoPDF;

                //adjuntando resolucion_de autorización
                arregloRutasArchivo[0] = pathArchivo;
                //arregloRutasArchivo[1] = pathArchivo + arrResultRESOL[1];
                confirmacion.arrArchivosRuta = arregloRutasArchivo;
                confirmacion.To.Add(correo);
                confirmacion.asunto = "Registro de Trámite ATU";
                confirmacion.enviar();

                rptaEnvioCorreo = "CORRECTO";
                codResultado = 1;
            }
            catch (Exception ex)
            {
                rptaEnvioCorreo = "error-->" + ex.Message;
                codResultado = 0;
            }

            return Json(new
            {
                codresultado = codResultado,
                resultado = rptaEnvioCorreo
            });
        }

        public ActionResult ValidarPago(ExpedienteVM expediente)
        {
            ResultadoProcedimientoVM resultado = new ResultadoProcedimientoVM();

            foreach (var item in expediente.ReciboVM)
            {
                var existeVoucher = new ReciboBLL().BuscarRecibo(item.NUMERO_RECIBO);

                if (existeVoucher.NUMERO_RECIBO != null)
                {
                    return Json(new { success = false, mensaje = "Voucher " + existeVoucher.NUMERO_RECIBO + " ya fue registrado el " + existeVoucher.FECHA_EMISION.ValorFechaCorta() });
                }

                if (item.ID_ENTIDAD_BANCARIA == EnumEntidadBancaria.SCOTIABANK.ValorEntero())
                {
                    string fechaMes = Convert.ToDateTime(item.FECHA_EMISION).Month.ToString("D2");
                    string fechaDia = Convert.ToDateTime(item.FECHA_EMISION).Day.ToString("D2");
                    var fechaConvertida = fechaMes + fechaDia;

                    resultado = new PagoBLL().ConsultarScotiabank(expediente.ID_MODALIDAD_SERVICIO, expediente.ID_PROCEDIMIENTO, item.NUMERO_RECIBO, fechaConvertida);

                    if (resultado.CodResultado == 0)
                    {
                        return Json(new { success = false, mensaje = "Por favor ingrese los datos correctos del recibo" });
                    }
                }

                else if (item.ID_ENTIDAD_BANCARIA == EnumEntidadBancaria.BANCO_NACION.ValorEntero())
                {
                    string fechaMes = Convert.ToDateTime(item.FECHA_EMISION).Month.ToString("D2");
                    string fechaDia = Convert.ToDateTime(item.FECHA_EMISION).Day.ToString("D2");
                    string fechaAnio = Convert.ToDateTime(item.FECHA_EMISION).Year.ToString();
                    var fechaConvertida = fechaAnio + fechaMes + fechaDia;

                    resultado = new PagoBLL().ConsultarNacion(expediente.ID_PROCEDIMIENTO, item.NUMERO_RECIBO, fechaConvertida);

                    if (resultado.CodResultado == 0)
                    {
                        return Json(new { success = false, mensaje = "Por favor ingrese los datos correctos del recibo" });
                    }
                }
            }
            return Json(new { success = true, mensaje = "Cargo Correctamente", modelo = resultado });
        }

        public JsonResult EnviarCorreoConfirmacionRegistroOper(string correo, List<OperadorVM> listOperador, string Empresa, string nombre_modalidad, string fechaRegistro, string id_expediente)
        {
            var rptaEnvioCorreo = "";
            var codResultado = 0;
            try
            {
                var urlArchivos = "~/Adjunto/resoluciones/";
                var pathArchivo = Server.MapPath(urlArchivos);

                // pathArchivo += nombreArchivoRegistro;
                //var strPathArchivoGenerado = pathArchivo;
                var strPathArchivoGenerado_Constancia = new ReportesBLL().genera_pdf_PADRON(0, pathArchivo, 1, Empresa, nombre_modalidad, fechaRegistro, id_expediente);

                //var strPathArchivoGenerado_RESOL = new ProcedimientoBLL().genera_pdf_res_taxi_independ("ACR-789", pathArchivo, 1, Persona, DNI);
                //
                //String[] arrResultRESOL = strPathArchivoGenerado_RESOLUCION.Split('|');
                //String[] arrResultRESOL;
                //
                var urlAdjuntoPDF = "";
                //
                Notificacion confirmacion = new Notificacion();
                String[] arrResult = strPathArchivoGenerado_Constancia.Split('|'); //new String[1];

                String[] arregloRutasArchivo = new String[1];

                urlAdjuntoPDF = pathArchivo + arrResult[1];
                arregloRutasArchivo[0] = urlAdjuntoPDF;
                String htmlListaOperadores = "<ul>";

                foreach (var item in listOperador)
                {
                    if (item.REGISTRO_AGREGADO)
                    {
                        htmlListaOperadores += "<li>" + item.NOMBRE_TIPO_DOCUMENTO + " " +
                                                       "NRO. DOCUMENTO: " + item.NRO_DOCUMENTO + " , " +
                                                        //item.NRO_LICENCIA + " " +
                                                        " NOMBRES Y APELLIDOS: " + item.NOMBRES + " " + item.APELLIDO_PATERNO + " " + item.APELLIDO_MATERNO +
                                               "</li>";
                    }
                }
                htmlListaOperadores += "</ul>";

                confirmacion.Body = "Estimado Administrado (a),<br><br>" +
                    "Por medio de la presente le comunicamos que el Registro de su Trámite en la ATU se realizó satisfactoriamente.<br><br><br>" +
                     "Los operadores registrados son los siguientes: <br>" +
                    htmlListaOperadores + "<br><br><br>" +
             "Atentamente," +
            "<h2>Comunicaciones ATU</h2><br>" +
                    "<hr>" +
                "Este mensaje de correo electrónico y / o el material adjunto puede contener información confidencial o legalmente protegida por la Ley N°  " +
                "29733 - Ley de Protección de Datos Personales, y es de uso exclusivo de la(s) persona(s) a quién(es)se dirige.Si no es usted el destinatario " +
                "indicado, queda notificado de que la lectura, uitilización, divulgación y / o copia puede estar prohibida en virtud de la legislación vigente, si " +
                "usted recibe este mensaje por error por favor elimine toda la información." +
                " <br>" +
                "* Este buzón es de envió automático, por favor no responder * ";


                confirmacion.arrArchivosRuta = arregloRutasArchivo;
                confirmacion.To.Add(correo);
                confirmacion.asunto = "Registro de Trámite ATU";

                confirmacion.enviar();

                rptaEnvioCorreo = "CORRECTO";
                codResultado = 1;
            }
            catch (Exception ex)
            {
                rptaEnvioCorreo = "error-->" + ex.Message;
                codResultado = 0;
            }

            return Json(new
            {
                codresultado = codResultado,
                resultado = rptaEnvioCorreo
            });
        }

        public JsonResult EnviarCorreoCredencial(string correo, List<OperadorVM> listOperador, int tipo_modalidad, int tipo_operador, string Empresa, string nombre_modalidad, string fechaRegistro, string id_expediente, int ID_TIPO_PESONA)
        {
            var rptaEnvioCorreo = "";
            var codResultado = 0;
            try
            {
                var urlArchivos = "~/Adjunto/resoluciones/";
                var url_foto = "~/Adjunto/foto_operador/";
                var pathArchivo = Server.MapPath(urlArchivos);
                var pathFoto = Server.MapPath(url_foto);
                var strPathArchivoGenerado_Constancia = "";
                var strPathArchivoGenerado_TarjetaCircu = "";

                if (ID_TIPO_PESONA == EnumParametroTipoPersona.PersonaJuridica.ValorEntero())
                {
                    if (tipo_modalidad == EnumModalidadServicio.TransporteRegularPersona.ValorEntero())
                    {
                        strPathArchivoGenerado_Constancia = new ReportesBLL().genera_pdf_Credencial(int.Parse(id_expediente), pathArchivo, 1, Empresa, nombre_modalidad, fechaRegistro);
                        strPathArchivoGenerado_TarjetaCircu = new ReportesBLL().genera_pdf_Tarje_Crendencial(int.Parse(id_expediente), pathArchivo, pathFoto, 1, Empresa, nombre_modalidad, fechaRegistro);
                    }
                    else if (tipo_modalidad == EnumModalidadServicio.ServicioTaxiEstacion.ValorEntero() || tipo_modalidad == EnumModalidadServicio.TransporteEstudianteEscolar.ValorEntero() || tipo_modalidad == EnumModalidadServicio.ServicioTaxiRemisse.ValorEntero() || tipo_modalidad == EnumModalidadServicio.TransportePersona.ValorEntero() || tipo_modalidad == EnumModalidadServicio.TransporteTuristico.ValorEntero())
                    {
                        // strPathArchivoGenerado_Constancia = new ReportesBLL().genera_pdf_Credencial_taxi(int.Parse(id_expediente), pathArchivo, 1, Empresa, nombre_modalidad, fechaRegistro);
                        strPathArchivoGenerado_Constancia = new ReportesBLL().genera_pdf_Credencial(int.Parse(id_expediente), pathArchivo, tipo_modalidad, Empresa, nombre_modalidad, fechaRegistro);
                        strPathArchivoGenerado_TarjetaCircu = new ReportesBLL().genera_pdf_Tarje_Crendencial_taxi(int.Parse(id_expediente), pathArchivo, pathFoto, tipo_modalidad, Empresa, nombre_modalidad, fechaRegistro);
                    }

                }
                else
                {
                    if (tipo_modalidad == EnumModalidadServicio.ServicioTaxiIndependiente.ValorEntero() || tipo_modalidad == EnumModalidadServicio.TransporteEstudianteEscolar.ValorEntero())
                    {
                        strPathArchivoGenerado_Constancia = new ReportesBLL().genera_pdf_Credencial_taxi(int.Parse(id_expediente), pathArchivo, tipo_modalidad, Empresa, nombre_modalidad, fechaRegistro);
                        strPathArchivoGenerado_TarjetaCircu = new ReportesBLL().genera_pdf_Tarje_Crendencial_taxi(int.Parse(id_expediente), pathArchivo, pathFoto, tipo_modalidad, Empresa, nombre_modalidad, fechaRegistro);
                    }
                    //else
                    //{
                    //    strPathArchivoGenerado_Constancia = new ReportesBLL().genera_pdf_Credencial(int.Parse(id_expediente), pathArchivo, 1, Empresa, nombre_modalidad, fechaRegistro);
                    //    strPathArchivoGenerado_TarjetaCircu = new ReportesBLL().genera_pdf_Tarje_Crendencial(int.Parse(id_expediente), pathArchivo, pathFoto, 1, Empresa, nombre_modalidad, fechaRegistro);
                    //}
                }


                Notificacion confirmacion = new Notificacion();
                //if (tipo_operador != 2) {

                String[] arrResult = strPathArchivoGenerado_Constancia.Split('|'); //new String[1];

                String[] arrResultTarj_Circu = strPathArchivoGenerado_TarjetaCircu.Split('|');

                var codResultadoPDF = int.Parse(arrResult[0]);
                var urlAdjuntoPDF = "";
                if (codResultadoPDF == 0)
                {
                    return Json(new
                    {
                        codresultado = 0,
                        resultado = arrResult[1]
                    });
                }


                String htmlListaOperadores = "<ul>";

                foreach (var item in listOperador)
                {
                    if (item.ID_OPERADOR == 0)
                    {
                        htmlListaOperadores += "<li>" + item.NOMBRE_TIPO_DOCUMENTO + " " +
                                                        item.NRO_DOCUMENTO + " " +
                                                        item.NRO_LICENCIA + " " +
                                                        item.NOMBRES +
                                               "</li>";
                    }
                }
                String[] arregloRutasArchivo = new String[2];
                htmlListaOperadores += "</ul>";
                //
                confirmacion.Body = "Estimado Administrado (a),<br><br>" +
                                    "Por medio de la presente le comunicamos que el Registro de su Trámite en la ATU se realizó satisfactoriamente.<br><br><br>" +
                                     "Atentamente," +
                                    "<h2>Comunicaciones ATU</h2><br>" +
                                    "<hr>" +
                                "Este mensaje de correo electrónico y / o el material adjunto puede contener información confidencial o legalmente protegida por la Ley N°  " +
                                "29733 - Ley de Protección de Datos Personales, y es de uso exclusivo de la(s) persona(s) a quién(es)se dirige.Si no es usted el destinatario " +
                                "indicado, queda notificado de que la lectura, uitilización, divulgación y / o copia puede estar prohibida en virtud de la legislación vigente, si " +
                                "usted recibe este mensaje por error por favor elimine toda la información." +
                                " <br>" +
                                "* Este buzón es de envió automático, por favor no responder * ";

                urlAdjuntoPDF += pathArchivo + arrResult[1];
                arregloRutasArchivo[0] = urlAdjuntoPDF;
                arregloRutasArchivo[1] = pathArchivo + arrResultTarj_Circu[1];
                confirmacion.arrArchivosRuta = arregloRutasArchivo;

                confirmacion.To.Add(correo);
                confirmacion.asunto = "Registro de Trámite ATU";
                confirmacion.enviar();

                rptaEnvioCorreo = "CORRECTO";
                codResultado = 1;
            }
            catch (Exception ex)
            {
                rptaEnvioCorreo = "error-->" + ex.Message;
                codResultado = 0;
            }
            return Json(new
            {
                codresultado = codResultado,
                resultado = rptaEnvioCorreo
            });
        }

        public ResultadoProcedimientoVM guardarFotoOperador(String base64Foto, string nombreFoto)
        {
            ResultadoProcedimientoVM respuesta = new ResultadoProcedimientoVM();
            try
            {
                var bytes = Convert.FromBase64String(base64Foto);
                string filePath = "~/Adjunto/foto_operador/" + nombreFoto;
                System.IO.File.WriteAllBytes(Server.MapPath(filePath), bytes);
                respuesta.CodResultado = 1;
                respuesta.NomResultado = "Creo la foto correctamente";
            }
            catch (Exception ex)
            {
                respuesta.CodResultado = 0;
                respuesta.NomResultado = ex.Message;
            }
            return respuesta;
        }

        public JsonResult EnviarCorreoBase(string correo, string id_expediente)
        {
            var rptaEnvioCorreo = "";
            var codResultado = 0;
            try
            {

                Notificacion confirmacion = new Notificacion();
                var DatosCita = new ExpedienteBLL().BuscarCita(id_expediente);

                confirmacion.Body = "Estimado Administrado (a),<br><br>" +
                                    "Por medio de la presente le comunicamos que el Registro electrónico N° " + DatosCita.NUMERO_SID + "-" + DatosCita.NUMERO_ANIO + " de su Trámite en la ATU se realizó satisfactoriamente.<br>" +
                                    "Por favor acercarse a ventanilla el día: " + DatosCita.FECHA_CITA.ValorFechaCorta() + ",para su atención.<br><br><br>" +
                                    "Atentamente," +
                                    "<h2>Comunicaciones ATU</h2><br>" +
                                    "<hr>" +
                                    "Este mensaje de correo electrónico y / o el material adjunto puede contener información confidencial o legalmente protegida por la Ley N°  " +
                                    "29733 - Ley de Protección de Datos Personales, y es de uso exclusivo de la(s) persona(s) a quién(es)se dirige.Si no es usted el destinatario " +
                                    "indicado, queda notificado de que la lectura, uitilización, divulgación y / o copia puede estar prohibida en virtud de la legislación vigente, si " +
                                    "usted recibe este mensaje por error por favor elimine toda la información." +
                                    " <br>" +
                                     " <br>" +
                                      " <br>" +
                                    "* Este buzón es de envió automático, por favor no responder * ";


                confirmacion.To.Add(correo);
                confirmacion.asunto = "Registro de Trámite ATU";
                confirmacion.enviar();

                rptaEnvioCorreo = "CORRECTO";
                codResultado = 1;
            }
            catch (Exception ex)
            {
                rptaEnvioCorreo = "error-->" + ex.Message;
                codResultado = 0;
            }

            return Json(new
            {
                codresultado = codResultado,
                resultado = rptaEnvioCorreo
            });
        }

        public JsonResult ConsultarProvinica(int Id_Departamento)
        {
            var comboProvincia = new ProvinciaBLL().ComboProvincia(Id_Departamento);
            comboProvincia.Add(new ComboProvinciaVM() { ID_PROVINCIA = 0, NOMBRE_PROVINCIA = ".:Seleccione Provincia:." });
            return Json(new { modelo = comboProvincia.OrderBy(x => x.ID_PROVINCIA) });
        }

        public JsonResult ConsultarDistrito(int Id_Provincia)
        {
            var comboDistrito = new DistritoBLL().ComboDistrito(Id_Provincia);
            comboDistrito.Add(new ComboDistritoVM() { ID_DISTRITO = 0, NOMBRE_DISTRITO = ".:Seleccione Distrito:." });
            return Json(new { modelo = comboDistrito.OrderBy(x => x.ID_DISTRITO) });
        }

        #region Correo Nuevos

        public JsonResult enviarCorreoReno_Auto(string correo, int id_expediente, int id_modalidad, int tipo_persona, int IDDOC_HIJO, int IDDOC_PADRE)
        {
            var rptaEnvioCorreo = "";
            var codResultado = 0;
            var strPathArchivoAutorizacion = "";
            var urlArchivos = "~/Adjunto/resoluciones/";
            var pathArchivo = Server.MapPath(urlArchivos);

            try
            {
                if (tipo_persona == 1) //persona juridica
                {
                    if (id_modalidad == 1 || id_modalidad == 7)
                    {
                        strPathArchivoAutorizacion = new ReportesBLL().ReporteReno_Autorizacion(id_expediente, pathArchivo);

                    }
                    else if (id_modalidad == 2 || id_modalidad == 3 || id_modalidad == 4 || id_modalidad == 5 || id_modalidad == 6)
                    {
                        strPathArchivoAutorizacion = new ReportesBLL().ReporteReno_Autorizacion_sste(id_expediente, pathArchivo);
                    }
                }
                else
                {
                    enviarRenoAuto(correo, IDDOC_HIJO, IDDOC_PADRE);
                }

                var urlAdjuntoPDF = "";
                Notificacion confirmacion = new Notificacion();
                String[] arrResult = strPathArchivoAutorizacion.Split('|');
                String[] arregloRutasArchivo = new String[1];
                urlAdjuntoPDF = pathArchivo + arrResult[1];
                arregloRutasArchivo[0] = urlAdjuntoPDF;

                confirmacion.Body = "Estimado Administrado (a),<br><br>" +
                                    "Por medio de la presente le comunicamos que el Registro de su Trámite en la ATU se realizó satisfactoriamente.<br><br><br>" +
                                    "Atentamente," +
                                    "<h2>Comunicaciones ATU</h2><br>" +
                                    "<hr>" +
                                    "Este mensaje de correo electrónico y / o el material adjunto puede contener información confidencial o legalmente protegida por la Ley N°  " +
                                    "29733 - Ley de Protección de Datos Personales, y es de uso exclusivo de la(s) persona(s) a quién(es)se dirige.Si no es usted el destinatario " +
                                    "indicado, queda notificado de que la lectura, uitilización, divulgación y / o copia puede estar prohibida en virtud de la legislación vigente, si " +
                                    "usted recibe este mensaje por error por favor elimine toda la información." +
                                    " <br>" +
                                    "* Este buzón es de envió automático, por favor no responder * ";

                confirmacion.arrArchivosRuta = arregloRutasArchivo;
                confirmacion.To.Add(correo);
                confirmacion.asunto = "Registro de Trámite ATU";
                confirmacion.enviar();

                rptaEnvioCorreo = "CORRECTO";
                codResultado = 1;
            }
            catch (Exception ex)
            {
                rptaEnvioCorreo = "error-->" + ex.Message;
                codResultado = 0;
            }

            return Json(new
            {
                codresultado = codResultado,
                resultado = rptaEnvioCorreo
            });
        }

        #endregion
        public JsonResult enviarRenoAuto(string correo, int IDDOC_HIJO, int IDDOC_PADRE)
        {
            var rptaEnvioCorreo = "";
            var codResultado = 0;
            try
            {
                var urlArchivos = "~/Adjunto/resoluciones/";
                var pathArchivo = Server.MapPath(urlArchivos);
                var strPathArchivoGenerado_TUC = new ReportesBLL().getDatosTarjetaUnicaCirculacion(IDDOC_HIJO, pathArchivo, 1);
                var strPathArchivoGenerado_RESOLUCION = new ReportesBLL().ReporteResolucion(IDDOC_PADRE, pathArchivo);

                String[] arrResultRESOL = strPathArchivoGenerado_RESOLUCION.Split('|');
                String[] arrResult = strPathArchivoGenerado_TUC.Split('|');

                var codResultadoPDF = int.Parse(arrResult[0]);
                var urlAdjuntoPDF = "";
                if (codResultadoPDF == 0)
                {
                    return Json(new
                    {
                        codresultado = 0,
                        resultado = arrResult[1]
                    });
                }
                //
                Notificacion confirmacion = new Notificacion();

                String[] arregloRutasArchivo = new String[2];
                confirmacion.Body = "Estimado Administrado (a),<br><br>" +
                                    "Por medio de la presente le comunicamos que el Registro de su Trámite en la ATU se realizó satisfactoriamente.<br><br><br>" +
                                    "Atentamente," +
                                    "<h2>Comunicaciones ATU</h2><br>" +
                                    "<hr>" +
                                    "Este mensaje de correo electrónico y / o el material adjunto puede contener información confidencial o legalmente protegida por la Ley N°  " +
                                    "29733 - Ley de Protección de Datos Personales, y es de uso exclusivo de la(s) persona(s) a quién(es)se dirige.Si no es usted el destinatario " +
                                    "indicado, queda notificado de que la lectura, uitilización, divulgación y / o copia puede estar prohibida en virtud de la legislación vigente, si " +
                                    "usted recibe este mensaje por error por favor elimine toda la información." +
                                    " <br>" +
                                    "* Este buzón es de envió automático, por favor no responder * ";

                urlAdjuntoPDF += pathArchivo + arrResult[1];

                arregloRutasArchivo[0] = urlAdjuntoPDF;
                arregloRutasArchivo[1] = pathArchivo + arrResultRESOL[1];
                confirmacion.arrArchivosRuta = arregloRutasArchivo;
                confirmacion.To.Add(correo);
                confirmacion.asunto = "Registro de Trámite ATU";
                confirmacion.enviar();

                rptaEnvioCorreo = "CORRECTO";
                codResultado = 1;
            }
            catch (Exception ex)
            {
                rptaEnvioCorreo = "error-->" + ex.Message;
                codResultado = 0;
            }

            return Json(new
            {
                codresultado = codResultado,
                resultado = rptaEnvioCorreo
            });
        }

    }
}