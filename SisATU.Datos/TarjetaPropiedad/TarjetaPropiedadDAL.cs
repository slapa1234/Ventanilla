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
    public class TarjetaPropiedadDAL
    {
        OracleConnection bdConn;
        string cadenaConexion = string.Empty;

        #region Constructor
        public TarjetaPropiedadDAL(ref Object _bdConn)
        {
            _bdConn = Conexion.iniciar(ref bdConn, _bdConn);
            cadenaConexion = Configuracion.GetConectionSting("sConexionSISREGISTRO");
        }
        #endregion

        #region Crear Tarjeta Propiedad
        public ResultadoProcedimientoVM CrearTarjetaPropiedad(TarjetaPropiedadModelo tarjetaPropiedad)
        {
            ResultadoProcedimientoVM modelo = new ResultadoProcedimientoVM();
            try
            {
                using (var bdCmd = new OracleCommand("PKG_VEHICULO.SP_INS_TARJ_PROPIEDAD", bdConn))
                {
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(ParametrosCrearTarjetaPropiedad(tarjetaPropiedad));
                    bdCmd.ExecuteNonQuery();
                    tarjetaPropiedad.ID_TARJETA_PROPIEDAD = int.Parse(bdCmd.Parameters["P_TARJETA_PROPIEDAD"].Value.ToString());

                    modelo.CodResultado = 1;
                    modelo.NomResultado = "Registro Correctamente";
                    modelo.CodAuxiliar = tarjetaPropiedad.ID_TARJETA_PROPIEDAD;
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
        private OracleParameter[] ParametrosCrearTarjetaPropiedad(TarjetaPropiedadModelo tarjetaPropiedad)
        {
            OracleParameter[] bdParameters = new OracleParameter[7];
            bdParameters[0] = new OracleParameter("P_VEHICULO", OracleDbType.Int32) { Value = tarjetaPropiedad.ID_VEHICULO };
            bdParameters[1] = new OracleParameter("P_DESDE", OracleDbType.Varchar2) { Value = tarjetaPropiedad.DESDE };
            bdParameters[2] = new OracleParameter("P_HASTA", OracleDbType.Varchar2) { Value = tarjetaPropiedad.HASTA };
            bdParameters[3] = new OracleParameter("P_NROTARJETA", OracleDbType.Varchar2) { Value = tarjetaPropiedad.NRO_TARJETA };
            bdParameters[4] = new OracleParameter("P_ESTADO", OracleDbType.Int32) { Value = EnumEstado.Activo.ValorEntero() };
            bdParameters[5] = new OracleParameter("P_USU_REG", OracleDbType.Varchar2) { Value = tarjetaPropiedad.USUARIO_REG };
            bdParameters[6] = new OracleParameter("P_TARJETA_PROPIEDAD", OracleDbType.Int32, direction: ParameterDirection.Output);
            return bdParameters;
        }
        #endregion

    }
}
