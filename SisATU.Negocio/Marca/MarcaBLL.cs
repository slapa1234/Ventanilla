using SisATU.Base.ViewModel;
using SisATU.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Negocio
{
    public class MarcaBLL
    {
        public List<ComboMarcaVM> ComboMarca()
        {
            List<ComboMarcaVM> combo = new List<ComboMarcaVM>();
            MarcaDAL obj = new MarcaDAL();
            combo = obj.ComboMarca();
            combo.RemoveAll(x => x.ID_MARCA == 0);
            return combo;
        }
    }
}
