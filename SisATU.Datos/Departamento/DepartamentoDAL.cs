//using Oracle.DataAccess.Client;
using Oracle.ManagedDataAccess.Client;
using SisATU.Base.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Datos
{
    public class DepartamentoDAL
    {
        string cadenaConexion = string.Empty;

        #region Constructor
        public DepartamentoDAL()
        {
            cadenaConexion = Configuracion.GetConectionSting("sConexionSISREGISTRO");
        }
        #endregion

        #region Combo Afocat
        public List<ComboDepartamentoVM> ComboDepartamento(int P_PARCOD)
        {
            try
            {
                List<ComboDepartamentoVM> resultado = new List<ComboDepartamentoVM>();
                using (var bdConn = new OracleConnection(cadenaConexion))
                {
                    using (var bdCmd = new OracleCommand("PKG_UBIGEO.SP_DEPARTAMENTO", bdConn))
                    {
                        bdCmd.CommandType = CommandType.StoredProcedure;
                        bdCmd.Parameters.AddRange(ParametrosComboDepartamento(P_PARCOD));
                        bdConn.Open();
                        using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                        {
                            if (bdRd.HasRows)
                            {
                                while (bdRd.Read())
                                {
                                    var item = new ComboDepartamentoVM();
                                    if (!DBNull.Value.Equals(bdRd["ID_DEPARTAMENTO"])) { item.ID_DEPARTAMENTO = bdRd["ID_DEPARTAMENTO"].ValorEntero(); }
                                    if (!DBNull.Value.Equals(bdRd["NOMBRE_DEPARTAMENTO"])) { item.NOMBRE_DEPARTAMENTO = bdRd["NOMBRE_DEPARTAMENTO"].ValorCadena(); }
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

        #region Parametro
        private OracleParameter[] ParametrosComboDepartamento(int P_PARCOD)
        {
            OracleParameter[] bdParameters = new OracleParameter[2];
            bdParameters[0] = new OracleParameter("P_PARCOD", OracleDbType.Int32) { Value = null };
            bdParameters[1] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            return bdParameters;
        }
        #endregion
    }
}
