//using Oracle.DataAccess.Client;
using Oracle.ManagedDataAccess.Client;
using SisATU.Base;
using SisATU.Datos.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Datos
{
    public class ParametroDAL
    {
        string cadenaConexion = string.Empty;

        #region Constructor
        public ParametroDAL()
        {
            cadenaConexion = Configuracion.GetConectionSting("sConexionSISREGISTRO");
        }
        #endregion

        #region Consulta Parametro
        public List<ParametroModelo> ConsultaParametro(int PARTIP)
        {
            try
            {
                List<ParametroModelo> resultado = new List<ParametroModelo>();
                using (var bdConn = new OracleConnection(cadenaConexion))
                {
                    using (var bdCmd = new OracleCommand("PKG_PARAMETRO.SP_BUSCAR", bdConn))
                    {
                        bdCmd.CommandType = CommandType.StoredProcedure;
                        bdCmd.Parameters.AddRange(CargarParametros(PARTIP));
                        bdConn.Open();
                        using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                        {
                            if (bdRd.HasRows)
                            {
                                while (bdRd.Read())
                                {
                                    var item = new ParametroModelo();
                                    if (!DBNull.Value.Equals(bdRd["PARCOD"])) { item.PARCOD = Convert.ToInt32(bdRd["PARCOD"]); }
                                    if (!DBNull.Value.Equals(bdRd["PARSEC"])) { item.PARSEC = Convert.ToInt32(bdRd["PARSEC"]); }
                                    if (!DBNull.Value.Equals(bdRd["PARNOM"])) { item.PARNOM = Convert.ToString(bdRd["PARNOM"]); }
                                    if (!DBNull.Value.Equals(bdRd["PARTIP"])) { item.PARTIP = Convert.ToInt32(bdRd["PARTIP"]); }//ToDouble
                                    if (!DBNull.Value.Equals(bdRd["PARSIG"])) { item.PARSIG = Convert.ToString(bdRd["PARSIG"]); }
                                    //if (!DBNull.Value.Equals(bdRd["PARSEC"])) { item.PARSEC = Convert.ToInt32(bdRd["PARSEC"]); }
                                    if (!DBNull.Value.Equals(bdRd["ICONO"])) { item.ICONO = (bdRd["ICONO"]).ValorCadena(); }
                                    resultado.Add(item);
                                }
                            }
                        }
                    }
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        public int BuscarValorTipoProcedimiento(int ID_PROCEDIMIENTO)
        {
            try
            {
                int valor = 0;
                String sql = "SELECT VAL FROM TM_PARAMETRO WHERE PARCOD = " + ID_PROCEDIMIENTO;
                using (var bdConn = new OracleConnection(cadenaConexion))
                {
                    using (var bdCmd = new OracleCommand(sql, bdConn))
                    {
                        bdCmd.CommandType = CommandType.Text;
                        bdConn.Open();
                        using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.SingleResult))
                        {
                            if (bdRd.HasRows)
                            {
                                while (bdRd.Read())
                                {
                                    if (!DBNull.Value.Equals(bdRd["PARVAL"])) { valor = Convert.ToInt32(bdRd["PARVAL"]); }
                                }
                            }
                        }
                    }
                }
                return valor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ConsultarCredencialAdministrado(string RUC, string codigo)
        {
            try
            {
                Int32 resultado = 0;
                using (var bdConn = new OracleConnection(cadenaConexion))
                {
                    using (var bdCmd = new OracleCommand("PKG_PARAMETRO.SP_BUSCAR_CREDENCIAL", bdConn))
                    {
                        bdCmd.CommandType = CommandType.StoredProcedure;

                        OracleParameter[] bdParameters = new OracleParameter[3];
                        bdParameters[0] = new OracleParameter("P_RUC", OracleDbType.Varchar2) { Value = RUC };
                        bdParameters[1] = new OracleParameter("P_CODIGO", OracleDbType.Varchar2) { Value = codigo };
                        bdParameters[2] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);

                        bdCmd.Parameters.AddRange(bdParameters);
                        bdConn.Open();
                        using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                        {
                            if (bdRd.HasRows)
                            {
                                while (bdRd.Read())
                                {
                                    if (!DBNull.Value.Equals(bdRd["VALIDACION"])) { resultado = Convert.ToInt32(bdRd["VALIDACION"]); }
                                }
                            }
                        }
                    }
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Parametros
        private OracleParameter[] CargarParametros(int PARTIP = 0)
        {
            OracleParameter[] bdParameters = new OracleParameter[6];
            bdParameters[0] = new OracleParameter("P_PARCOD", OracleDbType.Int32) { Value = null };
            bdParameters[1] = new OracleParameter("P_PARNOM", OracleDbType.Varchar2) { Value = null };
            bdParameters[2] = new OracleParameter("P_PARTIP", OracleDbType.Double) { Value = PARTIP };
            bdParameters[3] = new OracleParameter("P_PARSIG", OracleDbType.Varchar2) { Value = null };
            bdParameters[4] = new OracleParameter("P_VAL", OracleDbType.Varchar2) { Value = null };
            bdParameters[5] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            return bdParameters;
        }
        #endregion
    }
}
