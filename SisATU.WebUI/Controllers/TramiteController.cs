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
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.IO;
using System.Transactions;
using SisATU.Datos;

namespace SisATU.WebUI.Controllers
{
    public class TramiteController : Controller
    {
        private Object bdConn;
        // GET: Tramite
        public ActionResult Inicio()
        {

            DatosVentanaPrincipalVM datosVentanaPrincipal = new DatosVentanaPrincipalVM();
            var comboTipoDocumento = new ParametroBLL().ConsultaParametro(EnumParametroTipo.TipoDocumento.ValorEntero());

            var tipoPersona = new ParametroBLL().ConsultaParametro(EnumParametroTipo.TipoPersona.ValorEntero());
            var modalidades = new ModalidadServicioBLL().getModalidadByTipoPersona(0); //lista todos
            var tramites = new TramiteBLL().getListaTramiteByTipo(0); //lista todos
            var multas = new TramiteSATBLL().listarMultas();
            var ComboEntidadBancaria = new EntidadBancariaBLL().ConsultaComboEntidadBancaria();

            datosVentanaPrincipal.ListaTipoPersona = tipoPersona;
            datosVentanaPrincipal.ListaModalidadServicio = modalidades;
            datosVentanaPrincipal.ListaTramite = tramites;
            datosVentanaPrincipal.ListaMultas = multas;
            datosVentanaPrincipal.ListaEntidadBancaria = ComboEntidadBancaria;
            datosVentanaPrincipal.SelectTipoDocumento = comboTipoDocumento.OrderBy(x => x.PARCOD)
            .Select(j => new SelectListItem
            {
                Value = j.PARSEC.ToString(),
                Text = j.PARNOM,
            }).ToList();

            return View(datosVentanaPrincipal);
        }
        public string getDatosReniec(string nroDocumento)
        {
            var datosPersonaSITU = new PersonaBLL().consultaPersonaSITU(nroDocumento);
            JavaScriptSerializer jss = new JavaScriptSerializer();
            PersonaVM personaResult = new PersonaVM();
            personaResult = new PersonaBLL().consultaDatosReniec(nroDocumento); //consulta a la PIDE


            //if (datosPersonaSITU.NRO_DOCUMENTO == null)//si no encuentra en el situ 
            //{
            //    personaResult = new PersonaBLL().consultaDatosReniec(nroDocumento); //consulta a la PIDE
            //}
            //else
            //{
            //    personaResult = datosPersonaSITU;
            //}

            string output = jss.Serialize(personaResult);
            return output;
        }

