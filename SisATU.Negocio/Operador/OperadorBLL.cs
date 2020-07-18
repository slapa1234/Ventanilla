using SisATU.Base;
using SisATU.Datos;
using SisATU.Base.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net.Http;
using SisATU.Base.Enumeradores;

namespace SisATU.Negocio
{
    public class OperadorBLL
    {
        ParametroDAL obj = new ParametroDAL();
        OperadorDAL operadorDAL;
        private Object bdConn;
        public OperadorBLL()
        {
            operadorDAL = new OperadorDAL(ref bdConn);
        }

        public ResultadoProcedimientoVM CrearOperador(OperadorModelo modelo)
        {
            var resultado = operadorDAL.CrearOperador(modelo);
            if (resultado.CodResultado == 1)
            {
                if (modelo.FOTO_BASE64.Length > 1000)
                {
                    var nombreFoto = "foto_operador_" + resultado.CodAuxiliar.ToString() + ".jpg";
                    var rptaGuardaFoto = guardarFotoOperador(modelo.FOTO_BASE64, nombreFoto);
                    if (rptaGuardaFoto.CodResultado == 1)
                    {
                        var resultadoActualizaFoto = actualizaFotoOperador(resultado.CodAuxiliar, nombreFoto);
                    }
                }
            }
            return resultado;
        }

        public ResultadoProcedimientoVM guardarFotoOperador(String base64Foto, string nombreFoto)
        {
            ResultadoProcedimientoVM respuesta = new ResultadoProcedimientoVM();
            try
            {
                var bytes = Convert.FromBase64String(base64Foto);
                string filePath = "~/Adjunto/foto_operador/" + nombreFoto;
                System.IO.File.WriteAllBytes(System.Web.HttpContext.Current.Server.MapPath(filePath), bytes);
                respuesta.CodResultado = 1;
                respuesta.NomResultado = "Creo la foto correctamente";
            }
            catch (Exception ex)
            {
                respuesta.CodResultado = 0;
                respuesta.NomResultado = ex.Message;
            }
            return respuesta;
        }

        public ResultadoProcedimientoVM actualizaFotoOperador(int idOperador, string nombreOperador)
        {
            return operadorDAL.actualizaFotoOperador(idOperador, nombreOperador);
        }

        public ResultadoProcedimientoVM EnlazarOperadorEmpresa(OperadorEmpresaModelo Modelo)
        {
            return operadorDAL.EnlazarOperadorEmpresa(Modelo);
        }
        public OperadorVM BuscarOperadorCredencial(string NroDocumento)
        {
            return operadorDAL.BuscarOperadorCredencial(NroDocumento);
        }

        public List<OperadorVM> consultarListaOperador(string RUC)
        {
            var operador = operadorDAL.consultarListaOperador(RUC);
            return operador;
        }

        public OperadorVM consultaDatosPersonalesYLic(string tipoDocumento, string nroDocumento, int ID_TIPO_MODALIDAD, int ID_TIPO_PERSONA, int ID_PROCEDIMIENTO)
        {
            OperadorVM operador = new OperadorVM();
            var resultado = operadorDAL.BuscarOperador("", nroDocumento, ID_TIPO_MODALIDAD, ID_TIPO_PERSONA, ID_PROCEDIMIENTO);
            operador = operadorDAL.consultaDatosPersonalesYLic(tipoDocumento, nroDocumento);

            if (resultado.ResultadoProcedimientoVM.CodResultado == 1)
            {
                if (resultado.NOMBRES != null)
                {
                    operador.NOMBRES = resultado.NOMBRES;
                    operador.APELLIDO_PATERNO = resultado.APELLIDO_PATERNO;
                    operador.APELLIDO_MATERNO = resultado.APELLIDO_MATERNO;
                    operador.DIRECCION = resultado.DIRECCION;
                    operador.TieneCredencial = resultado.TieneCredencial;
                }
                operador.FECHA_NACIMIENTO = resultado.FECHA_NACIMIENTO;
                operador.ID_SEXO = resultado.ID_SEXO;
                operador.ID_DISTRITO_OPERADOR = resultado.ID_DISTRITO_OPERADOR;
                operador.ID_PROVINCIA_OPERADOR = resultado.ID_PROVINCIA_OPERADOR;
                operador.ID_DEPARTAMENTO_OPERADOR = resultado.ID_DEPARTAMENTO_OPERADOR;
                operador.TELEFONO_CASA = resultado.TELEFONO_CASA;
                operador.TELEFONO_CEL = resultado.TELEFONO_CEL;
                operador.RUC_EMPRESA_OPERADOR = resultado.RUC_EMPRESA_OPERADOR;
            }
            //if (resultado.ID_OPERADOR != 0)
            //{
            //operador.RUC_EMPRESA_OPERADOR = resultado.RUC_EMPRESA_OPERADOR;
            //}
            return operador;
        }

        

