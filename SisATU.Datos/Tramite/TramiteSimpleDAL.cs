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
    public class TramiteSimpleDAL
    {
        OracleConnection bdConn;
        string cadenaConexion = string.Empty;

        #region Constructor
        public TramiteSimpleDAL(ref Object _bdConn)
        {
            _bdConn = Conexion.iniciar(ref bdConn, _bdConn);
            cadenaConexion = Configuracion.GetConectionSting("sConexionSISREGISTRO");
        }
        #endregion

        #region registrar 

        public ResultadoProcedimientoVM registrarTramite(TramiteSimpleVM tramite)
        {
            ResultadoProcedimientoVM resultado = new ResultadoProcedimientoVM();
            try
            {
                using (var bdConn = new OracleConnection(cadenaConexion))
                {
                    using (var bdCmd = new OracleCommand("PKG_TRAMITE.SP_REGISTRAR_TRAMITE", bdConn))
                    {
                        bdCmd.CommandType = CommandType.StoredProcedure;
                        bdCmd.Parameters.AddRange(ParametrosRegistroTramite(tramite));
                        bdConn.Open();
                        bdCmd.ExecuteNonQuery();
                        resultado.CodResultado = 1;
                        resultado.NomResultado = "Registro Correctamente";
                        resultado.CodAuxiliar = int.Parse(bdCmd.Parameters["P_ID_TRAMITE"].Value.ToString());
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
        #region Parametro registro de tramite simple
        private OracleParameter[] ParametrosRegistroTramite(TramiteSimpleVM tramite)
        {
            OracleParameter[] bdParameters = new OracleParameter[19];
            
            bdParameters[0] = new OracleParameter("P_ID_MODALIDAD", OracleDbType.Int32) { Value = tramite.IDMODALIDAD };
            bdParameters[1] = new OracleParameter("P_ID_PROCEDIMIENTO", OracleDbType.Int32) { Value = tramite.IDPROCEDIMIENTO };
            bdParameters[2] = new OracleParameter("P_TIPO_DOCUMENTO", OracleDbType.Int32) { Value = tramite.IDTIPODOCUMENTO };
            bdParameters[3] = new OracleParameter("P_NRODOCUMENTO", OracleDbType.Varchar2) { Value = tramite.NRODOCUMENTO };
            bdParameters[4] = new OracleParameter("P_NOMBRES", OracleDbType.Varchar2) { Value = tramite.NOMBRES };
            bdParameters[5] = new OracleParameter("P_APEPAT", OracleDbType.Varchar2) { Value = tramite.APEPAT };
            bdParameters[6] = new OracleParameter("P_APEMAT", OracleDbType.Varchar2) { Value = tramite.APEMAT };
            bdParameters[7] = new OracleParameter("P_NRORECIBOPAGO", OracleDbType.Varchar2) { Value = tramite.NRORECIBOPAGO };
            bdParameters[8] = new OracleParameter("P_CORREOELECTRONICO", OracleDbType.Varchar2) { Value = tramite.CORREOELECTRONICO };
            bdParameters[9] = new OracleParameter("P_FECHACREACION", OracleDbType.Varchar2) { Value = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") };
            bdParameters[10] = new OracleParameter("P_ID_TIPO_PERSONA", OracleDbType.Int32) { Value = tramite.ID_TIPO_PERSONA };
            bdParameters[11] = new OracleParameter("P_RUC", OracleDbType.Varchar2) { Value = tramite.RUC };
            bdParameters[12] = new OracleParameter("P_AUTORIZA_EMAIL", OracleDbType.Int32) { Value = tramite.AUTORIZA_EMAIL };
            bdParameters[13] = new OracleParameter("P_NRO_TELEF", OracleDbType.Varchar2) { Value = tramite.NRO_TELEF };
            bdParameters[14] = new OracleParameter("P_DIRECCION", OracleDbType.Varchar2) { Value = tramite.DIRECCION };
            bdParameters[15] = new OracleParameter("P_PLACA", OracleDbType.Varchar2) { Value = tramite.PLACA };
            bdParameters[16] = new OracleParameter("P_IDBANCO", OracleDbType.Int32) { Value = tramite.IDBANCO };
            bdParameters[17] = new OracleParameter("P_FECHA_PAGO", OracleDbType.Varchar2) { Value = tramite.FECHA_PAGO };
            bdParameters[18] = new OracleParameter("P_ID_TRAMITE", OracleDbType.Int32, direction: ParameterDirection.Output);
            return bdParameters;
        }
        #endregion

        
        #region verifica Boleto
        public ResultadoProcedimientoVM busqueda_recibo(string nroRecibo)
        {
            ResultadoProcedimientoVM resultado = new ResultadoProcedimientoVM();

            try
            {
                using (var bdConn = new OracleConnection(cadenaConexion))
                {
                    using (var bdCmd = new OracleCommand("PKG_TRAMITE.SP_BUSQUEDA_NRORECIBOPAGO", bdConn))
                    {
                        bdCmd.CommandType = CommandType.StoredProcedure;
                        bdCmd.Parameters.AddRange(parametroBuscarRecibo(nroRecibo));
                        bdConn.Open();
                        using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                        {
                            if (bdRd.HasRows)
                            {
                                resultado.CodResultado = 1;
                                resultado.NomResultado = "El número del recibo ya fue registrado";
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

        #region Parametro Buscar Recibo
        public OracleParameter[] parametroBuscarRecibo(string nroRecibo)
        {
            OracleParameter[] bdParameters = new OracleParameter[2];
            bdParameters[0] = new OracleParameter("P_NRORECIBOPAGO", OracleDbType.Varchar2) { Value = nroRecibo };
            bdParameters[1] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            return bdParameters;
        }
        #endregion
    }
}