        public string validaVoucherBNacion(int idProcedimiento, string nroRecibo, string fecha)
        {
            // 000186 v bn -- fecha --- 2020 04 21
            //
            ResultadoProcedimientoVM resultado = new ResultadoProcedimientoVM();
            var validaExisteVoucher = new ExpedienteBLL().ConsultaRecibo(nroRecibo);

            if (validaExisteVoucher.CodResultado == 1) // si existe el vucher
            {
                resultado.CodResultado = 1;
                resultado.NomResultado = validaExisteVoucher.NomResultado;
                //return resultado;
            } else
            {
                string fechaMes = Convert.ToDateTime(fecha).Month.ToString("D2");
                string fechaDia = Convert.ToDateTime(fecha).Day.ToString("D2");
                string fechaAnio = Convert.ToDateTime(fecha).Year.ToString();
                var fechaConvertida = fechaAnio + fechaMes + fechaDia;
                //
                resultado = new PagoBLL().ConsultarNacion(idProcedimiento, nroRecibo, fechaConvertida);
                //
                if (resultado.CodAuxiliar == 0)//el voucher no es valido
                {
                    resultado.CodResultado = 0;
                    resultado.NomResultado = "No es válido.";
                }
                else
                {
                    resultado.CodResultado = 1;
                    resultado.NomResultado = "Es válido.";
                    //es valido
                }
            }

            JavaScriptSerializer jss = new JavaScriptSerializer();
            string rptaJSON = jss.Serialize(resultado);
            return rptaJSON;
        }
        public string validaVoucherScotiabank(int idProcedimiento, int idModalidadServicio, string nroRecibo, string fecha)
        {
            ResultadoProcedimientoVM resultado = new ResultadoProcedimientoVM();
            var validaExisteVoucher = new ExpedienteBLL().ConsultaRecibo(nroRecibo);

            if (validaExisteVoucher.CodResultado == 1) // si existe el vucher
            {
                resultado.CodResultado = 1;
                resultado.NomResultado = validaExisteVoucher.NomResultado;
                //return resultado;
            }
            else
            {
                string fechaMes = Convert.ToDateTime(fecha).Month.ToString("D2");
                string fechaDia = Convert.ToDateTime(fecha).Day.ToString("D2");
                string fechaAnio = Convert.ToDateTime(fecha).Year.ToString();
                var fechaConvertida = fechaAnio + fechaMes + fechaDia;
                //
                resultado = new PagoBLL().ConsultarScotiabank(idModalidadServicio, idProcedimiento, nroRecibo, fechaConvertida);
                //
                if (resultado.CodAuxiliar == 0)//el voucher no es valido
                {
                    resultado.CodResultado = 0;
                    resultado.NomResultado = "No es válido.";
                }
                else
                {
                    resultado.CodResultado = 1;
                    resultado.NomResultado = "Es válido.";
                    //es valido
                }
            }

            JavaScriptSerializer jss = new JavaScriptSerializer();
            string rptaJSON = jss.Serialize(resultado);
            return rptaJSON;
        }
        public string getDatosExt(string nroDocumento, string tipoDocumento)
        {
            var datosPersona = new PersonaBLL().consultaDatosExt(nroDocumento, tipoDocumento);
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string output = jss.Serialize(datosPersona);
            return output;
        }
        public JsonResult registrarTramite(int tipoPersona, int modalidad, int tramite, int procedimiento,
                                            int tipoDocumento, string nroDocumento, string apepat, string apemat,
                                            string nombres, string reciboPago, string email, string ruc,
                                            int autorizaEmail, string nroTelefono,string direccion, 
                                            int idBanco, string fechaPago, HttpPostedFileBase[] files)
        {
            TramiteSimpleVM t = new TramiteSimpleVM();
            t.TIPO_PERSONA = tipoPersona;
            t.IDMODALIDAD = modalidad;
            t.IDPROCEDIMIENTO = procedimiento;
            t.IDTIPODOCUMENTO = tipoDocumento;
            t.NRODOCUMENTO = nroDocumento;
            t.APEPAT = apepat;
            t.APEMAT = apemat;
            t.NOMBRES = nombres;
            t.NRORECIBOPAGO = reciboPago;
            t.CORREOELECTRONICO = email;
            t.RUC = ruc;
            t.ID_TIPO_PERSONA = tipoPersona;
            t.AUTORIZA_EMAIL = autorizaEmail;
            t.NRO_TELEF = nroTelefono;
            t.DIRECCION = direccion;
            t.PLACA = "";
            t.IDBANCO = idBanco;
            t.FECHA_PAGO = fechaPago;
            //Verifica El nro del recibo
            if (t.NRORECIBOPAGO.Length > 0)
            {
                var resultadoBusqueda = new TramiteSimpleBLL().busqueda_recibo(t.NRORECIBOPAGO);
                if (resultadoBusqueda.CodResultado == 1)
                {
                    return Json(new
                    {
                        codResultado = 0,
                        desResultado = resultadoBusqueda.NomResultado,
                        codAuxiliar = 0,
                        resultadoex = ""
                    });
                }
            }


            //Persona TRAMITE
            PersonaVM solicitante = new PersonaVM();
            solicitante.NRO_DOCUMENTO = t.NRODOCUMENTO;
            solicitante.APELLIDO_PATERNO = t.APEPAT;
            solicitante.APELLIDO_MATERNO = t.APEMAT;
            solicitante.NOMBRES = t.NOMBRES;
            solicitante.direccion = "";
            solicitante.NRO_RUC = t.RUC;
            solicitante.CORREO = t.CORREOELECTRONICO;

            //Busca persona en STD si no lo encuentra entonces lo crea
            //var resultadoBusqueda = new PersonaBLL().ConsultaPersonaSTD(t.NRODOCUMENTO);
            var codResultado = 0;
            var desResultado = "";
            var codAuxiliar = "";
            var resultadoex = "";
            using (TransactionScope scope = new TransactionScope())
            {
                var registroTramite = new TramiteSimpleBLL().registrarTramiteSimple(t);
                try
                {
                    if (registroTramite.CodAuxiliar > 0)
                    {
                        #region  registrar STD
                        var personaSTD = new PersonaVM();
                        var empresaSTD = new EmpresaVM();

                        switch (t.TIPO_PERSONA)
                        {
                            case 1: //juridica 
                                #region consulta STD persona juridica si no lo encuentra entonces crea en STD
                                empresaSTD = new EmpresaBLL().ConsultaEmpresaSTD(solicitante.NRO_RUC); // persona
                                #endregion
                                break;
                            case 2: //persona natural
                                #region consulta STD persona si no lo encuentra entonces crea en STD
                                personaSTD = new PersonaBLL().ConsultaPersonaSTD(solicitante); // persona
                                #endregion
                                break;
                            default:
                                break;
                        }

                        //setea datos para STD segun tipo  Persona
                        STDVM resultadoExpediente = new STDVM();
                        switch (t.TIPO_PERSONA)
                        {
                            case 1: //juridica 
                                #region consulta STD persona juridica si no lo encuentra entonces crea en STD
                                STDVM STD_JURIDICA = new STDVM()
                                {
                                    IDUNIDAD_STD = 7, //unidad de la institucion
                                    CODPAIS = empresaSTD.CODPAIS,
                                    CODDPTO = empresaSTD.CODDPTO,
                                    CODPROV = empresaSTD.CODPROV,
                                    CODDIST = empresaSTD.CODDIST,
                                    DIRECCION_STD = personaSTD.direccion,
                                    NOMBRE = personaSTD.NOMBRES,

                                    ID_PROCEDIMIENTO = t.IDPROCEDIMIENTO,
                                    ID_PROVEEDOR = empresaSTD.ID_EMPRESA,
                                    //Proceso TUPA STD
                                    TIPO_EXPEDIENTE = 3,
                                    OBSERVACION = "PROCEDIMIENTO TUPA",
                                };
                                resultadoExpediente = new ExpedienteBLL().creaSoloExpediente(STD_JURIDICA);
                                #endregion
                                break;
                            case 2: //persona natural
                                #region registra expediente en STD

                                STDVM STD_NATURAL = new STDVM()
                                {
                                    IDUNIDAD_STD = 7, //unidad de la institucion
                                    CODPAIS = personaSTD.CODPAIS,
                                    CODDPTO = personaSTD.CODDPTO,
                                    CODPROV = personaSTD.CODPROV,
                                    CODDIST = personaSTD.CODDIST,
                                    DIRECCION_STD = personaSTD.direccion,
                                    NOMBRE = personaSTD.NOMBRES,
                                    ID_PERSONA = personaSTD.ID_PERSONA,
                                    ID_PROCEDIMIENTO = t.IDPROCEDIMIENTO,
                                    //ID_PROVEEDOR = modelo.ID_EMPRESA,
                                    //Proceso TUPA STD
                                    TIPO_EXPEDIENTE = 3,
                                    OBSERVACION = "PROCEDIMIENTO TUPA",
                                };

                                resultadoExpediente = new ExpedienteBLL().creaSoloExpediente(STD_NATURAL);
                                #endregion
                                break;
                            default:
                                break;
                        }
                        #endregion

                        int contador = 0;
                        try
                        {
                            var nombreArchivosSerializado = "";
                            foreach (HttpPostedFileBase file in files)
                            {
                                if(file != null)
                                {
                                    contador++;
                                    var nombreArchivo = Path.GetFileName(file.FileName);
                                    var extensionArchivo = Path.GetExtension(file.FileName);
                                    var arrFileName = nombreArchivo.Split('.');
                                    var nuevoNombreArchivoExcel = "tramite_" + registroTramite.CodAuxiliar.ToString() + "_archivo_" + contador.ToString() + extensionArchivo;
                                    var pathArchivo = Server.MapPath("~/Adjunto/tramites/" + nuevoNombreArchivoExcel);
                                    string pathFinal = Path.Combine(pathArchivo);
                                    file.SaveAs(pathFinal);
                                    nombreArchivosSerializado += nuevoNombreArchivoExcel + "|";
                                }
                              
                            }
                            var ssid_exp = resultadoExpediente.NUMERO_SID.ToString() + "-" + resultadoExpediente.NUMERO_ANIO;
                            var actualizacionNombresArchivo = new TramiteSimpleBLL().actualizarDataTramiteSimple(registroTramite.CodAuxiliar, nombreArchivosSerializado, resultadoExpediente.IDDOC, ssid_exp);

                            codResultado = actualizacionNombresArchivo.CodResultado;
                            desResultado = actualizacionNombresArchivo.NomResultado;

                            if (codResultado == 1 && resultadoExpediente.IDDOC > 0)
                            {
                                scope.Complete();
                                if (t.AUTORIZA_EMAIL == 1)
                                {
                                    var rptaEnvioCorreo = enviarCorreoSimple(solicitante.CORREO, solicitante.NOMBRES + " " + solicitante.APELLIDO_PATERNO + " " + solicitante.APELLIDO_MATERNO, solicitante.NRO_DOCUMENTO, resultadoExpediente.NUMERO_SID.ToString(), resultadoExpediente.NUMERO_ANIO);

                                    if (rptaEnvioCorreo == 0)
                                    {
                                        codResultado = 0;
                                        desResultado = "Ocurrio un error en el registro del trámite. (ERR5)";
                                        scope.Dispose();
                                        Conexion.finalizar(ref bdConn);
                                    }
                                }

                                codAuxiliar = resultadoExpediente.NUMERO_SID.ToString() + "-" + resultadoExpediente.NUMERO_ANIO;
                             

                            } else
                            {
                                codResultado = 0;
                                desResultado = "Ocurrio un error en el registro del trámite.(ERR4)";
                                scope.Dispose();
                                Conexion.finalizar(ref bdConn);
                            }
                        }
                        catch (Exception ex)
                        {
                            codResultado = 0;
                            desResultado = "Ocurrio un error en la carga de archivos.(ERR3)";
                            resultadoex = ex.Message;
                            scope.Dispose();
                            Conexion.finalizar(ref bdConn);
                        }
                        //codAuxiliar = registroTramite.CodAuxiliar;
                    }
                    else
                    {
                        codResultado = 0;
                        desResultado = "Ocurrió un error en el registro del trámite.";
                        scope.Dispose();
                        Conexion.finalizar(ref bdConn);
                    }
                }
                catch (Exception ex)
                {
                    codResultado = 0;
                    desResultado = "Ocurrió un error en el registro del trámite.(ERR2)";
                    resultadoex = ex.Message;
                    scope.Dispose();
                    Conexion.finalizar(ref bdConn);
                }
            }
            return Json(new {
                codResultado = codResultado,
                desResultado = desResultado,
                codAuxiliar = codAuxiliar,
                resultadoex = resultadoex
            });
        }

