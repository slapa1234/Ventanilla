using SisATU.Base.ViewModel;
using SisATU.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Negocio
{
    public class TramiteBLL
    {
        
        public List<TramiteVM> getListaTramiteByTipo(int idTipoTramite)
        {
            TramiteDAL obj = new TramiteDAL();
            return obj.getListaTramiteByTipo(idTipoTramite);
        }
    }
}
