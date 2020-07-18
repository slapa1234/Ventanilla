using SisATU.Base;
using SisATU.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Negocio
{
    public class ParametroBLL
    {
        ParametroDAL obj = new ParametroDAL();
        public List<ParametroModelo> ConsultaParametro(int PARTIP)
        {
            return obj.ConsultaParametro(PARTIP);
        }

        public int BuscarValorTipoProcedimiento(int ID_PROCEDIMIENTO)
        {
            return obj.BuscarValorTipoProcedimiento(ID_PROCEDIMIENTO);
        }

        public int ConsultarCredencialAdministrado(string RUC, string codigo)
        {

            return obj.ConsultarCredencialAdministrado(RUC, codigo);
        }

        public int Validar(string RUC, string codigo)
        {

            return obj.ConsultarCredencialAdministrado(RUC, codigo);
        }
    }
}
