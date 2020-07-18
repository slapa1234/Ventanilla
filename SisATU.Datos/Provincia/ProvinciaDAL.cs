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
    public class ProvinciaDAL
    {
        string cadenaConexion = string.Empty;

        #region Constructor
        public ProvinciaDAL()
        {
            cadenaConexion = Configuracion.GetConectionSting("sConexionSISREGISTRO");
        }
        #endregion

        #region Combo Provincia
        public List<ComboProvinciaVM> ComboProvincia(int P_PARCOD)
        {
            try
            {
                List<ComboProvinciaVM> resultado = new List<ComboProvinciaVM>();
                using (var bdConn = new OracleConnection(cadenaConexion))
                {
                    using (var bdCmd = new OracleCommand("PKG_UBIGEO.SP_PROVINCIA", bdConn))
                    {
                        bdCmd.CommandType = CommandType.StoredProcedure;
                        bdCmd.Parameters.AddRange(ParametrosComboProvincia(P_PARCOD));
                        bdConn.Open();
                        using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                        {
                            if (bdRd.HasRows)
                            {
                                while (bdRd.Read())
                                {
                                    var item = new ComboProvinciaVM();
                                    if (!DBNull.Value.Equals(bdRd["ID_PROVINCIA"])) { item.ID_PROVINCIA = bdRd["ID_PROVINCIA"].ValorEntero(); }
                                    if (!DBNull.Value.Equals(bdRd["NOMBRE_PROVINCIA"])) { item.NOMBRE_PROVINCIA = bdRd["NOMBRE_PROVINCIA"].ValorCadena(); }
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
        private OracleParameter[] ParametrosComboProvincia(int P_PARCOD)
        {
            OracleParameter[] bdParameters = new OracleParameter[2];
            bdParameters[0] = new OracleParameter("P_PARCOD", OracleDbType.Int32) { Value = P_PARCOD };
            bdParameters[1] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            return bdParameters;
        }
        #endregion
    }

}
