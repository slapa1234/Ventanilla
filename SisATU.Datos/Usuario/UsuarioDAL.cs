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
    public class UsuarioDAL
    {
        string cadenaConexion = string.Empty;

        #region Constructor
        public UsuarioDAL()
        {
            cadenaConexion = Configuracion.GetConectionSting("sConexionSISREGISTRO");
        }
        #endregion

        #region BUSCA MODALIDAD
        public List<ComboModalidadServicioVM> BuscarModalidad(string RUC)
        {
            try
            {
                List<ComboModalidadServicioVM> resultado = new List<ComboModalidadServicioVM>();
                using (var bdConn = new OracleConnection(cadenaConexion))
                {
                    using (var bdCmd = new OracleCommand("PKG_USUARIO.SP_BUSCAR_MODALIDAD_SITU", bdConn))
                    {
                        bdCmd.CommandType = CommandType.StoredProcedure;
                        bdCmd.Parameters.AddRange(ParametrosBuscarModalidad(RUC));
                        bdConn.Open();
                        using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                        {
                            if (bdRd.HasRows)
                            {
                                while (bdRd.Read())
                                {
                                    var item = new ComboModalidadServicioVM();
                                    if (!DBNull.Value.Equals(bdRd["ID_MODALIDAD_SERVICIO"])) { item.ID_MODALIDAD_SERVICIO = Convert.ToInt32(bdRd["ID_MODALIDAD_SERVICIO"]); }
                                    if (!DBNull.Value.Equals(bdRd["NOMBRE_MODALIDAD_SERVICIO"])) { item.NOMBRE = bdRd["NOMBRE_MODALIDAD_SERVICIO"].ToString(); }
                                    resultado.Add(item);
                                }
                            }
                        }
                    }
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Parametros
        private OracleParameter[] ParametrosBuscarModalidad(string RUC)
        {
            OracleParameter[] bdParameters = new OracleParameter[2];
            bdParameters[0] = new OracleParameter("P_RUC", OracleDbType.Varchar2) { Value = RUC };
            bdParameters[1] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            return bdParameters;
        }
        #endregion

        #region CREAR MODALIDAD SERVICIO
        public ResultadoProcedimientoVM CrearModalidadServicio(string RUC, int ID_MODALIDAD_SERVICIO)
        {
            ResultadoProcedimientoVM resultado = new ResultadoProcedimientoVM();
            try
            {
                using (var bdConn = new OracleConnection(cadenaConexion))
                {
                    using (var bdCmd = new OracleCommand("PKG_USUARIO.SP_AGREGAR_MODALIDAD", bdConn))
                    {
                        bdCmd.CommandType = CommandType.StoredProcedure;
                        bdCmd.Parameters.AddRange(ParametrosCrearModalidadServicio(RUC, ID_MODALIDAD_SERVICIO));
                        bdConn.Open();
                        bdCmd.ExecuteNonQuery();
                        resultado.CodResultado = 1;
                        resultado.NomResultado = "Registro Correctamente";
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

        #region Parametros
        private OracleParameter[] ParametrosCrearModalidadServicio(string RUC, int ID_MODALIDAD_SERVICIO)
        {
            OracleParameter[] bdParameters = new OracleParameter[2];
            bdParameters[0] = new OracleParameter("P_ID_USUARIO", OracleDbType.Varchar2) { Value = RUC };
            bdParameters[1] = new OracleParameter("P_ID_MODALIDAD", OracleDbType.Int32) { Value = ID_MODALIDAD_SERVICIO };
            return bdParameters;
        }
        #endregion

        #region crear Usuario
        public ResultadoProcedimientoVM CrearUsuario(UsuarioVM usuario)
        {
            ResultadoProcedimientoVM resultado = new ResultadoProcedimientoVM();
            try
            {
                using (var bdConn = new OracleConnection(cadenaConexion))
                {
                    using (var bdCmd = new OracleCommand("PKG_USUARIO.SP_REGISTRAR_USUARIO", bdConn))
                    {
                        bdCmd.CommandType = CommandType.StoredProcedure;
                        bdCmd.Parameters.AddRange(ParametrosUsuario(usuario));
                        bdConn.Open();
                        bdCmd.ExecuteNonQuery();
                        resultado.CodResultado = 1;
                        resultado.NomResultado = "Registro Correctamente";
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

        public UsuarioModelo BuscarRepresentante(string RUC, string NRO_DOCUMENTO, int ID_TIPO_DOCUMENTO)
        {
            UsuarioModelo usuario = new UsuarioModelo();
            try
            {
                using (var bdConn = new OracleConnection(cadenaConexion))
                {
                    using (var bdCmd = new OracleCommand("PKG_USUARIO.SP_BUSCAR_REPRESENTANTE", bdConn))
                    {
                        bdCmd.CommandType = CommandType.StoredProcedure;
                        bdCmd.Parameters.AddRange(parametrosBuscarRepresentante(RUC, NRO_DOCUMENTO, ID_TIPO_DOCUMENTO));
                        bdConn.Open();
                        bdCmd.ExecuteNonQuery();
                        usuario.ResultadoUsuarioVM.Validacion = int.Parse(bdCmd.Parameters["P_VALIDACION"].Value.ToString());
                        usuario.ResultadoUsuarioVM.CodResultado = 1;
                        usuario.ResultadoUsuarioVM.NomResultado = "Cargo Correctamente";
                    }
                }
            }
            catch (Exception ex)
            {
                usuario.ResultadoUsuarioVM.CodResultado = 0;
                usuario.ResultadoUsuarioVM.NomResultado = "Error en la consulta";
            }
            return usuario;
        }

        private OracleParameter[] parametrosBuscarRepresentante(string RUC, string NRO_DOCUMENTO, int ID_TIPO_DOCUMENTO)
        {
            OracleParameter[] bdParameters = new OracleParameter[4];
            bdParameters[0] = new OracleParameter("P_RUC", OracleDbType.Varchar2) { Value = RUC };
            bdParameters[1] = new OracleParameter("P_NUMERO_DOCUMENTO", OracleDbType.Varchar2) { Value = NRO_DOCUMENTO };
            bdParameters[2] = new OracleParameter("P_ID_TIPO_DOCUMENTO", OracleDbType.Int32) { Value = ID_TIPO_DOCUMENTO };
            bdParameters[3] = new OracleParameter("P_VALIDACION", OracleDbType.Int32, direction: ParameterDirection.Output);

            return bdParameters;
        }

        public UsuarioModelo BuscarUsuario(string NRO_DOCUMENTO, string CLAVE, int ID_MODALIDAD_SERVICIO, int ID_TIPO_PERSONA)
        {

            UsuarioModelo usuario = new UsuarioModelo();
            try
            {
                using (var bdConn = new OracleConnection(cadenaConexion))
                {
                    using (var bdCmd = new OracleCommand("PKG_USUARIO.SP_BUSCAR_USUARIO", bdConn))
                    {
                        bdCmd.CommandType = CommandType.StoredProcedure;
                        bdCmd.Parameters.AddRange(parametrosBuscarUsuario(NRO_DOCUMENTO, CLAVE, ID_MODALIDAD_SERVICIO, ID_TIPO_PERSONA));
                        bdConn.Open();
                        bdCmd.ExecuteNonQuery();
                        usuario.ResultadoUsuarioVM.Acceso = int.Parse(bdCmd.Parameters["P_ACCESO"].Value.ToString());
                        usuario.ResultadoUsuarioVM.Validacion = int.Parse(bdCmd.Parameters["P_VALIDACION"].Value.ToString());
                        usuario.ResultadoUsuarioVM.Modalidad = int.Parse(bdCmd.Parameters["P_VAL_MODALIDAD"].Value.ToString());
                        usuario.NRO_DOCUMENTO_REPRESENTANTE_LEGAL = bdCmd.Parameters["P_NRO_DOC"].Value.ToString();
                        usuario.ID_TIPO_DOCUMENTO_REPRESENTANTE_LEGAL = int.Parse(bdCmd.Parameters["P_ID_TPO_DOC"].Value.ToString());
                        usuario.ResultadoUsuarioVM.CodResultado = 1;
                        usuario.ResultadoUsuarioVM.NomResultado = "Cargo Correctamente";

                    }
                }
            }
            catch (Exception ex)
            {
                usuario.ResultadoUsuarioVM.CodResultado = 0;
                usuario.ResultadoUsuarioVM.NomResultado = "Error en la consulta";
            }
            return usuario;
        }
         
        private OracleParameter[] parametrosBuscarUsuario(string NRO_DOCUMENTO, string CLAVE, int ID_MODALIDAD_SERVICIO, int ID_TIPO_PERSONA)
        {
            OracleParameter[] bdParameters = new OracleParameter[9];
            bdParameters[0] = new OracleParameter("P_ID_USUARIO", OracleDbType.Varchar2) { Value = NRO_DOCUMENTO };
            bdParameters[1] = new OracleParameter("P_ID_CLAVE", OracleDbType.Varchar2) { Value = CLAVE };
            bdParameters[2] = new OracleParameter("P_ID_MODALIDAD", OracleDbType.Int32) { Value = ID_MODALIDAD_SERVICIO };
            bdParameters[3] = new OracleParameter("P_ID_TIPO_PERSONA", OracleDbType.Int32) { Value = ID_TIPO_PERSONA };
            bdParameters[4] = new OracleParameter("P_ACCESO", OracleDbType.Int32, direction: ParameterDirection.Output);
            bdParameters[5] = new OracleParameter("P_VALIDACION", OracleDbType.Int32, direction: ParameterDirection.Output);
            bdParameters[6] = new OracleParameter("P_VAL_MODALIDAD", OracleDbType.Int32, direction: ParameterDirection.Output);
            bdParameters[7] = new OracleParameter("P_NRO_DOC", OracleDbType.Varchar2, direction: ParameterDirection.Output);
            bdParameters[7].Size = 100;
            bdParameters[8] = new OracleParameter("P_ID_TPO_DOC", OracleDbType.Int32, direction: ParameterDirection.Output);
            return bdParameters;
        }

        #region
        private OracleParameter[] ParametrosUsuario(UsuarioVM usuario)
        {
            OracleParameter[] bdParameters = new OracleParameter[14];
            bdParameters[0] = new OracleParameter("P_NOMBRE_USER", OracleDbType.Varchar2) { Value = usuario.NOMBRE_USUARIO };
            bdParameters[1] = new OracleParameter("P_CLAVE", OracleDbType.Varchar2) { Value = usuario.CLAVE };
            bdParameters[2] = new OracleParameter("P_TIPO_PERSONA", OracleDbType.Int32) { Value = usuario.ID_TIPO_PERSONA };
            bdParameters[3] = new OracleParameter("P_ID_TIPO_DOCUMENTO", OracleDbType.Int32) { Value = usuario.ID_TIPO_DOCUMENTO };
            bdParameters[4] = new OracleParameter("P_NRO_DOCUMENTO", OracleDbType.Varchar2) { Value = usuario.NRO_DOCUMENTO };
            bdParameters[5] = new OracleParameter("P_RAZON_SOCIAL", OracleDbType.Varchar2) { Value = usuario.RAZON_SOCIAL };
            bdParameters[6] = new OracleParameter("P_NOMBRES", OracleDbType.Varchar2) { Value = usuario.NOMBRES };
            bdParameters[7] = new OracleParameter("P_APELLIDO_PATERNO", OracleDbType.Varchar2) { Value = usuario.APEPAT };
            bdParameters[8] = new OracleParameter("P_APELLIDO_MATERNO", OracleDbType.Varchar2) { Value = usuario.APEMAT };
            bdParameters[9] = new OracleParameter("P_TELEFONO", OracleDbType.Varchar2) { Value = usuario.TELEFONO };
            bdParameters[10] = new OracleParameter("P_CORREO", OracleDbType.Varchar2) { Value = usuario.CORREO };
            bdParameters[11] = new OracleParameter("P_DIRECCION", OracleDbType.Varchar2) { Value = usuario.DIRECCION };
            bdParameters[12] = new OracleParameter("P_ID_TIPO_DOCUMENTO_R", OracleDbType.Varchar2) { Value = usuario.ID_TIPO_DOCUMENTO_REPRESENTANTE_LEGAL };
            bdParameters[13] = new OracleParameter("P_NRO_DOCUMENTO_R", OracleDbType.Varchar2) { Value = usuario.NRO_DOCUMENTO_REPRESENTANTE_LOCAL };

            return bdParameters;
        }
        #endregion
    }
}
