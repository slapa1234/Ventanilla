//using Oracle.DataAccess.Client;
using Oracle.ManagedDataAccess.Client;
using SisATU.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Datos
{
    public class PagoDAL
    {
        OracleConnection bdConn;
        string cadenaConexion = string.Empty;

        #region Constructor
        public PagoDAL(ref Object _bdConn)
        {
            _bdConn = Conexion.iniciar(ref bdConn, _bdConn);
            this.cadenaConexion = Configuracion.GetConectionSting("sConexionSISREGISTRO");
        }
        #endregion

        #region CONSULTAR BANCO SCOTIABANK
        public ResultadoProcedimientoVM ConsultarScotiabank(int ID_MODALIDAD_SERVICIO, int ID_PROCEDIMIENTO, string NRO_RECIBO, string FECHA_PAGO)
        {
            ResultadoProcedimientoVM resultado = new ResultadoProcedimientoVM();
            try
            {
                using (var bdCmd = new OracleCommand("PKG_PAGOS.SP_BUS_PAG_SCOTIA", bdConn))
                {
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(ParametrosConsultarScotiabank(ID_MODALIDAD_SERVICIO, ID_PROCEDIMIENTO, NRO_RECIBO, FECHA_PAGO));
                    bdCmd.ExecuteNonQuery();
                    resultado.CodResultado = int.Parse(bdCmd.Parameters["P_RESULTADO"].Value.ToString()); ;
                    resultado.NomResultado = "Cargo Correctamente";
                }
            }
            catch (Exception ex)
            {
                resultado.CodResultado = 0;
                resultado.NomResultado = ex.Message;
            }
            return resultado;
        }
        #endregion

        #region CONSULTAR BANCO NACION
        public ResultadoProcedimientoVM ConsultarNacion(int ID_PROCEDIMIENTO, string NRO_RECIBO, string FECHA_PAGO)
        {
            ResultadoProcedimientoVM resultado = new ResultadoProcedimientoVM();
            try
            {
                using (var bdCmd = new OracleCommand("PKG_PAGOS.SP_BUS_PAG_BN", bdConn))
                {
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(ParametrosConsultarNacion(ID_PROCEDIMIENTO, NRO_RECIBO, FECHA_PAGO));
                    bdCmd.ExecuteNonQuery();
                    resultado.CodResultado = 1;
                    resultado.NomResultado = "Cargo Correctamente";
                    resultado.CodAuxiliar = int.Parse(bdCmd.Parameters["P_RESULTADO"].Value.ToString());
                }
            }
            catch (Exception ex)
            {
                resultado.CodResultado = 0;
                resultado.NomResultado = ex.Message;
            }
            return resultado;
        }
        #endregion

        #region 
        private OracleParameter[] ParametrosConsultarScotiabank(int ID_MODALIDAD_SERVICIO, int ID_PROCEDIMIENTO, string NRO_RECIBO, string FECHA_PAGO)
        {
            OracleParameter[] bdParameters = new OracleParameter[5];
            bdParameters[0] = new OracleParameter("P_ID_MODALIDAD_SERVICIO", OracleDbType.Int32) { Value = ID_MODALIDAD_SERVICIO };
            bdParameters[1] = new OracleParameter("P_ID_PROCEDIMIENTO", OracleDbType.Int32) { Value = ID_PROCEDIMIENTO };
            bdParameters[2] = new OracleParameter("P_NUMERO_DOCUMENTO_PAGO", OracleDbType.Varchar2) { Value = NRO_RECIBO };
            bdParameters[3] = new OracleParameter("P_FECHA", OracleDbType.Varchar2) { Value = FECHA_PAGO };
            bdParameters[4] = new OracleParameter("P_RESULTADO", OracleDbType.Int32, direction: ParameterDirection.Output);
            return bdParameters;
        }
        #endregion

        #region 
        private OracleParameter[] ParametrosConsultarNacion(int ID_PROCEDIMIENTO, string NRO_RECIBO, string FECHA_PAGO)
        {
            OracleParameter[] bdParameters = new OracleParameter[4];
            bdParameters[0] = new OracleParameter("P_ID_PROCEDIMIENTO", OracleDbType.Int32) { Value = ID_PROCEDIMIENTO };
            bdParameters[1] = new OracleParameter("P_NUMERO_SECUENCIA", OracleDbType.Varchar2) { Value = NRO_RECIBO };
            bdParameters[2] = new OracleParameter("P_FECHA_MOVIMIENTO", OracleDbType.Varchar2) { Value = FECHA_PAGO };
            bdParameters[3] = new OracleParameter("P_RESULTADO", OracleDbType.Int32, direction: ParameterDirection.Output);
            return bdParameters;
        }
        #endregion



    }
}
