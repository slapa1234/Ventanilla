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
    public class ReporteDAL
    {
        string cadenaConexion = string.Empty;

        #region Constructor
        public ReporteDAL()
        {
            cadenaConexion = Configuracion.GetConectionSting("sConexionSISREGISTRO");
        }
        #endregion

        #region TUC
        public TarjetaUnicaCirculacionVM getDatosTarjetaUnicaCirculacion(int idExpedienteHijo)
        {
            TarjetaUnicaCirculacionVM resultado = new TarjetaUnicaCirculacionVM();
            try
            {
                using (var bdConn = new OracleConnection(cadenaConexion))
                {
                    using (var bdCmd = new OracleCommand("PKG_REPORTE.SP_EMITIR_TUC", bdConn))
                    {
                        bdCmd.CommandType = CommandType.StoredProcedure;
                        bdCmd.Parameters.AddRange(ParametrosTarjetaUnicaCirculacion(idExpedienteHijo));
                        bdConn.Open();
                        using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                        {
                            if (bdRd.HasRows)
                            {
                                bdRd.Read();
                                resultado = new TarjetaUnicaCirculacionVM();
                                if (!DBNull.Value.Equals(bdRd["ID_TARJETA_CIRCULACION"])) { resultado.ID_TARJETA_CIRCULACION = (bdRd["ID_TARJETA_CIRCULACION"]).ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["NOMBRE_MODALIDAD_SERVICIO"])) { resultado.NOMBRE_MODALIDAD_SERVICIO = (bdRd["NOMBRE_MODALIDAD_SERVICIO"]).ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["PLACA"])) { resultado.PLACA = (bdRd["PLACA"]).ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["SERIE_MOTOR"])) { resultado.SERIE_MOTOR = (bdRd["SERIE_MOTOR"]).ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["NUMERO_ASIENTOS"])) { resultado.NUMERO_ASIENTOS = (bdRd["NUMERO_ASIENTOS"]).ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["NRO_TARJETA"])) { resultado.NRO_TARJETA = (bdRd["NRO_TARJETA"]).ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["NOMBRE_PROPIETARIO"])) { resultado.NOMBRE_PROPIETARIO = (bdRd["NOMBRE_PROPIETARIO"]).ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["NRO_DOCUMENTO"])) { resultado.NRO_DOCUMENTO = (bdRd["NRO_DOCUMENTO"]).ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["FECHA_IMPRESION"])) { resultado.FECHA_IMPRESION = (bdRd["FECHA_IMPRESION"]).ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["FECHA_REG"])) { resultado.FECHA_REG = (bdRd["FECHA_REG"]).ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["FECHA_VENCIMIENTO_DOCUMENTO"])) { resultado.FECHA_VENCIMIENTO_DOCUMENTO = (bdRd["FECHA_VENCIMIENTO_DOCUMENTO"]).ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["IDDOC"])) { resultado.IDDOC = (bdRd["IDDOC"]).ValorEntero(); }
                                if (!DBNull.Value.Equals(bdRd["DIRECCION"])) { resultado.DIRECCION = (bdRd["DIRECCION"]).ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["TELEFONO"])) { resultado.TELEFONO = (bdRd["TELEFONO"]).ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["ANIO_FABRICACION"])) { resultado.ANIO_FABRICACION = (bdRd["ANIO_FABRICACION"]).ValorEntero(); }
                                if (!DBNull.Value.Equals(bdRd["MARCA"])) { resultado.MARCA = (bdRd["MARCA"]).ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["MODELO"])) { resultado.MODELO = (bdRd["MODELO"]).ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["CLASE"])) { resultado.CLASE = (bdRd["CLASE"]).ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["CAPACIDAD_PASAJERO"])) { resultado.CAPACIDAD_PASAJERO = (bdRd["CAPACIDAD_PASAJERO"]).ValorEntero(); }
                                if (!DBNull.Value.Equals(bdRd["TIPO_DOCUMENTO"])) { resultado.TPODOCUMENTO = (bdRd["TIPO_DOCUMENTO"]).ValorCadena(); }
                                
                                return resultado;
                            }
                        }
                    }
                }
                return resultado;
            }
            catch (Exception)
            {
                //throw ex;
            }

            return resultado;
        }
        #endregion

        #region parametros TUC
        private OracleParameter[] ParametrosTarjetaUnicaCirculacion(int idExpedienteHijo)
        {
            OracleParameter[] bdParameters = new OracleParameter[2];
            bdParameters[0] = new OracleParameter("P_IDEXPE", OracleDbType.Int32) { Value = idExpedienteHijo };
            bdParameters[1] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            return bdParameters;
        }
        #endregion

        #region RESOLUCION
        public ReporteResolucionVM ReporteResolucion(int IDDOC_PADRE)
        {
            ReporteResolucionVM resultado = new ReporteResolucionVM();
            try
            {
                using (var bdConn = new OracleConnection(cadenaConexion))
                {
                    using (var bdCmd = new OracleCommand("PKG_REPORTE.SP_EXPORT_RESOLUCION", bdConn))
                    {
                        bdCmd.CommandType = CommandType.StoredProcedure;
                        bdCmd.Parameters.AddRange(ParametrosResolucion(IDDOC_PADRE));
                        bdConn.Open();
                        using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                        {
                            if (bdRd.HasRows)
                            {
                                bdRd.Read();
                                resultado = new ReporteResolucionVM();
                                if (!DBNull.Value.Equals(bdRd["ID_EXPEDIENTE_PADRE"])) { resultado.ID_EXPEDIENTE_PADRE = (bdRd["ID_EXPEDIENTE_PADRE"]).ValorEntero(); }
                                if (!DBNull.Value.Equals(bdRd["ID_EXPEDIENTE_HIJO"])) { resultado.ID_EXPEDIENTE_HIJO = (bdRd["ID_EXPEDIENTE_HIJO"]).ValorEntero(); }
                                if (!DBNull.Value.Equals(bdRd["FECHA_REG"])) { resultado.FECHA_REG = (bdRd["FECHA_REG"]).ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["PROPIETARIO"])) { resultado.PROPIETARIO = (bdRd["PROPIETARIO"]).ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["NUMERO_RECURRENTE"])) { resultado.NUMERO_RECURRENTE = (bdRd["NUMERO_RECURRENTE"]).ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["PLACA"])) { resultado.PLACA = (bdRd["PLACA"]).ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["NUMERO_RESOLUCION"])) { resultado.NUMERO_RESOLUCION = (bdRd["NUMERO_RESOLUCION"]).ValorCadena(); }
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

        #region parametros TUC
        private OracleParameter[] ParametrosResolucion(int IDDOC_PADRE)
        {
            OracleParameter[] bdParameters = new OracleParameter[2];
            bdParameters[0] = new OracleParameter("P_IDEXPE", OracleDbType.Int32) { Value = IDDOC_PADRE };
            bdParameters[1] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            return bdParameters;
        }
        #endregion

        #region Parametros Credencial
        private OracleParameter[] ParametrosCredencialOper(int idExpediente)
        {
            OracleParameter[] bdParameters = new OracleParameter[2];
            bdParameters[0] = new OracleParameter("P_IDEXPE", OracleDbType.Int32) { Value = idExpediente };
            bdParameters[1] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            return bdParameters;
        }
        #endregion

        #region Parametros Tarj_Credenciakl
        private OracleParameter[] ParametrosTajcredencialOper(int idExpediente)
        {
            OracleParameter[] bdParameters = new OracleParameter[2];
            bdParameters[0] = new OracleParameter("P_IDEXPE", OracleDbType.Int32) { Value = idExpediente };
            bdParameters[1] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            return bdParameters;
        }
        #endregion


        #region Parametros Credencial
        private OracleParameter[] ParametrosTajcredencialOper_taxi(int idExpediente)
        {
            OracleParameter[] bdParameters = new OracleParameter[2];
            bdParameters[0] = new OracleParameter("P_IDEXPE", OracleDbType.Int32) { Value = idExpediente };
            bdParameters[1] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            return bdParameters;
        }

        #endregion

        #region CREDENCIAL PDF
        public ConstanciaOperadorVM getDatosConstanciaOpe(int idExpediente)
        {
            ConstanciaOperadorVM resultado = new ConstanciaOperadorVM();
            try
            {
                using (var bdConn = new OracleConnection(cadenaConexion))
                {
                    using (var bdCmd = new OracleCommand("PKG_REPORTE.SP_EXPORT_CONST_SSTR", bdConn))
                    {
                        bdCmd.CommandType = CommandType.StoredProcedure;
                        bdCmd.Parameters.AddRange(ParametrosCredencialOper(idExpediente));
                        bdConn.Open();
                        using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                        {
                            if (bdRd.HasRows)
                            {
                                bdRd.Read();
                                resultado = new ConstanciaOperadorVM();
                                if (!DBNull.Value.Equals(bdRd["CODIGO"])) { resultado.CODIGO = (bdRd["CODIGO"]).ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["NOMBRE_OPERADOR"])) { resultado.NOMBRE_OPERADOR = (bdRd["NOMBRE_OPERADOR"]).ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["NUMERO_DOCUMENTO"])) { resultado.NUMERO_DOCUMENTO = (bdRd["NUMERO_DOCUMENTO"]).ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["NUMERO_CREDENCIAL"])) { resultado.NUMERO_CREDENCIAL = (bdRd["NUMERO_CREDENCIAL"]).ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["FECHA_EMISION"])) { resultado.FECHA_EMISION = (bdRd["FECHA_EMISION"]).ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["FECHA_VENCIMIENTO"])) { resultado.FECHA_VENCIMIENTO = (bdRd["FECHA_VENCIMIENTO"]).ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["TIPO_OPERADOR"])) { resultado.TIPO_OPERADOR = (bdRd["TIPO_OPERADOR"]).ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["MODALIDAD_SERVICIO"])) { resultado.MODALIDAD_SERVICIO = (bdRd["MODALIDAD_SERVICIO"]).ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["EMPRESA"])) { resultado.EMPRESA = (bdRd["EMPRESA"]).ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["ID_EXPEDIENTE"])) { resultado.ID_EXPEDIENTE = (bdRd["ID_EXPEDIENTE"]).ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["FEC_REGISTRO_EXPEDIENTE"])) { resultado.FEC_REGISTRO_EXPEDIENTE = (bdRd["FEC_REGISTRO_EXPEDIENTE"]).ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["TIPO_DOCUMENTO"])) { resultado.TIPO_DOC_OPERADOR = (bdRd["TIPO_DOCUMENTO"]).ValorCadena(); }

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



        #region CREDENCIAL TAXI
        public ConstanciaOperadorVM getDatosConstanciaOpe_taxi(int idExpediente)
        {
            ConstanciaOperadorVM resultado = new ConstanciaOperadorVM();
            try
            {
                using (var bdConn = new OracleConnection(cadenaConexion))
                {
                    using (var bdCmd = new OracleCommand("PKG_REPORTE.SP_EXPORT_CONST_SSTE", bdConn))
                    {
                        bdCmd.CommandType = CommandType.StoredProcedure;
                        bdCmd.Parameters.AddRange(ParametrosTajcredencialOper_taxi(idExpediente));
                        bdConn.Open();
                        using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                        {
                            if (bdRd.HasRows)
                            {
                                bdRd.Read();
                                resultado = new ConstanciaOperadorVM();
                                if (!DBNull.Value.Equals(bdRd["CODIGO"])) { resultado.CODIGO = (bdRd["CODIGO"]).ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["NOMBRE_OPERADOR"])) { resultado.NOMBRE_OPERADOR = (bdRd["NOMBRE_OPERADOR"]).ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["NRO_DOCUMENTO_OPERADOR"])) { resultado.NUMERO_DOCUMENTO = (bdRd["NRO_DOCUMENTO_OPERADOR"]).ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["NUMERO_CREDENCIAL"])) { resultado.NUMERO_CREDENCIAL = (bdRd["NUMERO_CREDENCIAL"]).ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["FECHA_EMISION"])) { resultado.FECHA_EMISION = (bdRd["FECHA_EMISION"]).ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["FECHA_VENCIMIENTO"])) { resultado.FECHA_VENCIMIENTO = (bdRd["FECHA_VENCIMIENTO"]).ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["TIPO_OPERADOR"])) { resultado.TIPO_OPERADOR = (bdRd["TIPO_OPERADOR"]).ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["TIPO_MODALIDAD"])) { resultado.MODALIDAD_SERVICIO = (bdRd["TIPO_MODALIDAD"]).ValorCadena(); }
                            
                                if (!DBNull.Value.Equals(bdRd["ID_EXPEDIENTE"])) { resultado.ID_EXPEDIENTE = (bdRd["ID_EXPEDIENTE"]).ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["FEC_REGISTRO_EXPEDIENTE"])) { resultado.FEC_REGISTRO_EXPEDIENTE = (bdRd["FEC_REGISTRO_EXPEDIENTE"]).ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["TIPO_DOCUMENTO"])) { resultado.TIPO_DOC_OPERADOR = (bdRd["TIPO_DOCUMENTO"]).ValorCadena(); }
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


        #region Tarjeta Credencial

        public TarjetaCredencialOperadorVM getDatosTarje_CredencialOpe(int idExpediente)
        {
            TarjetaCredencialOperadorVM resultado = new TarjetaCredencialOperadorVM();
            try
            {
                using (var bdConn = new OracleConnection(cadenaConexion))
                {
                    using (var bdCmd = new OracleCommand("PKG_REPORTE.SP_IMP_CREDENCIAL", bdConn))
                    {
                        bdCmd.CommandType = CommandType.StoredProcedure;
                        bdCmd.Parameters.AddRange(ParametrosTajcredencialOper(idExpediente));
                        bdConn.Open();
                        using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                        {
                            if (bdRd.HasRows)
                            {
                                bdRd.Read();
                                resultado = new TarjetaCredencialOperadorVM();
                                if (!DBNull.Value.Equals(bdRd["NOMBRE_OPERADOR"])) { resultado.NOMBRE_OPERADOR = (bdRd["NOMBRE_OPERADOR"]).ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["NUMERO_LICENCIA"])) { resultado.NUMERO_LICENCIA = (bdRd["NUMERO_LICENCIA"]).ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["NUMERO_DOCUMENTO"])) { resultado.NUMERO_DOCUMENTO = (bdRd["NUMERO_DOCUMENTO"]).ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["NUMERO_CREDENCIAL"])) { resultado.NUMERO_CREDENCIAL = (bdRd["NUMERO_CREDENCIAL"]).ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["FECHA_EMISION"])) { resultado.FECHA_EMISION = (bdRd["FECHA_EMISION"]).ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["FECHA_VENCIMIENTO"])) { resultado.FECHA_VENCIMIENTO = (bdRd["FECHA_VENCIMIENTO"]).ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["TIPO_OPERADOR"])) { resultado.TIPO_OPERADOR = (bdRd["TIPO_OPERADOR"]).ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["MODALIDAD_SERVICIO"])) { resultado.MODALIDAD_SERVICIO = (bdRd["MODALIDAD_SERVICIO"]).ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["EMPRESA"])) { resultado.EMPRESA = (bdRd["EMPRESA"]).ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["ID_EXPEDIENTE"])) { resultado.ID_EXPEDIENTE = (bdRd["ID_EXPEDIENTE"]).ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["FOTO_OPERADOR"])) { resultado.FOTO_OPERADOR = (bdRd["FOTO_OPERADOR"]).ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["TIPO_DOCUMENTO"])) { resultado.TIPO_DOCUMENTO = (bdRd["TIPO_DOCUMENTO"]).ValorCadena(); }
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



        #region Tarjeta Credencial taxi

        public TarjetaCredencialOperadorVM getDatosTarje_CredencialOpe_taxi(int idExpediente)
        {
            TarjetaCredencialOperadorVM resultado = new TarjetaCredencialOperadorVM();
            try
            {
                using (var bdConn = new OracleConnection(cadenaConexion))
                {
                    using (var bdCmd = new OracleCommand("PKG_REPORTE.SP_IMP_CREDENCIAL_SSTE", bdConn))
                    {
                        bdCmd.CommandType = CommandType.StoredProcedure;
                        bdCmd.Parameters.AddRange(ParametrosTajcredencialOper(idExpediente));
                        bdConn.Open();
                        using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                        {
                            if (bdRd.HasRows)
                            {
                                bdRd.Read();
                                resultado = new TarjetaCredencialOperadorVM();
                                if (!DBNull.Value.Equals(bdRd["NOMBRE_OPERADOR"])) { resultado.NOMBRE_OPERADOR = (bdRd["NOMBRE_OPERADOR"]).ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["NUMERO_LICENCIA"])) { resultado.NUMERO_LICENCIA = (bdRd["NUMERO_LICENCIA"]).ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["NUMERO_DOCUMENTO"])) { resultado.NUMERO_DOCUMENTO = (bdRd["NUMERO_DOCUMENTO"]).ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["NUMERO_CREDENCIAL"])) { resultado.NUMERO_CREDENCIAL = (bdRd["NUMERO_CREDENCIAL"]).ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["FECHA_EMISION"])) { resultado.FECHA_EMISION = (bdRd["FECHA_EMISION"]).ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["FECHA_VENCIMIENTO"])) { resultado.FECHA_VENCIMIENTO = (bdRd["FECHA_VENCIMIENTO"]).ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["TIPO_OPERADOR"])) { resultado.TIPO_OPERADOR = (bdRd["TIPO_OPERADOR"]).ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["MODALIDAD_SERVICIO"])) { resultado.MODALIDAD_SERVICIO = (bdRd["MODALIDAD_SERVICIO"]).ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["EMPRESA"])) { resultado.EMPRESA = (bdRd["EMPRESA"]).ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["ID_EXPEDIENTE"])) { resultado.ID_EXPEDIENTE = (bdRd["ID_EXPEDIENTE"]).ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["FOTO_OPERADOR"])) { resultado.FOTO_OPERADOR = (bdRd["FOTO_OPERADOR"]).ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["TIPO_DOCUMENTO"])) { resultado.TIPO_DOCUMENTO = (bdRd["TIPO_DOCUMENTO"]).ValorCadena(); }
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


        #region Parametros Conductores
        private OracleParameter[] ParametrosConductores(int idExpediente)
        {
            OracleParameter[] bdParameters = new OracleParameter[2];
            bdParameters[0] = new OracleParameter("P_IDEXPE", OracleDbType.Int32) { Value = idExpediente };
            bdParameters[1] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            return bdParameters;
        }

        #endregion

        #region CONDUCTORES PDF
        public List<ConstanciaOperadorVM>  getDatosConductores(int idExpediente)
        {
           
            List<ConstanciaOperadorVM> resultado = new List<ConstanciaOperadorVM>();
            try
            {
                using (var bdConn = new OracleConnection(cadenaConexion))
                {
                    using (var bdCmd = new OracleCommand("PKG_REPORTE.SP_IMP_OPERADORES", bdConn))
                    {
                        bdCmd.CommandType = CommandType.StoredProcedure;
                        bdCmd.Parameters.AddRange(ParametrosConductores(idExpediente));
                        bdConn.Open();
                        using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                        {
                            if (bdRd.HasRows)
                            {

                                while (bdRd.Read())
                                {
                                    var item = new ConstanciaOperadorVM();
                                    if (!DBNull.Value.Equals(bdRd["TIPO_DOCUMENTO"])) { item.TIPO_DOC_OPERADOR = (bdRd["TIPO_DOCUMENTO"]).ValorCadena(); }
                                    if (!DBNull.Value.Equals(bdRd["NUMERO_DOCUMENTO"])) { item.NUMERO_DOCUMENTO = (bdRd["NUMERO_DOCUMENTO"]).ValorCadena(); }
                                    if (!DBNull.Value.Equals(bdRd["NOMBRE_OPERADOR"])) { item.NOMBRE_OPERADOR = (bdRd["NOMBRE_OPERADOR"]).ValorCadena(); }
                                    if (!DBNull.Value.Equals(bdRd["TIPO_OPERADOR"])) { item.TIPO_OPERADOR = (bdRd["TIPO_OPERADOR"]).ValorCadena(); }

                                    resultado.Add(item);
                                }
  
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

        #region Parametros Padron nuevo
        private OracleParameter[] Parametrospadron(int idExpediente)
        {
            OracleParameter[] bdParameters = new OracleParameter[2];
            bdParameters[0] = new OracleParameter("P_IDEXPE", OracleDbType.Int32) { Value = idExpediente };
            bdParameters[1] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            return bdParameters;
        }

        #endregion

        #region CONDUCTORES PDF
        public BackOfficeVM getDatosPadron(int idExpediente)
        {

            BackOfficeVM resultado = new BackOfficeVM();
            try
            {
                using (var bdConn = new OracleConnection(cadenaConexion))
                {
                    using (var bdCmd = new OracleCommand("PKG_REPORTE.SP_IMP_PADRON", bdConn))
                    {
                        bdCmd.CommandType = CommandType.StoredProcedure;
                        bdCmd.Parameters.AddRange(Parametrospadron(idExpediente));
                        bdConn.Open();
                        using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                        {
                            if (bdRd.HasRows)
                            {

                                while (bdRd.Read())
                                {
                                  
                                    if (!DBNull.Value.Equals(bdRd["TRAMITE"])) { resultado.TRAMITE = (bdRd["TRAMITE"]).ValorCadena(); }
                                    if (!DBNull.Value.Equals(bdRd["MODALIDAD"])) { resultado.MODALIDAD_SERVICIO = (bdRd["MODALIDAD"]).ValorCadena(); }
                                    if (!DBNull.Value.Equals(bdRd["FECHA_REG"])) { resultado.FECHAREG = (bdRd["FECHA_REG"]).ValorCadena(); }
                                    if (!DBNull.Value.Equals(bdRd["RAZON_SOCIAL"])) { resultado.PERSONA = (bdRd["RAZON_SOCIAL"]).ValorCadena(); }
                                    if (!DBNull.Value.Equals(bdRd["ID_EXPEDIENTE"])) { resultado.ID_EXPEDIENTE = (bdRd["ID_EXPEDIENTE"]).ValorEntero(); }
                                  
                                }

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
        #region Parametros  Renovacion Autorizacion
        private OracleParameter[] ParametrosReo_Auto(int idExpediente)
        {
            OracleParameter[] bdParameters = new OracleParameter[2];
            bdParameters[0] = new OracleParameter("P_IDEXPE", OracleDbType.Int32) { Value = idExpediente };
            bdParameters[1] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            return bdParameters;
        }

        #endregion
        #region Renovacion Autorizacion
        public ConstanciaOperadorVM getDatosRenoAutorizacion(int idExpediente)
        {
            ConstanciaOperadorVM resultado = new ConstanciaOperadorVM();
            try
            {
                using (var bdConn = new OracleConnection(cadenaConexion))
                {
                    using (var bdCmd = new OracleCommand("PKG_REPORTE.SP_RENO_AUTORIZACION", bdConn))
                    {
                        bdCmd.CommandType = CommandType.StoredProcedure;
                        bdCmd.Parameters.AddRange(ParametrosReo_Auto(idExpediente));
                        bdConn.Open();
                        using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                        {
                            if (bdRd.HasRows)
                            {
                                bdRd.Read();
                                resultado = new ConstanciaOperadorVM();
                                if (!DBNull.Value.Equals(bdRd["NUM_RESOLUCION"])) { resultado.CODIGO = (bdRd["NUM_RESOLUCION"]).ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["FECHA"])) { resultado.FEC_REGISTRO_EXPEDIENTE = (bdRd["FECHA"]).ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["NUM_EXPEDIENTE"])) { resultado.ID_EXPEDIENTE = (bdRd["NUM_EXPEDIENTE"]).ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["EMPRESA"])) { resultado.EMPRESA = (bdRd["EMPRESA"]).ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["MODALIDAD_SERVICIO"])) { resultado.MODALIDAD_SERVICIO = (bdRd["MODALIDAD_SERVICIO"]).ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["CANT_ANIOS"])) { resultado.CANT_ANIOS = (bdRd["CANT_ANIOS"]).ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["PERIODO_NOMBRE"])) { resultado.PERIODO_NOMBRE = (bdRd["PERIODO_NOMBRE"]).ValorCadena(); }

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
