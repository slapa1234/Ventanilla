using SisATU.Base;
using SisATU.Base.ViewModel;
using SisATU.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Negocio
{
    public class TramiteSATBLL
    {
        TramiteSATDAL tramiteSATDAL;
        MultaDAL multaDAL;
        private Object bdConn;
        public TramiteSATBLL()
        {
            tramiteSATDAL = new TramiteSATDAL(ref bdConn);
            multaDAL = new MultaDAL(ref bdConn);

        }

        public ResultadoProcedimientoVM registrarTramiteSAT(TramiteSATVM tramite)
        {
            TramiteSATDAL obj = new TramiteSATDAL(ref bdConn);
            return obj.registrarTramiteSAT(tramite);
        }

        public ResultadoProcedimientoVM actualizarNombreArchivos(int idTramiteSAT, string nombresArchivouch, string nomarch_desist)
        {
            TramiteSATDAL obj = new TramiteSATDAL(ref bdConn);
            return obj.actualizarNombreArchivos( idTramiteSAT, nombresArchivouch,  nomarch_desist);
        }
         
        public List<MultaVM> listarMultas()
        {
            MultaDAL obj = new MultaDAL(ref bdConn);
            return obj.listarMultas();
        }


    }
}
