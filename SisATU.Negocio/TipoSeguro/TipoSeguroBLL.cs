using SisATU.Base.ViewModel;
using SisATU.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Negocio
{
    public class TipoSeguroBLL
    {
        public List<ComboTipoSeguroVM> ComboTipoSeguro()
        {
            TipoSeguroDAL obj = new TipoSeguroDAL();
            return obj.ComboTipoSeguro();
        }
    }
}
