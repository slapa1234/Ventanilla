using SisATU.Base.ViewModel;
using SisATU.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Negocio
{
    public class DistritoBLL
    {
        public List<ComboDistritoVM> ComboDistrito(int P_PARCOD)
        {
            DistritoDAL obj = new DistritoDAL();
            var resultado = obj.ComboDistrito(P_PARCOD);
            resultado.RemoveAll(x => x.ID_DISTRITO == 0);
            return resultado;
        }
    }
}
