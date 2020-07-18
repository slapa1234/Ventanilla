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
    public class ProcedimientoDAL
    {
        string cadenaConexion = string.Empty;

        #region Constructor
        public ProcedimientoDAL()
        {
            cadenaConexion = Configuracion.GetConectionSting("sConexionSISREGISTRO");
        }
        #endregion

        #region Combo Procedimiento por Modalidad
        public List<ComboProcedimientoVM> ComboProcedimientoXModalidad(int ID_MODALIDAD_SERVICIO)
        {
            List<ComboProcedimientoVM> resultado = new List<ComboProcedimientoVM>();
            try
            {
                using (var bdConn = new OracleConnection(cadenaConexion))
                {
                    using (var bdCmd = new OracleCommand("PKG_PROCEDIMIENTO.SP_LISTA", bdConn))
                    {
                        bdCmd.CommandType = CommandType.StoredProcedure;
                        bdCmd.Parameters.AddRange(ParametrosComboProcedimientoXModalidad(ID_MODALIDAD_SERVICIO));
                        bdConn.Open();
                        using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                        {
                            if (bdRd.HasRows)
                            {
                                while (bdRd.Read())
                                {
                                    var item = new ComboProcedimientoVM();
                                    if (!DBNull.Value.Equals(bdRd["ID_PROCEDIMIENTO"])) { item.ID_PROCEDIMIENTO = (bdRd["ID_PROCEDIMIENTO"]).ValorEntero(); }
                                    if (!DBNull.Value.Equals(bdRd["NOMBRE_PROCEDIMIENTO"])) { item.NOMBRE_PROCEDIMIENTO = (bdRd["NOMBRE_PROCEDIMIENTO"]).ValorCadena(); }
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

        #region parametros Combo Procedimiento
        private OracleParameter[] ParametrosComboProcedimientoXModalidad(int ID_MODALIDAD_SERVICIO)
        {
            OracleParameter[] bdParameters = new OracleParameter[2];
            bdParameters[0] = new OracleParameter("P_ID_MODALIDAD_SERVICIO", OracleDbType.Int32) { Value = ID_MODALIDAD_SERVICIO };
            bdParameters[1] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            return bdParameters;
        }
        #endregion

        #region Consultar Datos Procedimiento
        public ProcedimientoVM ConsultarDatosProcedimiento(int ID_PROCEDIMIENTO, int ID_TIPO_PERSONA)
        {
            ProcedimientoVM resultado = new ProcedimientoVM();
            try
            {
                using (var bdConn = new OracleConnection(cadenaConexion))
                {
                    using (var bdCmd = new OracleCommand("PKG_PROCEDIMIENTO.SP_DATOS_PROCEDIMIENTO", bdConn))
                    {
                        bdCmd.CommandType = CommandType.StoredProcedure;
                        bdCmd.Parameters.AddRange(ParametrosConsultarDatosProcedimiento(ID_PROCEDIMIENTO, ID_TIPO_PERSONA));
                        bdConn.Open();
                        using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                        {
                            if (bdRd.HasRows)
                            {
                                while (bdRd.Read())
                                {
                                    if (!DBNull.Value.Equals(bdRd["VALOR_PROCEDIMIENTO"])) { resultado.VALOR_PROCEDIMIENTO = (bdRd["VALOR_PROCEDIMIENTO"]).ValorEntero(); }
                                    if (!DBNull.Value.Equals(bdRd["MONTO"])) { resultado.MONTO = (bdRd["MONTO"]).ValorCadena(); }
                                    if (!DBNull.Value.Equals(bdRd["DETALLE_MODALIDAD"])) { resultado.DETALLE_MODALIDAD = (bdRd["DETALLE_MODALIDAD"]).ValorCadena(); }
                                    if (!DBNull.Value.Equals(bdRd["FLAG_AUTOMATIZACION"])) { resultado.FLAG_AUTOMATIZACION = (bdRd["FLAG_AUTOMATIZACION"]).ValorEntero(); }
                                    if (!DBNull.Value.Equals(bdRd["ID_PROCEDIMIENTO_PADRE"])) { resultado.ID_PROCEDIMIENTO_PADRE = (bdRd["ID_PROCEDIMIENTO_PADRE"]).ValorEntero(); }
                                    if (!DBNull.Value.Equals(bdRd["ID_PROCEDIMIENTO_HIJO"])) { resultado.ID_PROCEDIMIENTO_HIJO = (bdRd["ID_PROCEDIMIENTO_HIJO"]).ValorEntero(); }
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

        #region parametros Consultar Datos
        private OracleParameter[] ParametrosConsultarDatosProcedimiento(int ID_PROCEDIMIENTO, int ID_TIPO_PERSONA)
        {
            OracleParameter[] bdParameters = new OracleParameter[3];
            bdParameters[0] = new OracleParameter("ID_PROCEDIMIENTO", OracleDbType.Int32) { Value = ID_PROCEDIMIENTO };
            bdParameters[1] = new OracleParameter("P_ID_TIPO_PERSONA", OracleDbType.Int32) { Value = ID_TIPO_PERSONA };
            bdParameters[2] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            return bdParameters;
        }
        #endregion

        #region Consulta Procedimiento Hijo
        public List<ProcedimientoVM> ConsultarDatosProcedimientoPadre(int ID_PROCEDIMIENTO, int ID_TIPO_PERSONA)
        {
            List<ProcedimientoVM> resultado = new List<ProcedimientoVM>();
            try
            {
                using (var bdConn = new OracleConnection(cadenaConexion))
                {
                    using (var bdCmd = new OracleCommand("PKG_PROCEDIMIENTO.SP_PROCEDIMIENTO_PADRE", bdConn))
                    {
                        bdCmd.CommandType = CommandType.StoredProcedure;
                        bdCmd.Parameters.AddRange(ParametrosConsultarDatosProcedimientoPadre(ID_PROCEDIMIENTO, ID_TIPO_PERSONA));
                        bdConn.Open();
                        using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                        {
                            if (bdRd.HasRows)
                            {
                                while (bdRd.Read())
                                {
                                    var item = new ProcedimientoVM();
                                    if (!DBNull.Value.Equals(bdRd["ID_PROCEDIMIENTO_PADRE"])) { item.ID_PROCEDIMIENTO_PADRE = (bdRd["ID_PROCEDIMIENTO_PADRE"]).ValorEntero(); }
                                    if (!DBNull.Value.Equals(bdRd["ID_PROCEDIMIENTO_HIJO"])) { item.ID_PROCEDIMIENTO_HIJO = (bdRd["ID_PROCEDIMIENTO_HIJO"]).ValorEntero(); }
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

        #region 
        private OracleParameter[] ParametrosConsultarDatosProcedimientoPadre(int ID_PROCEDIMIENTO, int ID_TIPO_PERSONA)
        {
            OracleParameter[] bdParameters = new OracleParameter[3];
            bdParameters[0] = new OracleParameter("P_PRO_HIJO", OracleDbType.Int32) { Value = ID_PROCEDIMIENTO };
            bdParameters[1] = new OracleParameter("P_ID_PERSONA", OracleDbType.Int32) { Value = ID_TIPO_PERSONA };
            bdParameters[2] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            return bdParameters;
        }
        #endregion

        #region Consultar Procedimientos Hijos
        public List<ProcedimientoVM> ConsultarDatosProcedimientoHijo(int ID_PROCEDIMIENTO, int ID_TIPO_PERSONA)
        {
            List<ProcedimientoVM> resultado = new List<ProcedimientoVM>();
            try
            {
                using (var bdConn = new OracleConnection(cadenaConexion))
                {
                    using (var bdCmd = new OracleCommand("PKG_PROCEDIMIENTO.SP_GET_PROC_HIJOS", bdConn))
                    {
                        bdCmd.CommandType = CommandType.StoredProcedure;
                        bdCmd.Parameters.AddRange(ParametrosConsultarDatosProcedimientoHijo(ID_PROCEDIMIENTO, ID_TIPO_PERSONA));
                        bdConn.Open();
                        using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                        {
                            if (bdRd.HasRows)
                            {
                                while (bdRd.Read())
                                {
                                    var item = new ProcedimientoVM();
                                    if (!DBNull.Value.Equals(bdRd["ID_PROCEDIMIENTO_HIJO"])) { item.ID_PROCEDIMIENTO = (bdRd["ID_PROCEDIMIENTO_HIJO"]).ValorEntero(); }
                                    if (!DBNull.Value.Equals(bdRd["NOMBRE_PROCEDIMIENTO"])) { item.PROCEDIMIENTO_PROCEDENCIA = (bdRd["NOMBRE_PROCEDIMIENTO"]).ValorCadena(); }
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

        #region 
        private OracleParameter[] ParametrosConsultarDatosProcedimientoHijo(int ID_PROCEDIMIENTO, int ID_TIPO_PERSONA)
        {
            OracleParameter[] bdParameters = new OracleParameter[3];
            bdParameters[0] = new OracleParameter("P_PRO_PADRE", OracleDbType.Int32) { Value = ID_PROCEDIMIENTO };
            bdParameters[1] = new OracleParameter("P_ID_PERSONA", OracleDbType.Int32) { Value = ID_TIPO_PERSONA };
            bdParameters[2] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            return bdParameters;
        }

        #endregion 
    }
}
