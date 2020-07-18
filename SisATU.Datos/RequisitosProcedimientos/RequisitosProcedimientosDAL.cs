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
    public class RequisitosProcedimientosDAL
    {
        string cadenaConexion = string.Empty;

        #region Constructor
        public RequisitosProcedimientosDAL()
        {
            cadenaConexion = Configuracion.GetConectionSting("sConexionSISREGISTRO");
        }
        #endregion

        #region Combo Requisitos Procedimiento
        public List<ComboRequisitosProcedimientosVM> ComboRequisitosProcedimiento(int ID_PROCEDIMIENTO)
        {
            List<ComboRequisitosProcedimientosVM> resultado = new List<ComboRequisitosProcedimientosVM>();
            try
            {
                using (var bdConn = new OracleConnection(cadenaConexion))
                {
                    using (var bdCmd = new OracleCommand("PKG_REQUISITOS_PROCEDIMIENTOS.SP_LISTA", bdConn))
                    {
                        bdCmd.CommandType = CommandType.StoredProcedure;
                        bdCmd.Parameters.AddRange(ParametrosComboRequisitosProcedimiento(ID_PROCEDIMIENTO));
                        bdConn.Open();
                        using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                        {
                            if (bdRd.HasRows)
                            {
                                while (bdRd.Read())
                                {
                                    var item = new ComboRequisitosProcedimientosVM();
                                    if (!DBNull.Value.Equals(bdRd["ID_REQUISITOS_PROCEDIMIENTOS"])) { item.ID_REQUISITOS_PROCEDIMIENTOS = (bdRd["ID_REQUISITOS_PROCEDIMIENTOS"]).ValorEntero(); }
                                    if (!DBNull.Value.Equals(bdRd["DESCRIPCION_REQUISITOS"])) { item.DESCRIPCION_REQUISITOS = (bdRd["DESCRIPCION_REQUISITOS"]).ValorCadena(); }
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

        #region Parametros
        private OracleParameter[] ParametrosComboRequisitosProcedimiento(int ID_PROCEDIMIENTO)
        {
            OracleParameter[] bdParameters = new OracleParameter[2];
            bdParameters[0] = new OracleParameter("P_ID_PROCEDIMIENTO", OracleDbType.Int32) { Value = ID_PROCEDIMIENTO };
            bdParameters[1] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            return bdParameters;
        }
        #endregion

    }
}
