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
    public class VehiculoAseguradoraDAL
    {
        OracleConnection bdConn;
        //string cadenaConexion = string.Empty;

        #region Constructor
        public VehiculoAseguradoraDAL(ref Object _bdConn)
        {
            _bdConn = Conexion.iniciar(ref bdConn, _bdConn);
            //cadenaConexion = Configuracion.GetConectionSting("sConexionSISREGISTRO");
        }
        #endregion

        #region Crear Aseguradora Vehiculo
        public ResultadoProcedimientoVM CrearVehiculoAseguradora(VehiculoAseguradoraModelo VehiculoAseguradora)
        {
            ResultadoProcedimientoVM modelo = new ResultadoProcedimientoVM();
            try
            {
                using (var bdCmd = new OracleCommand("PKG_VEHICULO.SP_INS_ASEG_VEHICULO", bdConn))
                {
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(ParametrosCrearVehiculoAseguradora(VehiculoAseguradora));
                    bdCmd.ExecuteNonQuery();
                    VehiculoAseguradora.ID_VEHICULO_ASEGURADORA = int.Parse(bdCmd.Parameters["P_VEHICULO_ASEGURADORA"].Value.ToString());

                    modelo.CodResultado = 1;
                    modelo.NomResultado = "Registro Correctamente";
                    modelo.CodAuxiliar = VehiculoAseguradora.ID_VEHICULO_ASEGURADORA;
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

        #region Parametros Crear Aseguradora Vehiculo
        private OracleParameter[] ParametrosCrearVehiculoAseguradora(VehiculoAseguradoraModelo VehiculoAseguradora)
        {
            OracleParameter[] bdParameters = new OracleParameter[9];
            bdParameters[0] = new OracleParameter("P_NOMBRE_ASEGURADORA", OracleDbType.Varchar2) { Value = VehiculoAseguradora.NOMBRE_ASEGURADORA };
            bdParameters[1] = new OracleParameter("P_TIPO_SEGURO", OracleDbType.Int32) { Value = VehiculoAseguradora.ID_TIPO_SEGURO };
            bdParameters[2] = new OracleParameter("P_VEHICULO", OracleDbType.Int32) { Value = VehiculoAseguradora.ID_VEHICULO };
            bdParameters[3] = new OracleParameter("P_POLIZA", OracleDbType.Varchar2) { Value = VehiculoAseguradora.POLIZA };
            bdParameters[4] = new OracleParameter("P_FEC_INI_VIGENCIA", OracleDbType.Varchar2) { Value = VehiculoAseguradora.FEC_INI_VIGENCIA.ValorFechaCorta() };
            bdParameters[5] = new OracleParameter("P_FEC_FIN_VIGENCIA", OracleDbType.Varchar2) { Value = VehiculoAseguradora.FEC_FIN_VIGENCIA.ValorFechaCorta() };
            bdParameters[6] = new OracleParameter("P_ESTADO", OracleDbType.Int32) { Value = EnumEstado.Activo.ValorEntero() };
            bdParameters[7] = new OracleParameter("P_USU_REG", OracleDbType.Varchar2) { Value = VehiculoAseguradora.USUARIO_REG };
            bdParameters[8] = new OracleParameter("P_VEHICULO_ASEGURADORA", OracleDbType.Int32, direction: ParameterDirection.Output);
            return bdParameters;
        }
        #endregion
    }
}
