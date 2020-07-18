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
    public class VehiculoCITVDAL
    {
        OracleConnection bdConn;
        //string cadenaConexion = string.Empty;

        #region Constructor
        public VehiculoCITVDAL(ref Object _bdConn)
        {
            _bdConn = Conexion.iniciar(ref bdConn, _bdConn);
            //cadenaConexion = Configuracion.GetConectionSting("sConexionSISREGISTRO");
        }
        #endregion
        public VehiculoCITVVM ConsultaCITV(string nroPlaca)
        {
            VehiculoCITVVM VehiculoCITV = new VehiculoCITVVM();
            CitvService obj = new CitvService();
            VehiculoCITV = obj.ConsultaCITV(nroPlaca);
            return VehiculoCITV;
        }

        #region Crear Aseguradora Vehiculo
        public ResultadoProcedimientoVM CrearVehiculoCITV(VehiculoCITVModelo VehiculoCITV)
        {
            ResultadoProcedimientoVM modelo = new ResultadoProcedimientoVM();
            try
            {
                using (var bdCmd = new OracleCommand("PKG_VEHICULO.SP_INS_CITV_VEHICULO", bdConn))
                {
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(ParametrosCrearVehiculoCITV(VehiculoCITV));
                    bdCmd.ExecuteNonQuery();
                    VehiculoCITV.ID_VEHICULO_CITV = int.Parse(bdCmd.Parameters["P_VEHICULO_CITV"].Value.ToString());

                    modelo.CodResultado = 1;
                    modelo.NomResultado = "Registro Correctamente";
                    modelo.CodAuxiliar = VehiculoCITV.ID_VEHICULO_CITV;
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
        private OracleParameter[] ParametrosCrearVehiculoCITV(VehiculoCITVModelo VehiculoCITV)
        {
            OracleParameter[] bdParameters = new OracleParameter[10];
            bdParameters[0] = new OracleParameter("P_VEHICULO", OracleDbType.Int32) { Value = VehiculoCITV.ID_VEHICULO };
            bdParameters[1] = new OracleParameter("P_CERTIFICADORA_CITV", OracleDbType.Varchar2) { Value = VehiculoCITV.CERTIFICADORA_CITV };
            bdParameters[2] = new OracleParameter("P_NRO_CERTIFICADO", OracleDbType.Varchar2) { Value = VehiculoCITV.NRO_CERTIFICADO };
            bdParameters[3] = new OracleParameter("P_FECHA_CERTIFICADO", OracleDbType.Varchar2) { Value = VehiculoCITV.FECHA_CERTIFICADO.ValorFechaCorta() };
            bdParameters[4] = new OracleParameter("P_FECHA_VENCIMIENTO", OracleDbType.Varchar2) { Value = VehiculoCITV.FECHA_VENCIMIENTO.ValorFechaCorta() };
            bdParameters[5] = new OracleParameter("P_RESULTADO", OracleDbType.Varchar2) { Value = VehiculoCITV.RESULTADO };
            bdParameters[6] = new OracleParameter("P_ESTADO_CITV", OracleDbType.Varchar2) { Value = VehiculoCITV.ESTADO_CITV };
            bdParameters[7] = new OracleParameter("P_ESTADO", OracleDbType.Int32) { Value = EnumEstado.Activo.ValorEntero() };
            bdParameters[8] = new OracleParameter("P_USU_REG", OracleDbType.Varchar2) { Value = VehiculoCITV.USUARIO_REG };
            bdParameters[9] = new OracleParameter("P_VEHICULO_CITV", OracleDbType.Int32, direction: ParameterDirection.Output);
            return bdParameters;
        }
        #endregion 
    }
}
