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
    public class DetalleSolicitudDAL
    {
        OracleConnection bdConn;
        string cadenaConexion = string.Empty;
        #region Constructor
        public DetalleSolicitudDAL(ref Object _bdConn)
        {
            _bdConn = Conexion.iniciar(ref bdConn, _bdConn);
            cadenaConexion = Configuracion.GetConectionSting("sConexionSISREGISTRO");
        }
        #endregion

        #region Crear Detalle Solicitud
        public ResultadoProcedimientoVM CrearDetalleSolicitud(DetalleSolicitudModelo detalleSolicitud)
        {
            ResultadoProcedimientoVM modelo = new ResultadoProcedimientoVM();
            try
            {
                using (var bdCmd = new OracleCommand("PKG_EXPEDIENTE.SP_INSERTAR_DETALLE_SOLICITUD", bdConn))
                {
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(ParametrosCrearDetalleSolicitud(detalleSolicitud));
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

        #region Parametros Crear Recibo
        private OracleParameter[] ParametrosCrearDetalleSolicitud(DetalleSolicitudModelo detalleSolicitud)
        {
            OracleParameter[] bdParameters = new OracleParameter[8];
            bdParameters[0] = new OracleParameter("P_IDEXPEDIENTE", OracleDbType.Int32) { Value = detalleSolicitud.ID_EXPEDIENTE };
            bdParameters[1] = new OracleParameter("P_DATO_REGISTRO", OracleDbType.Varchar2) { Value = detalleSolicitud.DATO_REGISTRO };
            bdParameters[2] = new OracleParameter("P_IDENTIDAD_BANCARIA", OracleDbType.Int32) { Value = detalleSolicitud.ID_ENTIDAD_BANCARIA };
            bdParameters[3] = new OracleParameter("P_DESCRIPCION", OracleDbType.Varchar2) { Value = detalleSolicitud.DESCRIPCION };
            bdParameters[4] = new OracleParameter("P_ESTADO", OracleDbType.Int32) { Value = EnumEstado.Activo.ValorEntero() };
            bdParameters[5] = new OracleParameter("P_USU_REG", OracleDbType.Varchar2) { Value = "joao"};
            bdParameters[6] = new OracleParameter("P_NUMERO_RECIBO", OracleDbType.Varchar2) { Value = detalleSolicitud.NUMERO_RECIBO };
            bdParameters[7] = new OracleParameter("P_DET_SOLICITUD", OracleDbType.Int32, direction: ParameterDirection.Output);
            return bdParameters;
        }
        #endregion
    }
}
