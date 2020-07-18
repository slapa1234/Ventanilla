using SisATU.Base;
using SisATU.Base.Enumeradores;
using SisATU.Base.ViewModel;
using SisATU.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using System.Net;
using SisATU.Base.Constante;

namespace SisATU.Negocio
{
    public class ExpedienteBLL
    {
        private ReciboDAL ReciboDAL;
        private DetalleSolicitudDAL DetalleSolicitudDAL;
        private VehiculoDAL VehiculoDAL;
        private TarjetaPropiedadDAL TarjetaPropiedadDAL;
        private ExpedienteDAL ExpedienteDAL;
        private TarjetaCirculacionDAL TarjetaCirculacionDAL;
        private PersonaDAL PersonaDAL;
        private EmpresaDAL EmpresaDAL;
        private ResolucionDAL ResolucionDAL;
        private ResolucionExpedienteDAL ResolucionExpedienteDAL;
        private VehiculoAseguradoraDAL VehiculoAseguradoraDAL;
        private VehiculoCITVDAL VehiculoCITVDAL;
        private Object bdConn;

        public ExpedienteBLL()
        {
            ReciboDAL = new ReciboDAL(ref bdConn);
            DetalleSolicitudDAL = new DetalleSolicitudDAL(ref bdConn);
            VehiculoDAL = new VehiculoDAL(ref bdConn);
            TarjetaPropiedadDAL = new TarjetaPropiedadDAL(ref bdConn);
            TarjetaCirculacionDAL = new TarjetaCirculacionDAL(ref bdConn);
            PersonaDAL = new PersonaDAL(ref bdConn);
            EmpresaDAL = new EmpresaDAL(ref bdConn);
            ResolucionExpedienteDAL = new ResolucionExpedienteDAL(ref bdConn);
            ResolucionDAL = new ResolucionDAL(ref bdConn);
            VehiculoAseguradoraDAL = new VehiculoAseguradoraDAL(ref bdConn);
            VehiculoCITVDAL = new VehiculoCITVDAL(ref bdConn);
            ExpedienteDAL = new ExpedienteDAL(ref bdConn);
        }

