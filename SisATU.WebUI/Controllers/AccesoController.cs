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
    public class AccesoController : Controller
    {
        #region Login
        public ActionResult Inicio()
        {
            var comboTipoDocumento = new ParametroBLL().ConsultaParametro(EnumParametroTipo.TipoDocumento.ValorEntero());

            DatosVentanaPrincipalVM datosVentanaPrincipal = new DatosVentanaPrincipalVM();
            var tipoPersona = new ParametroBLL().ConsultaParametro(EnumParametroTipo.TipoPersona.ValorEntero());
            var modalidades = new ModalidadServicioBLL().getModalidadByTipoPersona(0); //lista todos
            var tramites = new TramiteBLL().getListaTramiteByTipo(0); //lista todos
            datosVentanaPrincipal.ListaTipoPersona = tipoPersona;
            datosVentanaPrincipal.ListaModalidadServicio = modalidades;
            datosVentanaPrincipal.ListaTramite = tramites;
            comboTipoDocumento.Add(new ParametroModelo() { PARCOD = 0, PARNOM = ".:Tipo de Documento:." });
            datosVentanaPrincipal.SelectTipoDocumento = comboTipoDocumento.OrderBy(x => x.PARCOD)
            .Select(j => new SelectListItem
            {
                Value = j.PARSEC.ToString(),
                Text = j.PARNOM,
            }).ToList();
            return View(datosVentanaPrincipal);
        }
        public JsonResult Ingresar(string nroDocumento, string pw, int idTipoPersona, int idModalidad, int idTipoTramite, int idTipodocumento)
        {
            #region Valida Fecha y Hora de acceso
            var diaSemanaActual = DateTime.Now.DayOfWeek.ToString();
            var horaActual = DateTime.Now.Hour;

            //if ((diaSemanaActual.ToUpper() == "SATURDAY") || (diaSemanaActual.ToUpper() == "SUNDAY") || (horaActual <= 8) || (horaActual >= 20))
            //{
            //    return Json(new { codresultado = 0, mensaje = "No puede acceder al servicio porque se encuentra fuera del horario de atención. Hora de atencíon de Lunes a Viernes de 8am. a 8pm." });
            //}
            #endregion

            #region Consulta Acceso
            var consultaClaveAdministrado = new UsuarioBLL().BuscarUsuario(nroDocumento, pw.ToUpper(), idModalidad, idTipoPersona);
            if (consultaClaveAdministrado.ResultadoUsuarioVM.CodResultado != 0)
            {
                if (consultaClaveAdministrado.ResultadoUsuarioVM.Acceso == 0)
                {
                    return Json(new { codresultado = 0, mensaje = "Clave incorrecta", codauxiliar = 0 });
                }
                if (consultaClaveAdministrado.ResultadoUsuarioVM.Modalidad == 0)
                {
                    return Json(new { codresultado = 0, mensaje = "No tiene acceso a esta modalidad", codauxiliar = 0 });
                }
            }
            else
            {
                return Json(new { codresultado = 0, mensaje = "Error en consulta, por favor intente mas tarde.", codauxiliar = 0 });
            }
           
            #endregion

            #region Setea Variables de Session
            if (idModalidad == 0 && idTipoTramite == 0)
            {
                Session["TIPO_EXPEDIENTE"] = EnumTipoExpediente.NoTupa.ValorEntero();
                Session["OBSERVACION"] = "PROCEDIMIENTO NO TUPA";
            }
            else
            {
                Session["TIPO_EXPEDIENTE"] = EnumTipoExpediente.Tupa.ValorEntero();
                Session["OBSERVACION"] = "PROCEDIMIENTO TUPA";
            }
            Session["ID_MODALIDAD_SERVICIO"] = idModalidad;
            //Session["ID_TIPO_DOCUMENTO"] = idTipodocumento;
            //Session["NRO_DOCUMENTO"] = nroDocumento;
            Session["ID_TIPO_PERSONA"] = idTipoPersona;
            Session["ID_TIPO_TRAMITE"] = idTipoTramite;

            switch (idTipoPersona)
            {
                case 1: // PERSONA JURIDICA
                    var resultado = new EmpresaBLL().ConsultaRuc(nroDocumento);
                    Session["RUC"] = resultado.RUC;
                    Session["USUARIO"] = resultado.RAZON_SOCIAL.Trim();
                    Session["ID_EMPRESA"] = resultado.ID_EMPRESA.ValorEntero();
                    Session["NRO_DOCUMENTO"] = consultaClaveAdministrado.NRO_DOCUMENTO_REPRESENTANTE_LEGAL;
                    Session["CODPAIS"] = resultado.CODPAIS;
                    Session["CODDIST"] = resultado.CODDIST;
                    Session["CODDPTO"] = resultado.CODDPTO;
                    Session["CODPROV"] = resultado.CODPROV;
                    Session["DIRECCION_STD"] = resultado.DIRECCION_STD;
                    Session["FECHA_VENCIMIENTO_EXPEDIENTE"] = resultado.FECHA_VENCIMIENTO_EXPEDIENTE.ValorFechaCorta();

                    if (consultaClaveAdministrado.NRO_DOCUMENTO_REPRESENTANTE_LEGAL != "")
                    {
                        PersonaVM persona2 = new PersonaVM();
                        if (consultaClaveAdministrado.ID_TIPO_DOCUMENTO_REPRESENTANTE_LEGAL == 0)
                        {
                            if (idTipodocumento == EnumParametro.DNI.ValorEntero())
                            {
                                persona2 = new PersonaBLL().ConsultaDNI(consultaClaveAdministrado.NRO_DOCUMENTO_REPRESENTANTE_LEGAL);
                            }
                            else if (idTipodocumento == EnumParametro.CE.ValorEntero())
                            {
                                persona2 = new PersonaBLL().ConsultarCE(consultaClaveAdministrado.NRO_DOCUMENTO_REPRESENTANTE_LEGAL);
                            }
                            else if (idTipodocumento == EnumParametro.PTP.ValorEntero())
                            {
                                persona2 = new PersonaBLL().ConsultarPTP(consultaClaveAdministrado.NRO_DOCUMENTO_REPRESENTANTE_LEGAL);
                            }

                            Session["ID_TIPO_DOCUMENTO"] = idTipodocumento;
                        }
                        else
                        {
                            if (consultaClaveAdministrado.ID_TIPO_DOCUMENTO_REPRESENTANTE_LEGAL == EnumParametro.DNI.ValorEntero())
                            {
                                persona2 = new PersonaBLL().ConsultaDNI(consultaClaveAdministrado.NRO_DOCUMENTO_REPRESENTANTE_LEGAL);
                            }
                            else if (consultaClaveAdministrado.ID_TIPO_DOCUMENTO_REPRESENTANTE_LEGAL == EnumParametro.CE.ValorEntero())
                            {
                                persona2 = new PersonaBLL().ConsultarCE(consultaClaveAdministrado.NRO_DOCUMENTO_REPRESENTANTE_LEGAL);
                            }
                            else if (consultaClaveAdministrado.ID_TIPO_DOCUMENTO_REPRESENTANTE_LEGAL == EnumParametro.PTP.ValorEntero())
                            {
                                persona2 = new PersonaBLL().ConsultarPTP(consultaClaveAdministrado.NRO_DOCUMENTO_REPRESENTANTE_LEGAL);
                            }
                            Session["ID_TIPO_DOCUMENTO"] = consultaClaveAdministrado.ID_TIPO_DOCUMENTO_REPRESENTANTE_LEGAL;
                        }
                        
                        Session["NOMBRES"] = persona2.NOMBRES;
                        Session["APELLIDO_PATERNO"] = persona2.APELLIDO_PATERNO;
                        Session["APELLIDO_MATERNO"] = persona2.APELLIDO_MATERNO;
                        Session["ID_PERSONA"] = persona2.ID_PERSONA;
                        Session["CODPAIS"] = persona2.CODPAIS;
                        Session["CODDIST"] = persona2.CODDIST;
                        Session["CODDPTO"] = persona2.CODDPTO;
                        Session["CODPROV"] = persona2.CODPROV;

                        Session["DIRECCION_STD"] = persona2.DIRECCION_STD;
                        Session["DIRECCION"] = persona2.DIRECCION;
                        Session["DIRECCION_ACTUAL"] = persona2.DIRECCION_ACTUAL;
                        Session["TELEFONO"] = persona2.TELEFONO;
                        Session["CORREO"] = persona2.CORREO;
                        Session["ID_DEPARTAMENTO"] = persona2.ID_DEPARTAMENTO;
                        Session["ID_PROVINCIA"] = persona2.ID_PROVINCIA;
                        Session["ID_DISTRITO"] = persona2.ID_DISTRITO;

                    }
                    break;
                case 2: // PERSONA NATURAL
                    PersonaVM persona = new PersonaVM();
                    if (idTipodocumento == EnumParametro.DNI.ValorEntero())
                    {
                        persona = new PersonaBLL().ConsultaDNI(nroDocumento);
                    }
                    else if (idTipodocumento == EnumParametro.CE.ValorEntero())
                    {
                        persona = new PersonaBLL().ConsultarCE(nroDocumento);
                    }
                    else if (idTipodocumento == EnumParametro.PTP.ValorEntero())
                    {
                        persona = new PersonaBLL().ConsultarPTP(nroDocumento);
                    }
                    Session["ID_PERSONA"] = persona.ID_PERSONA;
                    Session["NRO_DOCUMENTO"] = nroDocumento;
                    Session["USUARIO"] = persona.NOMBRES;
                    Session["APELLIDO_PATERNO"] = persona.APELLIDO_PATERNO;
                    Session["APELLIDO_MATERNO"] = persona.APELLIDO_MATERNO;
                    Session["CODPAIS"] = persona.CODPAIS;
                    Session["CODDIST"] = persona.CODDIST;
                    Session["CODDPTO"] = persona.CODDPTO;
                    Session["CODPROV"] = persona.CODPROV;
                    Session["ID_TIPO_DOCUMENTO"] = idTipodocumento;
                    Session["DIRECCION_STD"] = persona.DIRECCION_STD;
                    Session["DIRECCION"] = persona.DIRECCION;
                    Session["DIRECCION_ACTUAL"] = persona.DIRECCION_ACTUAL;
                    Session["TELEFONO"] = persona.TELEFONO;
                    Session["CORREO"] = persona.CORREO;
                    Session["ID_DEPARTAMENTO"] = persona.ID_DEPARTAMENTO;
                    Session["ID_PROVINCIA"] = persona.ID_PROVINCIA;
                    Session["ID_DISTRITO"] = persona.ID_DISTRITO;
                    break;
            }
            #endregion

            return Json(new { codresultado = 1, mensaje = "Ok", codauxiliar = 0 });
        }

        #endregion

        #region Crear Cuenta
        public ActionResult RegistrarCuenta()
        {
            UsuarioVM usuario = new UsuarioVM();
            var combotipoPersona = new ParametroBLL().ConsultaParametro(EnumParametroTipo.TipoPersona.ValorEntero());
            var comboTipoDocumento = new ParametroBLL().ConsultaParametro(EnumParametroTipo.TipoDocumento.ValorEntero());

            comboTipoDocumento.Add(new ParametroModelo() { PARCOD = 0, PARNOM = ".:Tipo de Documento:." });
            combotipoPersona.Add(new ParametroModelo() { PARCOD = 0, PARNOM = ".:Tipo de Persona:." });

            usuario.SelectTipoDocumento = comboTipoDocumento.OrderBy(x => x.PARCOD)
            .Select(j => new SelectListItem
            {
                Value = j.PARSEC.ToString(),
                Text = j.PARNOM,
            }).ToList();

            usuario.SelectTipoPersona = combotipoPersona.OrderBy(x => x.PARCOD)
            .Select(j => new SelectListItem
            {
                Value = j.PARSEC.ToString(),
                Text = j.PARNOM,
            }).ToList();
            return PartialView(usuario);
        }

        public ActionResult registrarAdministrado(UsuarioVM modelo)
        {
            modelo.NRO_DOCUMENTO = (modelo.ID_TIPO_DOCUMENTO == EnumParametro.DNI.ValorEntero() ? modelo.DNI : modelo.NRO_DOCUMENTO);
            UsuarioVM usuario = new UsuarioVM();

            usuario.NOMBRE_USUARIO = modelo.NRO_DOCUMENTO;
            usuario.CLAVE = RandomString(6);
            usuario.ID_TIPO_PERSONA = modelo.ID_TIPO_PERSONA;
            usuario.ID_TIPO_DOCUMENTO = modelo.ID_TIPO_DOCUMENTO;
            usuario.NRO_DOCUMENTO = modelo.NRO_DOCUMENTO;
            usuario.RAZON_SOCIAL = modelo.RAZON_SOCIAL;
            usuario.TELEFONO = modelo.TELEFONO;
            usuario.CORREO = modelo.CORREO;
            usuario.DIRECCION = modelo.DIRECCION;
            usuario.NOMBRES = modelo.NOMBRES;
            usuario.APEPAT = modelo.APEPAT;
            usuario.APEMAT = modelo.APEMAT;
            usuario.NRO_DOCUMENTO_REPRESENTANTE_LOCAL = modelo.NRO_DOCUMENTO_REPRESENTANTE_LOCAL;
            usuario.ID_TIPO_DOCUMENTO_REPRESENTANTE_LEGAL = modelo.ID_TIPO_DOCUMENTO_REPRESENTANTE_LEGAL;

            //verifica si el usuario ya fue creado
            var resultadoBusqueda = new UsuarioBLL().BuscarUsuario(usuario.NRO_DOCUMENTO, "", 0, usuario.ID_TIPO_PERSONA);

            if (resultadoBusqueda.ResultadoUsuarioVM.Validacion == 1)
            {
                return Json(new { codresultado = 0, mensaje = "Ya existe un usuario para el nro documento " + usuario.NRO_DOCUMENTO });
            }

            if (resultadoBusqueda.ResultadoUsuarioVM.Validacion == 3)
            {
                return Json(new { codresultado = 0, mensaje = "La empresa " + usuario.NRO_DOCUMENTO + " no esta autorizada por el ATU." });
            }


            var resultadoRegistra = new UsuarioBLL().CrearUsuario(usuario);

            List<ComboModalidadServicioVM> resultaModalidadServicio = new List<ComboModalidadServicioVM>();

            if (resultadoRegistra.CodResultado == 1)
            {
                if (modelo.ID_TIPO_PERSONA == EnumParametroTipoPersona.PersonaJuridica.ValorEntero())
                {
                    resultaModalidadServicio = new UsuarioBLL().BuscarModalidad(usuario.NRO_DOCUMENTO);

                    foreach (var item in resultaModalidadServicio)
                    {
                        var resultadoModalidad = new UsuarioBLL().CrearModalidadServicio(usuario.NRO_DOCUMENTO, item.ID_MODALIDAD_SERVICIO);
                    }
                }

                enviarConfirmacion(usuario.CORREO, usuario.NRO_DOCUMENTO, usuario.CLAVE, resultaModalidadServicio);
                return Json(new { codresultado = 1, mensaje = "Registró correctamente" });
            }
            else
            {
                return Json(new { codresultado = 0, mensaje = resultadoRegistra.NomResultado });
            }
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        #region Enviar Correo ingreso
        public JsonResult enviarConfirmacion(string correo, string DNI, string clave, List<ComboModalidadServicioVM> ModalidadServicio)
        {
            var rptaEnvioCorreo = "";
            var codResultado = 0;
            try
            {
                String htmlListaOperadores = "<ul>";
                if (ModalidadServicio.Count >= 1)
                {
                    foreach (var item in ModalidadServicio)
                    {
                        htmlListaOperadores += "<li>" + item.NOMBRE + "</li>";
                    }
                }
                else
                {
                    htmlListaOperadores += "<li> SERVICIO DE TAXI INDEPENDIENTE </li>";
                }

                htmlListaOperadores += "</ul>";

                Notificacion confirmacion = new Notificacion();
                confirmacion.Body = "Estimado Administrado (a),<br><br>" +
                                    "Por medio de la presente le comunicamos que la creación de su usuario para acceder a realizar trámites en la ATU se realizó satisfactoriamente.<br>" +
                                    "Usuario: " + DNI + "<br>" +
                                    "Clave: " + clave + "<br>" +
                                    "Modalidad(es): <br>" +
                                    htmlListaOperadores + "<br><br>" +
                                    "Favor de ingresar al siguiente link de acceso para realizar sus trámites: http://sistemas.protransporte.gob.pe/SISSIT_2 <br><br>" +
                                    "<h2>Comunicaciones ATU</h2><br>" +
                                    "<hr>" +
                                    "Este mensaje de correo electrónico y / o el material adjunto puede contener información confidencial o legalmente protegida por la Ley N°  " +
                                    "29733 - Ley de Protección de Datos Personales, y es de uso exclusivo de la(s) persona(s) a quién(es)se dirige.Si no es usted el destinatario " +
                                    "indicado, queda notificado de que la lectura, uitilización, divulgación y / o copia puede estar prohibida en virtud de la legislación vigente, si " +
                                    "usted recibe este mensaje por error por favor elimine toda la información." +
                                    " <br>" +
                                    "* Este buzón es de envió automático, por favor no responder * ";


                confirmacion.To.Add(correo);
                confirmacion.asunto = "Registro de Usuarios - ATU";
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
        #endregion

        #region Recuperar Contraseña
        public ActionResult RecuperarContrasenia()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult RecuperarContrasenia(UsuarioVM usuario)
        {

            ResultadoProcedimientoVM resultado = new ResultadoProcedimientoVM();
            usuario.CLAVE = RandomString(6);

            resultado = new PersonaBLL().RecuperarContrasena(usuario.NRO_DOCUMENTO, usuario.CORREO, usuario.CLAVE);

            if (resultado.CodResultado == 0)
            {
                return Json(new { success = false, mensaje = resultado.NomResultado });
            }
            else
            {
                var rptaEnvioCorreo = "";
                var codResultado_correo = 0;
                try
                {
                    Notificacion confirmacion = new Notificacion();
                    String[] arregloRutasArchivo = new String[1];

                    confirmacion.Body = "Estimado Administrado (a),<br><br>" +
                                        "Por medio de la presente le comunicamos que la solicitud de recuperación de contraseña se realizó satisfactoriamente.<br>" +
                                        "La contraseña para acceder al sistema: " + usuario.CLAVE + " <br><br>" +
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
                    confirmacion.To.Add(usuario.CORREO);
                    confirmacion.asunto = "Recuperación de Contraseñas - ATU";
                    confirmacion.enviar();

                    rptaEnvioCorreo = "CORRECTO";
                    codResultado_correo = 1;
                }
                catch (Exception ex)
                {
                    rptaEnvioCorreo = "error-->" + ex.Message;
                    codResultado_correo = 0;
                }

                return Json(new { success = true, mensaje = resultado.NomResultado, clave = resultado.CLAVE_NUEVO, codresultado = codResultado_correo, resultado = rptaEnvioCorreo });
            }

        }
        #endregion 
    }
}