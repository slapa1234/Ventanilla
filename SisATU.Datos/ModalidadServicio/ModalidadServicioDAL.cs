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
    public class ModalidadServicioDAL
    {
        string cadenaConexion = string.Empty;

        #region Constructor
        public ModalidadServicioDAL()
        {
            cadenaConexion = Configuracion.GetConectionSting("sConexionSISREGISTRO");
        }
        #endregion

        #region Combo Modalida Servicio
        public List<ComboModalidadServicioVM> ComboModalidadServicio()
        {
            List<ComboModalidadServicioVM> resultado = new List<ComboModalidadServicioVM>();
            try
            {
                using (var bdConn = new OracleConnection(cadenaConexion))
                {
                    using (var bdCmd = new OracleCommand("PKG_MODALIDAD_SERVICIO.SP_LISTA", bdConn))
                    {
                        bdCmd.CommandType = CommandType.StoredProcedure;
                        bdCmd.Parameters.AddRange(ParametrosComboModalidadServicio());
                        bdConn.Open();
                        using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                        {
                            if (bdRd.HasRows)
                            {
                                while (bdRd.Read())
                                {
                                    var item = new ComboModalidadServicioVM();
                                    if (!DBNull.Value.Equals(bdRd["ID_MODALIDAD_SERVICIO"])) { item.ID_MODALIDAD_SERVICIO = (bdRd["ID_MODALIDAD_SERVICIO"]).ValorEntero(); }
                                    if (!DBNull.Value.Equals(bdRd["NOMBRE"])) { item.NOMBRE = (bdRd["NOMBRE"]).ValorCadena(); }
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
        #region parametros
        private OracleParameter[] ParametrosComboModalidadServicio()
        {
            OracleParameter[] bdParameters = new OracleParameter[1];
            bdParameters[0] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            return bdParameters;
        }
        #endregion

        #region lista modadalidad by tipo de persona
        public List<ComboModalidadServicioVM> getModalidadByTipoPersona(int idTipoPersona)
        {
            List<ComboModalidadServicioVM> resultado = new List<ComboModalidadServicioVM>();
            try
            {
                using (var bdConn = new OracleConnection(cadenaConexion))
                {
                    using (var bdCmd = new OracleCommand("PKG_MODALIDAD_SERVICIO.SP_LIS_MODALIDAD_VENT", bdConn))
                    {
                        bdCmd.CommandType = CommandType.StoredProcedure;
                        bdCmd.Parameters.AddRange(ParametrosModalidadByTipoPersona(idTipoPersona));
                        bdConn.Open();
                        using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                        {
                            if (bdRd.HasRows)
                            {
                                while (bdRd.Read())
                                {
                                    var item = new ComboModalidadServicioVM();
                                    if (!DBNull.Value.Equals(bdRd["ID_MODALIDAD_SERVICIO"])) { item.ID_MODALIDAD_SERVICIO = (bdRd["ID_MODALIDAD_SERVICIO"]).ValorEntero(); }
                                    if (!DBNull.Value.Equals(bdRd["NOMBRE"])) { item.NOMBRE = (bdRd["NOMBRE"]).ValorCadena(); }
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
        #region parametros  modadalidad by tipo de persona
        private OracleParameter[] ParametrosModalidadByTipoPersona(int idTipoPersona)
        {
            OracleParameter[] bdParameters = new OracleParameter[2];
            bdParameters[0] = new OracleParameter("P_TIPO_PERSONA", OracleDbType.Int32) { Value = idTipoPersona };
            bdParameters[1] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            return bdParameters;
        }
        #endregion


        #region Lista procedimientos by filtros
        public List<ComboProcedimientoVM> getProcedimientosByFiltro(int idTipoPersona, int idModalidad, int idTipoTramite)
        {
            List<ComboProcedimientoVM> resultado = new List<ComboProcedimientoVM>();
            try
            {
                using (var bdConn = new OracleConnection(cadenaConexion))
                {
                    using (var bdCmd = new OracleCommand("PKG_MODALIDAD_SERVICIO.SP_LIS_TPOPROC", bdConn))
                    {
                        bdCmd.CommandType = CommandType.StoredProcedure;
                        bdCmd.Parameters.AddRange(ParametrosProcedimientoByFiltro(idTipoPersona, idModalidad, idTipoTramite));
                        bdConn.Open();
                        using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                        {
                            if (bdRd.HasRows)
                            {
                                while (bdRd.Read())
                                {
                                    var item = new ComboProcedimientoVM();

                                    if (!DBNull.Value.Equals(bdRd["ID_PROCEDIMIENTO"])) { item.ID_PROCEDIMIENTO = (bdRd["ID_PROCEDIMIENTO"]).ValorEntero(); }
                                    //if (!DBNull.Value.Equals(bdRd["DETALLE_MODALIDAD"])) { item.DETALLE_MODALIDAD = (bdRd["DETALLE_MODALIDAD"]).ValorCadena(); }
                                    if (!DBNull.Value.Equals(bdRd["NOMBRE_PROCEDIMIENTO"])) { item.NOMBRE_PROCEDIMIENTO = (bdRd["NOMBRE_PROCEDIMIENTO"]).ValorCadena(); }
                                    if (!DBNull.Value.Equals(bdRd["MONTO"])) { item.MONTO = (bdRd["MONTO"]).ValorCadena(); }
                                    if (!DBNull.Value.Equals(bdRd["DOCUMENTACION_EVALUACION"])) { item.DOCUMENTACION_EVALUACION = (bdRd["DOCUMENTACION_EVALUACION"]).ValorCadena(); }
                                    if (!DBNull.Value.Equals(bdRd["PLATAFORMA"])) { item.PLATAFORMA = (bdRd["PLATAFORMA"]).ValorEntero(); }

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

        #region Parametros ListaProcedimiento by filtro
        private OracleParameter[] ParametrosProcedimientoByFiltro(int idTipoPersona, int idModalidad, int idTipoTramite)
        {
            OracleParameter[] bdParameters = new OracleParameter[4];

            bdParameters[0] = new OracleParameter("P_TIPO_PERSONA", OracleDbType.Int32) { Value = idTipoPersona };
            bdParameters[1] = new OracleParameter("P_ID_MODALIDAD", OracleDbType.Int32) { Value = idModalidad };
            bdParameters[2] = new OracleParameter("P_TIPO_TRAMITE", OracleDbType.Int32) { Value = idTipoTramite };
            bdParameters[3] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            return bdParameters;
        }
        #endregion
    }
}