        public JsonResult registrarTramiteSAT(int tipoPersona, int modalidad, int tramite, int procedimiento,
                                            int tipoDocumento, string nroDocumento, string apepat, string apemat,
                                            string nombres, string reciboPago, string email, string ruc,
                                            int autorizaEmail, string nroTelefono, string direccion, 
                                            string nroactacontrol, string fechaacta,
                                            int diashabiles, string idMulta, string placa, string nroressancion,
                                            double montoinresol, string fechaResol, int flagpresentorecurso,
                                            int idbanco, string fechapago, string nrorecibo, double montocancelado,
                                            double montoenresol,int nrocuotas, HttpPostedFileBase[] files)
        {
            var codResultado = 0;
            var desResultado = "";
            var codAuxiliar = 0;
            var desResultadoEx = "";

            //seteando datos del tramite simple
            TramiteSimpleVM t = new TramiteSimpleVM();
            t.TIPO_PERSONA = tipoPersona;
            t.IDMODALIDAD = modalidad;
            t.IDPROCEDIMIENTO = procedimiento;
            t.IDTIPODOCUMENTO = tipoDocumento;
            t.NRODOCUMENTO = nroDocumento;
            t.APEPAT = apepat;
            t.APEMAT = apemat;
            t.NOMBRES = nombres;
            t.NRORECIBOPAGO = "";
            t.CORREOELECTRONICO = email;
            t.RUC = ruc;
            t.ID_TIPO_PERSONA = tipoPersona;
            t.AUTORIZA_EMAIL = autorizaEmail;
            t.NRO_TELEF = nroTelefono;
            t.DIRECCION = direccion;

            //seteando datos para el tramite SAT
            TramiteSATVM tramiteSAT = new TramiteSATVM();
            tramiteSAT.ID_TRAMITE = 0;
            tramiteSAT.NRO_ACTA_CONTROL = nroactacontrol;
            tramiteSAT.FECHA_ACTA = fechaacta;
            tramiteSAT.DIAS_HABILES_ACTA = diashabiles;
            tramiteSAT.ID_MULTA = idMulta;
            tramiteSAT.PLACA = placa;
            tramiteSAT.NRO_RESOL_SANC = nroressancion;
            tramiteSAT.MONTO_IN_RESOL = montoinresol;
            tramiteSAT.FECHA_RESOLUCION = fechaResol;
            tramiteSAT.FLAG_PRESENTO_RECURSO = flagpresentorecurso;
            tramiteSAT.ID_BANCO = idbanco;
            tramiteSAT.FECHA_PAGO = fechapago;
            tramiteSAT.NRO_RECIBO = nrorecibo;
            tramiteSAT.MONTO_CANCELADO = montocancelado;
            tramiteSAT.MONTO_IN_RESOL = montoenresol;
            tramiteSAT.NRO_CUOTAS = nrocuotas;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    var registroTramite = new TramiteSimpleBLL().registrarTramiteSimple(t);

                    if (registroTramite.CodResultado == 1)
                    {
                        tramiteSAT.ID_TRAMITE = registroTramite.CodAuxiliar;
                        var registroTramiteSAT = new TramiteSATBLL().registrarTramiteSAT(tramiteSAT);
                        //
                        if (registroTramiteSAT.CodResultado == 1)//registro tramite sat
                        {
                            int confirmacionCorreo = enviarCorreoTramiteSAT(t.CORREOELECTRONICO, t.NOMBRES + " " + t.APEPAT + " " + t.APEMAT, t.NRODOCUMENTO);
                            codResultado = 1;
                            desResultado = "El trámite fue registrado correctamente";
                            scope.Complete();
                        }
                        else
                        {
                            codResultado = 0;
                            desResultado = "Ocurrio un error en el registro del trámite SAT.";
                            scope.Dispose();
                        }
                    }
                    else
                    {
                        codResultado = 0;
                        desResultado = registroTramite.NomResultado;
                        //desResultado = "Ocurrio un error en el registro del trámite virtual.";
                        scope.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                codResultado = 0;
                desResultado = "Ocurrio un error en el registro del trámite virtual.";
                desResultadoEx = ex.Message;
            }


            return Json(new
            {
                codResultado = codResultado,
                desResultado = desResultado,
                codAuxiliar = codAuxiliar,
                resultadoex = desResultadoEx 
            });
        }

