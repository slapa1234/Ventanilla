using SisATU.Base.Enumeradores;
using SisATU.Base.ViewModel;
using SisATU.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SisATU.Negocio.Reportes;
using SisATU.Negocio.Reportes.Resoluciones.resolucion_taxi_independiente;
using System.Net;
using System.Web;
using CrystalDecisions.CrystalReports.Engine;


namespace SisATU.Negocio
{
    public class ProcedimientoBLL
    {
        ProcedimientoDAL obj = new ProcedimientoDAL();
        public List<ComboProcedimientoVM> ComboProcedimientoXModalidad(int ID_MODALIDAD_SERVICIO)
        {
            var resultado = obj.ComboProcedimientoXModalidad(ID_MODALIDAD_SERVICIO);
            if (ID_MODALIDAD_SERVICIO == EnumModalidadServicio.ServicioTaxiRemisse.ValorEntero() || ID_MODALIDAD_SERVICIO == EnumModalidadServicio.ServicioTaxiEstacion.ValorEntero())
            {
                resultado.RemoveAll(modelo => modelo.ID_PROCEDIMIENTO == 51);
            }
            else if (ID_MODALIDAD_SERVICIO == EnumModalidadServicio.ServicioTaxiIndependiente.ValorEntero())
            {
                resultado.RemoveAll(modelo => modelo.ID_PROCEDIMIENTO == 52);
            }
            return resultado;
        }

        public ProcedimientoVM ConsultarDatosProcedimiento(int ID_PROCEDIMIENTO, int ID_TIPO_PERSONA)
        {
            ProcedimientoDAL obj = new ProcedimientoDAL();
            return obj.ConsultarDatosProcedimiento(ID_PROCEDIMIENTO, ID_TIPO_PERSONA);
        }

        //public List<ProcedimientoVM> ConsultarDatosProcedimientoPadre(int ID_PROCEDIMIENTO)
        //{
        //    ProcedimientoDAL obj = new ProcedimientoDAL();
        //    return obj.ConsultarDatosProcedimientoPadre(ID_PROCEDIMIENTO);
        //}

