using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Base.ViewModel
{
    public class PersonaVM
    {
        public PersonaVM()
        {
            ResultadoProcedimientoVM = new ResultadoProcedimientoVM();
        }
        public int ID_PERSONA { get; set; }
        public string NRO_DOCUMENTO { get; set; }
        public string APELLIDO_PATERNO { get; set; }
        public string APELLIDO_MATERNO { get; set; }
        public int ID_TIPO_DOCUMENTO { get; set; }
        public string RAZON_SOCIAL { get; set; }
        public string DIRECCION { get; set; }
        public string DIRECCION_ACTUAL { get; set; }
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
        public int CODPAIS { get; set; }
        public int CODDPTO { get; set; }
        public int CODPROV { get; set; }
        public int CODDIST { get; set; }
        //public int DIRECCI { get; set; }
        public string DIRECCION_STD { get; set; }
        public string FOTO { get; set; }

        public int ID_DEPARTAMENTO { get; set; }
        public int ID_PROVINCIA { get; set; }
        public int ID_DISTRITO { get; set; }
        public int ULTIMO_DIGITO { get; set; }
        #region API RES
        public string nombres { get; set; }
        public string primerApellido { get; set; }
        public string segundoApellido { get; set; }
        public string calidadMigratoria { get; set; }
        #endregion

        #region API REST
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public string direccion { get; set; }
        public string foto { get; set; }
        #endregion
        public ResultadoProcedimientoVM ResultadoProcedimientoVM { get; set; }
    }
}
