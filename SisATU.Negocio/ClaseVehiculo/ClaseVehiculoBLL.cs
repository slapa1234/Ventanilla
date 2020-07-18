using SisATU.Base.ViewModel;
using SisATU.Datos.ClaseVehiculo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Negocio
{
    public class ClaseVehiculoBLL
    {
        public List<ComboClaseVehiculoVM> ComboClaseVehiculo()
        {
            List<ComboClaseVehiculoVM> lista = new List<ComboClaseVehiculoVM>();
            ClaseVehiculoDAL obj = new ClaseVehiculoDAL();
            lista = obj.ComboClaseVehiculo();
            lista.RemoveAll(x => x.ID_CLASE_VEHICULO == 0);
            return lista;
        }
    }
}
