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
    public class CredencialOperadorDAL
    {
        OracleConnection bdConn;
        string cadenaConexion = string.Empty;
        public CredencialOperadorDAL(ref Object _bdConn)
        {
            _bdConn = Conexion.iniciar(ref bdConn, _bdConn);
            this.cadenaConexion = Configuracion.GetConectionSting("sConexionSISREGISTRO");
        }
        #region Crear Operador
        public ResultadoProcedimientoVM CrearCredencialOperador(CredencialOperadorModelo credencialOperador)
        {
            ResultadoProcedimientoVM resultado = new ResultadoProcedimientoVM();
            try
            {
                //using (var bdConn = new OracleConnection(cadenaConexion))
                //{
                    using (var bdCmd = new OracleCommand("PKG_OPERADOR.SP_INS_CREDENCIAL", bdConn))
                    {
                        bdCmd.CommandType = CommandType.StoredProcedure;
                        bdCmd.Parameters.AddRange(ParametrosCrearCredencialOperador(credencialOperador));
                        //bdConn.Open();
                        bdCmd.ExecuteNonQuery();
                        credencialOperador.ID_CREDENCIAL_OPERADOR = int.Parse(bdCmd.Parameters["P_CREDENCIAL_OPERADOR"].Value.ToString());
                        resultado.CodAuxiliar = credencialOperador.ID_CREDENCIAL_OPERADOR;
                        resultado.CodResultado = 1;
                        resultado.NomResultado = "Registro Correctamente";
                    }
                //}
            }
            catch (Exception ex)
            {
                resultado.CodResultado = 0;
                resultado.NomResultado = ex.Message;
            }

            return resultado;
        }
        #endregion

        #region Parametros Crear Recibo
        private OracleParameter[] ParametrosCrearCredencialOperador(CredencialOperadorModelo credencialOperador)
        {
            OracleParameter[] bdParameters = new OracleParameter[13];
            bdParameters[0] = new OracleParameter("P_OPERADOR", OracleDbType.Int32) { Value = credencialOperador.ID_OPERADOR };
            bdParameters[1] = new OracleParameter("P_EXPEDIENTE", OracleDbType.Int32) { Value = credencialOperador.ID_EXPEDIENTE };
            bdParameters[2] = new OracleParameter("P_FECHA_IMPRESION", OracleDbType.Varchar2) { Value = credencialOperador.FECHA_IMPRESION };
            bdParameters[3] = new OracleParameter("P_FECHA_VENCIMIENTO", OracleDbType.Varchar2) { Value = credencialOperador.FECHA_VENCIMIENTO };
            bdParameters[4] = new OracleParameter("P_FECHA_ENTREGA", OracleDbType.Varchar2) { Value = credencialOperador.FECHA_ENTREGA };
            bdParameters[5] = new OracleParameter("P_ANIO_CREDENCIAL", OracleDbType.Varchar2) { Value = credencialOperador.ANIO_CREDENCIAL };
            bdParameters[6] = new OracleParameter("P_TIPO_DOCUMENTO", OracleDbType.Int32) { Value = credencialOperador.ID_TIPO_DOCUMENTO_OPERADOR };
            bdParameters[7] = new OracleParameter("P_DOCUMENTO", OracleDbType.Varchar2) { Value = credencialOperador.NUMERO_DOCUMENTO_OPERADOR };
            bdParameters[8] = new OracleParameter("P_FECHA_INICIO", OracleDbType.Varchar2) { Value = credencialOperador.FECHA_INICIO };
            bdParameters[9] = new OracleParameter("P_ESTADO", OracleDbType.Int32) { Value = EnumEstado.Activo.ValorEntero() };
            bdParameters[10] = new OracleParameter("P_USUARIO_REG", OracleDbType.Varchar2) { Value = credencialOperador.USUARIO_REG };
            bdParameters[11] = new OracleParameter("P_TIPO", OracleDbType.Int32) { Value = credencialOperador.ID_TIPO_CREDENCIAL };
            bdParameters[12] = new OracleParameter("P_CREDENCIAL_OPERADOR", OracleDbType.Int32, direction: ParameterDirection.Output);
            return bdParameters;
        }
        #endregion
    }
}
