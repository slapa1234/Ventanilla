//using Oracle.DataAccess.Client;
using Oracle.ManagedDataAccess.Client;
using SisATU.Base;
using SisATU.Base.Enumeradores;
using SisATU.Base.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Datos
{
    public class ReciboDAL
    {
        OracleConnection bdConn;
        string cadenaConexion = string.Empty;
        #region Constructor
        public ReciboDAL(ref Object _bdConn)
        {
            _bdConn = Conexion.iniciar(ref bdConn, _bdConn);
            cadenaConexion = Configuracion.GetConectionSting("sConexionSISREGISTRO");
        }
        #endregion

        #region Busca Recibo
        public ReciboVM BuscarRecibo(string NroRecibo)
        {
            ReciboVM resultado = new ReciboVM();
            try
            {
                using (var bdConn = new OracleConnection(cadenaConexion))
                {
                    using (var bdCmd = new OracleCommand("PKG_GTU_RECIBO.SP_BUS_RECIBO", bdConn))
                    {
                        bdCmd.CommandType = CommandType.StoredProcedure;
                        bdCmd.Parameters.AddRange(ParametrosBuscarRecibo(NroRecibo));
                        bdConn.Open();
                        using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                        {
                            if (bdRd.HasRows)
                            {
                                bdRd.Read();
                                resultado = new ReciboVM();
                                if (!DBNull.Value.Equals(bdRd["NRO_RECIBO"])) { resultado.NUMERO_RECIBO = (bdRd["NRO_RECIBO"]).ValorCadena(); }
                                //if (!DBNull.Value.Equals(bdRd["EXPEDIENTE"])) { resultado.ID_EXPEDIENTE = (bdRd["EXPEDIENTE"]).ValorEntero(); }
                                if (!DBNull.Value.Equals(bdRd["FECHA_EMISION"])) { resultado.FECHA_EMISION = (bdRd["FECHA_EMISION"]).ValorCadena(); }
                                return resultado;
                            }
                        }
                    }
                }
                return resultado;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Crear Recibo
        public ResultadoProcedimientoVM CrearRecibo(ReciboModelo recibo)
        {
            ResultadoProcedimientoVM modelo = new ResultadoProcedimientoVM();
            try
            {
                using (var bdCmd = new OracleCommand("PKG_GTU_RECIBO.SP_INSERTAR_RECIBO", bdConn))
                {
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(ParametrosCrearRecibo(recibo));
                    bdCmd.ExecuteNonQuery();
                    recibo.ID_RECIBO = int.Parse(bdCmd.Parameters["P_RECIBO"].Value.ToString());

                    modelo.CodResultado = 1;
                    modelo.NomResultado = "Registro Correctamente";
                    modelo.CodAuxiliar = recibo.ID_RECIBO;
                }
            }
            catch (Exception ex)
            {
                modelo.CodResultado = 0;
                modelo.NomResultado = ex.Message;
            }
            return modelo;
        }
        #endregion 

        #region Parametros Crear Recibo
        private OracleParameter[] ParametrosCrearRecibo(ReciboModelo recibo)
        {
            OracleParameter[] bdParameters = new OracleParameter[7];
            bdParameters[0] = new OracleParameter("P_ENTIDAD_BANCARIA", OracleDbType.Int32) { Value = recibo.ID_ENTIDAD_BANCARIA };
            bdParameters[1] = new OracleParameter("P_NUMERO_RECIBO", OracleDbType.Varchar2) { Value = recibo.NUMERO_RECIBO };
            bdParameters[2] = new OracleParameter("P_FECHA_EMISION", OracleDbType.Varchar2) { Value = recibo.FECHA_EMISION.ValorFechaCorta() };
            bdParameters[3] = new OracleParameter("P_ESTADO", OracleDbType.Int32) { Value = EnumEstado.Activo.ValorEntero() };
            bdParameters[4] = new OracleParameter("P_EXPEDIENTE", OracleDbType.Int32) { Value = recibo.ID_EXPEDIENTE };
            bdParameters[5] = new OracleParameter("P_USUARIO_REG", OracleDbType.Varchar2) { Value = recibo.USUARIO_REG };
            bdParameters[6] = new OracleParameter("P_RECIBO", OracleDbType.Int32, direction: ParameterDirection.Output);
            return bdParameters;
        }
        #endregion

        #region parametros Buscar Recibo
        private OracleParameter[] ParametrosBuscarRecibo(string NroRecibo)
        {
            OracleParameter[] bdParameters = new OracleParameter[2];
            bdParameters[0] = new OracleParameter("P_NRO_RECIBO", OracleDbType.Varchar2) { Value = NroRecibo };
            bdParameters[1] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            return bdParameters;
        }
        #endregion
    }
}
