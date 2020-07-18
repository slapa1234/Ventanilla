using SisATU.Base;
using SisATU.Base.Enumeradores;
using SisATU.Base.ViewModel;
using SisATU.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Negocio
{
    public class VehiculoBLL
    {
        private VehiculoDAL VehiculoDAL;
        private VehiculoAseguradoraDAL VehiculoAseguradoraDAL;
        private VehiculoCITVDAL VehiculoCITVDAL;

        private Object bdConn;
        public VehiculoBLL()
        {
            VehiculoDAL = new VehiculoDAL(ref bdConn);
            VehiculoCITVDAL = new VehiculoCITVDAL(ref bdConn);
            VehiculoAseguradoraDAL = new VehiculoAseguradoraDAL(ref bdConn);
        }
        public ConsultarVehiculoVM ConsultarDatosVehiculo(string nroPlaca)
        {
            ResultadoProcedimientoVM resultadoVehiculo = new ResultadoProcedimientoVM();
            ResultadoProcedimientoVM resultadoSeguro = new ResultadoProcedimientoVM();
            ResultadoProcedimientoVM resultadoCITV = new ResultadoProcedimientoVM();
            var vehiculo = VehiculoDAL.ConsultarDatosVehiculo(nroPlaca);
            vehiculo.FECHA_VENC_TUC = vehiculo.FECHA_VENC_TUC.ValorFechaCorta();
            //if (vehiculo.ID_VEHICULO == 0 && vehiculo.ResultadoProcedimientoVM.CodResultado == 1)
            //{
            //#region CREAR VEHICULO
            //resultadoVehiculo = VehiculoDAL.CrearVehiculo(new VehiculoModelo()
            //{
            //    PLACA = vehiculo.PLACA,
            //    ID_MODALIDAD_SERVICIO = 1,
            //    ID_MARCA = vehiculo.ID_MODELO,
            //    ID_MODELO = vehiculo.ID_MODELO,
            //    ID_CLASE_VEHICULO = vehiculo.ID_CLASE_VEHICULO,
            //    ANIO_FABRICACION = vehiculo.ANIO_FABRICACION,
            //    ID_TIPO_COMBUSTIBLE = vehiculo.ID_TIPO_COMBUSTIBLE,
            //    CILINDRADA = vehiculo.CILINDRADA,
            //    SERIE = vehiculo.SERIE,
            //    SERIE_MOTOR = vehiculo.SERIE_MOTOR,
            //    PESO_SECO = vehiculo.PESO_SECO,
            //    PESO_BRUTO = vehiculo.PESO_BRUTO,
            //    LONGITUD = vehiculo.LONGITUD,
            //    ALTURA = vehiculo.ALTURA,
            //    ANCHO = vehiculo.ANCHO,
            //    CARGA_UTIL = vehiculo.CARGA_UTIL,
            //    CAPACIDAD_PASAJERO = vehiculo.CAPACIDAD_PASAJERO,
            //    NUMERO_ASIENTOS = vehiculo.NUMERO_ASIENTOS,
            //    NUMERO_RUEDA = vehiculo.NUMERO_RUEDA,
            //    NUMERO_EJE = vehiculo.NUMERO_EJE,
            //    NUMERO_PUERTA = vehiculo.NUMERO_PUERTA,
            //});
            //vehiculo.ID_VEHICULO = resultadoVehiculo.CodAuxiliar;
            //#endregion


            if (vehiculo.PLACA != null)
            {

                var SOAT = new AseguradoraDAL().ConsultaSoat(nroPlaca);
                if (SOAT.POLIZA != null)
                {
                    vehiculo.ID_TIPO_SEGURO = EnumTipoSeguro.SOAT.ValorEntero();
                    vehiculo.ASEGURADORA_NOMBRE = SOAT.NOMBRE;
                    vehiculo.ASEGURADORA_FEC_INI_VIGENCIA = SOAT.FEC_INI_VIGENCIA.ValorFechaCorta();
                    vehiculo.ASEGURADORA_FEC_FIN_VIGENCIA = SOAT.FEC_FIN_VIGENCIA.ValorFechaCorta();
                    vehiculo.ASEGURADORA_POLIZA = SOAT.POLIZA;

                    //#region CREAR SOAT
                    //resultadoSeguro = VehiculoAseguradoraDAL.CrearVehiculoAseguradora(new VehiculoAseguradoraModelo()
                    //{
                    //    NOMBRE_ASEGURADORA = vehiculo.ASEGURADORA_NOMBRE,
                    //    ID_TIPO_SEGURO = vehiculo.ID_TIPO_SEGURO,
                    //    ID_VEHICULO = vehiculo.ID_VEHICULO,
                    //    POLIZA = vehiculo.ASEGURADORA_POLIZA,
                    //    FEC_INI_VIGENCIA = vehiculo.ASEGURADORA_FEC_INI_VIGENCIA,
                    //    FEC_FIN_VIGENCIA = vehiculo.ASEGURADORA_FEC_FIN_VIGENCIA,
                    //    //USUARIO_REG = Session["USUARIO"],
                    //});
                    //#endregion

                    //if (resultadoSeguro.CodResultado == 0)
                    //{
                    //    vehiculo.ResultadoProcedimientoVM.CodResultado = 0;
                    //    vehiculo.ResultadoProcedimientoVM.NomResultado = resultadoSeguro.NomResultado;
                    //}
                }
                else
                {
                    vehiculo.ID_TIPO_SEGURO = EnumTipoSeguro.AFOCAT.ValorEntero();
                }


                #region CREAR CITV
                var CITV = VehiculoCITVDAL.ConsultaCITV(nroPlaca);
                if (CITV.CERTIFICADORA_CITV != null)
                {
                    vehiculo.CITV_NOMBRE = CITV.CERTIFICADORA_CITV;
                    vehiculo.CITV_FEC_INI_VIGENCIA = CITV.FECHA_CERTIFICADO.ValorFechaCorta();
                    vehiculo.CITV_FEC_FIN_VIGENCIA = CITV.FECHA_VENCIMIENTO.ValorFechaCorta();
                    vehiculo.CITV_POLIZA = CITV.NRO_CERTIFICADO;

                    //#region CREAR CITV
                    //resultadoCITV = VehiculoCITVDAL.CrearVehiculoCITV(new VehiculoCITVModelo()
                    //{
                    //    ID_VEHICULO = vehiculo.ID_VEHICULO,
                    //    CERTIFICADORA_CITV = vehiculo.CITV_NOMBRE,
                    //    FECHA_CERTIFICADO = vehiculo.CITV_FEC_INI_VIGENCIA,
                    //    FECHA_VENCIMIENTO = vehiculo.CITV_FEC_FIN_VIGENCIA,
                    //});
                    //#endregion

                    //if (resultadoCITV.CodResultado == 0)
                    //{
                    //    vehiculo.ResultadoProcedimientoVM.CodResultado = resultadoCITV.CodResultado;
                    //    vehiculo.ResultadoProcedimientoVM.NomResultado = resultadoCITV.NomResultado;
                    //}
                }
                #endregion
            }
            else
            {
                vehiculo.ResultadoProcedimientoVM.CodResultado = 0;
                vehiculo.ResultadoProcedimientoVM.NomResultado = "Vehiculo no se encuentra registrado";
            }




            //}
            //else if (vehiculo.ResultadoProcedimientoVM.CodResultado == 0)
            //{
            //    vehiculo.ResultadoProcedimientoVM.CodResultado = 0;
            //    vehiculo.ResultadoProcedimientoVM.NomResultado = "La placa no se encuentra registrada en la base de datos.";

            //}
            //else if (vehiculo.ResultadoProcedimientoVM.CodResultado == 1)
            //{
            //    var SOAT = new AseguradoraDAL().ConsultaSoat(nroPlaca);
            //    if (SOAT.POLIZA != null)
            //    {
            //        vehiculo.ID_TIPO_SEGURO = EnumTipoSeguro.SOAT.ValorEntero();
            //        vehiculo.ASEGURADORA_NOMBRE = SOAT.NOMBRE;
            //        vehiculo.ASEGURADORA_FEC_INI_VIGENCIA = SOAT.FEC_INI_VIGENCIA.ValorFechaCorta();
            //        vehiculo.ASEGURADORA_FEC_FIN_VIGENCIA = SOAT.FEC_FIN_VIGENCIA.ValorFechaCorta();
            //        vehiculo.ASEGURADORA_POLIZA = SOAT.POLIZA;
            //    }
            //    else
            //    {
            //        vehiculo.ID_TIPO_SEGURO = EnumTipoSeguro.AFOCAT.ValorEntero();
            //    }

            //    var CITV = VehiculoCITVDAL.ConsultaCITV(nroPlaca);
            //    if (CITV.CERTIFICADORA_CITV != null)
            //    {
            //        vehiculo.CITV_NOMBRE = CITV.CERTIFICADORA_CITV;
            //        vehiculo.CITV_FEC_INI_VIGENCIA = CITV.FECHA_CERTIFICADO.ValorFechaCorta();
            //        vehiculo.CITV_FEC_FIN_VIGENCIA = CITV.FECHA_VENCIMIENTO.ValorFechaCorta();
            //        vehiculo.CITV_POLIZA = CITV.NRO_CERTIFICADO;
            //    }
            //}
            vehiculo.ID_VEHICULO = vehiculo.ID_VEHICULO;
            //vehiculo.ID_VEHICULO_ASEGURADOR = resultadoSeguro.CodAuxiliar;
            return vehiculo;
        }
        public int ConsultaPerteneceSolicitante(string nroPlaca, string nroSolicitante)
        {
            int resultado = VehiculoDAL.ConsultaPerteneceSolicitante(nroPlaca, nroSolicitante);
            return resultado;
        }
    }
}
