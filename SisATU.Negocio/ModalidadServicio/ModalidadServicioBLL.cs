using SisATU.Base.Enumeradores;
using SisATU.Base.ViewModel;
using SisATU.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Negocio
{
    public class ModalidadServicioBLL
    {
        public List<ComboModalidadServicioVM> ComboModalidadServicio()
        {
            ModalidadServicioDAL obj = new ModalidadServicioDAL();
            return obj.ComboModalidadServicio();
        }

        public List<ComboModalidadServicioVM> getModalidadByTipoPersona(int idTipoModalidadPersona)
        {
            ModalidadServicioDAL obj = new ModalidadServicioDAL();
            return obj.getModalidadByTipoPersona(idTipoModalidadPersona);
        }

        public List<ComboProcedimientoVM> getProcedimientosByFiltro(int idTipoPersona, int idModalidad, int idTipoTramite)
        {
            List<ComboProcedimientoVM> resultado = new List<ComboProcedimientoVM>();
            ModalidadServicioDAL obj = new ModalidadServicioDAL();
            resultado = obj.getProcedimientosByFiltro(idTipoPersona, idModalidad, idTipoTramite).Where(x => x.PLATAFORMA == 0).ToList();

            if (idModalidad == EnumModalidadServicio.ServicioTaxiIndependiente.ValorEntero())
            {
                resultado.RemoveAll(x => x.ID_PROCEDIMIENTO == 52);
            }
            else if (idModalidad == EnumModalidadServicio.ServicioTaxiRemisse.ValorEntero() || idModalidad == EnumModalidadServicio.ServicioTaxiEstacion.ValorEntero())
            {
                resultado.RemoveAll(x => x.ID_PROCEDIMIENTO == 51);
            }
             
            return resultado;
        }
    }
}
