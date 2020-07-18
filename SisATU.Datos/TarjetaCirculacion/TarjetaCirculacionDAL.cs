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
    public class TarjetaCirculacionDAL
    {
        OracleConnection bdConn;
        string cadenaConexion = string.Empty;

        #region Constructor
        public TarjetaCirculacionDAL(ref Object _bdConn)
        {
            _bdConn = Conexion.iniciar(ref bdConn, _bdConn);
            cadenaConexion = Configuracion.GetConectionSting("sConexionSISREGISTRO");
        }
        #endregion

        #region Crear Tarjeta Propiedad
        public ResultadoProcedimientoVM CrearTarjetaCirculacion(TarjetaCirculacionModelo tarjetaCirculacion)
        {
            ResultadoProcedimientoVM modelo = new ResultadoProcedimientoVM();
            try
            {
                using (var bdCmd = new OracleCommand("PKG_RESOLUCION.SP_INS_TARJ_CIRCULACION", bdConn))
                {
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(ParametrosCrearTarjetaCirculacion(tarjetaCirculacion));
                    bdCmd.ExecuteNonQuery();
                    tarjetaCirculacion.ID_TARJETA_CIRCULACION = int.Parse(bdCmd.Parameters["P_TARJETA_CIRCULACION"].Value.ToString());

                    modelo.CodResultado = 1;
                    modelo.NomResultado = "Registro Correctamente";
                    modelo.CodAuxiliar = tarjetaCirculacion.ID_TARJETA_CIRCULACION;
                }
            }
            catch (Exception ex)
            {
                modelo.CodResultado = 0;
                modelo.NomResultado = ex.Message;
            }
            return modelo;
        }

        public ResultadoProcedimientoVM CrearDuplicadoTUC(TarjetaCirculacionModelo tarjetaCirculacion)
        {
            ResultadoProcedimientoVM modelo = new ResultadoProcedimientoVM();
            try
            {
                using (var bdCmd = new OracleCommand("PKG_RESOLUCION.SP_INS_DUP_TARJ_CIRCULACION", bdConn))
                {
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(ParametrosCrearDuplicadoTUC(tarjetaCirculacion));
                    bdCmd.ExecuteNonQuery();
                    tarjetaCirculacion.ID_TARJETA_CIRCULACION = int.Parse(bdCmd.Parameters["P_TARJETA_CIRCULACION"].Value.ToString());

                    modelo.CodResultado = 1;
                    modelo.NomResultado = "Registro Correctamente";
                    modelo.CodAuxiliar = tarjetaCirculacion.ID_TARJETA_CIRCULACION;
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

        #region Parametros Tarjeta Propiedad
        private OracleParameter[] ParametrosCrearTarjetaCirculacion(TarjetaCirculacionModelo tarjetaCirculacion)
        {
            OracleParameter[] bdParameters = new OracleParameter[7];
            bdParameters[0] = new OracleParameter("P_EXPEDIENTE", OracleDbType.Int32) { Value = tarjetaCirculacion.ID_EXPEDIENTE };
            bdParameters[1] = new OracleParameter("P_FECHA_IMPRESION", OracleDbType.Varchar2) { Value = tarjetaCirculacion.FECHA_IMPRESION };
            bdParameters[2] = new OracleParameter("P_ANIO", OracleDbType.Int32) { Value = tarjetaCirculacion.ANIO };
            bdParameters[3] = new OracleParameter("P_FECHA_VENCIMIENTO_DOCUMENTO", OracleDbType.Varchar2) { Value = tarjetaCirculacion.FECHA_VENCIMIENTO_DOCUMENTO };
            bdParameters[4] = new OracleParameter("P_ESTADO", OracleDbType.Int32) { Value = EnumEstado.Activo.ValorEntero() };
            bdParameters[5] = new OracleParameter("P_USUARIO_REG", OracleDbType.Varchar2) { Value = tarjetaCirculacion.USUARIO_REG };
            bdParameters[6] = new OracleParameter("P_TARJETA_CIRCULACION", OracleDbType.Int32, direction: ParameterDirection.Output);
            return bdParameters;
        }

        private OracleParameter[] ParametrosCrearDuplicadoTUC(TarjetaCirculacionModelo tarjetaCirculacion)
        {
            OracleParameter[] bdParameters = new OracleParameter[] { 
                new OracleParameter("P_EXPEDIENTE", OracleDbType.Int32) { Value = tarjetaCirculacion.ID_EXPEDIENTE },
                new OracleParameter("P_FECHA_IMPRESION", OracleDbType.Varchar2) { Value = tarjetaCirculacion.FECHA_IMPRESION },
                new OracleParameter("P_ESTADO", OracleDbType.Int32) { Value = EnumEstado.Activo.ValorEntero() },
                new OracleParameter("P_USUARIO_REG", OracleDbType.Varchar2) { Value = tarjetaCirculacion.USUARIO_REG },
                new OracleParameter("P_TARJETA_CIRCULACION", OracleDbType.Int32, direction: ParameterDirection.Output)
            };
            return bdParameters;
        }
        #endregion
    }
}