        public string genera_pdf_res_taxi_independ(string NroPlaca, string rutaArchivo, int tipoModalidad, string Persona, string DNI)
        {
            var resultado = "";

            try
            {
                DtsResolucion datosResolucion = new DtsResolucion();
                var dtResolucionDatos = new DtsResolucion.Resolucion_ObtencionDataTable();
                var dr = dtResolucionDatos.NewRow();

                dr["ID_EXPEDIENTE_PADRE"] = "154";
                dr["ID_EXPEDIENTE_HIJO"] = "155";
                dr["FECHA_REGISTRO"] = "20/02/2020";
                dr["PROPIETARIO"] = Persona;
                dr["NUMERO_DOC_SOLICITANTE"] = DNI;
                dr["PLACA"] = "ACR-789";
                dr["ANIO"] = "2020";
                dr["NUMERO_RESOLUCION"] = "154";



                dtResolucionDatos.Rows.Add(dr);
                //}

                datosResolucion.Tables["Resolucion_Obtencion"].Merge(dtResolucionDatos);
                ReportDocument rd = new ReportDocument();

                rd = new rptResObtencion();

                rd.SetDataSource(datosResolucion);
                resultado = "Resolucion N° 154.pdf";

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

        public List<ProcedimientoVM> ConsultarDatosProcedimientoHijo(int ID_PROCEDIMIENTO, int ID_TIPO_PERSONA)
        {
            ProcedimientoDAL obj = new ProcedimientoDAL();
            return obj.ConsultarDatosProcedimientoHijo(ID_PROCEDIMIENTO, ID_TIPO_PERSONA);
        }


        //public string genera_pdf_TUC(string NroPlaca, string rutaArchivo, int tipoModalidad)
        //{
        //    var resultado = "";
        //    try
        //    {
        //        dtsConstanciaTaxiTuc datosConstancia = new dtsConstanciaTaxiTuc();

        //        //var GTU = new VehiculoBLL().Datos_Vehiculo(NroPlaca, ref mensaje, ref tipo);
        //        //if (GTU != null)
        //        //{
        //        //DATOS DEL VEHICULO
        //        var dtVehiculoDatos = new dtsConstanciaTaxiTuc.DtVehiculoDatosDataTable();
        //        var dr = dtVehiculoDatos.NewRow();
        //        dr["NroAutorizacion"] = "7897897";
        //        dr["ModalidadServicio"] = "MODALIDAD";
        //        dr["Placa"] = "ACM-145";
        //        dr["NroMotor"] = "45648486S54DEE";
        //        dr["Asiento"] = "26";
        //        dr["TarjetaPropiedad"] = "54564564";
        //        dr["Color"] = "COLOR";
        //        dr["FechaInscripcion"] = "20/02/2020";
        //        dr["FechaEmision"] = "20/02/2020";
        //        dr["FechaVencimiento"] = "20/02/2020";
        //        dr["AnioFabricacion"] = "2000";
        //        dr["Marca"] = "GTMT";
        //        dr["Modelo"] = "MODELO";
        //        dr["NumeroSerie"] = "S1545457";
        //        dr["Clase"] = "AIII";
        //        dr["CantidadPasajero"] = "56";
        //        //dr["NroAutorizacion"] = GTU.NUMERO_AUTORIZACION;
        //        //dr["ModalidadServicio"] = GTU.NOMBRE_MODALIDAD_SERVICIO;
        //        //dr["Placa"] = GTU.PLACA;
        //        //dr["NroMotor"] = GTU.NUMERO_SERIE_MOTOR;
        //        //dr["Asiento"] = GTU.NUMERO_ASIENTO;
        //        //dr["TarjetaPropiedad"] = GTU.NRO_TARJETA_PROPIEDAD;
        //        //dr["Color"] = GTU.NOMBRE_COLOR;
        //        //dr["FechaInscripcion"] = GTU.FECHA_INSCRIPCION_VEHICULO;
        //        //dr["FechaEmision"] = GTU.FECHA_EMISION;
        //        //dr["FechaVencimiento"] = GTU.FECHA_VENCIMIENTO_DOCUMENTO;
        //        //dr["AnioFabricacion"] = GTU.ANIO_FABRICACION;
        //        //dr["Marca"] = GTU.NOMBRE_MARCA;
        //        //dr["Modelo"] = GTU.NOMBRE_MODELO;
        //        //dr["NumeroSerie"] = GTU.NUMERO_SERIE;
        //        //dr["Clase"] = GTU.NOMBRE_CLASE_VEHICULO;
        //        //dr["CantidadPasajero"] = GTU.CAPACIDAD_PASAJERO;
        //        //GENERAR QR
        //        var webClient = new WebClient();
        //        string texto = HttpUtility.UrlEncode(dr["NroAutorizacion"] + " [" + dr["ModalidadServicio"] + "] " + "Tarjeta Propiedad: " + dr["TarjetaPropiedad"]);
        //        byte[] imageBytes = webClient.DownloadData("https://api.qrserver.com/v1/create-qr-code/?data=" + texto + "&amp;size=220x220&amp;margin=0");
        //        dr["QRRegistro"] = imageBytes;

        //        dtVehiculoDatos.Rows.Add(dr);
        //        datosConstancia.Tables["DtVehiculoDatos"].Merge(dtVehiculoDatos);
        //        // DATOS DEL PROPIETARIO
        //        var dtPropietarioVehiculo = new dtsConstanciaTaxiTuc.DtPropietarioVehiculoDataTable();
        //        var drow = dtPropietarioVehiculo.NewRow();
        //        drow["Propietario"] = "PROPIETARIO"; //GTU.Propietario;
        //        drow["Direccion"] = "DIRECCION"; //GTU.Direccion;
        //        drow["Telefono"] = "987654321";//GTU.Telefono;
        //        drow["NroDocumento"] = "46393958"; //GTU.NumeroDocumento;
        //        dtPropietarioVehiculo.Rows.Add(drow);
        //        datosConstancia.Tables["DtPropietarioVehiculo"].Merge(dtPropietarioVehiculo);

        //        //DATOS EMPRESA
        //        var dtEmpresaVehiculo = new dtsConstanciaTaxiTuc.DtEmpresaVehiculoDataTable();
        //        var drowEmpresa = dtEmpresaVehiculo.NewRow();
        //        drowEmpresa["RazonSocial"] = "EMPRESA S.A.C";
        //        drowEmpresa["Ruc"] = "10463939581";
        //        drowEmpresa["Ruta"] = "301";
        //        drowEmpresa["DistritoOrigen"] = "ORIGEN";
        //        drowEmpresa["DistritoDestino"] = "DESTINO";
        //        drowEmpresa["ColorFondo"] = "C.FONDO";
        //        drowEmpresa["ColorFranja"] = "C.FRANJA";
        //        drowEmpresa["ResolucionAutorizacion"] = "SD-2454674";
        //        dtEmpresaVehiculo.Rows.Add(drowEmpresa);
        //        datosConstancia.Tables["DtEmpresaVehiculo"].Merge(dtEmpresaVehiculo);

        //        ReportDocument rd = new ReportDocument();
        //        if (tipoModalidad != 13)
        //        {
        //            rd = new Impresion_taxi_tuc();
        //        }
        //        else
        //        {
        //            rd = new Impresion_TPublico_tuc();
        //        }

        //        rd.SetDataSource(datosConstancia);
        //        resultado = "CERTIFICADO DE OPERACION Nro G00467782.pdf";
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
