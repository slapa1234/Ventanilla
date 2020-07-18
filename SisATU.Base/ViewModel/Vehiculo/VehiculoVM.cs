using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Base.ViewModel
{
    public class VehiculoVM
    {
        public int ID_VEHICULO { get; set; }
        public int ID_MODALIDAD_SERVICIO { get; set; }
        public int ID_CLASE_VEHICULO { get; set; }
        public int ID_ESTADO { get; set; }
        public int ID_MODELO { get; set; }
        public int ID_TIPO_COMBUSTIBLE { get; set; }
        public int ID_CATEGORIA_VEHICULO { get; set; }
        public string ANIO_FABRICACION { get; set; }
        public string SERIE { get; set; }
        public string SERIE_MOTOR { get; set; }
        public int PESO_SECO { get; set; }
        public int PESO_BRUTO { get; set; }
        public int LONGITUD { get; set; }
        public int ALTURA { get; set; }
        public int ANCHO { get; set; }
        public int CARGA_UTIL { get; set; }
        public int CAPACIDAD_PASAJERO { get; set; }
        public int NUMERO_ASIENTOS { get; set; }
        public int NUMERO_RUEDA { get; set; }
        public int NUMERO_EJE { get; set; }
        public int NUMERO_PUERTA { get; set; }
        public string FECHA_INSCRIPCION { get; set; }
        public string CILINDRADA { get; set; }
        public string OBSERVACION { get; set; }
        public string USUARIO_REG { get; set; }
        public string FECHA_REG { get; set; }
        public string USUARIO_MOD { get; set; }
        public string FECHA_MOD { get; set; }
        public string USUARIO_ELIM { get; set; }
        public string FECHA_ELIM { get; set; }
        public string PLACA { get; set; }

        public string NOMBRE_MODALIDAD_SERVICIO { get; set; }
        public string NOMBRE_MODELO { get; set; }
        public string NOMBRE_MARCA { get; set; }
        public string NOMBRE_CLASE_VEHICULO { get; set; }
        public string FECHA_VENCIMIENTO_DOCUMENTO { get; set; }
        public string FECHA_EMISION_DOCUMENTO { get; set; }

        public string ASEGURADORA_NOMBRE { get; set; }
        public string ASEGURADORA_FEC_INI_VIGENCIA { get; set; }
        public string ASEGURADORA_FEC_FIN_VIGENCIA { get; set; }
        public string ASEGURADORA_POLIZA { get; set; }

        public string CITV_NOMBRE { get; set; }
        public string CITV_FEC_INI_VIGENCIA { get; set; }
        public string CITV_FEC_FIN_VIGENCIA { get; set; }
        public string CITV_POLIZA { get; set; }

        public int ID_TIPO_SEGURO { get; set; }
        public string NUMERO_AUTORIZACION { get; set; }
        public string RESOLUCION_AUTORIZACION { get; set; }
        public string NRO_DOCUMENTO_PROPIETARIO { get; set; }
        public string NOMBRE_PROPIETARIO { get; set; }
        public string APELLIDO_PATERNO_PROPIETARIO { get; set; }
        public string APELLIDO_MATERNO_PROPIETARIO { get; set; }
        public int ID_VEHICULO_ASEGURADOR { get; set; }
    }
}
