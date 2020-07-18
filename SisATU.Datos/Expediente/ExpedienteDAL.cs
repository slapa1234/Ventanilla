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
    public class ExpedienteDAL
    {
        OracleConnection bdConn;
        string cadenaConexion = string.Empty;
        #region Constructor
        public ExpedienteDAL(ref Object _bdConn)
        {
            _bdConn = Conexion.iniciar(ref bdConn, _bdConn);
            cadenaConexion = Configuracion.GetConectionSting("sConexionSISREGISTRO");
        }
        #endregion

        #region Crear Expediente
        public ResultadoProcedimientoVM CrearExpediente(ExpedienteModelo expediente)
        {
            ResultadoProcedimientoVM modelo = new ResultadoProcedimientoVM();
            try
            {

                using (var bdCmd = new OracleCommand("PKG_EXPEDIENTE.SP_INSERTAR_EXPEDIENTE", bdConn))
                {
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(ParametrosCrearExpediente(expediente));
                    bdCmd.ExecuteNonQuery();
                    expediente.ID_EXPEDIENTE = int.Parse(bdCmd.Parameters["P_IDEXPEDIENTE"].Value.ToString());

                    modelo.CodResultado = 1;
                    modelo.NomResultado = "Registro Correctamente";
                    modelo.CodAuxiliar = expediente.ID_EXPEDIENTE;
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

        #region Parametros Crear Expediente
        private OracleParameter[] ParametrosCrearExpediente(ExpedienteModelo expediente)
        {
            OracleParameter[] bdParameters = new OracleParameter[14];
            bdParameters[0] = new OracleParameter("P_IDDOC", OracleDbType.Int32) { Value = expediente.IDDOC };
            bdParameters[1] = new OracleParameter("P_NUMERO_SID", OracleDbType.Varchar2) { Value = expediente.NUMERO_SID };
            bdParameters[2] = new OracleParameter("P_NUMERO_ANIO", OracleDbType.Varchar2) { Value = expediente.NUMERO_ANIO };
            bdParameters[3] = new OracleParameter("P_PROCEDIMIENTO", OracleDbType.Int32) { Value = expediente.ID_PROCEDIMIENTO };
            bdParameters[4] = new OracleParameter("P_MODALIDAD_SERVICIO", OracleDbType.Int32) { Value = expediente.ID_MODALIDAD_SERVICIO };
            bdParameters[5] = new OracleParameter("P_SOLICITUD", OracleDbType.Int32) { Value = expediente.ID_SOLICITUD };
            bdParameters[6] = new OracleParameter("P_NUMERO_SOLICITANTE", OracleDbType.Varchar2) { Value = expediente.NUMERO_SOLICITANTE };
            bdParameters[7] = new OracleParameter("P_NUMERO_RECURRENTE", OracleDbType.Varchar2) { Value = expediente.NUMERO_RECURRENTE };
            bdParameters[8] = new OracleParameter("P_IS_PADRE_EXPEDIENT", OracleDbType.Int32) { Value = expediente.IS_PADRE_EXPEDIENTE };
            bdParameters[9] = new OracleParameter("ID_EXPEDIENTE_PADRE", OracleDbType.Int32) { Value = expediente.ID_EXPEDIENTE_PADRE };
            bdParameters[10] = new OracleParameter("P_ESTADO", OracleDbType.Int32) { Value = EnumEstado.Activo.ValorEntero() };
            bdParameters[11] = new OracleParameter("P_USUARIO_REG", OracleDbType.Varchar2) { Value = expediente.USUARIO_REG };
            bdParameters[12] = new OracleParameter("P_VEHICULO", OracleDbType.Varchar2) { Value = expediente.ID_VEHICULO };
            bdParameters[13] = new OracleParameter("P_IDEXPEDIENTE", OracleDbType.Int32, direction: ParameterDirection.Output);
            return bdParameters;
        }
        #endregion

        #region CREAR EXPEDIENTE ACUMULADOR
        public ResultadoProcedimientoVM CrearExpedienteAcumulador(int ID_PROCEDIMIENTO_P, int ID_PROCEDIMIENTO_H)
        {
            ResultadoProcedimientoVM modelo = new ResultadoProcedimientoVM();
            try
            {
                using (var bdConn = new OracleConnection(cadenaConexion))
                {
                    using (var bdCmd = new OracleCommand("PKG_EXPEDIENTE.SP_INSERTAR_ACUMULADOR", bdConn))
                    {
                        bdCmd.CommandType = CommandType.StoredProcedure;
                        bdCmd.Parameters.AddRange(ParametrosCrearExpediente(ID_PROCEDIMIENTO_P, ID_PROCEDIMIENTO_H));
                        bdConn.Open();
                        bdCmd.ExecuteNonQuery();
                        modelo.CodResultado = 1;
                        modelo.NomResultado = "Registro Correctamente";
                        modelo.CodAuxiliar = int.Parse(bdCmd.Parameters["P_ACUMULADOR_EXP"].Value.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                modelo.CodResultado = 1;
                modelo.NomResultado = ex.Message;
            }
            return modelo;
        }
        #endregion

        #region Parametros 
        private OracleParameter[] ParametrosCrearExpediente(int ID_PROCEDIMIENTO_P, int ID_PROCEDIMIENTO_H)
        {
            OracleParameter[] bdParameters = new OracleParameter[11];
            bdParameters[0] = new OracleParameter("P_IDEXPEDIENTE_PADRE", OracleDbType.Int32) { Value = ID_PROCEDIMIENTO_P };
            bdParameters[1] = new OracleParameter("P_IDEXPEDIENTE_HIJO", OracleDbType.Varchar2) { Value = ID_PROCEDIMIENTO_H };
            bdParameters[2] = new OracleParameter("P_ESTADO", OracleDbType.Varchar2) { Value = EnumEstado.Activo.ValorEntero() };
            bdParameters[3] = new OracleParameter("P_USUARIO_REG", OracleDbType.Int32) { Value = "Joao" };
            bdParameters[4] = new OracleParameter("P_ACUMULADOR_EXP", OracleDbType.Int32, direction: ParameterDirection.Output);
            return bdParameters;
        }
        #endregion

        #region Guardar Expediente Cita
        public ResultadoProcedimientoVM CrearExpedienteCita(ExpedienteModelo expediente)
        {
            ResultadoProcedimientoVM modelo = new ResultadoProcedimientoVM();
            try
            {

                using (var bdCmd = new OracleCommand("PKG_EXPEDIENTE.SP_INSERTAR_EXPEDIENTE_CITA", bdConn))
                {
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(ParametrosCrearExpedienteCita(expediente));
                   bdCmd.ExecuteNonQuery();
                    expediente.ID_EXPEDIENTE = int.Parse(bdCmd.Parameters["P_IDEXPEDIENTE"].Value.ToString());

                    modelo.CodResultado = 1;
                    modelo.NomResultado = "Registro Correctamente";
                    modelo.CodAuxiliar = expediente.ID_EXPEDIENTE;
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

        #region Parametros 
        private OracleParameter[] ParametrosCrearExpedienteCita(ExpedienteModelo expediente)
        {
            OracleParameter[] bdParameters = new OracleParameter[15];
            bdParameters[0] = new OracleParameter("P_IDDOC", OracleDbType.Int32) { Value = expediente.IDDOC };
            bdParameters[1] = new OracleParameter("P_NUMERO_SID", OracleDbType.Varchar2) { Value = expediente.NUMERO_SID };
            bdParameters[2] = new OracleParameter("P_NUMERO_ANIO", OracleDbType.Varchar2) { Value = expediente.NUMERO_ANIO };
            bdParameters[3] = new OracleParameter("P_PROCEDIMIENTO", OracleDbType.Int32) { Value = expediente.ID_PROCEDIMIENTO };
            bdParameters[4] = new OracleParameter("P_MODALIDAD_SERVICIO", OracleDbType.Int32) { Value = expediente.ID_MODALIDAD_SERVICIO };
            bdParameters[5] = new OracleParameter("P_SOLICITUD", OracleDbType.Int32) { Value = expediente.ID_SOLICITUD };
            bdParameters[6] = new OracleParameter("P_NUMERO_SOLICITANTE", OracleDbType.Varchar2) { Value = expediente.NUMERO_SOLICITANTE };
            bdParameters[7] = new OracleParameter("P_NUMERO_RECURRENTE", OracleDbType.Varchar2) { Value = expediente.NUMERO_RECURRENTE };
            bdParameters[8] = new OracleParameter("P_IS_PADRE_EXPEDIENT", OracleDbType.Int32) { Value = expediente.IS_PADRE_EXPEDIENTE };
            bdParameters[9] = new OracleParameter("ID_EXPEDIENTE_PADRE", OracleDbType.Int32) { Value = expediente.ID_EXPEDIENTE_PADRE };
            bdParameters[10] = new OracleParameter("P_ESTADO", OracleDbType.Int32) { Value = EnumEstado.Activo.ValorEntero() };
            bdParameters[11] = new OracleParameter("P_USUARIO_REG", OracleDbType.Varchar2) { Value = expediente.USUARIO_REG };
            bdParameters[12] = new OracleParameter("P_FECHA_REG", OracleDbType.Varchar2) { Value = expediente.FECHA_REG };
            bdParameters[13] = new OracleParameter("P_ASUNTO", OracleDbType.Varchar2) { Value = expediente.ASUNTO_NO_TUPA };
            bdParameters[14] = new OracleParameter("P_IDEXPEDIENTE", OracleDbType.Int32, direction: ParameterDirection.Output);
            return bdParameters;
        }
        #endregion

        #region Parametros ConsultarCita
        private OracleParameter[] ParametrosConsultCita(string idExpediente)
        {
            OracleParameter[] bdParameters = new OracleParameter[2];
            bdParameters[0] = new OracleParameter("P_IDEXPE", OracleDbType.Int32) { Value = idExpediente };
            bdParameters[1] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            return bdParameters;
        }

        #endregion

        #region Consultar Cita
        public Expediente_CitaVM getDatosConsulCita(string idExpediente)
        {
            Expediente_CitaVM resultado = new Expediente_CitaVM();
            try
            {
                using (var bdConn = new OracleConnection(cadenaConexion))
                {
                    using (var bdCmd = new OracleCommand("PKG_EXPEDIENTE.SP_CONSULT_CITA", bdConn))
                    {
                        bdCmd.CommandType = CommandType.StoredProcedure;
                        bdCmd.Parameters.AddRange(ParametrosConsultCita(idExpediente));
                        bdConn.Open();
                        using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                        {
                            if (bdRd.HasRows)
                            {
                                bdRd.Read();
                                resultado = new Expediente_CitaVM();
                                if (!DBNull.Value.Equals(bdRd["NUMERO_SID"])) { resultado.NUMERO_SID = (bdRd["NUMERO_SID"]).ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["NUMERO_ANIO"])) { resultado.NUMERO_ANIO = (bdRd["NUMERO_ANIO"]).ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["FECHA_ATENCION"])) { resultado.FECHA_CITA = (bdRd["FECHA_ATENCION"]).ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["ADMINISTRADO"])) { resultado.ADMINISTRADO = (bdRd["ADMINISTRADO"]).ValorCadena(); }
                              

                                return resultado;
                            }
                        }
                    }
                }
                return resultado;
            }
            catch (Exception)
            {

            }

            return resultado;
        }
        #endregion


    }
}
