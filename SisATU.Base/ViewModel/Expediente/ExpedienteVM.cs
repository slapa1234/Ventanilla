using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SisATU.Base.ViewModel
{
    public class ExpedienteVM
    {
        public ExpedienteVM()
        {
            DetalleSolicitudVM = new List<DetalleSolicitudVM>();
            ReciboVM = new List<ReciboVM>();
            OperadorVM = new List<OperadorVM>();
        }
        public int ID_EXPEDIENTE { get; set; }
        public int IDDOC { get; set; }
        public string NUMERO_SID { get; set; }
        public string NUMERO_ANIO { get; set; }
        public List<SelectListItem> SelectTipoProcedimiento { get; set; }
        public int ID_PROCEDIMIENTO { get; set; }
        public int ID_MODALIDAD_SERVICIO { get; set; }
        public int ID_MODALIDAD_SERVICIO_OPERADOR { get; set; }
        public int ID_SOLICITUD { get; set; }
        public string NUMERO_SOLICITANTE { get; set; }
        public string NUMERO_RECURRENTE { get; set; }
        public int ID_ESTADO { get; set; }
        public string USUARIO_REG { get; set; }
        public string FECHA_REG { get; set; }
        public string USUARIO_MOD { get; set; }
        public string FECHA_MOD { get; set; }
        public string USUARIO_ELIM { get; set; }
        public string FECHA_ELIM { get; set; }
        public List<DetalleSolicitudVM> DetalleSolicitudVM { get; set; }
        public int ID_TIPO_PERSONA { get; set; }
        public List<SelectListItem> SelectTipoPersona { get; set; }
        public int ID_TIPO_DOCUMENTO { get; set; }
        public List<SelectListItem> SelectTipoDocumento { get; set; }
        #region PERSONA
        public string NRO_DOCUMENTO { get; set; }
        public string NOMBRES { get; set; }
        public string APELLIDO_PATERNO { get; set; }
        public string APELLIDO_MATERNO { get; set; }
        public string RAZON_SOCIAL { get; set; }
        #endregion

        public string USUREG { get; set; }
        public DateTime FECREG { get; set; }
        public string USUMOD { get; set; }
        public DateTime FECMOD { get; set; }
        public string ESTREG { get; set; }
        public string RUC { get; set; }
        public string TELEFONO { get; set; }
        public string CORREO { get; set; }
        public List<SelectListItem> SelectDepartamento { get; set; }
        public int ID_DEPARTAMENTO { get; set; }
        public List<SelectListItem> SelectProvincia { get; set; }
        public int ID_PROVINCIA { get; set; }
        public List<SelectListItem> SelectDistrito { get; set; }
        public int ID_DISTRITO { get; set; }
        //public int TIP_SOLI { get; set; }
        public int TIP_EXPE { get; set; }
        public List<SelectListItem> SelectTipoSolicitud { get; set; }
        public string ASUNTO_NO_TUPA { get; set; }
        public bool FLG_CORREO { get; set; }
        public bool DECLARACION_JURADA { get; set; }
        //public int ID_MODA { get; set; }
        public List<SelectListItem> SelectTipoModalidad { get; set; }
        public string DIRECCION { get; set; }
        public string DIRECCION_ACTUAL { get; set; }
        public string Observacion { get; set; }

        public int ID_ENTIDAD_BANCARIA { get; set; }
        public List<SelectListItem> SelectEntidadBancaria { get; set; }
        public string NUMERO_RECIBO { get; set; }
        public string FECHA_PAGO { get; set; }
        public int ID_TIPO_SEGURO { get; set; }
        public List<SelectListItem> SelectTipoSeguro { get; set; }

        //public string MODALIDAD_SERVICIO { get; set; }
        public List<SelectListItem> SelectModelo { get; set; }
        public int ID_MODELO { get; set; }
        //public string ModeloVehiculo { get; set; }
        public string NOMBRE_CLASE_VEHICULO { get; set; }
        public List<SelectListItem> SelectClaseVehiculo { get; set; }

        public int ID_CLASE_VEHICULO { get; set; }
        public string FECHA_VENCIMIENTO_DOCUMENTO { get; set; }
        public string ANIO_FABRICACION { get; set; }
        public string NOMBRE_ASEGURADORA { get; set; }
        public string POLIZA { get; set; }
        public string SeguroFechaInicio { get; set; }
        public string SeguroFechaFin { get; set; }
        public string CERTIFICADORA_CITV { get; set; }
        public string NRO_CERTIFICADO { get; set; }
        public string FECHA_INICIO_CITV { get; set; }
        public string FECHA_FIN_CITV { get; set; }
        public int ID_AFOCAT { get; set; }
        public List<SelectListItem> SelectAfocat { get; set; }
        public int IDUNIDAD_STD { get; set; }
        public int ID_PERSONA { get; set; }
        public int CODDIST { get; set; }
        public int CODDPTO { get; set; }
        public int CODPAIS { get; set; }
        public int CODPROV { get; set; }
        public string DIRECCION_STD { get; set; }
        public int? ID_EMPRESA { get; set; }
        public string ObtencionRenovacion { get; set; }
        public int ID_SEXO { get; set; }
        #region Vehiculo
        public int ID_VEHICULO { get; set; }
        public string NroPlaca { get; set; }

        public List<SelectListItem> SelectCategoriaVehiculo { get; set; }
        public int ID_CATEGORIA_VEHICULO { get; set; }
        public string FECHA_VENC_TUC { get; set; }

        #endregion
        public List<SelectListItem> SelectMarca { get; set; }
        public int ID_MARCA { get; set; }
        //public string NOMBRE_MARCA { get; set; }

        public List<SelectListItem> SelectTipoCombustible { get; set; }
        public int ID_TIPO_COMBUSTIBLE { get; set; }
        //public string NOMBRE_TIPO_COMBUSTIBLE { get; set; }


        public string SERIE { get; set; }
        public string SERIE_MOTOR { get; set; }
        public int? PESO_SECO { get; set; }
        public int? PESO_BRUTO { get; set; }
        public int? LONGITUD { get; set; }
        public int? ALTURA { get; set; }
        public int? ANCHO { get; set; }
        public int? CARGA_UTIL { get; set; }
        public int? CAPACIDAD_PASAJERO { get; set; }
        public int? NUMERO_ASIENTOS { get; set; }
        public int? NUMERO_RUEDA { get; set; }
        public int? NUMERO_EJE { get; set; }
        public int? NUMERO_PUERTA { get; set; }
        public string CILINDRADA { get; set; }
        public List<SelectListItem> SelectTipoModalidadOperador { get; set; }
        public List<SelectListItem> SelectSexo { get; set; }
        public List<SelectListItem> SelectTipoDocumentoOperador { get; set; }
        public string NRO_DOCUMENTO_OPERADOR { get; set; }
        public string APELLIDO_PATERNO_OPERADOR { get; set; }
        public int ID_TIPO_DOCUMENTO_OPERADOR { get; set; }
        public string APELLIDO_MATERNO_OPERADOR { get; set; }
        public string NOMBRE_OPERADOR { get; set; }
        public string DIRECCION_OPERADOR { get; set; }
        public string DIRECCION_RENIEC_OPERADOR { get; set; }
        public string TELEFONO_CEL_OPERADOR { get; set; }
        public string TELEFONO_CASA_OPERADOR { get; set; }
        public string CORREO_OPERADOR { get; set; }
        public string NRO_LICENCIA_OPERADOR { get; set; }
        public string FECHA_EXPEDICION_OPERADOR { get; set; }
        public string FECHA_REVALIDACION_OPERADOR { get; set; }
        public string CATEGORIA_OPERADOR { get; set; }
        public string FECHA_NACIMIENTO_OPERADOR { get; set; }
        public int ID_OPERADOR { get; set; }
        public int ID_TIPO_OPERADOR { get; set; }
        public String FOTO_OPERADOR { get; set; }
        public int ID_TIPO_SEXO { get; set; }
        public bool TIENE_FOTO { get; set; }
        public string FOTO_BASE64 { get; set; }


        public string FECHA_NACIMIENTO { get; set; }

        public string NRO_TARJETA_PROPIETARIO { get; set; }
        public string NOMBRE_PROPIETARIO { get; set; }
        public string NUMERO_DOCUMENTO_PROPIEDAD { get; set; }
        public List<SelectListItem> SelectTipoDocumentoPropietario { get; set; }
        public int ID_TIPO_DOCUMENTO_PROPIETARIO { get; set; }
        public string FECHA_INICIO_PROPIETARIO { get; set; }
        public string FECHA_FIN_PROPIETARIO { get; set; }
        public List<SelectListItem> SelectTipoOperador { get; set; }
        public List<SelectListItem> SelectTramite { get; set; }
        public int ID_TIPO_TRAMITE { get; set; }
        public string RUC_EMPRESA_OPERADOR { get; set; }
        public string FECHA_VENCIMIENTO_EXPEDIENTE { get; set; }
        public int ANIO_RENOVACION { get; set; }
        public List<ReciboVM> ReciboVM { get; set; }
        public List<OperadorVM> OperadorVM { get; set; }

        public int ID_DEPARTAMENTO_OPERADOR { get; set; }
        public int ID_PROVINCIA_OPERADOR { get; set; }
        public int ID_DISTRITO_OPERADOR { get; set; }
        public int PUNTOS_FIRME { get; set; }
        public int MUY_GRAVE { get; set; }
        public int GRAVE { get; set; }
        public string ESTADO_LICENCIA { get; set; }
        public string FECHA_VENCIMIENTO_CREDENCIAL { get; set; }
        public int ID_TIPO_CREDENCIAL { get; set; }
        public int TieneCredencial { get; set; }
    }

}
