using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SisATU.Base
{
    public class OperadorModelo
    {
        public int ID_OPERADOR { get; set; }
        public string NRO_DOCUMENTO { get; set; }
        public int ID_TIPO_OPERADOR { get; set; }
        public int ID_MODALIDAD_SERVICIO { get; set; }
        public string FOTO_OPERADOR { get; set; }
        public string FECHA_INSCRIPCION_OPERADOR { get; set; }
        public int AÑO { get; set; }
        public string OBSERVACION { get; set; }
        public int ID_ESTADO { get; set; }
        public string USUARIO_REG { get; set; }
        public string FECHA_REG { get; set; }
        public string USUARIO_MOD { get; set; }
        public string FECHA_MOD { get; set; }
        public string USUARIO_ELIM { get; set; }
        public string FECHA_ELIM { get; set; }
        public string NRO_LICENCIA { get; set; }
        public string CATEGORIA { get; set; }
        public string FECHA_EXPEDICION { get; set; }
        public string FECHA_REVALIDACION { get; set; }
        public string RESTRICCION { get; set; }
        public string ESTADO_LICENCIA { get; set; }
        public int ID_PERSONA { get; set; }

        public int ID_TIPO_PERSONA { get; set; }
        public string APELLIDO_PATERNO { get; set; }
        public string APELLIDO_MATERNO { get; set; }
        public string NOMBRE { get; set; }
        public int ID_TIPO_DOCUMENTO { get; set; }
        public string RAZON_SOCIAL { get; set; }
        public string DIRECCION { get; set; }
        public string TELEFONO_CEL { get; set; }
        public string TELEFONO_CASA { get; set; }
        public string CORREO { get; set; }
        //public string NOMBRE_FOTO { get; set; }
        public String FOTO_BASE64 { get; set; }
        public string FECHA_INSCRIPCION { get; set; }
        public int ID_SEXO { get; set; }
        public string FEC_NAC { get; set; }

        public int ID_DEPARTAMENTO_OPERADOR { get; set; }
        public int ID_PROVINCIA_OPERADOR { get; set; }
        public int ID_DISTRITO_OPERADOR { get; set; }

        //public int TIPO_DOCUMENTO { get; set; }
        //public string NRO_DOCUMENTO { get; set; }
        //public int TIPO_PERSONA { get; set; }
        //public string APELLIDO_PATERNO { get; set; }
        //public string APELLIDO_MATERNO { get; set; }
        //public string NOMBRE { get; set; }
        //public string RAZON_SOCIAL { get; set; }
        //public string DIRECCION { get; set; }
        //public string TELEFONO_CEL { get; set; }
        //public string TELEFONO_CASA { get; set; }
        //public string CORREO { get; set; }


        //public int TIPO_OPERADOR { get; set; }
        //public int MODALIDAD_SERVICIO { get; set; }
        //public string NOMBRE_FOTO { get; set; }
        //public string FECHA_INSCRIPCION { get; set; }
        //public string ANIO { get; set; }
        //public string NRO_LICENCIA { get; set; }
        //public string CATEGORIA { get; set; }
        //public string FECHA_EXPEDICION { get; set; }
        //public string FECHA_REVALIDACION { get; set; }
        //public string RESTRICCION { get; set; }
        //public string ESTADO_LICENCIA { get; set; }
        //public List<SelectListItem> SelectModalidad { get; set; }
    }

}
