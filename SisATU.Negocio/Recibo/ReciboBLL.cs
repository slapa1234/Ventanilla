using SisATU.Base.ViewModel;
using SisATU.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Negocio
{
    public class ReciboBLL
    {
        private Object bdConn;
        private ReciboDAL ReciboDAL;
        public ReciboBLL()
        {
            ReciboDAL = new ReciboDAL(ref bdConn);
        }
        public ReciboVM BuscarRecibo(string NroRecibo)
        {
            return ReciboDAL.BuscarRecibo(NroRecibo);
        }
    }
}
