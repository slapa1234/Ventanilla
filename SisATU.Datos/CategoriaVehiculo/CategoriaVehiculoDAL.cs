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
    public class CategoriaVehiculoDAL
    {
        string cadenaConexion = string.Empty;

        #region Constructor
        public CategoriaVehiculoDAL()
        {
            cadenaConexion = Configuracion.GetConectionSting("sConexionSISREGISTRO");
        }
        #endregion

        #region 
        public List<ComboCategoriaVehiculoVM> ComboCategoriaVehiculo()
        {
            try
            {
                List<ComboCategoriaVehiculoVM> resultado = new List<ComboCategoriaVehiculoVM>();
                using (var bdConn = new OracleConnection(cadenaConexion))
                {
                    using (var bdCmd = new OracleCommand("PKG_CATEGORIA_VEHICULO.SP_LISTA", bdConn))
                    {
                        bdCmd.CommandType = CommandType.StoredProcedure;
                        bdCmd.Parameters.AddRange(ParametrosComboAfocat());
                        bdConn.Open();
                        using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                        {
                            if (bdRd.HasRows)
                            {
                                while (bdRd.Read())
                                {
                                    var item = new ComboCategoriaVehiculoVM();
                                    if (!DBNull.Value.Equals(bdRd["ID_CATEGORIA_VEHICULO"])) { item.ID_CATEGORIA_VEHICULO = bdRd["ID_CATEGORIA_VEHICULO"].ValorEntero(); }
                                    if (!DBNull.Value.Equals(bdRd["NOMBRE"])) { item.NOMBRE = bdRd["NOMBRE"].ValorCadena(); }
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
        private OracleParameter[] ParametrosComboAfocat()
        {
            OracleParameter[] bdParameters = new OracleParameter[1];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            return bdParameters;
        }
        #endregion

    }
}
