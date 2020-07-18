//using Oracle.DataAccess.Client;
using SisATU.Base.ViewModel;
using SisATU.Base.Enumeradores;
using SisATU.Base;
using SisATU.Servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace SisATU.Datos
{
    public class OperadorDAL
    {
        OracleConnection bdConn;
        string cadenaConexion = string.Empty;

        #region Constructor
        public OperadorDAL(ref Object _bdConn)
        {
            _bdConn = Conexion.iniciar(ref bdConn, _bdConn);
            this.cadenaConexion = Configuracion.GetConectionSting("sConexionSISREGISTRO");
        }
        #endregion

        public OperadorVM consultaDatosPersonalesYLic(string tipoDocumento, string nroDocumento)
        {
            OperadorVM operador = new OperadorVM();
            OperadorService obj = new OperadorService();

            operador = obj.consultaDatosPersonalesYLic(tipoDocumento, nroDocumento);
            return operador;
        }

      

        #region Crear Operador
        public ResultadoProcedimientoVM CrearOperador(OperadorModelo operador)
        {
            ResultadoProcedimientoVM resultado = new ResultadoProcedimientoVM();
            try
            {
                //using (var bdConn = new OracleConnection(cadenaConexion))
                //{
                    using (var bdCmd = new OracleCommand("PKG_OPERADOR.SP_INSERTAR_OPERADOR", bdConn))
                    {
                        bdCmd.CommandType = CommandType.StoredProcedure;
                        bdCmd.Parameters.AddRange(ParametrosOperador(operador));
                        //bdConn.Open();
                        bdCmd.ExecuteNonQuery();
                        operador.ID_OPERADOR = int.Parse(bdCmd.Parameters["P_OPERADOR"].Value.ToString());
                        resultado.CodAuxiliar = operador.ID_OPERADOR;
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

        #region Consulta Operadores
        public List<OperadorVM> consultarListaOperador(string RUC)
        {
            List<OperadorVM> resultado = new List<OperadorVM>();
            try
            {
                using (var bdConn = new OracleConnection(cadenaConexion))
                {
                    using (var bdCmd = new OracleCommand("PKG_OPERADOR.SP_LISTA_OPERADOR", bdConn))
                    {
                        bdCmd.CommandType = CommandType.StoredProcedure;
                        bdCmd.Parameters.AddRange(ParametrosConsultaOperadores(RUC));
                        bdConn.Open();
                        using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                        {
                            if (bdRd.HasRows)
                            {
                                while (bdRd.Read())
                                {
                                    var item = new OperadorVM();
                                    if (!DBNull.Value.Equals(bdRd["ID_OPERADOR"])) { item.ID_OPERADOR = Convert.ToInt32(bdRd["ID_OPERADOR"]); }
                                    if (!DBNull.Value.Equals(bdRd["PARNOM"])) { item.NOMBRE_TIPO_OPERADOR = Convert.ToString(bdRd["PARNOM"]); }
                                    if (!DBNull.Value.Equals(bdRd["NRO_DOCUMENTO"])) { item.NRO_DOCUMENTO = Convert.ToString(bdRd["NRO_DOCUMENTO"]); }
                                    if (!DBNull.Value.Equals(bdRd["APELLIDO_PATERNO"])) { item.APELLIDO_PATERNO = Convert.ToString(bdRd["APELLIDO_PATERNO"]); }
                                    if (!DBNull.Value.Equals(bdRd["APELLIDO_MATERNO"])) { item.APELLIDO_MATERNO = Convert.ToString(bdRd["APELLIDO_MATERNO"]); }
                                    if (!DBNull.Value.Equals(bdRd["NOMBRES"])) { item.NOMBRES = Convert.ToString(bdRd["NOMBRES"]); }
                                    if (!DBNull.Value.Equals(bdRd["NOMBRE_TIPO_DOCUMENTO"])) { item.NOMBRE_TIPO_DOCUMENTO = Convert.ToString(bdRd["NOMBRE_TIPO_DOCUMENTO"]); }
                                    resultado.Add(item);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return resultado;
        }
        #endregion

        #region Parametros enlaza Operadores
        private OracleParameter[] ParametrosConsultaOperadores(string ruc)
        {
            OracleParameter[] bdParameters = new OracleParameter[2];
            bdParameters[0] = new OracleParameter("P_RUC", OracleDbType.Varchar2) { Value = ruc };
            bdParameters[1] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);

            return bdParameters;
        }
        #endregion

        #region ENLAZAR OPERADOR
        public ResultadoProcedimientoVM EnlazarOperadorEmpresa(OperadorEmpresaModelo operadorEmpresa)
        {
            ResultadoProcedimientoVM resultado = new ResultadoProcedimientoVM();
            try
            {
                //using (var bdConn = new OracleConnection(cadenaConexion))
                //{
                    using (var bdCmd = new OracleCommand("PKG_OPERADOR.SP_INS_EMPRESA_OPER", bdConn))
                    {
                        bdCmd.CommandType = CommandType.StoredProcedure;
                        bdCmd.Parameters.AddRange(ParametrosOperadorEmpresa(operadorEmpresa));
                        //bdConn.Open();
                        bdCmd.ExecuteNonQuery();
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

        #region Parametros enlaza Operadores
        private OracleParameter[] ParametrosOperadorEmpresa(OperadorEmpresaModelo operadorEmpresa)
        {
            OracleParameter[] bdParameters = new OracleParameter[6];
            bdParameters[0] = new OracleParameter("P_OPERADOR", OracleDbType.Int32) { Value = operadorEmpresa.ID_OPERADOR };
            bdParameters[1] = new OracleParameter("P_EXPEDIENTE", OracleDbType.Int32) { Value = operadorEmpresa.ID_EXPEDIENTE };
            bdParameters[2] = new OracleParameter("P_RUC", OracleDbType.Varchar2) { Value = operadorEmpresa.RUC };
            bdParameters[3] = new OracleParameter("P_ESTADO", OracleDbType.Int32) { Value = operadorEmpresa.ID_ESTADO };
            bdParameters[4] = new OracleParameter("P_USUARIO_REG", OracleDbType.Varchar2) { Value = operadorEmpresa.USUARIO_REG };
            bdParameters[5] = new OracleParameter("P_FECHA_REG", OracleDbType.Varchar2) { Value = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff") };

            return bdParameters;
        }
        #endregion

        #region BUSCAR OPERADOR

        public OperadorVM BuscarOperador(string RUC, string NroDocumento, int ID_TIPO_MODALIDAD, int ID_TIPO_PERSONAS,int ID_PROCEDIMIENTO)
        {
            OperadorVM Operador = new OperadorVM();
            try
            {
                using (var bdConn = new OracleConnection(cadenaConexion))
                {
                    using (var bdCmd = new OracleCommand("PKG_OPERADOR.SP_BUSCAR_OPERADOR", bdConn))
                    {
                        bdCmd.CommandType = CommandType.StoredProcedure;
                        bdCmd.Parameters.AddRange(ParametrosBuscarOperador(RUC, NroDocumento, ID_TIPO_MODALIDAD, ID_TIPO_PERSONAS, ID_PROCEDIMIENTO));
                        bdConn.Open();
                        using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                        {
                            if (bdRd.HasRows)
                            {
                                bdRd.Read();
                                if (!DBNull.Value.Equals(bdRd["NRO_DOCUMENTO"])) { Operador.NRO_DOCUMENTO = bdRd["NRO_DOCUMENTO"].ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["ID_OPERADOR"])) { Operador.ID_OPERADOR = bdRd["ID_OPERADOR"].ValorEntero(); }
                                if (!DBNull.Value.Equals(bdRd["ID_TIPO_DOCUMENTO"])) { Operador.ID_TIPO_DOCUMENTO = bdRd["ID_TIPO_DOCUMENTO"].ValorEntero(); }
                                if (!DBNull.Value.Equals(bdRd["APELLIDO_PATERNO"])) { Operador.APELLIDO_PATERNO = bdRd["APELLIDO_PATERNO"].ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["APELLIDO_MATERNO"])) { Operador.APELLIDO_MATERNO = bdRd["APELLIDO_MATERNO"].ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["NOMBRES"])) { Operador.NOMBRES = bdRd["NOMBRES"].ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["fec_nac"])) { Operador.FECHA_NACIMIENTO = bdRd["fec_nac"].ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["id_sexo"])) { Operador.ID_SEXO = bdRd["id_sexo"].ValorEntero(); }
                                if (!DBNull.Value.Equals(bdRd["direccion"])) { Operador.DIRECCION = bdRd["direccion"].ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["telefono"])) { Operador.TELEFONO_CEL = bdRd["telefono"].ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["telefono_casa"])) { Operador.TELEFONO_CASA = bdRd["telefono_casa"].ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["correo"])) { Operador.CORREO = bdRd["correo"].ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["id_tipo_operador"])) { Operador.ID_TIPO_OPERADOR = bdRd["id_tipo_operador"].ValorEntero(); }
                                if (!DBNull.Value.Equals(bdRd["nro_licencia"])) { Operador.NRO_LICENCIA = bdRd["nro_licencia"].ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["fecha_expedicion"])) { Operador.FECHA_EXPEDICION = bdRd["fecha_expedicion"].ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["fecha_revalidacion"])) { Operador.FECHA_REVALIDACION = bdRd["fecha_revalidacion"].ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["categoria"])) { Operador.CATEGORIA = bdRd["categoria"].ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["foto_operador"])) { Operador.NOMBRE_FOTO = (bdRd["foto_operador"].ValorCadena()); }
                                if (!DBNull.Value.Equals(bdRd["RUC"])) { Operador.RUC_EMPRESA_OPERADOR = (bdRd["RUC"].ValorCadena()); }
                                if (!DBNull.Value.Equals(bdRd["ID_DEPARTAMENTO"])) { Operador.ID_DEPARTAMENTO_OPERADOR = bdRd["ID_DEPARTAMENTO"].ValorEntero(); }
                                if (!DBNull.Value.Equals(bdRd["ID_PROVINCIA"])) { Operador.ID_PROVINCIA_OPERADOR = (bdRd["ID_PROVINCIA"].ValorEntero()); }
                                if (!DBNull.Value.Equals(bdRd["ID_DISTRITO"])) { Operador.ID_DISTRITO_OPERADOR = (bdRd["ID_DISTRITO"].ValorEntero()); }
                                if (!DBNull.Value.Equals(bdRd["BD"])) { Operador.BD = (bdRd["BD"].ValorCadena()); }
                                if (!DBNull.Value.Equals(bdRd["FECHA_VENCIMIENTO_CREDENCIAL"])) { Operador.FECHA_VENCIMIENTO_CREDENCIAL = (bdRd["FECHA_VENCIMIENTO_CREDENCIAL"].ValorCadena()); }

                                Operador.TieneCredencial = int.Parse(bdCmd.Parameters["P_RESULTADO"].Value.ToString());

                                
                                //return Operador;
                            }
                            Operador.ResultadoProcedimientoVM.CodResultado = 1;
                            Operador.ResultadoProcedimientoVM.NomResultado = "Cargo Correctamente";
                        }
                        
                    }
                }
            }
            catch (Exception ex)
            {
                Operador.ResultadoProcedimientoVM.CodAuxiliar = 0;
                Operador.ResultadoProcedimientoVM.NomResultado = ex.Message; ;
            }
            
            return Operador;
        }
        #endregion

        #region Validar operador credencial
        public OperadorVM BuscarOperadorCredencial(string NroDocumento)
        {
            OperadorVM Operador = new OperadorVM();
            try
            {
                using (var bdConn = new OracleConnection(cadenaConexion))
                {
                    using (var bdCmd = new OracleCommand("PKG_OPERADOR.SP_VALIDAR_CRED_OPERADOR", bdConn))
                    {
                        bdCmd.CommandType = CommandType.StoredProcedure;
                        bdCmd.Parameters.AddRange(ParametrosBuscarOperadorCredencial(NroDocumento));
                        bdConn.Open();
                        using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                        {
                            if (bdRd.HasRows)
                            {
                                bdRd.Read();
                                if (!DBNull.Value.Equals(bdRd["NUM_CREDENCIAL"])) { Operador.NUM_CREDENCIAL = bdRd["NUM_CREDENCIAL"].ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["RUC"])) { Operador.RUC_EMPRESA_OPERADOR = (bdRd["RUC"].ValorCadena()); }
                                if (!DBNull.Value.Equals(bdRd["NRO_DOCUMENTO"])) { Operador.NRO_DOCUMENTO = bdRd["NRO_DOCUMENTO"].ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["NOMBRE_MODALIDAD_SERVICIO"])) { Operador.NOMBRE_MODALIDAD_SERVICIO = bdRd["NOMBRE_MODALIDAD_SERVICIO"].ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["ID_MODALIDAD_SERVICIO"])) { Operador.ID_MODALIDAD_SERVICIO = bdRd["ID_MODALIDAD_SERVICIO"].ValorEntero(); }
                                if (!DBNull.Value.Equals(bdRd["RAZON_SOCIAL"])) { Operador.RAZON_SOCIAL = bdRd["RAZON_SOCIAL"].ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["BD"])) { Operador.BD = bdRd["BD"].ValorCadena(); }
                                //if (!DBNull.Value.Equals(bdRd["ID_OPERADOR"])) { Operador.ID_OPERADOR = bdRd["ID_OPERADOR"].ValorEntero(); }
                                //if (!DBNull.Value.Equals(bdRd["ID_TIPO_DOCUMENTO"])) { Operador.ID_TIPO_DOCUMENTO = bdRd["ID_TIPO_DOCUMENTO"].ValorEntero(); }
                                //if (!DBNull.Value.Equals(bdRd["APELLIDO_PATERNO"])) { Operador.APELLIDO_PATERNO = bdRd["APELLIDO_PATERNO"].ValorCadena(); }
                                //if (!DBNull.Value.Equals(bdRd["APELLIDO_MATERNO"])) { Operador.APELLIDO_MATERNO = bdRd["APELLIDO_MATERNO"].ValorCadena(); }
                                //if (!DBNull.Value.Equals(bdRd["NOMBRES"])) { Operador.NOMBRES = bdRd["NOMBRES"].ValorCadena(); }
                                //if (!DBNull.Value.Equals(bdRd["fec_nac"])) { Operador.FECHA_NACIMIENTO = bdRd["fec_nac"].ValorCadena(); }
                                //if (!DBNull.Value.Equals(bdRd["id_sexo"])) { Operador.ID_SEXO = bdRd["id_sexo"].ValorEntero(); }
                                //if (!DBNull.Value.Equals(bdRd["direccion"])) { Operador.DIRECCION = bdRd["direccion"].ValorCadena(); }
                                //if (!DBNull.Value.Equals(bdRd["telefono"])) { Operador.TELEFONO_CEL = bdRd["telefono"].ValorCadena(); }
                                //if (!DBNull.Value.Equals(bdRd["telefono_casa"])) { Operador.TELEFONO_CASA = bdRd["telefono_casa"].ValorCadena(); }
                                //if (!DBNull.Value.Equals(bdRd["correo"])) { Operador.CORREO = bdRd["correo"].ValorCadena(); }
                                //if (!DBNull.Value.Equals(bdRd["id_tipo_operador"])) { Operador.ID_TIPO_OPERADOR = bdRd["id_tipo_operador"].ValorEntero(); }
                                //if (!DBNull.Value.Equals(bdRd["nro_licencia"])) { Operador.NRO_LICENCIA = bdRd["nro_licencia"].ValorCadena(); }
                                //if (!DBNull.Value.Equals(bdRd["fecha_expedicion"])) { Operador.FECHA_EXPEDICION = bdRd["fecha_expedicion"].ValorCadena(); }
                                //if (!DBNull.Value.Equals(bdRd["fecha_revalidacion"])) { Operador.FECHA_REVALIDACION = bdRd["fecha_revalidacion"].ValorCadena(); }
                                //if (!DBNull.Value.Equals(bdRd["categoria"])) { Operador.CATEGORIA = bdRd["categoria"].ValorCadena(); }
                                //if (!DBNull.Value.Equals(bdRd["foto_operador"])) { Operador.NOMBRE_FOTO = (bdRd["foto_operador"].ValorCadena()); }

                                //if (!DBNull.Value.Equals(bdRd["ID_DEPARTAMENTO"])) { Operador.ID_DEPARTAMENTO_OPERADOR = bdRd["ID_DEPARTAMENTO"].ValorEntero(); }
                                //if (!DBNull.Value.Equals(bdRd["ID_PROVINCIA"])) { Operador.ID_PROVINCIA_OPERADOR = (bdRd["ID_PROVINCIA"].ValorEntero()); }
                                //if (!DBNull.Value.Equals(bdRd["ID_DISTRITO"])) { Operador.ID_DISTRITO_OPERADOR = (bdRd["ID_DISTRITO"].ValorEntero()); }

                                //Operador.RESULTADO = int.Parse(bdCmd.Parameters["P_RESULTADO"].Value.ToString());
                                return Operador;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //vehiculo.ResultadoProcedimientoVM.CodAuxiliar = 0;
                //vehiculo.ResultadoProcedimientoVM.NomResultado = ex.Message;
            }
            return Operador;
        }
        #endregion

        private OracleParameter[] ParametrosBuscarOperadorCredencial(string NRO_DOCUMENTO)
        {
            OracleParameter[] bdParameters = new OracleParameter[2];
            bdParameters[0] = new OracleParameter("P_NUMDOC", OracleDbType.Varchar2) { Value = NRO_DOCUMENTO };
            bdParameters[1] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            return bdParameters;
        }

        #region PARAMETRO BUSCAR OPERADOR
        private OracleParameter[] ParametrosBuscarOperador(string RUC, string NRO_DOCUMENTO, int ID_MODALIDAD_REGISTRO, int ID_TIPO_PERSONAS, int ID_PROCEDIMIENTO)
        {
            OracleParameter[] bdParameters = new OracleParameter[7];
            bdParameters[0] = new OracleParameter("P_RUC", OracleDbType.Varchar2) { Value = RUC };
            bdParameters[1] = new OracleParameter("P_NUMDOC", OracleDbType.Varchar2) { Value = NRO_DOCUMENTO };
            bdParameters[2] = new OracleParameter("P_TIPO_MODALIDAD", OracleDbType.Int32) { Value = ID_MODALIDAD_REGISTRO };
            bdParameters[3] = new OracleParameter("P_TIPO_PERSONA", OracleDbType.Int32) { Value = ID_TIPO_PERSONAS };
            bdParameters[4] = new OracleParameter("P_RESULTADO", OracleDbType.Int32, direction: ParameterDirection.Output);
            bdParameters[5] = new OracleParameter("P_ID_PROCEDIMIENTO", OracleDbType.Int32) { Value = ID_PROCEDIMIENTO };
            bdParameters[6] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            return bdParameters;
        }
        #endregion




        private OracleParameter[] ParametrosOperador(OperadorModelo operador)
        {
            OracleParameter[] bdParameters = new OracleParameter[31];

            string foto = operador.FOTO_OPERADOR;
            bool png;
            bool jpg;
            bool gif;

            png = foto.Contains(".png");
            jpg = foto.Contains(".jpg");
            gif = foto.Contains(".gif");

            if (!png && !jpg && !gif)
            {
                operador.FOTO_OPERADOR = "";
            }


            bdParameters[0] = new OracleParameter("P_DOCUMENTO", OracleDbType.Varchar2) { Value = operador.NRO_DOCUMENTO };
            bdParameters[1] = new OracleParameter("P_TIPO_PERSONA", OracleDbType.Int32) { Value = operador.ID_TIPO_PERSONA };
            bdParameters[2] = new OracleParameter("P_APELLIDO_PATERNO", OracleDbType.Varchar2) { Value = operador.APELLIDO_PATERNO };
            bdParameters[3] = new OracleParameter("P_APELLIDO_MATERNO", OracleDbType.Varchar2) { Value = operador.APELLIDO_MATERNO };
            bdParameters[4] = new OracleParameter("P_NOMBRES", OracleDbType.Varchar2) { Value = operador.NOMBRE };
            bdParameters[5] = new OracleParameter("P_TIPO_DOCUMENTO", OracleDbType.Int32) { Value = operador.ID_TIPO_DOCUMENTO };
            bdParameters[6] = new OracleParameter("P_RAZON_SOCIAL", OracleDbType.Varchar2) { Value = operador.RAZON_SOCIAL };
            bdParameters[7] = new OracleParameter("P_DIRECCION", OracleDbType.Varchar2) { Value = operador.DIRECCION };
            bdParameters[8] = new OracleParameter("P_TELEFONO", OracleDbType.Varchar2) { Value = operador.TELEFONO_CEL };
            bdParameters[9] = new OracleParameter("P_TELEFONO_CASA", OracleDbType.Varchar2) { Value = operador.TELEFONO_CASA };
            bdParameters[10] = new OracleParameter("P_CORREO", OracleDbType.Varchar2) { Value = operador.CORREO };
            bdParameters[11] = new OracleParameter("P_TIPO_OPERADOR", OracleDbType.Int32) { Value = operador.ID_TIPO_OPERADOR };
            bdParameters[12] = new OracleParameter("P_MODALIDAD_SERVICIO", OracleDbType.Int32) { Value = operador.ID_MODALIDAD_SERVICIO };
            bdParameters[13] = new OracleParameter("P_FOTO_OPERADOR", OracleDbType.Varchar2) { Value = operador.FOTO_OPERADOR };


            bdParameters[14] = new OracleParameter("P_FECHA_INSCRIPCION_OPERADOR", OracleDbType.Varchar2) { Value = operador.FECHA_INSCRIPCION };
            bdParameters[15] = new OracleParameter("P_AÑO", OracleDbType.Int32) { Value = operador.AÑO };
            bdParameters[16] = new OracleParameter("P_NRO_LICENCIA", OracleDbType.Varchar2) { Value = operador.NRO_LICENCIA };
            bdParameters[17] = new OracleParameter("P_CATEGORIA", OracleDbType.Varchar2) { Value = operador.CATEGORIA };
            bdParameters[18] = new OracleParameter("P_FECHA_EXPEDICION", OracleDbType.Varchar2) { Value = operador.FECHA_EXPEDICION };
            bdParameters[19] = new OracleParameter("P_FECHA_REVALIDACION", OracleDbType.Varchar2) { Value = operador.FECHA_REVALIDACION };
            bdParameters[20] = new OracleParameter("P_RESTRICCION", OracleDbType.Varchar2) { Value = operador.RESTRICCION };
            bdParameters[21] = new OracleParameter("P_ESTADO_LICENCIA", OracleDbType.Varchar2) { Value = operador.ESTADO_LICENCIA };
            bdParameters[22] = new OracleParameter("P_ESTADO", OracleDbType.Int32) { Value = EnumEstado.Activo.ValorEntero() };
            bdParameters[23] = new OracleParameter("P_USUARIO_REG", OracleDbType.Varchar2) { Value = operador.USUARIO_REG };
            bdParameters[24] = new OracleParameter("P_SEXO", OracleDbType.Int32) { Value = operador.ID_SEXO };
            bdParameters[25] = new OracleParameter("P_DIRECCION_ACTUAL", OracleDbType.Varchar2) { Value = operador.DIRECCION };
            bdParameters[26] = new OracleParameter("P_FEC_NAC", OracleDbType.Varchar2) { Value = operador.FEC_NAC };


            bdParameters[27] = new OracleParameter("P_DEPARTAMENTO", OracleDbType.Int32) { Value = operador.ID_DEPARTAMENTO_OPERADOR };
            bdParameters[28] = new OracleParameter("P_PROVINCIA", OracleDbType.Int32) { Value = operador.ID_PROVINCIA_OPERADOR };
            bdParameters[29] = new OracleParameter("P_DISTRITO", OracleDbType.Int32) { Value = operador.ID_DISTRITO_OPERADOR };

            bdParameters[30] = new OracleParameter("P_OPERADOR", OracleDbType.Int32, direction: ParameterDirection.Output);
            return bdParameters;
        }

        #region ACTUALIZA FOTO OPERADOR
        public ResultadoProcedimientoVM actualizaFotoOperador(int idOperador, string nombreOperador)
        {
            ResultadoProcedimientoVM resultado = new ResultadoProcedimientoVM();
            try
            {
                //using (var bdConn = new OracleConnection(cadenaConexion))
                //{
                    using (var bdCmd = new OracleCommand("PKG_OPERADOR.SP_ACTUALIZA_FOTO_OPER", bdConn))
                    {
                        bdCmd.CommandType = CommandType.StoredProcedure;
                        bdCmd.Parameters.AddRange(ParametrosOperadorActualizaFoto(idOperador, nombreOperador));
                        //bdConn.Open();
                        bdCmd.ExecuteNonQuery();
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

        private OracleParameter[] ParametrosOperadorActualizaFoto(int idOperador, string nombreOperador)
        {
            OracleParameter[] bdParameters = new OracleParameter[2];
            bdParameters[0] = new OracleParameter("P_ID_OPERADOR", OracleDbType.Int32) { Value = idOperador };
            bdParameters[1] = new OracleParameter("P_FOTO_OPERADOR", OracleDbType.Varchar2) { Value = nombreOperador };

            return bdParameters;
        }
    }
}
