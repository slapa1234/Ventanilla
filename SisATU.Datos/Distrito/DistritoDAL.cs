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
    public class DistritoDAL
    {
        string cadenaConexion = string.Empty;

        #region Constructor
        public DistritoDAL()
        {
            cadenaConexion = Configuracion.GetConectionSting("sConexionSISREGISTRO");
        }
        #endregion

        #region Combo Distrito
        public List<ComboDistritoVM> ComboDistrito(int P_PARCOD)
        {
            try
            {
                List<ComboDistritoVM> resultado = new List<ComboDistritoVM>();
                using (var bdConn = new OracleConnection(cadenaConexion))
                {
                    using (var bdCmd = new OracleCommand("PKG_UBIGEO.SP_DISTRITO", bdConn))
                    {
                        bdCmd.CommandType = CommandType.StoredProcedure;
                        bdCmd.Parameters.AddRange(ParametrosComboDistrito(P_PARCOD));
                        bdConn.Open();
                        using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                        {
                            if (bdRd.HasRows)
                            {
                                while (bdRd.Read())
                                {
                                    var item = new ComboDistritoVM();
                                    if (!DBNull.Value.Equals(bdRd["ID_DISTRITO"])) { item.ID_DISTRITO = bdRd["ID_DISTRITO"].ValorEntero(); }
                                    if (!DBNull.Value.Equals(bdRd["NOMBRE_DISTRITO"])) { item.NOMBRE_DISTRITO = bdRd["NOMBRE_DISTRITO"].ValorCadena(); }
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
        private OracleParameter[] ParametrosComboDistrito(int P_PARCOD)
        {
            OracleParameter[] bdParameters = new OracleParameter[2];
            bdParameters[0] = new OracleParameter("P_PARCOD", OracleDbType.Int32) { Value = P_PARCOD };
            bdParameters[1] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            return bdParameters;
        }
        #endregion
    }
}
