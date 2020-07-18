using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Base
{
    public class PersonaModelo
    {
        public int ID_PERSONA { get; set; }
        public string NRO_DOCUMENTO { get; set; }
        public string APELLIDO_PATERNO { get; set; }
        public string APELLIDO_MATERNO { get; set; }
        public int ID_TIPO_DOCUMENTO { get; set; }
        public string RAZON_SOCIAL { get; set; }
        public string DIRECCION { get; set; }
        public string TELEFONO { get; set; }
        public string CORREO { get; set; }
        public int ID_ESTADO { get; set; }
        public string USUARIO_REG { get; set; }
        public string FECHA_REG { get; set; }
        public string USUARIO_MOD { get; set; }
        public string FECHA_MOD { get; set; }
        public string USUARIO_ELIM { get; set; }
        public string FECHA_ELIM { get; set; }
        public int ID_TIPO_PERSONA { get; set; }
        public string NRO_RUC { get; set; }
        public string NOMBRES { get; set; }
        public int AUTORIZA_ENVIO_MAIL { get; set; }
        public string TELEFONO_CASA { get; set; }
        public int ID_SEXO { get; set; }
        public string DIRECCION_ACTUAL { get; set; }
        public string FEC_NAC { get; set; }
        public int ID_DEPARTAMENTO { get; set; }
        public int ID_PROVINCIA { get; set; }
        public int ID_DISTRITO { get; set; }


        

        //public int Id_Persona { get; set; }
        //public string Numero_Documento { get; set; }
        //public string Nombre { get; set; }
        //public string Apellido_Paterno { get; set; }
        //public string Apellido_Materno { get; set; }
        //public DateTime FechaRegistro { get; set; }
        //public string Razon_Social { get; set; }
        //public string Correo { get; set; }
        //public string Foto { get; set; }
        //public string Direccion { get; set; }

        ////
        //public int CODPAIS { get; set; }
        //public int CODDPTO { get; set; }
        //public int CODPROV { get; set; }
        //public int CODDIST { get; set; }
        //public string DIRECCION_STD { get; set; }
    }
}
