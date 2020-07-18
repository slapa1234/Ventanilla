using SisATU.Base;
using SisATU.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Negocio
{
    public class PagoBLL
    {
        PagoDAL PagoDAL;
        private Object bdConn;
        public PagoBLL()
        {
            PagoDAL = new PagoDAL(ref bdConn);
        }
        public ResultadoProcedimientoVM ConsultarScotiabank(int ID_MODALIDAD_SERVICIO, int ID_PROCEDIMIENTO, string NRO_RECIBO, string FECHA_PAGO)
        {
            return PagoDAL.ConsultarScotiabank(ID_MODALIDAD_SERVICIO, ID_PROCEDIMIENTO, NRO_RECIBO, FECHA_PAGO);
        }

        public ResultadoProcedimientoVM ConsultarNacion(int ID_PROCEDIMIENTO, string NRO_RECIBO, string FECHA_PAGO)
        {
            return PagoDAL.ConsultarNacion(ID_PROCEDIMIENTO, NRO_RECIBO, FECHA_PAGO);
        }
    }
}
