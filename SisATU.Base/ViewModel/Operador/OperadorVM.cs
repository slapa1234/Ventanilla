using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Base.ViewModel
{
    public class OperadorVM
    {
        public OperadorVM()
        {
            ResultadoProcedimientoVM = new ResultadoProcedimientoVM();
        }
        public int NRO_ORDEN { get; set; }
        public int ID_OPERADOR { get; set; }
        public int ID_TIPO_DOCUMENTO { get; set; }
        public string NRO_DOCUMENTO { get; set; }
        public string NUM_CREDENCIAL { get; set; }
        public int TIPO_PERSONA { get; set; }
        public string APELLIDO_PATERNO { get; set; }
        public string APELLIDO_MATERNO { get; set; }
        public string NOMBRES { get; set; }
        public string RAZON_SOCIAL { get; set; }
        public string DIRECCION { get; set; }
        public string TELEFONO_CEL { get; set; }
        public string TELEFONO_CASA { get; set; }
        public string CORREO { get; set; }
        public string ANIO { get; set; }
        public int ID_SEXO { get; set; }
        public string FECHA_NACIMIENTO { get; set; }
        public int ID_MODALIDAD_SERVICIO { get; set; }
        public string NOMBRE_MODALIDAD_SERVICIO { get; set; }
        public string BD { get; set; }

        public string FOTO_BASE64 { get; set; }

        //datos Operador
        //public int TIPO_OPERADOR { get; set; }
        public int ID_TIPO_OPERADOR { get; set; }
        public int MODALIDAD_SERVICIO { get; set; }
        public string NOMBRE_FOTO { get; set; }
        public string FECHA_INSCRIPCION { get; set; }

        public string NRO_LICENCIA { get; set; }
        public string CATEGORIA { get; set; }
        public string FECHA_EXPEDICION { get; set; }
        public string FECHA_VENCIMIENTO_CREDENCIAL { get; set; }
        public string FECHA_REVALIDACION { get; set; }
        public string RESTRICCION { get; set; }
        public string ESTADO_LICENCIA { get; set; }
        public string NOMBRE_TIPO_DOCUMENTO { get; set; }
        public string NOMBRE_TIPO_OPERADOR { get; set; }
        public string RUC_EMPRESA_OPERADOR { get; set; }

        public int ID_DEPARTAMENTO_OPERADOR { get; set; }
        public int ID_PROVINCIA_OPERADOR { get; set; }
        public int ID_DISTRITO_OPERADOR { get; set; }
        public bool REGISTRO_AGREGADO { get; set; }
        public double PUNTOS_FIRME { get; set; }
        public int MUY_GRAVE { get; set; }
        public int GRAVE { get; set; }
        public int RESULTADO { get; set; }
        public int TieneCredencial { get; set; }
        public ResultadoProcedimientoVM ResultadoProcedimientoVM { get; set; }
    }
    
}