        #region Crea Expediente TUC
        public ResultadoProcedimientoVM CrearExpedienteTUC(ExpedienteModelo expediente, EmpresaModelo empresa, PersonaModelo persona, List<ReciboModelo> recibo,
                                                        STDVM STD, VehiculoModelo vehiculo, TarjetaPropiedadModelo tarjetaPropiedad, PropietarioModelo propietario,
                                                        TarjetaCirculacionModelo tarjetaCirculacion, ResolucionModelo resolucion, ResolucionExpedienteModelo resolucionExpediente, VehiculoAseguradoraModelo vehiculoAseguradora,
                                                        VehiculoCITVModelo vehiculoCITV/*, List<DetalleSolicitudModelo> detalleSolicitud*/)
        {
            ResultadoProcedimientoVM resultadoExpediente = new ResultadoProcedimientoVM();
            ResultadoProcedimientoVM resultadoPersona = new ResultadoProcedimientoVM();
            ResultadoProcedimientoVM resultadoEmpresa = new ResultadoProcedimientoVM();
            ResultadoProcedimientoVM resultadoRecibo = new ResultadoProcedimientoVM();
            ResultadoProcedimientoVM resultadoVehiculo = new ResultadoProcedimientoVM();
            ResultadoProcedimientoVM resultadoTarjetaPropiedad = new ResultadoProcedimientoVM();
            ResultadoProcedimientoVM resultadoPropietario = new ResultadoProcedimientoVM();
            ResultadoProcedimientoVM resultadoTarjetaCirculacion = new ResultadoProcedimientoVM();
            ResultadoProcedimientoVM resultadoVehiculoAseguradora = new ResultadoProcedimientoVM();
            ResultadoProcedimientoVM resultadoVehiculoCITV = new ResultadoProcedimientoVM();
            ResultadoProcedimientoVM resultadoDetalleSolicitud = new ResultadoProcedimientoVM();
            ResultadoProcedimientoVM resultadoResolucion = new ResultadoProcedimientoVM();
            ResultadoProcedimientoVM resultadoResolucionExpediente = new ResultadoProcedimientoVM();
            ResultadoProcedimientoVM resultadoVehiculoPersona = new ResultadoProcedimientoVM();

            //#region CONSULTA SI EL VOUCHER EXISTE
            //foreach (var item in recibo)
            //{
            //    var resultadoComprobante = ReciboDAL.BuscarRecibo(item.NUMERO_RECIBO.ValorCadena());

            //    if (resultadoComprobante.NUMERO_RECIBO != null)
            //    {
            //        resultadoExpediente.CodResultado = 0;
            //        resultadoExpediente.NomResultado = "Voucher " + resultadoComprobante.NUMERO_RECIBO + "ya fue registrado el " + resultadoComprobante.FECHA_EMISION.ValorFechaCorta();
            //        return resultadoExpediente;
            //    }
            //}
            //#endregion

            #region SETEA CAMPOS DEPENDIENDO EL TIPO DE PERSONA
            if (persona.ID_TIPO_PERSONA == EnumParametro.PersonaNatural.ValorEntero())
            {
                STD.ID_PERSONA = persona.ID_PERSONA;
                STD.PARA = persona.NOMBRES;
                expediente.NUMERO_RECURRENTE = persona.NRO_DOCUMENTO;
                expediente.NUMERO_SOLICITANTE = persona.NRO_DOCUMENTO;
            }
            else
            {
                STD.ID_PROVEEDOR = STD.ID_PROVEEDOR;
                expediente.NUMERO_RECURRENTE = persona.NRO_DOCUMENTO;
                expediente.NUMERO_SOLICITANTE = empresa.RUC;
            }
            #endregion

            int IDDOC_PADRE = 0;
            int IDDOC_HIJO = 0;
            int IDFLUJO = 0;
            int Contador = 0;
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    #region REGISTRA VEHICULO
                    //if (vehiculo.ID_VEHICULO == 0)
                    //{
                    resultadoVehiculo = VehiculoDAL.CrearVehiculo(vehiculo);
                    vehiculo.ID_VEHICULO = resultadoVehiculo.CodAuxiliar;
                    //}
                    #endregion

                    if (vehiculo.ID_VEHICULO != 0)
                    {
                        #region REGISTRA TARJETA PROPIEDAD
                        tarjetaPropiedad.ID_VEHICULO = vehiculo.ID_VEHICULO;
                        resultadoTarjetaPropiedad = TarjetaPropiedadDAL.CrearTarjetaPropiedad(tarjetaPropiedad);
                        #endregion

                        if (resultadoTarjetaPropiedad.CodResultado == 1)
                        {

                            #region REGISTRA PROPIETARIO
                            propietario.ID_TARJETA_PROPIEDAD = resultadoTarjetaPropiedad.CodAuxiliar;
                            resultadoPropietario = PersonaDAL.CrearPropietario(propietario);

                            if (resultadoPropietario.CodResultado != 1)
                            {
                                resultadoExpediente.CodResultado = resultadoPropietario.CodResultado;
                                resultadoExpediente.NomResultado = resultadoPropietario.NomResultado;
                                scope.Dispose();
                                Conexion.finalizar(ref bdConn);
                                return resultadoExpediente;
                            }
                            #endregion
                            if (persona.ID_TIPO_PERSONA == EnumParametro.PersonaJuridica.ValorEntero())
                            {
                                resultadoVehiculoPersona = VehiculoDAL.VehiculoPersona(empresa.RUC, vehiculo.PLACA);
                            }

                            #region REGISTRA VEHICULO ASEGURADORA
                            vehiculoAseguradora.ID_VEHICULO = vehiculo.ID_VEHICULO;
                            resultadoVehiculoAseguradora = VehiculoAseguradoraDAL.CrearVehiculoAseguradora(vehiculoAseguradora);

                            if (resultadoVehiculoAseguradora.CodResultado != 1)
                            {
                                resultadoExpediente.CodResultado = resultadoVehiculoAseguradora.CodResultado;
                                resultadoExpediente.NomResultado = resultadoVehiculoAseguradora.NomResultado;
                                scope.Dispose();
                                Conexion.finalizar(ref bdConn);
                                return resultadoExpediente;
                            }
                            #endregion

                            #region REGISTRA VEHICULO CITV
                            vehiculoCITV.ID_VEHICULO = vehiculo.ID_VEHICULO;
                            resultadoVehiculoCITV = VehiculoCITVDAL.CrearVehiculoCITV(vehiculoCITV);

                            if (resultadoVehiculoCITV.CodResultado != 1)
                            {
                                resultadoExpediente.CodResultado = resultadoVehiculoCITV.CodResultado;
                                resultadoExpediente.NomResultado = resultadoVehiculoCITV.NomResultado;
                                scope.Dispose();
                                Conexion.finalizar(ref bdConn);
                                return resultadoExpediente;
                            }
                            #endregion

                            #region CONSULTA PROCEDIMIENTOS HIJOS
                            var detalleProcedimiento = new ProcedimientoDAL().ConsultarDatosProcedimientoPadre(expediente.ID_PROCEDIMIENTO, persona.ID_TIPO_PERSONA);
                            int expedientePadre = 0;
                            foreach (var itemProcedimiento in detalleProcedimiento)
                            {
                                #region REGISTRA STD 
                                var resultadoRegistroSTD = new STDDAL().CrearExpedienteSTD(STD);

                                expediente.IDDOC = resultadoRegistroSTD.IDDOC;
                                expediente.NUMERO_SID = resultadoRegistroSTD.NUMERO_SID;
                                expediente.NUMERO_ANIO = resultadoRegistroSTD.NUMERO_ANIO;
                                expediente.IDFLUJO = resultadoRegistroSTD.IDFLUJO.ValorEntero();

                                if (Contador == 0)
                                {
                                    IDDOC_PADRE = resultadoRegistroSTD.IDDOC;
                                }
                                else
                                {
                                    IDFLUJO = resultadoRegistroSTD.IDFLUJO.ValorEntero();
                                    IDDOC_HIJO = resultadoRegistroSTD.IDDOC;
                                }
                                //
                                //
                                #endregion

                                if (expediente.IDDOC != 0)
                                {
                                    #region REGISTRA EXPEDIENTE

                                    var PROCEDIMIENTO_PADRE = itemProcedimiento.ID_PROCEDIMIENTO_PADRE;
                                    if (PROCEDIMIENTO_PADRE == expediente.ID_PROCEDIMIENTO)
                                    {
                                        expediente.IS_PADRE_EXPEDIENTE = 1;
                                        //expediente.ID_EXPEDIENTE_PADRE = detalleProcedimiento[0].ID_PROCEDIMIENTO_PADRE;
                                    }
                                    else
                                    {
                                        expediente.ID_PROCEDIMIENTO = itemProcedimiento.ID_PROCEDIMIENTO_PADRE;
                                        expediente.IS_PADRE_EXPEDIENTE = 0;
                                        expediente.ID_EXPEDIENTE_PADRE = expedientePadre; //detalleProcedimiento[0].ID_PROCEDIMIENTO_PADRE;
                                    }

                                    expediente.ID_VEHICULO = vehiculo.ID_VEHICULO;

                                    resultadoExpediente = ExpedienteDAL.CrearExpediente(expediente);

                                    if (resultadoExpediente.CodResultado != 1)
                                    {
                                        scope.Dispose();
                                        Conexion.finalizar(ref bdConn);
                                        return resultadoExpediente;
                                    }
                                    if (Contador == 0)
                                    {
                                        expedientePadre = resultadoExpediente.CodAuxiliar;
                                    }
                                    #endregion
                                }

                                Contador++;
                            }

                            STD.IDDOC_PADRE = IDDOC_PADRE;
                            STD.IDDOC_HIJO = IDDOC_HIJO;
                            STD.IDFLUJO = IDFLUJO;
                            if (detalleProcedimiento.Count > 1)
                            {
                                var generaAcumulador = new STDDAL().AcumuladorSTD(STD);
                            }

                            STD.ASUNTO = "SOLICITO RENOVACION.";

                            resultadoExpediente.IDDOC_PADRE = IDDOC_PADRE;
                            resultadoExpediente.IDDOC_HIJO = IDDOC_HIJO;

                            #region REGISTRAR RESOLUCION
                            var generaResolucion = new STDDAL().GenerarResolucionSTD(STD);

                            resultadoResolucion = ResolucionDAL.CrearResolucion(resolucion);
                            if (resultadoResolucion.CodResultado == 1)
                            {
                                resolucionExpediente.ID_RESOLUCION = resultadoResolucion.CodAuxiliar;
                                resolucionExpediente.ID_EXPEDIENTE = expedientePadre;
                                resolucionExpediente.NUMERO_RESOLUCION = generaResolucion.IDCRTLNUM.ValorCadena();
                            }

                            resultadoResolucionExpediente = ResolucionExpedienteDAL.CrearResolucionExpediente(resolucionExpediente);

                            #endregion
                            if (resultadoExpediente.CodResultado == 1)
                            {
                                #region REGISTRA TARJETA CIRCULACION
                                tarjetaCirculacion.ID_EXPEDIENTE = resultadoExpediente.CodAuxiliar;
                                resultadoTarjetaCirculacion = TarjetaCirculacionDAL.CrearTarjetaCirculacion(tarjetaCirculacion);

                                if (resultadoTarjetaCirculacion.CodResultado != 1)
                                {
                                    resultadoExpediente.CodResultado = resultadoTarjetaCirculacion.CodResultado;
                                    resultadoExpediente.NomResultado = resultadoTarjetaCirculacion.NomResultado;
                                    scope.Dispose();
                                    Conexion.finalizar(ref bdConn);
                                    return resultadoExpediente;
                                }
                                #endregion


                                #region REGISTRO PERSONA NATURAL O JURIDICA
                                if (persona.ID_TIPO_PERSONA == EnumParametro.PersonaNatural.ValorEntero())
                                {
                                    resultadoPersona = PersonaDAL.CrearPersona(persona);
                                }
                                else
                                {
                                    resultadoPersona = PersonaDAL.CrearPersona(persona);
                                    resultadoEmpresa = EmpresaDAL.CrearEmpresa(empresa);
                                }
                                #endregion

                                #region REGISTRA RECIBO
                                foreach (var item in recibo)
                                {
                                    item.ID_EXPEDIENTE = resultadoExpediente.CodAuxiliar;
                                    resultadoRecibo = ReciboDAL.CrearRecibo(item);
                                }

                                #endregion

                                /*#region REGISTRA DETALLE SOLICITUD
                                foreach (var item in detalleSolicitud)
                                {
                                    item.ID_EXPEDIENTE = resultadoExpediente.CodAuxiliar;
                                    resultadoDetalleSolicitud = DetalleSolicitudDAL.CrearDetalleSolicitud(item);
                                }
                                #endregion*/

                            }

                            #endregion

                        }
                        else
                        {
                            resultadoExpediente.CodResultado = resultadoTarjetaPropiedad.CodResultado;
                            resultadoExpediente.NomResultado = resultadoTarjetaPropiedad.NomResultado;
                            scope.Dispose();
                            Conexion.finalizar(ref bdConn);
                            return resultadoExpediente;
                        }
                    }
                }
                catch (Exception ex)
                {
                    resultadoExpediente.CodResultado = 0;
                    resultadoExpediente.NomResultado = ex.Message;
                }
                if (resultadoExpediente.CodResultado == 1 && resultadoRecibo.CodResultado == 1)
                { scope.Complete(); }
                else { scope.Dispose(); }
            }
            Conexion.finalizar(ref bdConn);
            return resultadoExpediente;
        }

        #endregion

        #region Crea Duplicado TUC
        public ResultadoProcedimientoVM CrearDuplicadoTUC(ExpedienteModelo expediente, EmpresaModelo empresa, PersonaModelo persona, List<ReciboModelo> recibo,
                                                        STDVM STD, VehiculoModelo vehiculo, TarjetaPropiedadModelo tarjetaPropiedad, PropietarioModelo propietario,
                                                        TarjetaCirculacionModelo tarjetaCirculacion)
        {
            ResultadoProcedimientoVM resultadoExpediente = new ResultadoProcedimientoVM();
            ResultadoProcedimientoVM resultadoPersona = new ResultadoProcedimientoVM();
            ResultadoProcedimientoVM resultadoEmpresa = new ResultadoProcedimientoVM();
            ResultadoProcedimientoVM resultadoRecibo = new ResultadoProcedimientoVM();
            ResultadoProcedimientoVM resultadoVehiculo = new ResultadoProcedimientoVM();
            ResultadoProcedimientoVM resultadoTarjetaPropiedad = new ResultadoProcedimientoVM();
            ResultadoProcedimientoVM resultadoPropietario = new ResultadoProcedimientoVM();
            ResultadoProcedimientoVM resultadoTarjetaCirculacion = new ResultadoProcedimientoVM();


            //#region CONSULTA SI EL VOUCHER EXISTE
            //foreach (var item in recibo)
            //{
            //    var resultadoComprobante = ReciboDAL.BuscarRecibo(item.NUMERO_RECIBO.ValorCadena());

            //    if (resultadoComprobante.NUMERO_RECIBO != null)
            //    {
            //        resultadoExpediente.CodResultado = 0;
            //        resultadoExpediente.NomResultado = "Voucher " + resultadoComprobante.NUMERO_RECIBO + "ya fue registrado el " + resultadoComprobante.FECHA_EMISION.ValorFechaCorta();
            //        return resultadoExpediente;
            //    }
            //}
            //#endregion

            #region SETEA CAMPOS DEPENDIENDO EL TIPO DE PERSONA
            if (persona.ID_TIPO_PERSONA == EnumParametro.PersonaNatural.ValorEntero())
            {
                STD.ID_PERSONA = persona.ID_PERSONA;
                STD.PARA = persona.NOMBRES;
                expediente.NUMERO_RECURRENTE = persona.NRO_DOCUMENTO;
                expediente.NUMERO_SOLICITANTE = persona.NRO_DOCUMENTO;
            }
            else
            {
                STD.ID_PROVEEDOR = STD.ID_PROVEEDOR;
                expediente.NUMERO_RECURRENTE = persona.NRO_DOCUMENTO;
                expediente.NUMERO_SOLICITANTE = empresa.RUC;
            }
            #endregion

            int IDDOC_PADRE = 0;
            int IDDOC_HIJO = 0;
            int IDFLUJO = 0;
            int Contador = 0;
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    #region REGISTRA VEHICULO
                    //if (vehiculo.ID_VEHICULO == 0)
                    //{
                    resultadoVehiculo = VehiculoDAL.CrearVehiculo(vehiculo);
                    vehiculo.ID_VEHICULO = resultadoVehiculo.CodAuxiliar;
                    //}
                    #endregion

                    if (vehiculo.ID_VEHICULO != 0)
                    {
                        #region REGISTRA TARJETA PROPIEDAD
                        tarjetaPropiedad.ID_VEHICULO = vehiculo.ID_VEHICULO;
                        resultadoTarjetaPropiedad = TarjetaPropiedadDAL.CrearTarjetaPropiedad(tarjetaPropiedad);
                        #endregion

                        if (resultadoTarjetaPropiedad.CodResultado == 1)
                        {

                            #region REGISTRA PROPIETARIO
                            propietario.ID_TARJETA_PROPIEDAD = resultadoTarjetaPropiedad.CodAuxiliar;
                            resultadoPropietario = PersonaDAL.CrearPropietario(propietario);

                            if (resultadoPropietario.CodResultado != 1)
                            {
                                resultadoExpediente.CodResultado = resultadoPropietario.CodResultado;
                                resultadoExpediente.NomResultado = resultadoPropietario.NomResultado;
                                scope.Dispose();
                                Conexion.finalizar(ref bdConn);
                                return resultadoExpediente;
                            }
                            #endregion


                            //resultadoVehiculoPersona = VehiculoDAL.VehiculoPersona(empresa.RUC, vehiculo.PLACA);





                            #region REGISTRA STD 
                            var resultadoRegistroSTD = new STDDAL().CrearExpedienteSTD(STD);

                            expediente.IDDOC = resultadoRegistroSTD.IDDOC;
                            expediente.NUMERO_SID = resultadoRegistroSTD.NUMERO_SID;
                            expediente.NUMERO_ANIO = resultadoRegistroSTD.NUMERO_ANIO;
                            expediente.IDFLUJO = resultadoRegistroSTD.IDFLUJO.ValorEntero();

                            /*if (Contador == 0)
                            {
                                IDDOC_PADRE = resultadoRegistroSTD.IDDOC;
                            }
                            else
                            {
                                IDFLUJO = resultadoRegistroSTD.IDFLUJO.ValorEntero();*/
                            IDDOC_HIJO = resultadoRegistroSTD.IDDOC;
                            new STDDAL().CerrarExpedienteSTD(resultadoRegistroSTD);
                            //}
                            //
                            //
                            #endregion

                            if (expediente.IDDOC != 0)
                            {

                                #region REGISTRA EXPEDIENTE

                                expediente.ID_VEHICULO = vehiculo.ID_VEHICULO;

                                resultadoExpediente = ExpedienteDAL.CrearExpediente(expediente);

                                if (resultadoExpediente.CodResultado != 1)
                                {
                                    scope.Dispose();
                                    Conexion.finalizar(ref bdConn);
                                    return resultadoExpediente;
                                }
                                #endregion
                            }
                            resultadoExpediente.IDDOC_HIJO = IDDOC_HIJO;



                            /*STD.IDDOC_PADRE = IDDOC_PADRE;
                            STD.IDDOC_HIJO = IDDOC_HIJO;
                            STD.IDFLUJO = IDFLUJO;
                            var generaAcumulador = new STDDAL().AcumuladorSTD(STD);
                            STD.ASUNTO = "SOLICITO DUPLICADO.";

                            resultadoExpediente.IDDOC_PADRE = IDDOC_PADRE;
                            resultadoExpediente.IDDOC_HIJO = IDDOC_HIJO;*/

                            if (resultadoExpediente.CodResultado == 1)
                            {
                                #region REGISTRA TARJETA CIRCULACION
                                tarjetaCirculacion.ID_EXPEDIENTE = resultadoExpediente.CodAuxiliar;
                                resultadoTarjetaCirculacion = TarjetaCirculacionDAL.CrearDuplicadoTUC(tarjetaCirculacion);

                                if (resultadoTarjetaCirculacion.CodResultado != 1)
                                {
                                    resultadoExpediente.CodResultado = resultadoTarjetaCirculacion.CodResultado;
                                    resultadoExpediente.NomResultado = resultadoTarjetaCirculacion.NomResultado;
                                    scope.Dispose();
                                    Conexion.finalizar(ref bdConn);
                                    return resultadoExpediente;
                                }
                                #endregion


                                #region REGISTRO PERSONA NATURAL O JURIDICA
                                if (persona.ID_TIPO_PERSONA == EnumParametro.PersonaNatural.ValorEntero())
                                {
                                    resultadoPersona = PersonaDAL.CrearPersona(persona);
                                }
                                else
                                {
                                    resultadoPersona = PersonaDAL.CrearPersona(persona);
                                    resultadoEmpresa = EmpresaDAL.CrearEmpresa(empresa);
                                }
                                #endregion

                                #region REGISTRA RECIBO
                                foreach (var item in recibo)
                                {
                                    item.ID_EXPEDIENTE = resultadoExpediente.CodAuxiliar;
                                    resultadoRecibo = ReciboDAL.CrearRecibo(item);
                                }

                                #endregion

                                /*#region REGISTRA DETALLE SOLICITUD
                                foreach (var item in detalleSolicitud)
                                {
                                    item.ID_EXPEDIENTE = resultadoExpediente.CodAuxiliar;
                                    resultadoDetalleSolicitud = DetalleSolicitudDAL.CrearDetalleSolicitud(item);
                                }
                                #endregion*/

                            }
                        }
                        else
                        {
                            resultadoExpediente.CodResultado = resultadoTarjetaPropiedad.CodResultado;
                            resultadoExpediente.NomResultado = resultadoTarjetaPropiedad.NomResultado;
                            scope.Dispose();
                            Conexion.finalizar(ref bdConn);
                            return resultadoExpediente;
                        }
                    }
                }
                catch (Exception ex)
                {
                    resultadoExpediente.CodResultado = 0;
                    resultadoExpediente.NomResultado = ex.Message;
                }
                if (resultadoExpediente.CodResultado == 1 && resultadoRecibo.CodResultado == 1)
                { scope.Complete(); }
                else { scope.Dispose(); }
            }
            Conexion.finalizar(ref bdConn);
            return resultadoExpediente;
        }

        #endregion

        #region Crear Requisitos

        public ResultadoProcedimientoVM CrearRequisitos(ExpedienteModelo expediente, EmpresaModelo empresa, PersonaModelo persona, List<ReciboModelo> recibo,
                                                        STDVM STD, List<DetalleSolicitudModelo> detalleSolicitud)
        {
            ResultadoProcedimientoVM resultadoExpediente = new ResultadoProcedimientoVM();
            ResultadoProcedimientoVM resultadoPersona = new ResultadoProcedimientoVM();
            ResultadoProcedimientoVM resultadoEmpresa = new ResultadoProcedimientoVM();
            ResultadoProcedimientoVM resultadoRecibo = new ResultadoProcedimientoVM();
            ResultadoProcedimientoVM resultadoDetalleSolicitud = new ResultadoProcedimientoVM();

            #region SETEA CAMPOS DEPENDIENDO EL TIPO DE PERSONA
            if (persona.ID_TIPO_PERSONA == EnumParametro.PersonaNatural.ValorEntero())
            {
                STD.ID_PERSONA = persona.ID_PERSONA;
                STD.PARA = persona.NOMBRES;
                expediente.NUMERO_RECURRENTE = persona.NRO_DOCUMENTO;
                expediente.NUMERO_SOLICITANTE = persona.NRO_DOCUMENTO;
            }
            else
            {
                STD.ID_PROVEEDOR = STD.ID_PROVEEDOR;
                expediente.NUMERO_RECURRENTE = persona.NRO_DOCUMENTO;
                expediente.NUMERO_SOLICITANTE = empresa.RUC;
            }
            #endregion

            int IDDOC_PADRE = 0;
            int IDDOC_HIJO = 0;
            int IDFLUJO = 0;
            int Contador = 0;
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {

                    //var detalleProcedimiento = new ProcedimientoDAL().ConsultarDatosProcedimientoPadre(expediente.ID_PROCEDIMIENTO);
                    //int expedientePadre = 0;
                    //foreach (var itemProcedimiento in detalleProcedimiento)
                    //{
                    #region REGISTRA STD 
                    var resultadoRegistroSTD = new STDDAL().CrearExpedienteSTD(STD);
                    expediente.IDDOC = resultadoRegistroSTD.IDDOC;
                    expediente.NUMERO_SID = resultadoRegistroSTD.NUMERO_SID;
                    expediente.NUMERO_ANIO = resultadoRegistroSTD.NUMERO_ANIO;
                    expediente.IDFLUJO = resultadoRegistroSTD.IDFLUJO.ValorEntero();

                    if (Contador == 0)
                    {
                        IDDOC_PADRE = resultadoRegistroSTD.IDDOC;
                    }
                    else
                    {
                        IDFLUJO = resultadoRegistroSTD.IDFLUJO.ValorEntero();
                        IDDOC_HIJO = resultadoRegistroSTD.IDDOC;
                    }
                    #endregion

                    if (expediente.IDDOC != 0)
                    {
                        expediente.IS_PADRE_EXPEDIENTE = 1;
                    }

                    resultadoExpediente = ExpedienteDAL.CrearExpediente(expediente);

                    if (resultadoExpediente.CodResultado != 1)
                    {
                        scope.Dispose();
                        Conexion.finalizar(ref bdConn);
                        return resultadoExpediente;
                    }

                    STD.IDDOC_PADRE = IDDOC_PADRE;
                    STD.IDDOC_HIJO = IDDOC_HIJO;
                    STD.IDFLUJO = IDFLUJO;
                    //var generaAcumulador = new STDDAL().AcumuladorSTD(STD);
                    STD.ASUNTO = "SOLICITO RENOVACION.";

                    resultadoExpediente.IDDOC_PADRE = IDDOC_PADRE;
                    resultadoExpediente.IDDOC_HIJO = IDDOC_HIJO;

                    #region REGISTRAR RESOLUCION
                    var generaResolucion = new STDDAL().GenerarResolucionSTD(STD);

                    #endregion

                    if (resultadoExpediente.CodResultado == 1)
                    {
                        #region REGISTRO PERSONA NATURAL O JURIDICA
                        if (persona.ID_TIPO_PERSONA == EnumParametro.PersonaNatural.ValorEntero())
                        {
                            resultadoPersona = PersonaDAL.CrearPersona(persona);
                        }
                        else
                        {
                            resultadoPersona = PersonaDAL.CrearPersona(persona);
                            resultadoEmpresa = EmpresaDAL.CrearEmpresa(empresa);
                        }
                        #endregion

                        #region REGISTRA RECIBO
                        foreach (var item in recibo)
                        {
                            item.ID_EXPEDIENTE = resultadoExpediente.CodAuxiliar;
                            resultadoRecibo = ReciboDAL.CrearRecibo(item);
                        }

                        #endregion

                        #region REGISTRA DETALLE SOLICITUD
                        foreach (var item in detalleSolicitud)
                        {
                            item.ID_EXPEDIENTE = resultadoExpediente.CodAuxiliar;
                            resultadoDetalleSolicitud = DetalleSolicitudDAL.CrearDetalleSolicitud(item);
                        }
                        #endregion

                    }

                }
                catch (Exception ex)
                {
                    resultadoExpediente.CodResultado = 0;
                    resultadoExpediente.NomResultado = ex.Message;
                }
                if (resultadoExpediente.CodResultado == 1 && resultadoRecibo.CodResultado == 1)
                { scope.Complete(); }
                else { scope.Dispose(); }
            }
            Conexion.finalizar(ref bdConn);
            return resultadoExpediente;
        }


        #endregion

        //#region Crea Expediente Operador
        //public ResultadoProcedimientoVM CrearExpedienteBase(ExpedienteModelo expediente, EmpresaModelo empresa, PersonaModelo persona, List<ReciboModelo> recibo, STDVM STD, List<DetalleSolicitudModelo> detalleSolicitud)
        //{
        //    ResultadoProcedimientoVM resultadoExpediente = new ResultadoProcedimientoVM();
        //    ResultadoProcedimientoVM resultadoRecibo = new ResultadoProcedimientoVM();
        //    ResultadoProcedimientoVM resultadoPersona = new ResultadoProcedimientoVM();
        //    ResultadoProcedimientoVM resultadoEmpresa = new ResultadoProcedimientoVM();
        //    ResultadoProcedimientoVM resultadoDetalleSolicitud = new ResultadoProcedimientoVM();

        //    #region CONSULTA SI EL VOUCHER EXISTE
        //    foreach (var item in recibo)
        //    {
        //        var resultadoComprobante = ReciboDAL.BuscarRecibo(item.NUMERO_RECIBO.ValorCadena());

        //        if (resultadoComprobante.NUMERO_RECIBO != null)
        //        {
        //            resultadoExpediente.CodResultado = 0;
        //            resultadoExpediente.NomResultado = "Voucher " + resultadoComprobante.NUMERO_RECIBO + "ya fue registrado el " + resultadoComprobante.FECHA_EMISION.ValorFechaCorta();
        //            return resultadoExpediente;
        //        }
        //    }
        //    #endregion

        //    #region SETEA CAMPOS DEPENDIENDO EL TIPO DE PERSONA
        //    if (persona.ID_TIPO_PERSONA == EnumParametro.PersonaNatural.ValorEntero())
        //    {
        //        STD.ID_PERSONA = persona.ID_PERSONA;
        //        STD.PARA = persona.NOMBRES;
        //        expediente.NUMERO_RECURRENTE = persona.NRO_DOCUMENTO;
        //        expediente.NUMERO_SOLICITANTE = persona.NRO_DOCUMENTO;
        //    }
        //    else
        //    {
        //        STD.ID_PROVEEDOR = STD.ID_PROVEEDOR;
        //        expediente.NUMERO_RECURRENTE = persona.NRO_DOCUMENTO;
        //        expediente.NUMERO_SOLICITANTE = empresa.RUC;
        //    }
        //    #endregion

        //    int IDDOC_PADRE = 0;
        //    int IDDOC_HIJO = 0;
        //    int IDFLUJO = 0;
        //    int Contador = 0;
        //    using (TransactionScope scope = new TransactionScope())
        //    {
        //        try
        //        {

        //            var detalleProcedimiento = new ProcedimientoDAL().ConsultarDatosProcedimientoHijo(expediente.ID_PROCEDIMIENTO, persona.ID_TIPO_PERSONA);
        //            int expedientePadre = 0;
        //            var id_procedimiento_padre = 0;
        //            foreach (var itemProcedimiento in detalleProcedimiento)
        //            {
        //                #region REGISTRA STD 
        //                var resultadoRegistroSTD = new STDDAL().CrearExpedienteSTD(STD);

        //                expediente.IDDOC = resultadoRegistroSTD.IDDOC;
        //                expediente.NUMERO_SID = resultadoRegistroSTD.NUMERO_SID;
        //                expediente.NUMERO_ANIO = resultadoRegistroSTD.NUMERO_ANIO;
        //                expediente.IDFLUJO = resultadoRegistroSTD.IDFLUJO.ValorEntero();

        //                if (Contador == 0)
        //                {
        //                    IDDOC_PADRE = resultadoRegistroSTD.IDDOC;
        //                    id_procedimiento_padre = itemProcedimiento.ID_PROCEDIMIENTO;
        //                }
        //                else
        //                {
        //                    IDFLUJO = resultadoRegistroSTD.IDFLUJO.ValorEntero();
        //                    IDDOC_HIJO = resultadoRegistroSTD.IDDOC;
        //                }
        //                #endregion

        //                if (expediente.IDDOC != 0)
        //                {
        //                    #region REGISTRA EXPEDIENTE
        //                    if (Contador == 0) //expediente padre
        //                    {
        //                        expediente.IS_PADRE_EXPEDIENTE = 1;
        //                    }
        //                    else //expediente hijos
        //                    {
        //                        expediente.IS_PADRE_EXPEDIENTE = 0;
        //                        expediente.ID_PROCEDIMIENTO = id_procedimiento_padre;
        //                        expediente.ID_EXPEDIENTE_PADRE = expedientePadre;
        //                    }

        //                    resultadoExpediente = ExpedienteDAL.CrearExpediente(expediente);
        //                    if (Contador == 0)
        //                    {
        //                        expedientePadre = resultadoExpediente.CodAuxiliar;
        //                        resultadoExpediente.IDDOC_PADRE = IDDOC_PADRE;
        //                    }

        //                    if (resultadoExpediente.CodResultado == 1)
        //                    {
        //                        foreach (var item in recibo)
        //                        {
        //                            if (item.ID_PROCEDIMIENTO == expediente.ID_PROCEDIMIENTO)
        //                            {
        //                                item.ID_EXPEDIENTE = resultadoExpediente.CodAuxiliar;
        //                                resultadoRecibo = ReciboDAL.CrearRecibo(item);
        //                            }
        //                        }

        //                        #region REGISTRO PERSONA NATURAL O JURIDICA
        //                        if (persona.ID_TIPO_PERSONA == EnumParametro.PersonaNatural.ValorEntero())
        //                        {
        //                            resultadoPersona = PersonaDAL.CrearPersona(persona);
        //                        }
        //                        else
        //                        {
        //                            resultadoPersona = PersonaDAL.CrearPersona(persona);
        //                            resultadoEmpresa = EmpresaDAL.CrearEmpresa(empresa);
        //                        }
        //                        #endregion

        //                        #region REGISTRA DETALLE SOLICITUD
        //                        foreach (var item in detalleSolicitud)
        //                        {
        //                            item.ID_EXPEDIENTE = resultadoExpediente.CodAuxiliar;
        //                            resultadoDetalleSolicitud = DetalleSolicitudDAL.CrearDetalleSolicitud(item);
        //                        }
        //                        #endregion
        //                    }
        //                    #endregion
        //                }
        //                else
        //                {
        //                    resultadoExpediente.CodResultado = 0;
        //                    resultadoExpediente.NomResultado = "Ocurrió un error al registrar el STD";
        //                }
        //                Contador++;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            resultadoExpediente.CodResultado = 0;
        //            resultadoExpediente.NomResultado = ex.Message;
        //        }
        //    }
        //    return resultadoExpediente;
        //}
        //#endregion

        #region Crea solo expediente
        public STDVM creaSoloExpediente(STDVM std)
        {
            var resultadoSTD = new STDDAL().CrearExpedienteSTD(std);

            return resultadoSTD;
        }
        #endregion


        #region Crea Expediente Principal
        public ResultadoProcedimientoVM CrearExpediente2(ExpedienteModelo expediente, EmpresaModelo empresa, PersonaModelo persona, List<ReciboModelo> recibo, STDVM STD, List<DetalleSolicitudModelo> detalleSolicitud, ResolucionModelo resolucion, ResolucionExpedienteModelo resolucionExpediente, List<OperadorModelo> detalleOperador, OperadorModelo operador, CredencialOperadorModelo credencialOperador)
        {
            ResultadoProcedimientoVM resultadoExpediente = new ResultadoProcedimientoVM();
            ResultadoProcedimientoVM resultadoRecibo = new ResultadoProcedimientoVM();
            ResultadoProcedimientoVM resultadoPersona = new ResultadoProcedimientoVM();
            ResultadoProcedimientoVM resultadoEmpresa = new ResultadoProcedimientoVM();
            ResultadoProcedimientoVM resultadoDetalleSolicitud = new ResultadoProcedimientoVM();
            ResultadoProcedimientoVM resultadoResolucion = new ResultadoProcedimientoVM();
            ResultadoProcedimientoVM resultadoResolucionExpediente = new ResultadoProcedimientoVM();
            ResultadoProcedimientoVM resultadoOperador = new ResultadoProcedimientoVM();
            ResultadoProcedimientoVM resultadoOperadorEmpresa = new ResultadoProcedimientoVM();
            ResultadoProcedimientoVM resultadoCredencial = new ResultadoProcedimientoVM();

            #region CONSULTA SI EL VOUCHER EXISTE
            if (recibo.Count > 0)
            {
                resultadoRecibo = ConsultaRecibo(recibo);
                if (resultadoRecibo.CodResultado == 1)
                {
                    resultadoExpediente.CodResultado = 0;
                    resultadoExpediente.NomResultado = resultadoRecibo.NomResultado;
                    return resultadoExpediente;
                }
            }
            #endregion

            #region SETEA CAMPOS DEPENDIENDO EL TIPO DE PERSONA
            if (persona.ID_TIPO_PERSONA == EnumParametro.PersonaNatural.ValorEntero())
            {
                STD.ID_PERSONA = persona.ID_PERSONA;
                STD.PARA = persona.NOMBRES;
                expediente.NUMERO_RECURRENTE = persona.NRO_DOCUMENTO;
                expediente.NUMERO_SOLICITANTE = persona.NRO_DOCUMENTO;
            }
            else
            {
                STD.ID_PROVEEDOR = STD.ID_PROVEEDOR;
                expediente.NUMERO_RECURRENTE = persona.NRO_DOCUMENTO;
                expediente.NUMERO_SOLICITANTE = empresa.RUC;
            }
            #endregion

            int IDDOC_PADRE = 0;
            int IDDOC_HIJO = 0;
            int IDFLUJO = 0;


            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    var detalleProcedimiento = new ProcedimientoDAL().ConsultarDatosProcedimientoHijo(expediente.ID_PROCEDIMIENTO, persona.ID_TIPO_PERSONA);
                    int expedientePadre = 0;
                    var id_procedimiento_padre = 0;


                    foreach (var itemProcedimiento in detalleProcedimiento)
                    {
                        #region REGISTRA STD 
                        var resultadoRegistroSTD = CrearExpediente(expediente, STD, persona);

                        if (detalleProcedimiento.First() == itemProcedimiento) //Primer registro
                        {
                            IDDOC_PADRE = resultadoRegistroSTD.IDDOC;
                            id_procedimiento_padre = itemProcedimiento.ID_PROCEDIMIENTO;
                        }
                        else if (detalleProcedimiento.Last() == itemProcedimiento)
                        {
                            IDFLUJO = resultadoRegistroSTD.IDFLUJO.ValorEntero();
                            IDDOC_HIJO = resultadoRegistroSTD.IDDOC;
                        }
                        #endregion

                        if (resultadoRegistroSTD.IDDOC != 0)
                        {
                            #region REGISTRA EXPEDIENTE
                            if (detalleProcedimiento.First() == itemProcedimiento) //expediente padre
                            {
                                expediente.IS_PADRE_EXPEDIENTE = 1;
                            }
                            else //expediente hijos
                            {
                                expediente.IS_PADRE_EXPEDIENTE = 0;
                                expediente.ID_PROCEDIMIENTO = id_procedimiento_padre;
                                expediente.ID_EXPEDIENTE_PADRE = expedientePadre;
                            }

                            resultadoExpediente = ExpedienteDAL.CrearExpediente(expediente);

                            if (detalleProcedimiento.First() == itemProcedimiento)
                            {
                                expedientePadre = resultadoExpediente.CodAuxiliar;
                                resultadoExpediente.IDDOC_PADRE = IDDOC_PADRE;
                            }


                            if (resultadoExpediente.CodResultado == 1)
                            {
                                foreach (var item in recibo)
                                {
                                    if (item.ID_PROCEDIMIENTO == expediente.ID_PROCEDIMIENTO)
                                    {
                                        item.ID_EXPEDIENTE = resultadoExpediente.CodAuxiliar;
                                        resultadoRecibo = ReciboDAL.CrearRecibo(item);
                                    }
                                }

                                #region REGISTRO PERSONA NATURAL O JURIDICA
                                resultadoPersona = CrearPersona(persona, empresa);
                                #endregion

                                #region REGISTRA DETALLE SOLICITUD
                                foreach (var item in detalleSolicitud)
                                {
                                    item.ID_EXPEDIENTE = resultadoExpediente.CodAuxiliar;
                                    resultadoDetalleSolicitud = DetalleSolicitudDAL.CrearDetalleSolicitud(item);
                                }
                                #endregion
                            }
                            #endregion
                        }
                        else
                        {
                            resultadoExpediente.CodResultado = 0;
                            resultadoExpediente.NomResultado = "Ocurrió un error al registrar el STD";
                        }

                    }

                    #region REGISTRAR RESOLUCION

                    if (Procedimiento.RA.BuscaValorArray(expediente.ID_PROCEDIMIENTO))
                    {
                        STD.IDDOC_PADRE = IDDOC_PADRE;
                        STD.IDDOC_HIJO = IDDOC_HIJO;
                        STD.IDFLUJO = IDFLUJO;
                        STD.ASUNTO = "SOLICITO RENOVACION.";

                        resolucionExpediente.RUC = empresa.RUC;
                        resolucionExpediente.ID_TIPO_PERSONA = persona.ID_TIPO_PERSONA;
                        resolucionExpediente.ID_PROCEDIMIENTO = expediente.ID_PROCEDIMIENTO;
                        resultadoResolucion = GenerarResolucion(STD, expedientePadre, resolucion, resolucionExpediente);

                        STD.IDDOC = resultadoExpediente.IDDOC_PADRE;
                        CerrarExpedienteSTD(STD);
                    }
                    #endregion

                    #region CIERRA EXPEDIENTE STD
                    if (Procedimiento.OPE.BuscaValorArray(expediente.ID_PROCEDIMIENTO) || Procedimiento.CREDOPE.BuscaValorArray(expediente.ID_PROCEDIMIENTO) || Procedimiento.DUPOPE.BuscaValorArray(expediente.ID_PROCEDIMIENTO))
                    {
                        STD.IDDOC = resultadoExpediente.IDDOC_PADRE;
                        CerrarExpedienteSTD(STD);



                        if (Procedimiento.OPE.BuscaValorArray(expediente.ID_PROCEDIMIENTO))
                        {
                            OperadorEmpresaModelo operadorEmpresa = new OperadorEmpresaModelo();
                            foreach (var item in detalleOperador)
                            {
                                resultadoOperador = new OperadorBLL().CrearOperador(item);

                                operadorEmpresa.ID_OPERADOR = resultadoOperador.CodAuxiliar;
                                operadorEmpresa.ID_EXPEDIENTE = resultadoExpediente.CodAuxiliar;
                                operadorEmpresa.ID_ESTADO = 1;
                                operadorEmpresa.RUC = empresa.RUC;
                                operadorEmpresa.USUARIO_REG = expediente.USUARIO_REG;

                                resultadoOperadorEmpresa = new OperadorBLL().EnlazarOperadorEmpresa(operadorEmpresa); //aqui agregar transaccion para asegurar 

                                resultadoExpediente.NomResultado = resultadoOperadorEmpresa.NomResultado;
                                resultadoExpediente.CodResultado = resultadoOperadorEmpresa.CodResultado;
                                resultadoExpediente.CodAuxiliar = operadorEmpresa.ID_EXPEDIENTE;
                            }
                        }
                        else if (Procedimiento.CREDOPE.BuscaValorArray(expediente.ID_PROCEDIMIENTO) || Procedimiento.DUPOPE.BuscaValorArray(expediente.ID_PROCEDIMIENTO))
                        {
                            OperadorEmpresaModelo operadorEmpresa = new OperadorEmpresaModelo();
                            resultadoOperador = new OperadorBLL().CrearOperador(operador);
                            resultadoExpediente.COD_OPERADOR = resultadoOperador.CodAuxiliar;

                            credencialOperador.ID_EXPEDIENTE = resultadoExpediente.CodAuxiliar;
                            credencialOperador.ID_OPERADOR = resultadoExpediente.COD_OPERADOR;

                            resultadoCredencial = new CredencialOperadorBLL().CrearCredencialOperador(credencialOperador);

                            if (resultadoCredencial.CodResultado == 0)
                            {
                                resultadoExpediente.CodResultado = resultadoCredencial.CodResultado;
                                resultadoExpediente.NomResultado = resultadoCredencial.NomResultado;
                                scope.Dispose();
                                Conexion.finalizar(ref bdConn);
                                return resultadoExpediente;
                            }

                            if (expediente.ID_MODALIDAD_SERVICIO != EnumModalidadServicio.TransporteRegularPersona.ValorEntero())
                            {
                                operadorEmpresa.ID_OPERADOR = resultadoOperador.CodAuxiliar;
                                operadorEmpresa.ID_EXPEDIENTE = resultadoExpediente.CodAuxiliar;
                                operadorEmpresa.ID_ESTADO = 1;
                                operadorEmpresa.RUC = empresa.RUC;
                                operadorEmpresa.USUARIO_REG = expediente.USUARIO_REG;
                                resultadoOperadorEmpresa = new OperadorBLL().EnlazarOperadorEmpresa(operadorEmpresa);
                            }


                        }


                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    resultadoExpediente.CodResultado = 0;
                    resultadoExpediente.NomResultado = ex.Message;
                }

                if (resultadoExpediente.CodResultado == 1)
                {
                    scope.Complete();
                }
                else { scope.Dispose(); }
            }
            Conexion.finalizar(ref bdConn);
            return resultadoExpediente;
        }
        #endregion

        #region Crea Expediente Operador
        public ResultadoProcedimientoVM CrearExpedienteCita(ExpedienteModelo expediente, EmpresaModelo empresa, PersonaModelo persona, STDVM STD)
        {
            ResultadoProcedimientoVM resultadoExpediente = new ResultadoProcedimientoVM();
            ResultadoProcedimientoVM resultadoPersona = new ResultadoProcedimientoVM();
            ResultadoProcedimientoVM resultadoEmpresa = new ResultadoProcedimientoVM();

            #region SETEA CAMPOS DEPENDIENDO EL TIPO DE PERSONA
            if (persona.ID_TIPO_PERSONA == EnumParametro.PersonaNatural.ValorEntero())
            {
                STD.ID_PERSONA = persona.ID_PERSONA;
                STD.PARA = persona.NOMBRES;
                expediente.NUMERO_RECURRENTE = persona.NRO_DOCUMENTO;
                expediente.NUMERO_SOLICITANTE = persona.NRO_DOCUMENTO;
            }
            else
            {
                STD.ID_PROVEEDOR = STD.ID_PROVEEDOR;
                expediente.NUMERO_RECURRENTE = persona.NRO_DOCUMENTO;
                expediente.NUMERO_SOLICITANTE = empresa.RUC;
            }
            #endregion

            int IDDOC_PADRE = 0;
            int IDDOC_HIJO = 0;
            int IDFLUJO = 0;
            int Contador = 0;
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    #region REGISTRA STD 
                    var resultadoRegistroSTD = new STDDAL().CrearExpedienteSTD(STD);

                    expediente.IDDOC = resultadoRegistroSTD.IDDOC;
                    expediente.NUMERO_SID = resultadoRegistroSTD.NUMERO_SID;
                    expediente.NUMERO_ANIO = resultadoRegistroSTD.NUMERO_ANIO;
                    expediente.IDFLUJO = resultadoRegistroSTD.IDFLUJO.ValorEntero();

                    if (Contador == 0)
                    {
                        IDDOC_PADRE = resultadoRegistroSTD.IDDOC;
                    }
                    else
                    {
                        IDFLUJO = resultadoRegistroSTD.IDFLUJO.ValorEntero();
                        IDDOC_HIJO = resultadoRegistroSTD.IDDOC;
                    }
                    #endregion

                    if (expediente.IDDOC != 0)
                    {
                        expediente.IS_PADRE_EXPEDIENTE = 1;
                        resultadoExpediente = ExpedienteDAL.CrearExpedienteCita(expediente);

                        if (resultadoExpediente.CodResultado == 1)
                        {
                            #region REGISTRO PERSONA NATURAL O JURIDICA
                            if (persona.ID_TIPO_PERSONA == EnumParametro.PersonaNatural.ValorEntero())
                            {
                                resultadoPersona = PersonaDAL.CrearPersona(persona);
                            }
                            else
                            {
                                resultadoPersona = PersonaDAL.CrearPersona(persona);
                                resultadoEmpresa = EmpresaDAL.CrearEmpresa(empresa);
                            }
                            #endregion
                        }

                    }
                    else
                    {
                        resultadoExpediente.CodResultado = 0;
                        resultadoExpediente.NomResultado = "Ocurrió un error al registrar el STD";
                    }
                    Contador++;
                    //}
                }
                catch (Exception ex)
                {
                    resultadoExpediente.CodResultado = 0;
                    resultadoExpediente.NomResultado = ex.Message;
                }
            }
            return resultadoExpediente;
        }
        #endregion

        #region CREAR PERSONA NATURAL O JURIDICA
        public ResultadoProcedimientoVM CrearPersona(PersonaModelo persona, EmpresaModelo empresa)
        {
            ResultadoProcedimientoVM resultadoPersona = new ResultadoProcedimientoVM();
            if (persona.ID_TIPO_PERSONA == EnumParametro.PersonaJuridica.ValorEntero())
            {
                resultadoPersona = EmpresaDAL.CrearEmpresa(empresa);
            }
            resultadoPersona = PersonaDAL.CrearPersona(persona);
            return resultadoPersona;
        }
        #endregion

        #region CERRAR EXPEDIENTE 
        public ResultadoProcedimientoVM CerrarExpedienteSTD(STDVM STD)
        {
            ResultadoProcedimientoVM resultadoExpediente = new ResultadoProcedimientoVM();
            var resultadocierraSTD = new STDDAL().CerrarExpedienteSTD(STD);
            return resultadoExpediente;
        }
        #endregion

        #region Consultar Expediente Cita

        public Expediente_CitaVM BuscarCita(string idexpediente)
        {
            // ExpedienteDAL resultadoExpediente = new ExpedienteDAL();

            return ExpedienteDAL.getDatosConsulCita(idexpediente);
        }
        #endregion


        #region consulta recibo
        public ResultadoProcedimientoVM ConsultaRecibo(string nrorecibo)
        {
            ResultadoProcedimientoVM resultadoExpediente = new ResultadoProcedimientoVM();
            var resultadoComprobante = ReciboDAL.BuscarRecibo(nrorecibo);
            //
            if (resultadoComprobante.NUMERO_RECIBO != null)
            {
                resultadoExpediente.CodResultado = 1;
                resultadoExpediente.NomResultado = "Voucher " + resultadoComprobante.NUMERO_RECIBO + " ya fue registrado el " + resultadoComprobante.FECHA_EMISION.ValorFechaCorta();
            }
            
            return resultadoExpediente;
        }
        #endregion


        #region Consulta Recibo
        public ResultadoProcedimientoVM ConsultaRecibo(List<ReciboModelo> recibo)
        {
            ResultadoProcedimientoVM resultadoExpediente = new ResultadoProcedimientoVM();

            foreach (var item in recibo)
            {
                var resultadoComprobante = ReciboDAL.BuscarRecibo(item.NUMERO_RECIBO.ValorCadena());

                if (resultadoComprobante.NUMERO_RECIBO != null)
                {
                    resultadoExpediente.CodResultado = 1;
                    resultadoExpediente.NomResultado = "Voucher " + resultadoComprobante.NUMERO_RECIBO + " ya fue registrado el " + resultadoComprobante.FECHA_EMISION.ValorFechaCorta();
                }
            }
            return resultadoExpediente;
        }
        #endregion



        #region GENERAR RESOLUCION
        public ResultadoProcedimientoVM GenerarResolucion(STDVM STD, int ID_EXPEDIENTE, ResolucionModelo resolucion, ResolucionExpedienteModelo resolucionExpediente)
        {
            ResultadoProcedimientoVM resultadoResolucion = new ResultadoProcedimientoVM();
            ResultadoProcedimientoVM resultadoResolucionExpediente = new ResultadoProcedimientoVM();

            var generaResolucion = new STDDAL().GenerarResolucionSTD(STD);
            resultadoResolucion = ResolucionDAL.CrearResolucion(resolucion);

            if (resultadoResolucion.CodResultado == 1)
            {
                resolucionExpediente.ID_RESOLUCION = resultadoResolucion.CodAuxiliar;
                resolucionExpediente.ID_EXPEDIENTE = ID_EXPEDIENTE;
                resolucionExpediente.NUMERO_RESOLUCION = generaResolucion.IDCRTLNUM.ValorCadena();
                resultadoResolucionExpediente = ResolucionExpedienteDAL.CrearResolucionExpediente(resolucionExpediente);
            }
            return resultadoResolucionExpediente;
        }
        #endregion

        #region CREAR EXPEDIENTE
        public STDVM CrearExpediente(ExpedienteModelo expediente, STDVM STD, PersonaModelo persona)
        {
            STDVM resultadoSTD = new STDVM();
            //if (persona.ID_TIPO_PERSONA == EnumParametro.PersonaNatural.ValorEntero())
            //{
            //    STD.ID_PERSONA = persona.ID_PERSONA;
            //    STD.PARA = persona.NOMBRES;
            //}
            //else
            //{
            //    STD.ID_PROVEEDOR = STD.ID_PROVEEDOR;
            //}

            resultadoSTD = new STDDAL().CrearExpedienteSTD(STD);

            expediente.IDDOC = resultadoSTD.IDDOC;
            expediente.NUMERO_SID = resultadoSTD.NUMERO_SID;
            expediente.NUMERO_ANIO = resultadoSTD.NUMERO_ANIO;
            expediente.IDFLUJO = resultadoSTD.IDFLUJO.ValorEntero();

            return resultadoSTD;
        }
        #endregion

    }

}