        public string getProcedimientoByFiltro(int tipoPersona, int idModalidad, int tipoTramite)
        {
            var comboTipoProcedimiento = new ModalidadServicioBLL().getProcedimientosByFiltro(tipoPersona, idModalidad, tipoTramite);
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string output = jss.Serialize(comboTipoProcedimiento);

            return output;
        }


        public int enviarCorreoSimple(string correo, string persona, string DNI, string nro_sid, string anio)
        {
            var codResultado = 0;
            try
            {
                Notificacion confirmacion = new Notificacion();
                confirmacion.Body = "Estimado (a) " + persona + " ,<br><br>" +
                                    "Por medio de la presente le comunicamos que el Registro de su Trámite en la ATU se realizó satisfactoriamente.<br><br>" +
                                    "Se ha generado el expediente número <strong>" + nro_sid + "-" + anio + " </strong>" +
                         
                                    "<br><br>Atentamente," +
                                    "<h2>Comunicaciones ATU</h2><br>" +
                                    "<hr>" +
                                    "Este mensaje de correo electrónico y / o el material adjunto puede contener información confidencial o legalmente protegida por la Ley N°  " +
                                    "29733 - Ley de Protección de Datos Personales, y es de uso exclusivo de la(s) persona(s) a quién(es)se dirige.Si no es usted el destinatario " +
                                    "indicado, queda notificado de que la lectura, uitilización, divulgación y / o copia puede estar prohibida en virtud de la legislación vigente, si " +
                                    "usted recibe este mensaje por error por favor elimine toda la información." +
                                    " <br>" +
                                    "* Este buzón es de envió automático, por favor no responder * ";

                confirmacion.arrArchivosRuta = null;
                confirmacion.To.Add(correo);
                confirmacion.asunto = "Registro de Trámite en ventanilla virtual ATU";
                confirmacion.enviar();
                codResultado = 1;
            }
            catch (Exception ex)
            {
                codResultado = 0;
            }

            return codResultado;
        }



