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
    public class MultaDAL
    {
        OracleConnection bdConn;
        string cadenaConexion = string.Empty;

        #region Constructor
        public MultaDAL(ref Object _bdConn)
        {
            _bdConn = Conexion.iniciar(ref bdConn, _bdConn);
            cadenaConexion = Configuracion.GetConectionSting("sConexionSISREGISTRO");
        }
        #endregion

        #region registrar 

        public List<MultaVM> listarMultas()
        {
            List<MultaVM> resultado = new List<MultaVM>();
            try
            {
           
                using (var bdCmd = new OracleCommand("PKG_MULTA.SP_LISTAR_MULTAS", bdConn))
                {
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(parametrosListaMulta());
            
                    using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                    {
                        if (bdRd.HasRows)
                        {
                            while (bdRd.Read())
                            {
                                var item = new MultaVM();
                                if (!DBNull.Value.Equals(bdRd["ID_MULTA"])) { item.ID_MULTA = Convert.ToString(bdRd["ID_MULTA"]); }
                                if (!DBNull.Value.Equals(bdRd["DESCRIPCION_MULTA"])) { item.DESCRIPCION = Convert.ToString(bdRd["DESCRIPCION_MULTA"]); }
                                if (!DBNull.Value.Equals(bdRd["MONTO_MULTA"])) { item.MONTO_MULTA = Convert.ToDouble(Convert.ToString(bdRd["MONTO_MULTA"])); }

                                resultado.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
      
            }

            return resultado;
        }
        #endregion

        #region actualizar nombres de archivo
        public ResultadoProcedimientoVM actualizarNombreArchivos(int idTramite, string nombresArchivos)
        {
            ResultadoProcedimientoVM resultado = new ResultadoProcedimientoVM();
            try
            {
                using (var bdConn = new OracleConnection(cadenaConexion))
                {
                    using (var bdCmd = new OracleCommand("PKG_TRAMITE.SP_ACTUALIZAR_NOMBRE_ARCHIVO", bdConn))
                    {
                        OracleParameter[] bdParameters = new OracleParameter[2];
                        bdParameters[0] = new OracleParameter("P_ID_TRAMITE", OracleDbType.Int32) { Value = idTramite };
                        bdParameters[1] = new OracleParameter("P_NOMBREARCHIVOS", OracleDbType.Varchar2) { Value = nombresArchivos };
                        //
                        bdCmd.CommandType = CommandType.StoredProcedure;
                        bdCmd.Parameters.AddRange(bdParameters);
                        bdConn.Open();
                        bdCmd.ExecuteNonQuery();
                        resultado.CodResultado = 1;
                        resultado.NomResultado = "Actualizó Correctamente";
                        bdConn.Close();
                        bdConn.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                resultado.CodResultado = 0;
                resultado.NomResultado = ex.Message;
            }

            return resultado;
        }
        #endregion


        #region actualizar DATA Tramite
        public ResultadoProcedimientoVM actualizarDataTramiteSimple(int idTramite, string nombresArchivos, int idDocExpediente, string ssid_exp)
        {
            ResultadoProcedimientoVM resultado = new ResultadoProcedimientoVM();
            try
            {
                using (var bdConn = new OracleConnection(cadenaConexion))
                {
                    using (var bdCmd = new OracleCommand("PKG_TRAMITE.SP_ACTUALIZAR_DATA_TRAMITE", bdConn))
                    {
                        OracleParameter[] bdParameters = new OracleParameter[4];
                        bdParameters[0] = new OracleParameter("P_ID_TRAMITE", OracleDbType.Int32) { Value = idTramite };
                        bdParameters[1] = new OracleParameter("P_NOMBREARCHIVOS", OracleDbType.Varchar2) { Value = nombresArchivos };
                        bdParameters[2] = new OracleParameter("P_IDDOC_EXP", OracleDbType.Int32) { Value = idDocExpediente };
                        bdParameters[3] = new OracleParameter("P_ID_SSI_EXP", OracleDbType.Varchar2) { Value = ssid_exp };
                        //
                        bdCmd.CommandType = CommandType.StoredProcedure;
                        bdCmd.Parameters.AddRange(bdParameters);
                        bdConn.Open();
                        bdCmd.ExecuteNonQuery();
                        resultado.CodResultado = 1;
                        resultado.NomResultado = "Actualizó Correctamente";
                        bdConn.Close();
                        bdConn.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                resultado.CodResultado = 0;
                resultado.NomResultado = ex.Message;
            }

            return resultado;
        }
        #endregion
        #region Parametro registro de tramite sat
        private OracleParameter[] parametrosListaMulta()
        {
            OracleParameter[] bdParameters = new OracleParameter[1];
  
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            return bdParameters;
        }

        #endregion
    }
}
