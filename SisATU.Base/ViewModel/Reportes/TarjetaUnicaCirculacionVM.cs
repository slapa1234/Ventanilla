using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SisATU.Base.ViewModel
{
    public class TarjetaUnicaCirculacionVM
    {
        public int ID_TARJETA_CIRCULACION { get; set; }
        public string NOMBRE_MODALIDAD_SERVICIO { get; set; }
        public string PLACA { get; set; }
        public string SERIE_MOTOR { get; set; }
        public string NUMERO_ASIENTOS { get; set; }
        public string NRO_TARJETA { get; set; }
        public string NOMBRE_PROPIETARIO { get; set; }
        public string NRO_DOCUMENTO { get; set; }
        public string FECHA_IMPRESION { get; set; }
        public string FECHA_REG { get; set; }
        public string FECHA_VENCIMIENTO_DOCUMENTO { get; set; }

        public string DIRECCION { get; set; }
        public string TELEFONO { get; set; }

        public int IDDOC { get; set; }   
    }
}
