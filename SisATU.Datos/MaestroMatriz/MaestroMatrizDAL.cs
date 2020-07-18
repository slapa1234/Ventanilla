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
    public class MaestroMatrizDAL
    {
        string cadenaConexion = string.Empty;
        public MaestroMatrizDAL()
        {
            cadenaConexion = Configuracion.GetConectionSting("sConexionSISREGISTRO");
        }
        public MaestroMatrizVM ConsultaMaestroMatriz(int ID_TIPO_PERSONA, int ANIO_PERIODO, int ID_MODALIDAD_SERVICIO, string ANIO_FABRICACION)
        {
            MaestroMatrizVM maeastroMatriz = new MaestroMatrizVM();
            try
            {
                using (var bdConn = new OracleConnection(cadenaConexion))
                {
                    using (var bdCmd = new OracleCommand("PKG_MATRIZ_CONSULTA.SP_CONSULTA_VEHICULO", bdConn))
                    {
                        bdCmd.CommandType = CommandType.StoredProcedure;
                        bdCmd.Parameters.AddRange(ParametrosConsultaMaestroMatriz(ID_TIPO_PERSONA, ANIO_PERIODO, ID_MODALIDAD_SERVICIO, ANIO_FABRICACION));
                        bdConn.Open();
                        bdCmd.ExecuteNonQuery();
                        maeastroMatriz.ANIOS = int.Parse(bdCmd.Parameters["P_ANIO"].Value.ToString());
                        bdConn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                //resultado = 0;
            }
            return maeastroMatriz;
        }

        #region 
        public OracleParameter[] ParametrosConsultaMaestroMatriz(int ID_TIPO_PERSONA = 0, int ANIO_PERIODO = 0, int ID_MODALIDAD_SERVICIO = 0, string ANIO_FABRICACION = "")
        {
            OracleParameter[] bdParameters = new OracleParameter[] {
                new OracleParameter("P_TIPO_PERSONA", OracleDbType.Int32) { Value = ID_TIPO_PERSONA },
                new OracleParameter("P_PERIODO", OracleDbType.Int32) { Value = ANIO_PERIODO },
                new OracleParameter("P_MODALIDAD_SERVICIO", OracleDbType.Int32) { Value = ID_MODALIDAD_SERVICIO },
                new OracleParameter("P_ANIO_FABRICACION", OracleDbType.Varchar2) { Value = "01/01/" + ANIO_FABRICACION },
                new OracleParameter("P_RESULTADO", OracleDbType.Int32, direction: ParameterDirection.Output),
                new OracleParameter("P_ANIO", OracleDbType.Int32, direction: ParameterDirection.Output),
            };
            return bdParameters;
        }
        #endregion
    }
}
