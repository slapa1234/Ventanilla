//using Oracle.DataAccess.Client;
using Oracle.ManagedDataAccess.Client;
using SisATU.Base;
using SisATU.Base.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Datos
{
    public class TramiteDAL
    {
        string cadenaConexion = string.Empty;

        #region Constructor
        public TramiteDAL()
        {
            cadenaConexion = Configuracion.GetConectionSting("sConexionSISREGISTRO");
        }
        #endregion

        #region lista de tramites
        public List<TramiteVM> getListaTramiteByTipo(int idTipoTramite)
        {
            List<TramiteVM> resultado = new List<TramiteVM>();
            try
            {
                using (var bdConn = new OracleConnection(cadenaConexion))
                {
                    using (var bdCmd = new OracleCommand("PKG_MODALIDAD_SERVICIO.SP_LIS_TPOTRAMITE", bdConn))
                    {
                        bdCmd.CommandType = CommandType.StoredProcedure;
                        bdCmd.Parameters.AddRange(ParametrosListaTramite(idTipoTramite));
                        bdConn.Open();
                        using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                        {
                            if (bdRd.HasRows)
                            {
                                while (bdRd.Read())
                                {
                                    var item = new TramiteVM();
                                    if (!DBNull.Value.Equals(bdRd["ID_TIPO_TRAMITE"])) { item.ID_TIPO_TRAMITE =  (bdRd["ID_TIPO_TRAMITE"]).ValorEntero(); }
                                    if (!DBNull.Value.Equals(bdRd["NOMBRE_TRAMITE"])) { item.NOMBRE_TRAMITE = (bdRd["NOMBRE_TRAMITE"]).ValorCadena(); }
                                    if (!DBNull.Value.Equals(bdRd["ICONO"])) { item.ICONO = (bdRd["ICONO"]).ValorCadena(); }
                                    resultado.Add(item);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return resultado;
        }
        #endregion

        #region lista de tramites
        private OracleParameter[] ParametrosListaTramite(int idTipoTramite)
        {
            OracleParameter[] bdParameters = new OracleParameter[2];
            bdParameters[0] = new OracleParameter("P_TIPO_TRAMITE", OracleDbType.Int32) { Value = idTipoTramite };
            bdParameters[1] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            return bdParameters;
        }        
        #endregion
    }
}
