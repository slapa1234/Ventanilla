using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SisATU.WebUI.Models
{
    public class RenovacionDuplicadoVM
    {
        public string NroPlaca { get; set; }
        public string AnioFabricacion { get; set; }
        public string ModeloVehiculo { get; set; }
        public string NombreMarca { get; set; }
        public string ClaseVehiculo { get; set; }
        public string ModalidadServicio { get; set; }
        public string FechaVencimientoDocumento { get; set; }
        public int CodTipoSeguro { get; set; }
        public string NomAseguradora { get; set; }
        public string NomPoliza { get; set; }
        public string SeguroFechaInicio { get; set; }
        public string SeguroFechaFin { get; set; }
        public string EntidadRevision { get; set; }
        public string CertificadoRevision { get; set; }
        public string FechaIncio { get; set; }
        public string FechaFin { get; set; }
        public int IdEntidadBancaria { get; set; }
        public string NroVoucher { get; set; }
        public string FechaPago { get; set; }
        public int CodEntidadBancaria { get; set; }
        public int protupac { get; set; }
        public int tipmoda { get; set; }
        public string EmailConduc { get; set; }
    }
}