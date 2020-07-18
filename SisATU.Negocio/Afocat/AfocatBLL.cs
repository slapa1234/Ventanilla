using SisATU.Base.ViewModel;
using SisATU.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Negocio
{
    public class AfocatBLL
    {
        public List<ComboAfocatVM> ConsultaAfocat()
        {
            AfocatDAL obj = new AfocatDAL();
            return obj.ComboAfocat();
        }
    }
}
