using SisATU.Base.ViewModel;
using SisATU.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Negocio
{
    public class EntidadBancariaBLL
    {
        public List<EntidadBancariaVM> ConsultaComboEntidadBancaria()
        {
            EntidadBancariaDAL obj = new EntidadBancariaDAL();
            return obj.ConsultaComboEntidadBancaria();
        }
    }
}
