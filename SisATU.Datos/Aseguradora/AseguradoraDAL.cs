using SisATU.Base.ViewModel;
using SisATU.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Datos
{
    public class AseguradoraDAL
    {
        public AseguradoraVM ConsultaSoat(string nroPlaca)
        {
            AseguradoraVM aseguradora = new AseguradoraVM();
            SoatService obj = new SoatService();
            aseguradora = obj.ConsultaSOAT(nroPlaca);
            return aseguradora;
        }
         
    }
}
