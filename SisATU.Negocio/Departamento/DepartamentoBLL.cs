using SisATU.Base.ViewModel;
using SisATU.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Negocio
{
    public  class DepartamentoBLL
    {
        public List<ComboDepartamentoVM> ComboDepartamento(int P_PARCOD)
        {
            DepartamentoDAL obj = new DepartamentoDAL();
            return obj.ComboDepartamento(P_PARCOD);

        }
    }
}
