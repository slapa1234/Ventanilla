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
    public class EmpresaDAL
    {
        OracleConnection bdConn;
        string cadenaConexion = string.Empty;

        #region Constructor
        public EmpresaDAL(ref Object _bdConn)
        {
            _bdConn = Conexion.iniciar(ref bdConn, _bdConn);
            this.cadenaConexion = Configuracion.GetConectionSting("sConexionSISREGISTRO");
        }
        #endregion

        public EmpresaVM ConsultaRuc(string RUC)
        {
            EmpresaVM empresa = new EmpresaVM();
            SunatService obj = new SunatService();
            empresa = obj.ConsultaRUC2(RUC);
            return empresa;
        }

        public EmpresaVM BuscaEmpresaSTD(string RUC)
        {
            EmpresaVM empresa = new EmpresaVM();
            SunatService obj = new SunatService();
            empresa = obj.BuscaEmpresaSTD(RUC);
            return empresa;
        }


        public EmpresaVM CrearEmpresaSTD(EmpresaVM empresa)
        {
            EmpresaVM modelo = new EmpresaVM();
            SunatService obj = new SunatService();
            modelo = obj.CrearEmpresaSTD(empresa);
            return modelo;
        }


        #region Crear Empresa
        public ResultadoProcedimientoVM CrearEmpresa(EmpresaModelo empresa)
        {
            ResultadoProcedimientoVM modelo = new ResultadoProcedimientoVM();
            try
            {
                //using (var bdConn = new OracleConnection(cadenaConexion))
                //{
                using (var bdCmd = new OracleCommand("PKG_EMPRESA.SP_INSERTAR_EMPRESA", bdConn))
                {
                    bdCmd.CommandType = CommandType.StoredProcedure;
                    bdCmd.Parameters.AddRange(ParametrosCrearEmpresa(empresa));
                    //bdConn.Open();
                    bdCmd.ExecuteNonQuery();
                    modelo.CodResultado = 1;
                    modelo.NomResultado = "Registro Correctamente";
                }
                //}
            }
            catch (Exception ex)
            {
                modelo.CodResultado = 0;
                modelo.NomResultado = ex.Message;
            }
            return modelo;
        }
        #endregion

        #region Consultar Empresa
        public EmpresaVM ConsultarEmpresa(string RUC)
        {
            EmpresaVM empresa = new EmpresaVM();
            try
            {
                using (var bdConn = new OracleConnection(cadenaConexion))
                {
                    using (var bdCmd = new OracleCommand("PKG_EMPRESA.SP_CONSULTAR_EMPRESA", bdConn))
                    {
                        bdCmd.CommandType = CommandType.StoredProcedure;
                        bdCmd.Parameters.AddRange(ParametroConsultarEmpresa(RUC));
                        bdConn.Open();
                        using (var bdRd = bdCmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult))
                        {
                            if (bdRd.HasRows)
                            {
                                bdRd.Read();
                                if (!DBNull.Value.Equals(bdRd["RUC"])) { empresa.RUC = bdRd["RUC"].ValorCadena(); }
                                if (!DBNull.Value.Equals(bdRd["HASTA_FECHA"])) { empresa.FECHA_VENCIMIENTO_EXPEDIENTE = bdRd["HASTA_FECHA"].ValorCadena(); }
                                if(!DBNull.Value.Equals(bdRd["RAZON_SOCIAL"])) { empresa.RAZON_SOCIAL = bdRd["RAZON_SOCIAL"].ValorCadena(); }
                                empresa.ResultadoProcedimientoVM.CodResultado = 1;
                                empresa.ResultadoProcedimientoVM.NomResultado = "Cargo Correctamente";
                                //return empresa;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                empresa.ResultadoProcedimientoVM.CodAuxiliar = 0;
                empresa.ResultadoProcedimientoVM.NomResultado = ex.Message;
            }
            return empresa;
        }
        #endregion


        #region Parametro Buscar Empresa
        public OracleParameter[] ParametroConsultarEmpresa(string RUC)
        {
            OracleParameter[] bdParameters = new OracleParameter[2];
            bdParameters[0] = new OracleParameter("P_RUC", OracleDbType.Varchar2) { Value = RUC };
            bdParameters[1] = new OracleParameter("P_CURSOR", OracleDbType.RefCursor, direction: ParameterDirection.Output);
            return bdParameters;
        }
        #endregion

        #region Parametros Crear Recibo
        private OracleParameter[] ParametrosCrearEmpresa(EmpresaModelo empresa)
        {
            OracleParameter[] bdParameters = new OracleParameter[14];
            bdParameters[0] = new OracleParameter("P_RUC", OracleDbType.Varchar2) { Value = empresa.RUC };
            bdParameters[1] = new OracleParameter("P_RAZON_SOCIAL", OracleDbType.Varchar2) { Value = empresa.RAZON_SOCIAL };
            bdParameters[2] = new OracleParameter("P_DIRECCION_LEGAL", OracleDbType.Varchar2) { Value = empresa.DIRECCION_LEGAL };
            bdParameters[3] = new OracleParameter("P_TELEFONO1", OracleDbType.Varchar2) { Value = empresa.TELEFONO1 };
            bdParameters[4] = new OracleParameter("P_TELEFONO2", OracleDbType.Varchar2) { Value = empresa.TELEFONO2 };
            bdParameters[5] = new OracleParameter("P_MAIL", OracleDbType.Varchar2) { Value = empresa.MAIL };
            bdParameters[6] = new OracleParameter("P_CAPITAL_SOCIAL", OracleDbType.Int32) { Value = empresa.CAPITAL_SOCIAL };
            bdParameters[7] = new OracleParameter("P_FECHA_INSCRIPCION_EMPRESA", OracleDbType.Varchar2) { Value = "" };
            bdParameters[8] = new OracleParameter("P_OBSERVACION", OracleDbType.Varchar2) { Value = empresa.OBSERVACION };
            bdParameters[9] = new OracleParameter("P_COLOR_UNIFORME", OracleDbType.Varchar2) { Value = empresa.COLOR_UNIFORME };
            bdParameters[10] = new OracleParameter("P_TIPO_PERSONA", OracleDbType.Int32) { Value = empresa.ID_TIPO_PERSONA };
            bdParameters[11] = new OracleParameter("P_TIPO_CONTRIBUYENTE", OracleDbType.Int32) { Value = empresa.ID_TIPO_CONTRIBUYENTE };
            bdParameters[12] = new OracleParameter("P_ESTADO", OracleDbType.Int32) { Value = EnumEstado.Activo.ValorEntero() };
            bdParameters[13] = new OracleParameter("P_USU_REG", OracleDbType.Int32) { Value = empresa.USUARIO_REG };
            return bdParameters;
        }
        #endregion
    }
}
