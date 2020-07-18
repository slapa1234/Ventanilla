//using Oracle.DataAccess.Client;
using Oracle.ManagedDataAccess.Client;
using SisATU.Base.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Datos.ClaseVehiculo
{
    public class ClaseVehiculoDAL
    {
        string cadenaConexion = string.Empty;

        #region Constructor
        public ClaseVehiculoDAL()
        {
            cadenaConexion = Configuracion.GetConectionSting("sConexionSISREGISTRO");
        }
        #endregion

        #region Combo Clase Vehiculo
        public List<ComboClaseVehiculoVM> ComboClaseVehiculo()
        {
            try
            {
                List<ComboClaseVehiculoVM> resultado = new List<ComboClaseVehiculoVM>();
                using (var bdConn = new OracleConnection(cadenaConexion))
                {
                    using (var bdCmd = new OracleCommand("PKG_CLASE_VEHICULO.SP_LISTA", bdConn))
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
                                    var item = new ComboClaseVehiculoVM();
                                    if (!DBNull.Value.Equals(bdRd["ID_CLASE_VEHICULO"])) { item.ID_CLASE_VEHICULO = bdRd["ID_CLASE_VEHICULO"].ValorEntero(); }
                                    if (!DBNull.Value.Equals(bdRd["NOMBRE_CLASE_VEHICULO"])) { item.NOMBRE = bdRd["NOMBRE_CLASE_VEHICULO"].ValorCadena(); }
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
