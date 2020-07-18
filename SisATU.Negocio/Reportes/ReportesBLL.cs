using CrystalDecisions.CrystalReports.Engine;
using SisATU.Base.ViewModel;
using SisATU.Datos;
using SisATU.Negocio.Reportes;
using SisATU.Negocio.Reportes.Credenciales;
using SisATU.Negocio.Reportes.Credenciales.Constancia;
using SisATU.Negocio.Reportes.Resoluciones.Credenciales;
using SisATU.Negocio.Reportes.Renovacion_Autorizacion;
using SisATU.Negocio.Reportes.Resoluciones.resolucion_taxi_independiente;
using SISCRE.WEB.ADM.Documentos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SisATU.Negocio
{
    public class ReportesBLL
    {
        ReporteDAL reporteDAL = new ReporteDAL();
        public string getDatosTarjetaUnicaCirculacion(int idExpedienteHijo, string rutaArchivo, int tipoModalidad)
        {
            var tarjetaCirculacion = reporteDAL.getDatosTarjetaUnicaCirculacion(idExpedienteHijo);

            var resultado = "";
            try
            {
                dtsConstanciaTaxiTuc datosConstancia = new dtsConstanciaTaxiTuc();

                //var GTU = new VehiculoBLL().Datos_Vehiculo(NroPlaca, ref mensaje, ref tipo);
                //if (GTU != null)
                //{
                //DATOS DEL VEHICULO
                var dtVehiculoDatos = new dtsConstanciaTaxiTuc.DtVehiculoDatosDataTable();
                var dr = dtVehiculoDatos.NewRow();
                // using the method 
                String[] arrFechaImpresion = tarjetaCirculacion.FECHA_IMPRESION.Split(' ');
                String[] arrFechaVencimiento = tarjetaCirculacion.FECHA_VENCIMIENTO_DOCUMENTO.Split(' ');

                dr["NroAutorizacion"] = tarjetaCirculacion.ID_TARJETA_CIRCULACION;
                dr["ModalidadServicio"] = tarjetaCirculacion.NOMBRE_MODALIDAD_SERVICIO;
                dr["Placa"] = tarjetaCirculacion.PLACA;
                dr["NroMotor"] = tarjetaCirculacion.SERIE_MOTOR;
                dr["Asiento"] = tarjetaCirculacion.NUMERO_ASIENTOS;
                dr["TarjetaPropiedad"] = tarjetaCirculacion.NRO_TARJETA;
                dr["Color"] = "";
                dr["FechaInscripcion"] = arrFechaImpresion[0];
                dr["FechaEmision"] = arrFechaImpresion[0];
                dr["FechaVencimiento"] = arrFechaVencimiento[0];
                dr["AnioFabricacion"] = tarjetaCirculacion.ANIO_FABRICACION;
                dr["Marca"] = tarjetaCirculacion.MARCA;
                dr["Modelo"] = tarjetaCirculacion.MODELO;//"MODELO";
                dr["NumeroSerie"] = tarjetaCirculacion.SERIE_MOTOR;
                dr["Clase"] = tarjetaCirculacion.CLASE;// "AIII";
                dr["CantidadPasajero"] = tarjetaCirculacion.CAPACIDAD_PASAJERO;// "56";
              

                //GENERAR QR
                var webClient = new WebClient();
                string texto = HttpUtility.UrlEncode(dr["NroAutorizacion"] + " [" + dr["ModalidadServicio"] + "] " + "Tarjeta Propiedad: " + dr["TarjetaPropiedad"]);
                byte[] imageBytes = webClient.DownloadData("https://api.qrserver.com/v1/create-qr-code/?data=" + texto + "&amp;size=220x220&amp;margin=0");
                dr["QRRegistro"] = imageBytes;

                dtVehiculoDatos.Rows.Add(dr);
                datosConstancia.Tables["DtVehiculoDatos"].Merge(dtVehiculoDatos);
                // DATOS DEL PROPIETARIO
                var dtPropietarioVehiculo = new dtsConstanciaTaxiTuc.DtPropietarioVehiculoDataTable();
                var drow = dtPropietarioVehiculo.NewRow();
                drow["Propietario"] = tarjetaCirculacion.NOMBRE_PROPIETARIO; //GTU.Propietario;
                drow["Direccion"] = tarjetaCirculacion.DIRECCION; //GTU.Direccion;
                drow["Telefono"] = tarjetaCirculacion.TELEFONO;//GTU.Telefono;
                drow["NroDocumento"] = tarjetaCirculacion.NRO_DOCUMENTO; //GTU.NumeroDocumento;
                drow["TpoDocumento"] = tarjetaCirculacion.TPODOCUMENTO;
                dtPropietarioVehiculo.Rows.Add(drow);
                datosConstancia.Tables["DtPropietarioVehiculo"].Merge(dtPropietarioVehiculo);

                //DATOS EMPRESA
                var dtEmpresaVehiculo = new dtsConstanciaTaxiTuc.DtEmpresaVehiculoDataTable();
                var drowEmpresa = dtEmpresaVehiculo.NewRow();
                drowEmpresa["RazonSocial"] = tarjetaCirculacion.NOMBRE_PROPIETARIO;
                drowEmpresa["Ruc"] = tarjetaCirculacion.NRO_DOCUMENTO;
                drowEmpresa["Ruta"] = "301";
                drowEmpresa["DistritoOrigen"] = "";
                drowEmpresa["DistritoDestino"] = "";
                drowEmpresa["ColorFondo"] = "";
                drowEmpresa["ColorFranja"] = "";
                drowEmpresa["ResolucionAutorizacion"] = "";
                dtEmpresaVehiculo.Rows.Add(drowEmpresa);
                datosConstancia.Tables["DtEmpresaVehiculo"].Merge(dtEmpresaVehiculo);

                ReportDocument rd = new ReportDocument();
                if (tipoModalidad != 13)
                {
                    rd = new Impresion_taxi_tuc();
                }
                else
                {
                    rd = new Impresion_TPublico_tuc();
                }

                rd.SetDataSource(datosConstancia);
                resultado = "TARJETA ÚNICA CIRCULACIÓN "+ tarjetaCirculacion.ID_TARJETA_CIRCULACION + ".pdf"; //+ tarjetaCirculacion.ID_TARJETA_CIRCULACION + ".pdf";//  G00467782.pdf";
                System.IO.File.Delete(rutaArchivo + resultado);
                rd.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, rutaArchivo + resultado);
                rd.Close();
                return "1" + "|" + resultado;
            }
            catch (Exception ex)
            {
                resultado += "0" + "|" + ex.Message;
            }
            return resultado;
        }

        //public ReporteResolucionVM ReporteResolucion(int IDDOC_PADRE)
        //{

        //}
        public string ReporteResolucion(int IDDOC_PADRE, string rutaArchivo)
        {
            var tarjetaCirculacion = reporteDAL.ReporteResolucion(IDDOC_PADRE);

            var resultado = "";
            try
            {
                DtsResolucion datosResolucion = new DtsResolucion();
                var dtResolucionDatos = new DtsResolucion.Resolucion_ObtencionDataTable();
                var dr = dtResolucionDatos.NewRow();

                dr["ID_EXPEDIENTE_PADRE"] = tarjetaCirculacion.ID_EXPEDIENTE_PADRE;
                dr["ID_EXPEDIENTE_HIJO"] = tarjetaCirculacion.ID_EXPEDIENTE_HIJO;
                dr["FECHA_REGISTRO"] = tarjetaCirculacion.FECHA_REG.ValorFechaCorta();
                dr["PROPIETARIO"] = tarjetaCirculacion.PROPIETARIO;
                dr["NUMERO_DOC_SOLICITANTE"] = tarjetaCirculacion.NUMERO_RECURRENTE;
                dr["PLACA"] = tarjetaCirculacion.PLACA;
                dr["ANIO"] = DateTime.Now.Year;
                dr["NUMERO_RESOLUCION"] = tarjetaCirculacion.NUMERO_RESOLUCION;

                dtResolucionDatos.Rows.Add(dr);
                //}

                datosResolucion.Tables["Resolucion_Obtencion"].Merge(dtResolucionDatos);
                ReportDocument rd = new ReportDocument();

                rd = new rptResObtencion();

                rd.SetDataSource(datosResolucion);

                resultado = "RESOLUCIÓN "+ tarjetaCirculacion.ID_EXPEDIENTE_PADRE + "_" + DateTime.Now.Year +".pdf";
                System.IO.File.Delete(rutaArchivo + resultado);
                rd.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, rutaArchivo + resultado);
                rd.Close();

                return "1|" + resultado;
            }
            catch (Exception ex)
            {
                resultado += "0|" + ex.Message;
            }

            return resultado;
        }


        public string genera_pdf_PADRON(int EXPEDIENTE, string rutaArchivo, int tipoModalidad, string Empresa, string nombre_modalidad, string fechaRegistro, string id_expediente)
        {
            var resultado = "";
            try
            {
                dtsConstanciasV2 datosConstancia = new dtsConstanciasV2();
                
                var dtConstancia = new dtsConstanciasV2.CONSTANCIA_PADRONDataTable();
                var dtConductores = new dtsConstanciasV2.CONDUCTORESDataTable();
        


                var dr = dtConstancia.NewRow();

               
                //dr["EMPRESA"] = "CORPORACION METROMOBIL SOCIEDAD ANONIMA-CORPORACION METROMOBIL S.A.";
                //dr["MODALIDAD_SERVICIO"] = "TRANSPORTE REGULAR";
                //dr["FECHA_REGISTRO"] = "04/03/2020";
                //dr["NUMERO_EXPEDIENTE"] = "03474-2020";

                dr["EMPRESA"] = Empresa;
                dr["MODALIDAD_SERVICIO"] = nombre_modalidad;
                dr["FECHA_REGISTRO"] = fechaRegistro;
                dr["NUMERO_EXPEDIENTE"] = id_expediente;


                var datosCredencial = reporteDAL.getDatosConductores(Convert.ToInt32(id_expediente));

              



                foreach (var datos2 in datosCredencial)
                {

                    var dr2 = dtConductores.NewRow();

                    dr2["TIPO_DOCUMENTO"] = datos2.TIPO_DOC_OPERADOR;
                    dr2["NUMERO_DOCUMENTO"] = datos2.NUMERO_DOCUMENTO;
                    dr2["NOMBRES_CONDUCTOR"] = datos2.NOMBRE_OPERADOR;
                    dr2["TIPO_OPERADOR"] = datos2.TIPO_OPERADOR;
                    dtConductores.Rows.Add(dr2);
                  
                }


                dtConstancia.Rows.Add(dr);
                
                datosConstancia.Tables["CONSTANCIA_PADRON"].Merge(dtConstancia);
                datosConstancia.Tables["CONDUCTORES"].Merge(dtConductores);


                ReportDocument rd = new ReportDocument();
                rd = new Reportes.Resoluciones.Credenciales.Imp_Constancia_Padron();

                rd.SetDataSource(datosConstancia);
                resultado = "CONSTANCIA REGISTRO PADRON - "+ id_expediente + ".pdf";
                System.IO.File.Delete(rutaArchivo + resultado);
                rd.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, rutaArchivo + resultado);
                rd.Close();
                return "1" + "|" + resultado;
                //}
                //else
                //{
                //    tipo = 0;
                //    mensaje = "Vehiculo no se encutra registrado en la base de datos de GTU";
                //}
            }
            catch (Exception ex)
            {
                resultado += "0" + "|" + ex.Message;
            }
            return resultado;
        }


        //Nuevo Padron
        public string genera_PADRON(int EXPEDIENTE, string rutaArchivo, int tipoModalidad)
        {
            var resultado = "";
            try
            {
                dtsConstanciasV2 datosConstancia = new dtsConstanciasV2();

                var dtConstancia = new dtsConstanciasV2.CONSTANCIA_PADRONDataTable();
                var dtConductores = new dtsConstanciasV2.CONDUCTORESDataTable();

                var datosPadron = reporteDAL.getDatosPadron(Convert.ToInt32(EXPEDIENTE));

                var dr = dtConstancia.NewRow();


                //dr["EMPRESA"] = "CORPORACION METROMOBIL SOCIEDAD ANONIMA-CORPORACION METROMOBIL S.A.";
                //dr["MODALIDAD_SERVICIO"] = "TRANSPORTE REGULAR";
                //dr["FECHA_REGISTRO"] = "04/03/2020";
                //dr["NUMERO_EXPEDIENTE"] = "03474-2020";

                dr["EMPRESA"] = datosPadron.PERSONA;
                dr["MODALIDAD_SERVICIO"] = datosPadron.MODALIDAD_SERVICIO;
                dr["FECHA_REGISTRO"] = datosPadron.FECHAREG;
                dr["NUMERO_EXPEDIENTE"] = datosPadron.TRAMITE;


                var datosCredencial = reporteDAL.getDatosConductores(Convert.ToInt32(EXPEDIENTE));





                foreach (var datos2 in datosCredencial)
                {

                    var dr2 = dtConductores.NewRow();

                    dr2["TIPO_DOCUMENTO"] = datos2.TIPO_DOC_OPERADOR;
                    dr2["NUMERO_DOCUMENTO"] = datos2.NUMERO_DOCUMENTO;
                    dr2["NOMBRES_CONDUCTOR"] = datos2.NOMBRE_OPERADOR;
                    dr2["TIPO_OPERADOR"] = datos2.TIPO_OPERADOR;
                    dtConductores.Rows.Add(dr2);

                }


                dtConstancia.Rows.Add(dr);

                datosConstancia.Tables["CONSTANCIA_PADRON"].Merge(dtConstancia);
                datosConstancia.Tables["CONDUCTORES"].Merge(dtConductores);


                ReportDocument rd = new ReportDocument();
                rd = new Reportes.Resoluciones.Credenciales.Imp_Constancia_Padron();

                rd.SetDataSource(datosConstancia);
                resultado = "CONSTANCIA REGISTRO PADRON - " + EXPEDIENTE + ".pdf";
                System.IO.File.Delete(rutaArchivo + resultado);
                rd.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, rutaArchivo + resultado);
                rd.Close();
                return "1" + "|" + resultado;
                //}
                //else
                //{
                //    tipo = 0;
                //    mensaje = "Vehiculo no se encutra registrado en la base de datos de GTU";
                //}
            }
            catch (Exception ex)
            {
                resultado += "0" + "|" + ex.Message;
            }
            return resultado;
        }



        public string genera_pdf_Credencial(int EXPEDIENTE, string rutaArchivo, int tipoModalidad, string Empresa, string nombre_modalidad, string fechaRegistro)
        {
            var resultado = "";
 
        
            try
            {
                dtsConstanciasV2 datosConstancia = new dtsConstanciasV2();

                var dtConstancia = new dtsConstanciasV2.CONSTANCIA_TRANS_REGULARDataTable();
                
                var dr = dtConstancia.NewRow();
                var datosCredencial = reporteDAL.getDatosConstanciaOpe(EXPEDIENTE);
                //dr["EMPRESA"] = "CORPORACION METROMOBIL SOCIEDAD ANONIMA-CORPORACION METROMOBIL S.A.";
                //dr["MODALIDAD_SERVICIO"] = "TRANSPORTE REGULAR";
                //dr["FECHA_REGISTRO"] = "04/03/2020";
                //dr["NUMERO_EXPEDIENTE"] = "03474-2020";

                dr["CODIGO"] = datosCredencial.CODIGO;
                dr["MODALIDAD_SERVICIO"] = datosCredencial.MODALIDAD_SERVICIO;
                dr["NUMERO_DOCUMENTO"] = datosCredencial.NUMERO_DOCUMENTO;
                dr["NOMBRE_OPERADOR"] = datosCredencial.NOMBRE_OPERADOR;
                dr["FECHA_EMISION"] = datosCredencial.FECHA_EMISION.ValorFechaCorta();
                dr["FECHA_VENCIMIENTO"] = datosCredencial.FECHA_VENCIMIENTO.ValorFechaCorta();
                dr["TIPO_OPERADOR"] = datosCredencial.TIPO_OPERADOR;
                dr["EMPRESA"] = datosCredencial.EMPRESA;
                dr["NUMERO_CREDENCIAL"] = datosCredencial.NUMERO_CREDENCIAL;
                dr["ID_EXPEDIENTE"] = datosCredencial.ID_EXPEDIENTE;
                dr["FEC_REGISTRO_EXPEDIENTE"] = datosCredencial.FEC_REGISTRO_EXPEDIENTE;
                dr["TIPO_DOCUMENTO"] = datosCredencial.TIPO_DOC_OPERADOR;
                //dr["MODALIDAD_SERVICIO"] = nombre_modalidad;
                //dr["FECHA_REGISTRO"] = fechaRegistro;
                //dr["NUMERO_EXPEDIENTE"] = id_expediente;

                dtConstancia.Rows.Add(dr);
                datosConstancia.Tables["CONSTANCIA_TRANS_REGULAR"].Merge(dtConstancia);
                ReportDocument rd = new ReportDocument();
                rd = new Imp_Constancia_SSTR();

                rd.SetDataSource(datosConstancia);
                resultado = "CONSTANCIA CREDENCIAL - "+ datosCredencial.NUMERO_CREDENCIAL + ".pdf";
                System.IO.File.Delete(rutaArchivo + resultado);
                rd.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, rutaArchivo + resultado);
                rd.Close();
                return "1" + "|" + resultado;
                //}
                //else
                //{
                //    tipo = 0;
                //    mensaje = "Vehiculo no se encutra registrado en la base de datos de GTU";
                //}
            }
            catch (Exception ex)
            {
                resultado += "0" + "|" + ex.Message;
            }
            return resultado;
        }


        public string genera_pdf_Credencial_taxi(int EXPEDIENTE, string rutaArchivo, int tipoModalidad, string Empresa, string nombre_modalidad, string fechaRegistro)
        {
            var resultado = "";
        
            try
            {
                dtsConstanciasV2 datosConstancia = new dtsConstanciasV2();

                var dtConstancia = new dtsConstanciasV2.CONSTANCIA_TRANS_ESPECIALDataTable();

                var dr = dtConstancia.NewRow();
                var datosCredencial = reporteDAL.getDatosConstanciaOpe_taxi(EXPEDIENTE);
               
                dr["CODIGO"] = datosCredencial.CODIGO;
                dr["NOMBRE_OPERADOR"] = datosCredencial.NOMBRE_OPERADOR;
                dr["NRO_DOCUMENTO_OPERADOR"] = datosCredencial.NUMERO_DOCUMENTO;
                dr["NUMERO_CREDENCIAL"] = datosCredencial.NUMERO_CREDENCIAL;
                dr["FECHA_VENCIMIENTO"] = datosCredencial.FECHA_VENCIMIENTO.ValorFechaCorta();
                dr["FECHA_EMISION"] = datosCredencial.FECHA_EMISION.ValorFechaCorta();
                dr["TIPO_OPERADOR"] = datosCredencial.TIPO_OPERADOR;
                dr["TIPO_MODALIDAD"] = datosCredencial.MODALIDAD_SERVICIO;
                dr["ID_EXPEDIENTE"] = datosCredencial.ID_EXPEDIENTE;
                dr["FEC_REGISTRO_EXPEDIENTE"] = datosCredencial.FEC_REGISTRO_EXPEDIENTE;
                dr["TIPO_DOCUMENTO"] = datosCredencial.TIPO_DOC_OPERADOR;
                //dr["MODALIDAD_SERVICIO"] = nombre_modalidad;
                //dr["FECHA_REGISTRO"] = fechaRegistro;
                //dr["NUMERO_EXPEDIENTE"] = id_expediente;

                dtConstancia.Rows.Add(dr);
                datosConstancia.Tables["CONSTANCIA_TRANS_ESPECIAL"].Merge(dtConstancia);
                ReportDocument rd = new ReportDocument();
                rd = new Imp_Constancia_SSTE();

                rd.SetDataSource(datosConstancia);
                resultado = "CONSTANCIA CREDENCIAL-"+ datosCredencial.NUMERO_CREDENCIAL + ".pdf";
                System.IO.File.Delete(rutaArchivo + resultado);
                rd.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, rutaArchivo + resultado);
                rd.Close();
                return "1" + "|" + resultado;
                //}
                //else
                //{
                //    tipo = 0;
                //    mensaje = "Vehiculo no se encutra registrado en la base de datos de GTU";
                //}
            }
            catch (Exception ex)
            {
                resultado += "0" + "|" + ex.Message;
            }
            return resultado;
        }



        public string genera_pdf_Tarje_Crendencial(int EXPEDIENTE, string rutaArchivo, string rutaFoto,int tipoModalidad, string Empresa, string nombre_modalidad, string fechaRegistro)
        {
            var resultado = "";
    
            try
            {
                //dtsConstanciasV2 datosConstancia = new dtsConstanciasV2();
                dtsCredencial datosConstancia = new dtsCredencial();

                //var dtConstancia = new dtsConstanciasV2.TARJETA_CREDENCIALDataTable();
                var dtConstancia = new dtsCredencial.TARJETA_CREDENCIALDataTable();
                var dr = dtConstancia.NewRow();
                var datosCredencial = reporteDAL.getDatosTarje_CredencialOpe(EXPEDIENTE);

                var foto = File.ReadAllBytes(rutaFoto + datosCredencial.FOTO_OPERADOR);
                dr["NOMBRE_OPERADOR"] = datosCredencial.NOMBRE_OPERADOR;
                dr["NUMERO_LICENCIA"] = datosCredencial.NUMERO_LICENCIA;
                dr["NUMERO_DOCUMENTO"] = datosCredencial.NUMERO_DOCUMENTO;
                dr["NUMERO_CREDENCIAL"] = datosCredencial.NUMERO_CREDENCIAL;
                dr["FECHA_EMISION"] = datosCredencial.FECHA_EMISION.ValorFechaCorta();
                dr["FECHA_VENCIMIENTO"] = datosCredencial.FECHA_VENCIMIENTO.ValorFechaCorta();
                dr["TIPO_OPERADOR"] = datosCredencial.TIPO_OPERADOR;
                dr["MODALIDAD_SERVICIO"] = datosCredencial.MODALIDAD_SERVICIO;
                dr["EMPRESA"] = datosCredencial.EMPRESA;
                dr["ID_EXPEDIENTE"] = datosCredencial.ID_EXPEDIENTE;

                dr["FOTO_OPERADOR"] = foto;
                dr["TIPO_DOCUMENTO"] = datosCredencial.TIPO_DOCUMENTO;
                if (tipoModalidad != 4)
                {
                    dr["DATO"] = "Empresa";
                    dr["EMPRESA"] = datosCredencial.EMPRESA;
                }
                else
                {
                    dr["DATO"] = "";
                    dr["EMPRESA"] = "";
                }

                var webClient = new WebClient();
                string texto = HttpUtility.UrlEncode("NroCredencial :" + dr["NUMERO_CREDENCIAL"] + " Vigencia  : " + dr["FECHA_VENCIMIENTO"] + " Nro Documento   : " + dr["NUMERO_DOCUMENTO"] + " Nombres y Apellidos : " + dr["NOMBRE_OPERADOR"]);
                byte[] imageBytes = webClient.DownloadData("https://api.qrserver.com/v1/create-qr-code/?data=" + texto + "&amp;size=220x220&amp;margin=0");
                dr["QR"] = imageBytes;
                dtConstancia.Rows.Add(dr);
                datosConstancia.Tables["TARJETA_CREDENCIAL"].Merge(dtConstancia);
                ReportDocument rd = new ReportDocument();
                //rd = new Imp_Credencial();
                rd = new Formato_Credencial_ATU();
                rd.SetDataSource(datosConstancia);
                resultado = "CREDENCIAL - "+ datosCredencial.NUMERO_CREDENCIAL + ".pdf";
                System.IO.File.Delete(rutaArchivo + resultado);
                rd.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, rutaArchivo + resultado);
                rd.Close();
                return "1" + "|" + resultado;
                //}
                //else
                //{
                //    tipo = 0;
                //    mensaje = "Vehiculo no se encutra registrado en la base de datos de GTU";
                //}
            }
            catch (Exception ex)
            {
                resultado += "0" + "|" + ex.Message;
            }
            return resultado;
        }


        public string genera_pdf_Tarje_Crendencial_taxi(int EXPEDIENTE, string rutaArchivo, string rutafoto,int tipoModalidad, string Empresa, string nombre_modalidad, string fechaRegistro)
        {
            var resultado = "";
            //System.IO.File.Delete(rutaArchivo + "*.pdf");
            try
            {
                //dtsConstanciasV2 datosConstancia = new dtsConstanciasV2();

                //var dtConstancia = new dtsConstanciasV2.TARJETA_CREDENCIALDataTable();
                dtsCredencial datosConstancia = new dtsCredencial();
                var dtConstancia = new dtsCredencial.TARJETA_CREDENCIALDataTable();


                var dr = dtConstancia.NewRow();
                var datosCredencial = reporteDAL.getDatosTarje_CredencialOpe_taxi(EXPEDIENTE);
                var foto = File.ReadAllBytes(rutafoto + datosCredencial.FOTO_OPERADOR);

                dr["NOMBRE_OPERADOR"] = datosCredencial.NOMBRE_OPERADOR;
                dr["NUMERO_LICENCIA"] = datosCredencial.NUMERO_LICENCIA;
                dr["NUMERO_DOCUMENTO"] = datosCredencial.NUMERO_DOCUMENTO;
                dr["NUMERO_CREDENCIAL"] = datosCredencial.NUMERO_CREDENCIAL;
                dr["FECHA_EMISION"] = datosCredencial.FECHA_EMISION.ValorFechaCorta();
                dr["FECHA_VENCIMIENTO"] = datosCredencial.FECHA_VENCIMIENTO.ValorFechaCorta();
                dr["TIPO_OPERADOR"] = datosCredencial.TIPO_OPERADOR;
                dr["MODALIDAD_SERVICIO"] = datosCredencial.MODALIDAD_SERVICIO; /*datosCredencial.MODALIDAD_SERVICIO*/;
                dr["EMPRESA"] = datosCredencial.EMPRESA;
                dr["ID_EXPEDIENTE"] = datosCredencial.ID_EXPEDIENTE;

                dr["FOTO_OPERADOR"] = foto;
                dr["TIPO_DOCUMENTO"] = datosCredencial.TIPO_DOCUMENTO;
                if (tipoModalidad != 4)
                {
                    dr["DATO"] = "Empresa";
                    dr["EMPRESA"] = datosCredencial.EMPRESA;
                }
                else
                {
                    dr["DATO"] = "";
                    dr["EMPRESA"] = "";
                }
                var webClient = new WebClient();
                string texto = HttpUtility.UrlEncode("NroCredencial :" + dr["NUMERO_CREDENCIAL"] + " Vigencia  : " + dr["FECHA_VENCIMIENTO"] + " Nro Documento   : " + dr["NUMERO_DOCUMENTO"] + " Nombres y Apellidos : " + dr["NOMBRE_OPERADOR"]);
                byte[] imageBytes = webClient.DownloadData("https://api.qrserver.com/v1/create-qr-code/?data=" + texto + "&amp;size=220x220&amp;margin=0");
                dr["QR"] = imageBytes;
                dtConstancia.Rows.Add(dr);
                datosConstancia.Tables["TARJETA_CREDENCIAL"].Merge(dtConstancia);
                ReportDocument rd = new ReportDocument();
                //rd = new Imp_Credencial();
                rd = new Formato_Credencial_ATU();
                rd.SetDataSource(datosConstancia);
                resultado = "CREDENCIAL - "+ datosCredencial.NUMERO_CREDENCIAL + ".pdf";
                System.IO.File.Delete(rutaArchivo + resultado);
                rd.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, rutaArchivo + resultado);
                rd.Close();
                return "1" + "|" + resultado;
                //}
                //else
                //{
                //    tipo = 0;
                //    mensaje = "Vehiculo no se encutra registrado en la base de datos de GTU";
                //}
            }
            catch (Exception ex)
            {
                resultado += "0" + "|" + ex.Message;
            }
            return resultado;
        }

        public string ReporteReno_Autorizacion(int id_expediente, string rutaArchivo)
        {
            var Renovacion_autorizacion = reporteDAL.getDatosRenoAutorizacion(id_expediente);

            var resultado = "";
            try
            {
                dtsRenovacion datosResolucion = new dtsRenovacion();
               
                var dtResolucionDatos = new dtsRenovacion.Renovacion_AutorizacionDataTable();
                var dr = dtResolucionDatos.NewRow();

                dr["NUM_RESOLUCION"] = Renovacion_autorizacion.CODIGO;
                dr["FECHA"] = Renovacion_autorizacion.FEC_REGISTRO_EXPEDIENTE.ValorFechaCorta();
                dr["NUM_EXPEDIENTE"] = Renovacion_autorizacion.ID_EXPEDIENTE;
                dr["EMPRESA"] = Renovacion_autorizacion.EMPRESA;
                dr["MODALIDAD_SERVICIO"] = Renovacion_autorizacion.MODALIDAD_SERVICIO;
                dr["CANT_ANIOS"] = Renovacion_autorizacion.CANT_ANIOS;
                dr["PERIODO_NOMBRE"] = Renovacion_autorizacion.PERIODO_NOMBRE;

                dtResolucionDatos.Rows.Add(dr);
             

                datosResolucion.Tables["Renovacion_Autorizacion"].Merge(dtResolucionDatos);
                ReportDocument rd = new ReportDocument();

                rd = new rptResolucion_RenovacionSTR();

                rd.SetDataSource(datosResolucion);

                resultado = "RENOVACION AUTORIZACION-" + Renovacion_autorizacion.ID_EXPEDIENTE + "_" + DateTime.Now.Year + ".pdf";
                System.IO.File.Delete(rutaArchivo + resultado);
                rd.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, rutaArchivo + resultado);
                rd.Close();

                return "1|" + resultado;
            }
            catch (Exception ex)
            {
                resultado += "0|" + ex.Message;
            }

            return resultado;
        }

        public string ReporteReno_Autorizacion_sste(int id_expediente, string rutaArchivo)
        {
            var Renovacion_autorizacion = reporteDAL.getDatosRenoAutorizacion(id_expediente);

            var resultado = "";
            try
            {
                dtsRenovacion datosResolucion = new dtsRenovacion();

                var dtResolucionDatos = new dtsRenovacion.Renovacion_AutorizacionDataTable();
                var dr = dtResolucionDatos.NewRow();

                dr["NUM_RESOLUCION"] = Renovacion_autorizacion.CODIGO;
                dr["FECHA"] = Renovacion_autorizacion.FEC_REGISTRO_EXPEDIENTE.ValorFechaCorta();
                dr["NUM_EXPEDIENTE"] = Renovacion_autorizacion.ID_EXPEDIENTE;
                dr["EMPRESA"] = Renovacion_autorizacion.EMPRESA;
                dr["MODALIDAD_SERVICIO"] = Renovacion_autorizacion.MODALIDAD_SERVICIO;
                dr["CANT_ANIOS"] = Renovacion_autorizacion.CANT_ANIOS;
                dr["PERIODO_NOMBRE"] = Renovacion_autorizacion.PERIODO_NOMBRE;

                dtResolucionDatos.Rows.Add(dr);


                datosResolucion.Tables["Renovacion_Autorizacion"].Merge(dtResolucionDatos);
                ReportDocument rd = new ReportDocument();

                rd = new rptResolucion_Renovacion();

                rd.SetDataSource(datosResolucion);

                resultado = "RENOVACION AUTORIZACION" + Renovacion_autorizacion.ID_EXPEDIENTE + "_" + DateTime.Now.Year + ".pdf";
                System.IO.File.Delete(rutaArchivo + resultado);
                rd.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, rutaArchivo + resultado);
                rd.Close();

                return "1|" + resultado;
            }
            catch (Exception ex)
            {
                resultado += "0|" + ex.Message;
            }

            return resultado;
        }
        //public string genera_pdf_CONST(int EXPEDIENTE, string rutaArchivo, int tipoModalidad)
        //{
        //    var resultado = "";
        //    try
        //    {
        //        dtsConstancias datosConstancia = new dtsConstancias();
        //        var dtConstancia = new dtsConstancias.CONSTANCIA_TRANS_REGULARDataTable();
        //        var dr = dtConstancia.NewRow();
        //        dr["CODIGO"] = "03474-2020";
        //        dr["MODALIDAD_SERVICIO"] = "TRANSPORTE REGULAR";
        //        dr["NUMERO_DOCUMENTO"] = "747875487";
        //        dr["NOMBRE_OPERADOR"] = "SECMAR KEVIN REYES REYES";
        //        dr["FECHA_EMISION"] = "26/02/2020";
        //        dr["FECHA_VENCIMIENTO"] = "26/02/2025";
        //        dr["TIPO_OPERADOR"] = "CONDUCTOR";
        //        dr["EMPRESA"] = "CORPORACION METROMOBIL SOCIEDAD ANONIMA-CORPORACION METROMOBIL S.A.";
        //        dr["NUMERO_CREDENCIAL"] = "COD0013230742947708";
        //        dr["ID_EXPEDIENTE"] = "03474-2020";
        //        dr["FEC_REGISTRO_EXPEDIENTE"] = "26/02/2020";

        //        dtConstancia.Rows.Add(dr);
        //        datosConstancia.Tables["CONSTANCIA_TRANS_REGULAR"].Merge(dtConstancia);


        //        ReportDocument rd = new ReportDocument();

        //        rd = new Reportes.Resoluciones.Credenciales.Imp_Constancia_SSTR();

        //        rd.SetDataSource(datosConstancia);
        //        resultado = "CONSTANCIA DE CREDENCIAL Nro COD0013230742947708.pdf";
        //        rd.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, rutaArchivo + resultado);
        //        rd.Close();
        //        return "1" + "|" + resultado;
        //        //}
        //        //else
        //        //{
        //        //    tipo = 0;
        //        //    mensaje = "Vehiculo no se encutra registrado en la base de datos de GTU";
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        resultado += "0" + "|" + ex.Message;
        //    }
        //    return resultado;
        //}

    }
}
