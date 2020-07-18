using SisATU.Base.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SisATU.Base
{
    public class DatosVentanaPrincipalVM
    {
        public DatosVentanaPrincipalVM()
        {
            ListaTipoPersona = new List<ParametroModelo>();
            ListaModalidadServicio = new List<ComboModalidadServicioVM>();
            ListaTramite = new List<TramiteVM>();
        }

        public List<SelectListItem> SelectTipoDocumento { get; set; }

        public List<ParametroModelo> ListaTipoPersona { get; set; }
        public List<ComboModalidadServicioVM> ListaModalidadServicio { get; set; }
        public List<TramiteVM> ListaTramite { get; set; }
        public List<MultaVM> ListaMultas { get; set; }
        public List<EntidadBancariaVM> ListaEntidadBancaria { get; set; }
        
    }
}