        public int enviarCorreoTramiteSAT(string correo, string persona, string DNI)
        {
            var codResultado = 0;
            try
            {
                Notificacion confirmacion = new Notificacion();
                confirmacion.Body = "Estimado (a) " + persona + " ,<br><br>" +
                                    "Por medio de la presente le comunicamos que el Registro de su Trámite en la ATU se realizó satisfactoriamente.<br><br>" +
                                    "Se ha generado su trámite correctamente.<strong>" +
                                    "<br><br>Atentamente," +
                                    "<h2>Comunicaciones ATU</h2><br>" +
                                    "<hr>" +
                                    "Este mensaje de correo electrónico y / o el material adjunto puede contener información confidencial o legalmente protegida por la Ley N°  " +
                                    "29733 - Ley de Protección de Datos Personales, y es de uso exclusivo de la(s) persona(s) a quién(es)se dirige.Si no es usted el destinatario " +
                                    "indicado, queda notificado de que la lectura, uitilización, divulgación y / o copia puede estar prohibida en virtud de la legislación vigente, si " +
                                    "usted recibe este mensaje por error por favor elimine toda la información." +
                                    " <br>" +
                                    "* Este buzón es de envió automático, por favor no responder * ";

                confirmacion.arrArchivosRuta = null;
                confirmacion.To.Add(correo);
                confirmacion.asunto = "Registro de Trámite en ventanilla virtual ATU";
                confirmacion.enviar();
                codResultado = 1;
            }
            catch (Exception ex)
            {
                codResultado = 0;
            }

            return codResultado;
        }


        public void validarDNI()
        {
            //aqui aver

        }
        public string getRequisitosByIdProcedimiento(int idProcedimiento)
        {
            var requisitos = new RequisitosProcedimientosBLL().ComboRequisitosProcedimiento(idProcedimiento);
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string output = jss.Serialize(requisitos);
            return output;
        }

    }
}