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
    public class BackOfficeDAL
    {
        string cadenaConexion = string.Empty;

        #region Constructor
        public BackOfficeDAL()
        {
            cadenaConexion = Configuracion.GetConectionSting("sConexionSISREGISTRO");
        }
        #endregion
        #region Paginado
        public async Task<DTListaExpedienteVM> BuscarPag(string expediente, string NroDocumento, string persona, int id_modalidad_servicio, string fechaRegistro, string orden, int pagina = 1, int registros = 50)
        {
            try
            {
                DTListaExpedienteVM resultado = new DTListaExpedienteVM();

                using (var bdConn = new OracleConnection(this.cadenaConexion))
                {
                    using (var bdCmd = new OracleCommand("PKG_BACKOFFICE.SP_BUSCAR_PAG", bdConn))
                    {
                        bdCmd.CommandType = CommandType.StoredProcedure;
                        bdCmd.Parameters.AddRange(ParametroBackOffice(expediente, NroDocumento, persona, id_modalidad_servicio, fechaRegistro, null, pagina, registros));
                        bdConn.Open();
                        using (var bdRd = await bdCmd.ExecuteReaderAsync(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                        {
                            if (bdRd.HasRows)
                            {
                                resultado.TotalPagina = Convert.ToInt32(bdCmd.Parameters["P_TOTPAG"].Value.ToString());
                                resultado.TotalRegistro = Convert.ToInt32(bdCmd.Parameters["P_TOTREG"].Value.ToString());

                                while (bdRd.Read())
                                {
                                    var item = new ListaExpediente();
                                    if (!DBNull.Value.Equals(bdRd["TRAMITE"])) { item.TRAMITE = Convert.ToString(bdRd["TRAMITE"]); }
                                    if (!DBNull.Value.Equals(bdRd["FECHA_REG"])) { item.FECHA_REG = Convert.ToString(bdRd["FECHA_REG"]); }
                                    if (!DBNull.Value.Equals(bdRd["NUMERO_DOCUMENTO"])) { item.NUMERO_DOCUMENTO = Convert.ToString(bdRd["NUMERO_DOCUMENTO"]); }
                                    if (!DBNull.Value.Equals(bdRd["PERSONA"])) { item.PERSONA = Convert.ToString(bdRd["PERSONA"]); }
                                    if (!DBNull.Value.Equals(bdRd["PARNOM"])) { item.PARNOM = Convert.ToString(bdRd["PARNOM"]); }
                                    if (!DBNull.Value.Equals(bdRd["NOMBRE_MODALIDAD_SERVICIO"])) { item.MODALIDAD_SERVICIO = Convert.ToString(bdRd["NOMBRE_MODALIDAD_SERVICIO"]); }
                                    if (!DBNull.Value.Equals(bdRd["NOMBRE_PROCEDIMIENTO"])) { item.NOMBRE_PROCEDIMIENTO = Convert.ToString(bdRd["NOMBRE_PROCEDIMIENTO"]); }
                                    if (!DBNull.Value.Equals(bdRd["ID_EXPEDIENTE"])) { item.ID_EXPEDIENTE = Convert.ToInt32(bdRd["ID_EXPEDIENTE"]); }
                                    if (!DBNull.Value.Equals(bdRd["NROREG"])) { item.NROREG = Convert.ToInt32(bdRd["NROREG"]); }
                                    if (!DBNull.Value.Equals(bdRd["ID_PROCEDIMIENTO"])) { item.ID_PROCEDIMIENTO = Convert.ToInt32(bdRd["ID_PROCEDIMIENTO"]); }
                                    if (!DBNull.Value.Equals(bdRd["ID_MODALIDAD_SERVICIO"])) { item.ID_MODALIDAD_SERVICIO = Convert.ToInt32(bdRd["ID_MODALIDAD_SERVICIO"]); }
                                    if (!DBNull.Value.Equals(bdRd["IDDOC"])) { item.IDDOC = Convert.ToInt32(bdRd["IDDOC"]); }
                                    if (!DBNull.Value.Equals(bdRd["ESTADO_SISTEMA"])) { item.ESTADO = Convert.ToString(bdRd["ESTADO_SISTEMA"]); }
                                    resultado.ListaExpediente.Add(item);
                                }
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

        #region Parametros Back Office
        public OracleParameter[] ParametroBackOffice(string expediente, string NroDocumento, string persona, int id_modalidad_servicio, string fechaRegistro, string orden, int pagina, int registros)
        {
            OracleParameter[] bdParameters = new OracleParameter[12];

            bdParameters[0] = new OracleParameter("P_EXPEDIENTE", OracleDbType.Varchar2) { Value = expediente };
            bdParameters[1] = new OracleParameter("P_CONNUMDOC", OracleDbType.Varchar2) { Value = NroDocumento };
            bdParameters[2] = new OracleParameter("P_PERSONA", OracleDbType.Varchar2) { Value = persona };
            bdParameters[3] = new OracleParameter("P_SOLICITUD", OracleDbType.Int32) { Value = null };
            bdParameters[4] = new OracleParameter("P_MODALIDAD", OracleDbType.Int32) { Value = id_modalidad_servicio };
            bdParameters[5] = new OracleParameter("P_FECHA", OracleDbType.Varchar2) { Value = fechaRegistro };
            bdParameters[6] = new OracleParameter("P_ORDEN", OracleDbType.Varchar2) { Value = orden };
            bdParameters[7] = new OracleParameter("P_NUMPAG", OracleDbType.Int32) { Value = pagina };
            bdParameters[8] = new OracleParameter("P_NUMREG", OracleDbType.Int32) { Value = registros };
            bdParameters[9] = new OracleParameter("P_TOTPAG", OracleDbType.Int32, direction: ParameterDirection.Output);
            bdParameters[10] = new OracleParameter("P_TOTREG", OracleDbType.Int32, direction: ParameterDirection.Output);
            bdParameters[11] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            return bdParameters;
        }
        #endregion
        #region Parametros Cabecera
        public OracleParameter[] ParametroCabecera(int id_expediente)
        {
            OracleParameter[] bdParameters = new OracleParameter[2];

            bdParameters[0] = new OracleParameter("P_IDEXPE", OracleDbType.Int32) { Value = id_expediente };
            bdParameters[1] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            return bdParameters;
        }
        #endregion

        #region Cabecera
        public CabeceraBackOfficeVM ConsultaCabecera(int id_expediente)
        {
            CabeceraBackOfficeVM resultado = new CabeceraBackOfficeVM();
            try
            {
                using (var bdConn = new OracleConnection(cadenaConexion))
                {
                    using (var bdCmd = new OracleCommand("PKG_BACKOFFICE.SP_DETALLE_CABECERA", bdConn))
                    {
                        bdCmd.CommandType = CommandType.StoredProcedure;
                        bdCmd.Parameters.AddRange(ParametroCabecera(id_expediente));
                        bdConn.Open();
                        using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                        {
                            if (bdRd.HasRows)
                            {
                                while (bdRd.Read())
                                {
                                    if (!DBNull.Value.Equals(bdRd["TRAMITE"])) { resultado.TRAMITE = (bdRd["TRAMITE"]).ValorCadena(); }
                                    if (!DBNull.Value.Equals(bdRd["NUMERO_SOLICITANTE"])) { resultado.NUMERO_SOLICITANTE = (bdRd["NUMERO_SOLICITANTE"]).ValorCadena(); }
                                    if (!DBNull.Value.Equals(bdRd["SOLICITANTE"])) { resultado.SOLICITANTE = (bdRd["SOLICITANTE"]).ValorCadena(); }
                                    if (!DBNull.Value.Equals(bdRd["NUMERO_RECURRENTE"])) { resultado.NUMERO_RECURRENTE = (bdRd["NUMERO_RECURRENTE"]).ValorCadena(); }
                                    if (!DBNull.Value.Equals(bdRd["RECURRENTE"])) { resultado.RECURRENTE = (bdRd["RECURRENTE"]).ValorCadena(); }
                                    if (!DBNull.Value.Equals(bdRd["SOLICITUD"])) { resultado.SOLICITUD = (bdRd["SOLICITUD"]).ValorCadena(); }
                                    if (!DBNull.Value.Equals(bdRd["PROCEDIMIENTO"])) { resultado.NOMBRE_PROCEDIMIENTO = (bdRd["PROCEDIMIENTO"]).ValorCadena(); }
                                    if (!DBNull.Value.Equals(bdRd["MODALIDAD_SERVICIO"])) { resultado.MODALIDAD_SERVICIO = (bdRd["MODALIDAD_SERVICIO"]).ValorCadena(); }
                                    if (!DBNull.Value.Equals(bdRd["DATOREGISTRO"])) { resultado.DATOREGISTRO = (bdRd["DATOREGISTRO"]).ValorCadena(); }
                                    if (!DBNull.Value.Equals(bdRd["FECHA_ATENCION"])) { resultado.FECHA_ATENCION = (bdRd["FECHA_ATENCION"]).ValorCadena(); }
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
    }
}
