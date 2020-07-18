using SisATU.Base.ViewModel;
using SisATU.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Negocio
{
    public class ModeloBLL
    {
        public List<ComboModeloVM> ComboModelo(int ID_MARCA)
        {
            ModeloDAL obj = new ModeloDAL();
            return obj.ComboModelo(ID_MARCA);
        }
    }
}
