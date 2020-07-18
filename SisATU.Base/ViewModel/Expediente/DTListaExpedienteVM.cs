using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Base.ViewModel
{
    public class DTListaExpedienteVM
    {
        public DTListaExpedienteVM()
        {
            ListaExpediente = new List<ListaExpediente>();
        }
        public List<ListaExpediente> ListaExpediente { get; set; }
        public int TotalPagina { get; set; }
        public int TotalRegistro { get; set; }
    }

    public class ListaExpediente
    {
        public int NROREG { get; set; }
        public int ID_EXPEDIENTE { get; set; }
        public string TRAMITE { get; set; }
        public string FECHA_REG { get; set; }
        public string NUMERO_DOCUMENTO { get; set; }
        public string PERSONA { get; set; }
        public string PARNOM { get; set; }
        public string MODALIDAD_SERVICIO { get; set; }
        public string NOMBRE_PROCEDIMIENTO { get; set; }
        public int ID_PROCEDIMIENTO { get; set; }
        public int ID_MODALIDAD_SERVICIO { get; set; }
        public int IDDOC { get; set; }
        public string ESTADO { get; set; }

    }
}
