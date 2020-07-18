using SisATU.Base.ViewModel;
using SisATU.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Negocio
{
    public class CategoriaVehiculoBLL
    {
        public List<ComboCategoriaVehiculoVM> ComboCategoriaVehiculo()
        {
            CategoriaVehiculoDAL obj = new CategoriaVehiculoDAL();
            return obj.ComboCategoriaVehiculo();
        }
    }
}
