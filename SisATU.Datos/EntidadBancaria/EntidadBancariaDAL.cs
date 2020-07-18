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
    public class EntidadBancariaDAL
    {
        string cadenaConexion = string.Empty;

        #region Constructor
        public EntidadBancariaDAL()
        {
            cadenaConexion = Configuracion.GetConectionSting("sConexionSISREGISTRO");
        }
        #endregion

        #region Consulta Combo Entidad Bancaria
        public List<EntidadBancariaVM> ConsultaComboEntidadBancaria()
        {
            try
            {
                List<EntidadBancariaVM> resultado = new List<EntidadBancariaVM>();
                using (var bdConn = new OracleConnection(cadenaConexion))
                {
                    using (var bdCmd = new OracleCommand("PKG_ENTIDAD_BANCARIA.SP_LISTA", bdConn))
                    {
                        bdCmd.CommandType = CommandType.StoredProcedure;
                        bdCmd.Parameters.AddRange(ParametrosComboEntidadBancaria());
                        bdConn.Open();
                        using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                        {
                            if (bdRd.HasRows)
                            {
                                while (bdRd.Read())
                                {
                                    var item = new EntidadBancariaVM();
                                    if (!DBNull.Value.Equals(bdRd["ID_ENTIDAD_BANCARIA"])) { item.ID_ENTIDAD_BANCARIA = Convert.ToInt32(bdRd["ID_ENTIDAD_BANCARIA"]); }
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

        #region Parametro
        private OracleParameter[] ParametrosComboEntidadBancaria()
        {
            OracleParameter[] bdParameters = new OracleParameter[1];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            return bdParameters;
        }
        #endregion
    }
}