        public OperadorVM BuscarOperador(string RUC, int ID_TIPO_DOCUMENTO, string NroDocumento, int ID_TIPO_MODALIDAD, int ID_TIPO_PERSONA, int ID_PROCEDIMIENTO)
        {
            OperadorVM operador = new OperadorVM();
            string TIPO_DOCUMENTO = "";

            var resultado = operadorDAL.BuscarOperador(RUC, NroDocumento, ID_TIPO_MODALIDAD, ID_TIPO_PERSONA, ID_PROCEDIMIENTO);
            
            if ((resultado.ResultadoProcedimientoVM.CodResultado == 1) && (resultado.NOMBRES != ""))
            {
                if (ID_TIPO_DOCUMENTO == EnumParametro.DNI.ValorEntero())
                {
                    TIPO_DOCUMENTO = "2";
                }
                else if (ID_TIPO_DOCUMENTO == EnumParametro.CE.ValorEntero())
                {
                    TIPO_DOCUMENTO = "4";
                }
                else if (ID_TIPO_DOCUMENTO == EnumParametro.PTP.ValorEntero())
                {
                    TIPO_DOCUMENTO = "14";
                }
                operador = operadorDAL.consultaDatosPersonalesYLic(TIPO_DOCUMENTO, NroDocumento);
                if ((resultado.NOMBRES == null))
                {
                    resultado.NOMBRES = operador.NOMBRES;
                    resultado.APELLIDO_PATERNO = operador.APELLIDO_PATERNO;
                    resultado.APELLIDO_MATERNO = operador.APELLIDO_MATERNO;
                    resultado.DIRECCION = operador.DIRECCION;
                    resultado.ID_TIPO_DOCUMENTO = ID_TIPO_DOCUMENTO;
                    resultado.ID_TIPO_OPERADOR = EnumParametroTipoOperador.CONDUCTOR.ValorEntero();
                }
               
                resultado.ESTADO_LICENCIA = operador.ESTADO_LICENCIA;
                resultado.PUNTOS_FIRME = operador.PUNTOS_FIRME;
                resultado.GRAVE = operador.GRAVE;
                resultado.MUY_GRAVE = operador.MUY_GRAVE;
                resultado.FECHA_EXPEDICION = operador.FECHA_EXPEDICION;
                resultado.FECHA_REVALIDACION = operador.FECHA_REVALIDACION;
                resultado.CATEGORIA = operador.CATEGORIA;
                resultado.NRO_LICENCIA = operador.NRO_LICENCIA;
                if (resultado.FECHA_VENCIMIENTO_CREDENCIAL != null)
                {
                    resultado.FECHA_VENCIMIENTO_CREDENCIAL = resultado.FECHA_VENCIMIENTO_CREDENCIAL.ValorFechaCorta();
                }
                //resultado.BD = operador.BD;
                if (resultado.BD == "SISGTU" || resultado.BD == null)
                {
                    resultado.NOMBRE_FOTO = operador.FOTO_BASE64;
                }

            }

            return resultado;
        }
    }
}
