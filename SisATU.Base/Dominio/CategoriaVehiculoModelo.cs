using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Base
{
    public class CategoriaVehiculoModelo
    {
        public int ID_CATEGORIA_VEHICULO { get; set; }
        public string NOMBRE { get; set; }
        public int PESO_BRUTO_INICIO { get; set; }
        public int PESO_BRUTO_FINAL { get; set; }
        public int ID_ESTADO { get; set; }
        public string USUARIO_REG { get; set; }
        public string FECHA_REG { get; set; }
        public string USUARIO_MOD { get; set; }
        public string FECHA_MOD { get; set; }
        public string USUARIO_ELIM { get; set; }
        public string FECHA_ELIM { get; set; }
    }
}
