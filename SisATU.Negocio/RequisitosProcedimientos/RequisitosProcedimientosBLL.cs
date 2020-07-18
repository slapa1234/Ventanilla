using SisATU.Base.ViewModel;
using SisATU.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Negocio
{
    public class RequisitosProcedimientosBLL
    {
        public List<ComboRequisitosProcedimientosVM> ComboRequisitosProcedimiento(int ID_PROCEDIMIENTO)
        {
            RequisitosProcedimientosDAL obj = new RequisitosProcedimientosDAL();
            return obj.ComboRequisitosProcedimiento(ID_PROCEDIMIENTO);
        }
    }
}
