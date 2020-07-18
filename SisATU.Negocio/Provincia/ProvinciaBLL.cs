using SisATU.Base.ViewModel;
using SisATU.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Negocio
{
    public class ProvinciaBLL
    {
        public List<ComboProvinciaVM> ComboProvincia(int P_PARCOD)
        {
            ProvinciaDAL obj = new ProvinciaDAL();
            var resultado = obj.ComboProvincia(P_PARCOD);
            resultado.RemoveAll(x => x.ID_PROVINCIA == 0);
            return resultado;
        }
        
    }
}
