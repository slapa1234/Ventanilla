using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Base
{
    public class UsuarioModelo
    {
        public UsuarioModelo()
        {
            ResultadoUsuarioVM = new ResultadoUsuarioVM();
        }
        public int ID_USARIO { get; set; }
        public int ID_PERSONA { get; set; }
        public string NOMBRE_USUARIO { get; set; }
        public string CLAVE { get; set; }
        public int ID_ESTADO { get; set; }
        public string USU_REG { get; set; }
        public string FECHA_REG { get; set; }
        public string USU_MODIF { get; set; }
        public string FECHA_MODIF { get; set; }
        public string USU_ANULA { get; set; }
        public string FECHA_ANULA { get; set; }
        public int ID_TIPO_DOCUMENTO_REPRESENTANTE_LEGAL { get; set; }
        public string NRO_DOCUMENTO_REPRESENTANTE_LEGAL { get; set; }
        public ResultadoUsuarioVM ResultadoUsuarioVM { get; set; }
    }
}
