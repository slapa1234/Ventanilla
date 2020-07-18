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
    public class ResolucionDAL
    {
        OracleConnection bdConn;
        string cadenaConexion = string.Empty;
        #region Constructor
        public ResolucionDAL(ref Object _bdConn)
        {
            //mira aca me sale error 
            _bdConn = Conexion.iniciar(ref bdConn, _bdConn);
            cadenaConexion = Configuracion.GetConectionSting("sConexionSISREGISTRO");
        }
        #endregion

        #region Crear Resolucion
        public ResultadoProcedimientoVM CrearResolucion(ResolucionModelo resolucion)
        {
            ResultadoProcedimientoVM modelo = new ResultadoProcedimientoVM();
            try
            {
                using (var bdCmd = new OracleCommand("PKG_RESOLUCION.SP_INSERTAR_RESOLUCION", bdConn))
                {
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(ParametrosCrearResolucion(resolucion));
                    bdCmd.ExecuteNonQuery();
                    //resolucion.ID_RECIBO = int.Parse(bdCmd.Parameters["P_RECIBO"].Value.ToString());

                    modelo.CodResultado = 1;
                    modelo.NomResultado = "Registro Correctamente";
                    modelo.CodAuxiliar = int.Parse(bdCmd.Parameters["P_RESOLUCION"].Value.ToString());
                    //modelo.CodAuxiliar = recibo.ID_RECIBO;
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


        #region Parametros Crear Recibo
        private OracleParameter[] ParametrosCrearResolucion(ResolucionModelo resolucion)
        {
            OracleParameter[] bdParameters = new OracleParameter[9];
            bdParameters[0] = new OracleParameter("P_TIPO_AUTORIZACION", OracleDbType.Int32) { Value = resolucion.ID_TIPO_AUTORIZACION };
            bdParameters[1] = new OracleParameter("P_FECHA_AUTORIZACION", OracleDbType.Varchar2) { Value = resolucion.FECHA_AUTORIZACION };
            bdParameters[2] = new OracleParameter("P_FECHA_VIGENCIA", OracleDbType.Varchar2) { Value = resolucion.FECHA_VIGENCIA };
            bdParameters[3] = new OracleParameter("P_TIPO_RESOLUCION", OracleDbType.Int32) { Value = resolucion.ID_TIPO_RESOLUCION };
            bdParameters[4] = new OracleParameter("P_FECHA_NOTIFICACION", OracleDbType.Varchar2) { Value = resolucion.FECHA_NOTIFICACION };
            bdParameters[5] = new OracleParameter("P_ASUNTO", OracleDbType.Varchar2) { Value = resolucion.ASUNTO };
            bdParameters[6] = new OracleParameter("P_ESTADO", OracleDbType.Int32) { Value = EnumEstado.Activo.ValorEntero() };
            bdParameters[7] = new OracleParameter("P_USUARIO_REG", OracleDbType.Varchar2) { Value = resolucion.USUARIO_REG };
            //bdParameters[7] = new OracleParameter("P_MODALIDAD_SERVICIO", OracleDbType.Int32) { Value = resolucion.ID_MOVILIDAD_SERVICIO };
            bdParameters[8] = new OracleParameter("P_RESOLUCION", OracleDbType.Int32, direction: ParameterDirection.Output);
            return bdParameters;
        }
        #endregion
    }
}