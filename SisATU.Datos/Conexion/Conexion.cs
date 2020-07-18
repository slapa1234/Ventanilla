//using Oracle.DataAccess.Client;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Datos
{
    public class Conexion
    {
        public static OracleConnection iniciar(ref OracleConnection bdConn, Object _bdConn = null)
        {
            if (bdConn == null)
            {
                if (_bdConn != null)
                {
                    bdConn = (OracleConnection)_bdConn;
                }
                else
                {
                    bdConn = new OracleConnection(Configuracion.GetConectionSting("sConexionSISREGISTRO"));
                }
            }
            if (bdConn.State == ConnectionState.Closed)
            {
                bdConn.Open();
            }
            return bdConn;
        }

        public static void finalizar(ref Object bdConn)
        {
            try
            {
                if (bdConn != null)
                {
                    if (((OracleConnection)bdConn).State == ConnectionState.Open)
                    {
                        ((OracleConnection)bdConn).Close();
                        ((OracleConnection)bdConn).Dispose();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
