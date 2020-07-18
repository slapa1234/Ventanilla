//using Oracle.DataAccess.Client;
using Oracle.ManagedDataAccess.Client;
using SisATU.Base;
using SisATU.Base.Enumeradores;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Datos
{
    public class ResolucionExpedienteDAL
    {
        OracleConnection bdConn;
        string cadenaConexion = string.Empty;
        #region Constructor
        public ResolucionExpedienteDAL(ref Object _bdConn)
        {
            _bdConn = Conexion.iniciar(ref bdConn, _bdConn);
            cadenaConexion = Configuracion.GetConectionSting("sConexionSISREGISTRO");
        }
        #endregion

        #region Crear Resolucion
        public ResultadoProcedimientoVM CrearResolucionExpediente(ResolucionExpedienteModelo resolucionExpediente)
        {
            ResultadoProcedimientoVM modelo = new ResultadoProcedimientoVM();
            try
            {
                using (var bdCmd = new OracleCommand("PKG_RESOLUCION.SP_INS_RESO_EXPE", bdConn))
                {
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(ParametrosCrearResolucionExpediente(resolucionExpediente));
                    bdCmd.ExecuteNonQuery();
                    //resolucion.ID_RECIBO = int.Parse(bdCmd.Parameters["P_RECIBO"].Value.ToString());

                    modelo.CodResultado = 1;
                    modelo.NomResultado = "Registro Correctamente";
                    //modelo.CodAuxiliar = recibo.ID_RECIBO;
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
        private OracleParameter[] ParametrosCrearResolucionExpediente(ResolucionExpedienteModelo resolucionExpediente)
        {
            OracleParameter[] bdParameters = new OracleParameter[11];
            bdParameters[0] = new OracleParameter("P_RESOLUCION", OracleDbType.Int32) { Value = resolucionExpediente.ID_RESOLUCION };
            bdParameters[1] = new OracleParameter("P_EXPEDIENTE", OracleDbType.Int32) { Value = resolucionExpediente.ID_EXPEDIENTE };
            bdParameters[2] = new OracleParameter("P_DESDE_FECHA", OracleDbType.Varchar2) { Value = resolucionExpediente.DESDE_FECHA };
            bdParameters[3] = new OracleParameter("P_HASTA_FECHA", OracleDbType.Varchar2) { Value = resolucionExpediente.HASTA_FECHA };
            bdParameters[4] = new OracleParameter("P_NUMERO_RESOLUCION", OracleDbType.Varchar2) { Value = resolucionExpediente.NUMERO_RESOLUCION };
            bdParameters[5] = new OracleParameter("P_ESTADO", OracleDbType.Int32) { Value = EnumEstado.Activo.ValorEntero() };
            bdParameters[6] = new OracleParameter("P_USUARIO_REG", OracleDbType.Varchar2) { Value = resolucionExpediente.USUARIO_REG };
            bdParameters[7] = new OracleParameter("P_RUC", OracleDbType.Varchar2) { Value = resolucionExpediente.RUC };
            bdParameters[8] = new OracleParameter("P_ID_PERSONA", OracleDbType.Int32) { Value = resolucionExpediente.ID_TIPO_PERSONA };
            bdParameters[9] = new OracleParameter("P_ID_PROCEDIMIENTO", OracleDbType.Int32) { Value = resolucionExpediente.ID_PROCEDIMIENTO };
            bdParameters[10] = new OracleParameter("P_RESOLUCION_EXPEDIENTE", OracleDbType.Int32, direction: ParameterDirection.Output);
            return bdParameters;
        }
        #endregion
    }
}
