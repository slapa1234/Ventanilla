//using Oracle.DataAccess.Client;
using Oracle.ManagedDataAccess.Client;
using SisATU.Base;
using SisATU.Base.Enumeradores;
using SisATU.Base.ViewModel;
using SisATU.Servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Datos
{
    public class PersonaDAL
    {
        OracleConnection bdConn;
        string cadenaConexion = string.Empty;

        #region Constructor
        public PersonaDAL(ref Object _bdConn)
        {
            _bdConn = Conexion.iniciar(ref bdConn, _bdConn);
            this.cadenaConexion = Configuracion.GetConectionSting("sConexionSISREGISTRO");
        }
        #endregion
        public PersonaVM ConsultaDNI(string DNI)
        {
            PersonaVM persona = new PersonaVM();
            ReniecService obj = new ReniecService();
            persona = obj.ConsultaDNI2(DNI);
            return persona;
        }

        public PersonaVM ConsultarCE(string DNI)
        {
            MigracionesService obj = new MigracionesService();
            var resultado = obj.ConsultaCE2(DNI);
            return resultado;
        }



        public PersonaVM consultarPersonaEnSITUS(string nroDocumento)
        {
            PersonaVM persona = new PersonaVM();
            try
            {
                using (var bdConn = new OracleConnection(cadenaConexion))
                {
                    using (var bdCmd = new OracleCommand("PKG_PERSONA.SP_CONSULTAR_PERSONA_SITU", bdConn))
                    {
                        bdCmd.CommandType = CommandType.StoredProcedure;
                        bdCmd.Parameters.AddRange(ParametroConsultaPersonaSITU(nroDocumento));
                        bdConn.Open();
                        using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                        {
                            if (bdRd.HasRows)
                            {
                                bdRd.Read();
                                if (!DBNull.Value.Equals(bdRd["NUMERO_DOCUMENTO"])) { persona.NRO_DOCUMENTO = bdRd["NUMERO_DOCUMENTO"].ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["APELLIDO_PATERNO"])) { persona.APELLIDO_PATERNO = bdRd["APELLIDO_PATERNO"].ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["APELLIDO_MATERNO"])) { persona.APELLIDO_MATERNO = bdRd["APELLIDO_MATERNO"].ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["NOMBRE"])) { persona.NOMBRES = bdRd["NOMBRE"].ValorCadena(); }
                                
                                persona.ResultadoProcedimientoVM.CodResultado = 1;
                                persona.ResultadoProcedimientoVM.NomResultado = "Cargo Correctamente";
                                //return persona;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                persona.ResultadoProcedimientoVM.CodAuxiliar = 0;
                persona.ResultadoProcedimientoVM.NomResultado = ex.Message;
            }
            return persona;
        }

        public PersonaVM ConsultarPTP(string DNI)
        {
            PersonaVM persona = new PersonaVM();
            MtcService obj = new MtcService();
            persona = obj.ConsultaPTP(DNI);
            return persona;
        }

        #region Crear Persona
        public ResultadoProcedimientoVM CrearPersona(PersonaModelo persona)
        {
            ResultadoProcedimientoVM modelo = new ResultadoProcedimientoVM();
            try
            {
                using (var bdCmd = new OracleCommand("PKG_PERSONA.SP_INSERTAR_PERSONA", bdConn))
                {
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(ParametrosCrearPersona(persona));
                    bdCmd.ExecuteNonQuery();
                    modelo.CodResultado = 1;
                    modelo.NomResultado = "Registro Correctamente";
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

        #region Parametros consulta parametros para consulta situ y situ web
        private OracleParameter[] ParametroConsultaPersonaSITU(string nroDocumento)
        {
           
            OracleParameter[] bdParameters = new OracleParameter[2];
            bdParameters[0] = new OracleParameter("P_NUMERO_DOC", OracleDbType.Varchar2) { Value = nroDocumento };
            bdParameters[1] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);

            return bdParameters;
        }
        #endregion


        #region Parametros Crear Persona
        private OracleParameter[] ParametrosCrearPersona(PersonaModelo persona)
        {
            OracleParameter[] bdParameters = new OracleParameter[17];
            bdParameters[0] = new OracleParameter("P_DOCUMENTO", OracleDbType.Varchar2) { Value = persona.NRO_DOCUMENTO };
            bdParameters[1] = new OracleParameter("P_TIPO_PERSONA", OracleDbType.Int32) { Value = persona.ID_TIPO_PERSONA };
            bdParameters[2] = new OracleParameter("P_APELLIDO_PATERNO", OracleDbType.Varchar2) { Value = persona.APELLIDO_PATERNO };
            bdParameters[3] = new OracleParameter("P_APELLIDO_MATERNO", OracleDbType.Varchar2) { Value = persona.APELLIDO_MATERNO };
            bdParameters[4] = new OracleParameter("P_NOMBRES", OracleDbType.Varchar2) { Value = persona.NOMBRES };
            bdParameters[5] = new OracleParameter("P_TIPO_DOCUMENTO", OracleDbType.Int32) { Value = persona.ID_TIPO_DOCUMENTO };
            bdParameters[6] = new OracleParameter("P_RAZON_SOCIAL", OracleDbType.Varchar2) { Value = persona.RAZON_SOCIAL };
            bdParameters[7] = new OracleParameter("P_DIRECCION", OracleDbType.Varchar2) { Value = persona.DIRECCION };
            bdParameters[8] = new OracleParameter("P_DIRECCION_ACTUAL", OracleDbType.Varchar2) { Value = persona.DIRECCION_ACTUAL };
            bdParameters[9] = new OracleParameter("P_TELEFONO", OracleDbType.Varchar2) { Value = persona.TELEFONO };
            bdParameters[10] = new OracleParameter("P_CORREO", OracleDbType.Varchar2) { Value = persona.CORREO };
            bdParameters[11] = new OracleParameter("P_ESTADO", OracleDbType.Int32) { Value = EnumEstado.Activo.ValorEntero() };
            bdParameters[12] = new OracleParameter("P_USUARIO_REG", OracleDbType.Varchar2) { Value = persona.USUARIO_REG };
            bdParameters[13] = new OracleParameter("P_DEPARTAMENTO", OracleDbType.Int32) { Value = persona.ID_DEPARTAMENTO };
            bdParameters[14] = new OracleParameter("P_PROVINCIA", OracleDbType.Int32) { Value = persona.ID_PROVINCIA };
            bdParameters[15] = new OracleParameter("P_DISTRITO", OracleDbType.Int32) { Value = persona.ID_DISTRITO };
            bdParameters[16] = new OracleParameter("P_IDPERSONA", OracleDbType.Int32, direction: ParameterDirection.Output);
            return bdParameters;
        }
        #endregion

        #region Crear Propietario
        public ResultadoProcedimientoVM CrearPropietario(PropietarioModelo propietario)
        {
            ResultadoProcedimientoVM modelo = new ResultadoProcedimientoVM();
            try
            {
                using (var bdCmd = new OracleCommand("PKG_PERSONA.SP_INSERTAR_PROPIETARIO", bdConn))
                {
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(ParametrosCrearPropietario(propietario));
                    bdCmd.ExecuteNonQuery();

                    modelo.CodResultado = 1;
                    modelo.NomResultado = "Registro Correctamente";
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

        #region Parametro Crear Propietario 
        private OracleParameter[] ParametrosCrearPropietario(PropietarioModelo propietario)
        {
            OracleParameter[] bdParameters = new OracleParameter[9];
            bdParameters[0] = new OracleParameter("P_DOCUMENTO", OracleDbType.Varchar2) { Value = propietario.NRO_DOCUMENTO };
            bdParameters[1] = new OracleParameter("P_TIPO_CONTRIBUYENTE", OracleDbType.Int32) { Value = propietario.ID_TIPO_CONTRIBUYENTE };
            bdParameters[2] = new OracleParameter("P_TIPO_DOCUMENTO", OracleDbType.Int32) { Value = propietario.ID_TIPO_DOCUMENTO };
            bdParameters[3] = new OracleParameter("P_TARJETA_PROPIEDAD", OracleDbType.Int32) { Value = propietario.ID_TARJETA_PROPIEDAD };
            bdParameters[4] = new OracleParameter("P_OBSERVACION", OracleDbType.Varchar2) { Value = propietario.OBSERVACION };
            bdParameters[5] = new OracleParameter("P_NOMBRE_PROPIETARIO", OracleDbType.Varchar2) { Value = propietario.NOMBRE_PROPIETARIO };
            bdParameters[6] = new OracleParameter("P_ESTADO", OracleDbType.Int32) { Value = EnumEstado.Activo.ValorEntero() };
            bdParameters[7] = new OracleParameter("P_USUARIO_REG", OracleDbType.Varchar2) { Value = propietario.USUARIO_REG };
            bdParameters[8] = new OracleParameter("P_PROPIETARIO", OracleDbType.Int32, direction: ParameterDirection.Output);
            return bdParameters;
        }
        #endregion

        #region Consultar Persona
        public PersonaVM ConsultarPersona(int ID_TIPO_DOCUMENTO, string NRO_DOCUMENTO)
        {
            PersonaVM persona = new PersonaVM();
            try
            {
                using (var bdConn = new OracleConnection(cadenaConexion))
                {
                    using (var bdCmd = new OracleCommand("PKG_PERSONA.SP_CONSULTAR_PERSONA", bdConn))
                    {
                        bdCmd.CommandType = CommandType.StoredProcedure;
                        bdCmd.Parameters.AddRange(ParametroConsultarPersona(ID_TIPO_DOCUMENTO, NRO_DOCUMENTO));
                        bdConn.Open();
                        using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                        {
                            if (bdRd.HasRows)
                            {
                                bdRd.Read();
                                if (!DBNull.Value.Equals(bdRd["ID_PERSONA"])) { persona.ID_PERSONA = bdRd["ID_PERSONA"].ValorEntero(); }
                                if (!DBNull.Value.Equals(bdRd["ID_TIPO_DOCUMENTO"])) { persona.ID_TIPO_DOCUMENTO = bdRd["ID_TIPO_DOCUMENTO"].ValorEntero(); }
                                if (!DBNull.Value.Equals(bdRd["NRO_DOCUMENTO"])) { persona.NRO_DOCUMENTO = bdRd["NRO_DOCUMENTO"].ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["APELLIDO_PATERNO"])) { persona.APELLIDO_PATERNO = bdRd["APELLIDO_PATERNO"].ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["APELLIDO_MATERNO"])) { persona.APELLIDO_MATERNO = bdRd["APELLIDO_MATERNO"].ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["NOMBRES"])) { persona.NOMBRES = bdRd["NOMBRES"].ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["TELEFONO"])) { persona.TELEFONO = bdRd["TELEFONO"].ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["CORREO"])) { persona.CORREO = bdRd["CORREO"].ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["DIRECCION"])) { persona.DIRECCION = bdRd["DIRECCION"].ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["DIRECCION_ACTUAL"])) { persona.DIRECCION_ACTUAL = bdRd["DIRECCION_ACTUAL"].ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["ID_DEPARTAMENTO"])) { persona.ID_DEPARTAMENTO = bdRd["ID_DEPARTAMENTO"].ValorEntero(); }
                                if (!DBNull.Value.Equals(bdRd["ID_PROVINCIA"])) { persona.ID_PROVINCIA = bdRd["ID_PROVINCIA"].ValorEntero(); }
                                if (!DBNull.Value.Equals(bdRd["ID_DISTRITO"])) { persona.ID_DISTRITO = bdRd["ID_DISTRITO"].ValorEntero(); }
                                persona.ResultadoProcedimientoVM.CodResultado = 1;
                                persona.ResultadoProcedimientoVM.NomResultado = "Cargo Correctamente";
                                //return persona;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                persona.ResultadoProcedimientoVM.CodAuxiliar = 0;
                persona.ResultadoProcedimientoVM.NomResultado = ex.Message;
            }
            return persona;
        }
        #endregion

        #region Parametro Buscar Persona
        public OracleParameter[] ParametroConsultarPersona(int ID_TIPO_DOCUMENTO, string NRO_DOCUMENTO)
        {
            OracleParameter[] bdParameters = new OracleParameter[3];
            bdParameters[0] = new OracleParameter("P_NUMERO_DOC", OracleDbType.Int32) { Value = ID_TIPO_DOCUMENTO };
            bdParameters[1] = new OracleParameter("P_NUMERO_DOC", OracleDbType.Varchar2) { Value = NRO_DOCUMENTO };
            bdParameters[2] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            return bdParameters;
        }
        #endregion


        #region Parametro Recuperar Contraseña
        public OracleParameter[] ParametroRecuperarClave(string nroDocumento, string correo, string contrasenia)
        {
            OracleParameter[] bdParameters = new OracleParameter[5];
            bdParameters[0] = new OracleParameter("P_NOMBRE_USER", OracleDbType.Varchar2) { Value = nroDocumento };
            bdParameters[1] = new OracleParameter("P_CORREO", OracleDbType.Varchar2) { Value = correo };
            bdParameters[2] = new OracleParameter("P_CLAVE", OracleDbType.Varchar2) { Value = contrasenia };
            bdParameters[3] = new OracleParameter("P_VALIDACION", OracleDbType.Int32, direction: ParameterDirection.Output);
            bdParameters[4] = new OracleParameter("P_MENSAJE", OracleDbType.Varchar2, direction: ParameterDirection.Output);
            bdParameters[4].Size = 200;
            return bdParameters;
        }
        #endregion

        #region Recuperar Contraseña
        public ResultadoProcedimientoVM RecuperarClave(string nroDocumento, string correo, string contrasenia)
        {
            ResultadoProcedimientoVM modelo = new ResultadoProcedimientoVM();

            try
            {
                using (var bdCmd = new OracleCommand("PKG_USUARIO.SP_RECUPERAR_CLAVE", bdConn))
                {
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(ParametroRecuperarClave(nroDocumento, correo, contrasenia));
                    bdCmd.ExecuteNonQuery();
                    modelo.CodResultado = int.Parse(bdCmd.Parameters["P_VALIDACION"].Value.ToString());
                    //modelo.CLAVE_NUEVO = bdCmd.Parameters["P_CLAVE"].Value.ToString();
                    modelo.NomResultado = bdCmd.Parameters["P_MENSAJE"].Value.ToString();
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

    }
}
