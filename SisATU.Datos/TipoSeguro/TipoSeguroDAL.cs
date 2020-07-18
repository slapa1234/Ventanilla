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
    public class TipoSeguroDAL
    {
        string cadenaConexion = string.Empty;

        #region Constructor
        public TipoSeguroDAL()
        {
            cadenaConexion = Configuracion.GetConectionSting("sConexionSISREGISTRO");
        }
        #endregion

        #region Combo Tipo Seguro
        public List<ComboTipoSeguroVM> ComboTipoSeguro()
        {
            try
            {
                List<ComboTipoSeguroVM> resultado = new List<ComboTipoSeguroVM>();
                using (var bdConn = new OracleConnection(cadenaConexion))
                {
                    using (var bdCmd = new OracleCommand("PKG_TIPO_SEGURO.SP_LISTA", bdConn))
                    {
                        bdCmd.CommandType = CommandType.StoredProcedure;
                        bdCmd.Parameters.AddRange(ParametrosComboTipoSeguro());
                        bdConn.Open();
                        using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                        {
                            if (bdRd.HasRows)
                            {
                                while (bdRd.Read())
                                {
                                    var item = new ComboTipoSeguroVM();
                                    if (!DBNull.Value.Equals(bdRd["ID_TIPO_SEGURO"])) { item.ID_TIPO_SEGURO = Convert.ToInt32(bdRd["ID_TIPO_SEGURO"]); }
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
        private OracleParameter[] ParametrosComboTipoSeguro()
        {
            OracleParameter[] bdParameters = new OracleParameter[1];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            return bdParameters;
        }
        #endregion

    }
}
