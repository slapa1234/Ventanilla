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
    public class TramiteSimpleBLL
    {
        TramiteSimpleDAL tramiteDAL;
        private Object bdConn;
        public TramiteSimpleBLL()
        {
            tramiteDAL = new TramiteSimpleDAL(ref bdConn);
        }

        public ResultadoProcedimientoVM registrarTramiteSimple(TramiteSimpleVM tramite)
        {
            TramiteSimpleDAL obj = new TramiteSimpleDAL(ref bdConn);
            return obj.registrarTramite(tramite);
        }

        public ResultadoProcedimientoVM actualizarDataTramiteSimple(int idTramite, string nombresArchivo, int idDocExpediente, string ssid_exp)
        {
            TramiteSimpleDAL obj = new TramiteSimpleDAL(ref bdConn);
            return obj.actualizarDataTramiteSimple(idTramite, nombresArchivo, idDocExpediente, ssid_exp);
        }
        public ResultadoProcedimientoVM busqueda_recibo(string nroRecibo)
        {
            TramiteSimpleDAL obj = new TramiteSimpleDAL(ref bdConn);
            return obj.busqueda_recibo(nroRecibo);
        }



    }
}
