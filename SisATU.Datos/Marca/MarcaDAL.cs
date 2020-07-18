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
    public class MarcaDAL
    {
        string cadenaConexion = string.Empty;

        #region Constructor
        public MarcaDAL()
        {
            cadenaConexion = Configuracion.GetConectionSting("sConexionSISREGISTRO");
        }
        #endregion

        #region Combo Tipo Seguro
        public List<ComboMarcaVM> ComboMarca()
        {
            try
            {
                List<ComboMarcaVM> resultado = new List<ComboMarcaVM>();
                using (var bdConn = new OracleConnection(cadenaConexion))
                {
                    using (var bdCmd = new OracleCommand("PKG_MARCA.SP_LISTA", bdConn))
                    {
                        bdCmd.CommandType = CommandType.StoredProcedure;
                        bdCmd.Parameters.AddRange(ParametrosComboMarca());
                        bdConn.Open();
                        using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                        {
                            if (bdRd.HasRows)
                            {
                                while (bdRd.Read())
                                {
                                    var item = new ComboMarcaVM();
                                    if (!DBNull.Value.Equals(bdRd["ID_MARCA"])) { item.ID_MARCA = Convert.ToInt32(bdRd["ID_MARCA"]); }
                                    if (!DBNull.Value.Equals(bdRd["NOMBRE"])) { item.NOMBRE = Convert.ToString(bdRd["NOMBRE"]); }
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

        #region Parametros
        private OracleParameter[] ParametrosComboMarca()
        {
            OracleParameter[] bdParameters = new OracleParameter[1];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            return bdParameters;
        }
        #endregion
    }
}
