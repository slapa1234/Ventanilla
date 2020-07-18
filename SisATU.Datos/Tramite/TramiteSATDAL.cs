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
    public class TramiteSATDAL
    {
        OracleConnection bdConn;
        string cadenaConexion = string.Empty;

        #region Constructor
        public TramiteSATDAL(ref Object _bdConn)
        {
            //_bdConn = Conexion.iniciar(ref bdConn, _bdConn);
            cadenaConexion = Configuracion.GetConectionSting("sConexionSISREGISTRO");
        }
        #endregion

        #region registrar 

        public ResultadoProcedimientoVM registrarTramiteSAT(TramiteSATVM tramite)
        {
            ResultadoProcedimientoVM resultado = new ResultadoProcedimientoVM();
            try
            {
                using (var bdConn = new OracleConnection(cadenaConexion))
                {
                    using (var bdCmd = new OracleCommand("PKG_TRAMITE_SAT.SP_REGISTRAR_TRAMITE_SAT", bdConn))
                    {
                        bdCmd.CommandType = CommandType.StoredProcedure;
                        bdCmd.Parameters.AddRange(ParametrosRegistroTramiteSAT(tramite));
                        bdConn.Open();
                        bdCmd.ExecuteNonQuery();
                        resultado.CodResultado = 1;
                        resultado.NomResultado = "Registro Correctamente";
                        resultado.CodAuxiliar = int.Parse(bdCmd.Parameters["P_ID_TRAMITE"].Value.ToString());
                        //bdConn.Close();
                        //bdConn.Dispose();
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

        #region actualizar nombres de archivo
        public ResultadoProcedimientoVM actualizarNombreArchivos(int idTramiteSAT, string nombresArchivouch, string nomarch_desist)
        {
            ResultadoProcedimientoVM resultado = new ResultadoProcedimientoVM();
            try
            {
                using (var bdConn = new OracleConnection(cadenaConexion))
                {
                    using (var bdCmd = new OracleCommand("PKG_TRAMITE_SAT.SP_ACTUALIZAR_NOMBRE_ARCHIVO", bdConn))
                    {
                        OracleParameter[] bdParameters = new OracleParameter[3];
                        bdParameters[0] = new OracleParameter("P_ID_TRAMITE_SAT", OracleDbType.Int32) { Value = idTramiteSAT };
                        bdParameters[1] = new OracleParameter("P_NOM_ARCH_VOUCH", OracleDbType.Varchar2) { Value = nombresArchivouch };
                        bdParameters[2] = new OracleParameter("P_NOM_ARCHIVO_DESIST", OracleDbType.Varchar2) { Value = nomarch_desist };

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
        private OracleParameter[] ParametrosRegistroTramiteSAT(TramiteSATVM tramite)
        {
            OracleParameter[] bdParameters = new OracleParameter[18];
            
            bdParameters[0] = new OracleParameter("P_ID_TRAMITE", OracleDbType.Int32) { Value = tramite.ID_TRAMITE };
            bdParameters[1] = new OracleParameter("P_NRO_ACTA_CONTROL", OracleDbType.Varchar2) { Value = tramite.NRO_ACTA_CONTROL };
            bdParameters[2] = new OracleParameter("P_FECHA_ACTA", OracleDbType.Varchar2) { Value = tramite.FECHA_ACTA };
            bdParameters[3] = new OracleParameter("P_DIAS_HABILES_ACTA", OracleDbType.Int32) { Value = tramite.DIAS_HABILES_ACTA };
            bdParameters[4] = new OracleParameter("P_ID_MULTA", OracleDbType.Varchar2) { Value = tramite.ID_MULTA };
            bdParameters[5] = new OracleParameter("P_PLACA", OracleDbType.Varchar2) { Value = tramite.PLACA };
            bdParameters[6] = new OracleParameter("P_NRO_RESOL_SANC", OracleDbType.Varchar2) { Value = tramite.NRO_RESOL_SANC };
            bdParameters[7] = new OracleParameter("P_MONTO_IN_RESOL", OracleDbType.Double) { Value = tramite.MONTO_IN_RESOL };
            bdParameters[8] = new OracleParameter("P_FECHA_RESOLUCION", OracleDbType.Varchar2) { Value = tramite.FECHA_RESOLUCION };
            bdParameters[9] = new OracleParameter("P_FLAG_PRESENTO_RECURSO", OracleDbType.Int32) { Value = tramite.FLAG_PRESENTO_RECURSO };
            bdParameters[10] = new OracleParameter("P_ID_BANCO", OracleDbType.Int32) { Value = tramite.ID_BANCO };
            bdParameters[11] = new OracleParameter("P_FECHA_PAGO", OracleDbType.Varchar2) { Value = tramite.FECHA_PAGO };
            bdParameters[12] = new OracleParameter("P_NRO_RECIBO", OracleDbType.Varchar2) { Value = tramite.NRO_RECIBO };
            bdParameters[13] = new OracleParameter("P_MONTO_CANCELADO", OracleDbType.Varchar2) { Value = tramite.MONTO_CANCELADO };
            bdParameters[14] = new OracleParameter("P_FECHA_REGISTRO", OracleDbType.Varchar2) { Value = DateTime.Now.ToString("dd/MM/yyyy") };
            bdParameters[15] = new OracleParameter("P_FECHA_HORA_REG", OracleDbType.Varchar2) { Value = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss")};
            bdParameters[16] = new OracleParameter("P_NRO_CUOTAS", OracleDbType.Int32) { Value = tramite.NRO_CUOTAS };
            bdParameters[17] = new OracleParameter("P_ID_TRAMITE_SAT", OracleDbType.Int32, direction: ParameterDirection.Output);
            
            return bdParameters;
        }        
        #endregion
    }
}
