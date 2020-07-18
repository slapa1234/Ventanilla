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
    public class VehiculoDAL
    {
        OracleConnection bdConn;
        string cadenaConexion = string.Empty;

        #region Constructor
        public VehiculoDAL(ref Object _bdConn)
        {
            _bdConn = Conexion.iniciar(ref bdConn, _bdConn);
            cadenaConexion = Configuracion.GetConectionSting("sConexionSISREGISTRO");
        }
        #endregion

        public ConsultarVehiculoVM ConsultarDatosVehiculo(string nroPlaca)
        {
            ConsultarVehiculoVM vehiculo = new ConsultarVehiculoVM();
            try
            {
                using (var bdConn = new OracleConnection(cadenaConexion))
                {
                    using (var bdCmd = new OracleCommand("PKG_VEHICULO.SP_BUSCAR_VEHICULO", bdConn))
                    {
                        bdCmd.CommandType = CommandType.StoredProcedure;
                        bdCmd.Parameters.AddRange(ConsultarVehiculoParametros(nroPlaca));
                        bdConn.Open();
                        using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                        {
                            if (bdRd.HasRows)
                            {
                                bdRd.Read();
                                if (!DBNull.Value.Equals(bdRd["ID_VEHICULO"])) { vehiculo.ID_VEHICULO = bdRd["ID_VEHICULO"].ValorEntero(); }
                                if (!DBNull.Value.Equals(bdRd["MODALIDAD"])) { vehiculo.NOMBRE_MODALIDAD_SERVICIO = bdRd["MODALIDAD"].ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["ID_CLASE_VEHICULO"])) { vehiculo.ID_CLASE_VEHICULO = bdRd["ID_CLASE_VEHICULO"].ValorEntero(); }
                                if (!DBNull.Value.Equals(bdRd["CLASE"])) { vehiculo.NOMBRE_CLASE_VEHICULO = bdRd["CLASE"].ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["ID_MODELO"])) { vehiculo.ID_MODELO = bdRd["ID_MODELO"].ValorEntero(); }
                                if (!DBNull.Value.Equals(bdRd["MODELO"])) { vehiculo.NOMBRE_MODELO = bdRd["MODELO"].ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["COMBUSTIBLE"])) { vehiculo.NOMBRE_COMBUSTIBLE = bdRd["COMBUSTIBLE"].ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["ID_TIPO_COMBUSTIBLE"])) { vehiculo.ID_TIPO_COMBUSTIBLE = bdRd["ID_TIPO_COMBUSTIBLE"].ValorEntero(); }
                                if (!DBNull.Value.Equals(bdRd["ID_CATEGORIA_VEHICULO"])) { vehiculo.ID_CATEGORIA_VEHICULO = bdRd["ID_CATEGORIA_VEHICULO"].ValorEntero(); }
                                if (!DBNull.Value.Equals(bdRd["ANIO_FABRICACION"])) { vehiculo.ANIO_FABRICACION = bdRd["ANIO_FABRICACION"].ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["SERIE"])) { vehiculo.SERIE = bdRd["SERIE"].ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["SERIE_MOTOR"])) { vehiculo.SERIE_MOTOR = bdRd["SERIE_MOTOR"].ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["PESO_SECO"])) { vehiculo.PESO_SECO = bdRd["PESO_SECO"].ValorEntero(); }
                                if (!DBNull.Value.Equals(bdRd["PESO_BRUTO"])) { vehiculo.PESO_BRUTO = bdRd["PESO_BRUTO"].ValorEntero(); }
                                if (!DBNull.Value.Equals(bdRd["LONGITUD"])) { vehiculo.LONGITUD = bdRd["LONGITUD"].ValorEntero(); }
                                if (!DBNull.Value.Equals(bdRd["ALTURA"])) { vehiculo.ALTURA = bdRd["ALTURA"].ValorEntero(); }
                                if (!DBNull.Value.Equals(bdRd["ANCHO"])) { vehiculo.ANCHO = bdRd["ANCHO"].ValorEntero(); }
                                if (!DBNull.Value.Equals(bdRd["CARGA_UTIL"])) { vehiculo.CARGA_UTIL = bdRd["CARGA_UTIL"].ValorEntero(); }
                                if (!DBNull.Value.Equals(bdRd["CAPACIDAD_PASAJERO"])) { vehiculo.CAPACIDAD_PASAJERO = bdRd["CAPACIDAD_PASAJERO"].ValorEntero(); }
                                if (!DBNull.Value.Equals(bdRd["NUMERO_ASIENTOS"])) { vehiculo.NUMERO_ASIENTOS = bdRd["NUMERO_ASIENTOS"].ValorEntero(); }
                                if (!DBNull.Value.Equals(bdRd["NUMERO_RUEDA"])) { vehiculo.NUMERO_RUEDA = bdRd["NUMERO_RUEDA"].ValorEntero(); }
                                if (!DBNull.Value.Equals(bdRd["NUMERO_EJE"])) { vehiculo.NUMERO_EJE = bdRd["NUMERO_EJE"].ValorEntero(); }
                                if (!DBNull.Value.Equals(bdRd["NUMERO_PUERTA"])) { vehiculo.NUMERO_PUERTA = bdRd["NUMERO_PUERTA"].ValorEntero(); }
                                if (!DBNull.Value.Equals(bdRd["CILINDRADA"])) { vehiculo.CILINDRADA = bdRd["CILINDRADA"].ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["PLACA"])) { vehiculo.PLACA = bdRd["PLACA"].ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["ID_MARCA"])) { vehiculo.ID_MARCA = bdRd["ID_MARCA"].ValorEntero(); }
                                if (!DBNull.Value.Equals(bdRd["MARCA"])) { vehiculo.NOMBRE_MARCA = bdRd["MARCA"].ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["NRO_TARJETA_PROPIEDAD"])) { vehiculo.NRO_TARJETA_PROPIEDAD = bdRd["NRO_TARJETA_PROPIEDAD"].ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["INI_PRO"])) { vehiculo.FECHA_INICIO_PROPIEDAD = bdRd["INI_PRO"].ValorFechaCorta(); }
                                if (!DBNull.Value.Equals(bdRd["FIN_PRO"])) { vehiculo.FECHA_FIN_PROPIEDAD = bdRd["FIN_PRO"].ValorFechaCorta(); }
                                if (!DBNull.Value.Equals(bdRd["PROPIE"])) { vehiculo.NOMBRE_PROPIETARIO = bdRd["PROPIE"].ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["NRO_DOCUMENTO"])) { vehiculo.NUMERO_DOCUMENTO_PROPIEDAD = bdRd["NRO_DOCUMENTO"].ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["ID_TIPO_DOCUMENTO"])) { vehiculo.ID_TIPO_DOCUMENTO = bdRd["ID_TIPO_DOCUMENTO"].ValorEntero(); }
                                if (!DBNull.Value.Equals(bdRd["FECHA_VENC_TUC"])) { vehiculo.FECHA_VENC_TUC = bdRd["FECHA_VENC_TUC"].ValorCadena(); }
                                vehiculo.ResultadoProcedimientoVM.CodResultado = 1;
                                vehiculo.ResultadoProcedimientoVM.NomResultado = "Cargo Correctamente";
                                return vehiculo;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                vehiculo.ResultadoProcedimientoVM.CodAuxiliar = 0;
                vehiculo.ResultadoProcedimientoVM.NomResultado = ex.Message;
            }
            return vehiculo;
        }

        public int ConsultaPerteneceSolicitante(string nroPlaca, string nroSolicitante)
        {
            int resultado = 0;
            try
            {
                using (var bdConn = new OracleConnection(cadenaConexion))
                {
                    using (var bdCmd = new OracleCommand("PKG_VEHICULO.SP_PERTENECE_SOLICITANTE", bdConn))
                    {
                        bdCmd.CommandType = CommandType.StoredProcedure;
                        bdCmd.Parameters.AddRange(ConsultaPerteneceSolicitanteParametros(nroPlaca, nroSolicitante));
                        bdConn.Open();
                        bdCmd.ExecuteNonQuery();
                        resultado = int.Parse(bdCmd.Parameters["P_RESULTADO"].Value.ToString());
                        bdConn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                resultado = 0;
            }
            return resultado;
        }

        #region Parametros
        public OracleParameter[] ConsultarVehiculoParametros(string nroPlaca = "")
        {
            OracleParameter[] bdParameters = new OracleParameter[2];
            bdParameters[0] = new OracleParameter("P_PLACA", OracleDbType.Varchar2) { Value = nroPlaca };
            bdParameters[1] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            return bdParameters;
        }

        public OracleParameter[] ConsultaPerteneceSolicitanteParametros(string nroPlaca = "", string nroSolicitante = "")
        {
            OracleParameter[] bdParameters = new OracleParameter[] {
                new OracleParameter("P_PLACA", OracleDbType.Varchar2) { Value = nroPlaca },
                new OracleParameter("P_SOLICITANTE", OracleDbType.Varchar2) { Value = nroSolicitante },
                new OracleParameter("P_RESULTADO", OracleDbType.Int32, direction: ParameterDirection.Output)
            };
            return bdParameters;
        }
        #endregion

        #region Crear Vehiculo
        public ResultadoProcedimientoVM CrearVehiculo(VehiculoModelo Vehiculo)
        {
            ResultadoProcedimientoVM modelo = new ResultadoProcedimientoVM();
            try
            {
                using (var bdCmd = new OracleCommand("PKG_VEHICULO.SP_INSERTAR_VEHICULO", bdConn))
                {
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(ParametrosCrearVehiculo(Vehiculo));
                    bdCmd.ExecuteNonQuery();
                    Vehiculo.ID_VEHICULO = int.Parse(bdCmd.Parameters["P_VEHICULO"].Value.ToString());

                    modelo.CodResultado = 1;
                    modelo.NomResultado = "Registro Correctamente";
                    modelo.CodAuxiliar = Vehiculo.ID_VEHICULO;
                }
            }
            catch (Exception ex)
            {
                modelo.CodResultado = 0;
                modelo.NomResultado = ex.Message;
            }
            return modelo;
        }

        public ResultadoProcedimientoVM VehiculoPersona(string NRO_DOCUMENTO, string PLACA)
        {
            ResultadoProcedimientoVM modelo = new ResultadoProcedimientoVM();
            try
            {
                using (var bdCmd = new OracleCommand("PKG_VEHICULO.SP_VEHICULO_PERSONA", bdConn))
                {
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(ParametrosCrearVehiculoPersona(NRO_DOCUMENTO, PLACA));
                    bdCmd.ExecuteNonQuery();
                    //Vehiculo.ID_VEHICULO = ;

                    modelo.CodResultado = 1;
                    modelo.NomResultado = "Registro Correctamente";
                    modelo.CodAuxiliar = int.Parse(bdCmd.Parameters["P_RESULTADO"].Value.ToString());
                }
            }
            catch (Exception ex)
            {
                modelo.CodResultado = 0;
                modelo.NomResultado = ex.Message;
            }
            return modelo;
        }

        //public ResultadoProcedimientoVM CrearVehiculo(VehiculoModelo Vehiculo)
        //{
        //    ResultadoProcedimientoVM modelo = new ResultadoProcedimientoVM();
        //    DataSet ds = new DataSet();
        //    using (var bdConn = new OracleConnection(cadenaConexion))
        //    {
        //        bdConn.Open();
        //        using (OracleCommand command = new OracleCommand())
        //        {
        //            command.CommandText = "PKG_VEHICULO.SP_INSERTAR_VEHICULO";
        //            command.CommandType = CommandType.StoredProcedure;
        //            command.Connection = bdConn;

        //            command.Parameters.AddRange(ParametrosCrearVehiculo(Vehiculo));

        //            OracleDataAdapter da = new OracleDataAdapter();
        //            da.SelectCommand = command;
        //            da.Fill(ds);

        //        }
        //        bdConn.Close();
        //        return modelo;

        //    }
        //}
        #endregion

        #region Vehiculo Persona
        private OracleParameter[] ParametrosCrearVehiculoPersona(string NRO_DOCUMENTO, string PLACA)
        {
            OracleParameter[] bdParameters = new OracleParameter[3];
            bdParameters[0] = new OracleParameter("P_DOCUMENTO", OracleDbType.Varchar2) { Value = NRO_DOCUMENTO };
            bdParameters[1] = new OracleParameter("P_PLACA", OracleDbType.Varchar2) { Value = PLACA };
            bdParameters[2] = new OracleParameter("P_RESULTADO", OracleDbType.Int32, direction: ParameterDirection.Output);
            return bdParameters;
        }
        #endregion

        #region Parametros Crear Vehiculo
        private OracleParameter[] ParametrosCrearVehiculo(VehiculoModelo vehiculo)
        {
            OracleParameter[] bdParameters = new OracleParameter[27];
            bdParameters[0] = new OracleParameter("P_MODALIDAD_SERVICIO", OracleDbType.Int32) { Value = vehiculo.ID_MODALIDAD_SERVICIO };
            bdParameters[1] = new OracleParameter("P_CLASE_VEHICULO", OracleDbType.Int32) { Value = vehiculo.ID_CLASE_VEHICULO };
            bdParameters[2] = new OracleParameter("P_MODELO", OracleDbType.Int32) { Value = vehiculo.ID_MODELO };
            bdParameters[3] = new OracleParameter("P_TIPO_COMBUSTIBLE", OracleDbType.Int32) { Value = vehiculo.ID_TIPO_COMBUSTIBLE };
            bdParameters[4] = new OracleParameter("P_CATEGORIA_VEHICULO", OracleDbType.Int32) { Value = vehiculo.ID_CATEGORIA_VEHICULO };
            bdParameters[5] = new OracleParameter("P_ANIO_FABRICACION", OracleDbType.Varchar2) { Value = vehiculo.ANIO_FABRICACION };
            bdParameters[6] = new OracleParameter("P_SERIE", OracleDbType.Varchar2) { Value = vehiculo.SERIE };
            bdParameters[7] = new OracleParameter("P_SERIE_MOTOR", OracleDbType.Varchar2) { Value = vehiculo.SERIE_MOTOR };
            bdParameters[8] = new OracleParameter("P_PESO_SECO", OracleDbType.Int32) { Value = vehiculo.PESO_SECO };
            bdParameters[9] = new OracleParameter("P_PESO_BRUTO", OracleDbType.Int32) { Value = vehiculo.PESO_BRUTO };
            bdParameters[10] = new OracleParameter("P_LONGITUD", OracleDbType.Int32) { Value = vehiculo.LONGITUD };
            bdParameters[11] = new OracleParameter("P_ALTURA", OracleDbType.Int32) { Value = vehiculo.ALTURA };
            bdParameters[12] = new OracleParameter("P_ANCHO", OracleDbType.Int32) { Value = vehiculo.ANCHO };
            bdParameters[13] = new OracleParameter("P_CARGA_UTIL", OracleDbType.Int32) { Value = vehiculo.CARGA_UTIL };
            bdParameters[14] = new OracleParameter("P_CAPACIDAD_PASAJERO", OracleDbType.Int32) { Value = vehiculo.CAPACIDAD_PASAJERO };
            bdParameters[15] = new OracleParameter("P_NUMERO_ASIENTOS", OracleDbType.Int32) { Value = vehiculo.NUMERO_ASIENTOS };
            bdParameters[16] = new OracleParameter("P_NUMERO_RUEDA", OracleDbType.Int32) { Value = vehiculo.NUMERO_RUEDA };
            bdParameters[17] = new OracleParameter("P_NUMERO_EJE", OracleDbType.Int32) { Value = vehiculo.NUMERO_EJE };
            bdParameters[18] = new OracleParameter("P_NUMERO_PUERTA", OracleDbType.Int32) { Value = vehiculo.NUMERO_PUERTA };
            bdParameters[19] = new OracleParameter("P_FECHA_INSCRIPCION", OracleDbType.Varchar2) { Value = "" };
            bdParameters[20] = new OracleParameter("P_CILINDRADA", OracleDbType.Varchar2) { Value = vehiculo.CILINDRADA };
            bdParameters[21] = new OracleParameter("P_OBSERVACION", OracleDbType.Varchar2) { Value = vehiculo.OBSERVACION };
            bdParameters[22] = new OracleParameter("P_PLACA", OracleDbType.Varchar2) { Value = vehiculo.PLACA };
            bdParameters[23] = new OracleParameter("P_ID_MARCA", OracleDbType.Int32) { Value = vehiculo.ID_MARCA };
            bdParameters[24] = new OracleParameter("P_ESTADO", OracleDbType.Int32) { Value = EnumEstado.Activo.ValorEntero() };
            bdParameters[25] = new OracleParameter("P_USU_REG", OracleDbType.Varchar2) { Value = vehiculo.USUARIO_REG };
            bdParameters[26] = new OracleParameter("P_VEHICULO", OracleDbType.Int32, direction: ParameterDirection.Output);
            return bdParameters;
        }
        #endregion

    }
}
