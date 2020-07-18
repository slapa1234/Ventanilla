using SisATU.Base;
using SisATU.Base.Enumeradores;
using SisATU.Base.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Servicios
{
    public class STDService
    {
        #region CREAR EXPEDIENTE STD
        public STDVM CrearExpedienteSTD(STDVM std)
        {
            STDVM modelo = new STDVM();
            try
            {
                Servicio_STD.Servicio_STD servicio = new Servicio_STD.Servicio_STD();

                var STD = new Servicio_STD.HojaRuta()
                {
                    NUMERO_DOC = "S/N",
                    IDCLASE = 44,
                    FOLIOS_INI = 1,
                    FOLIOS_FIN = 1,
                    TIPOEXPEDIENTE = std.TIPO_EXPEDIENTE,
                    IDUNIDAD = std.IDUNIDAD_STD,
                    IDUSER_CREA = 20,
                    CODPAIS = std.CODPAIS,
                    CODDPTO = std.CODDPTO,
                    CODPROV = std.CODPROV,
                    CODDIST = std.CODDIST,
                    DIRECCION = std.DIRECCION_STD,
                    ESTADO = 3,
                    IDPERSON = std.ID_PERSONA,
                    IDPROVEE = std.ID_PROVEEDOR,
                    IDPROC = std.ID_PROCEDIMIENTO,
                    INSTITUCION_NO_REG = std.NOMBRE,
                    ASUNTO = std.OBSERVACION,
                };

                var resultado = servicio.AgregarHojaRuta(STD);

                modelo.IDDOC = resultado.IDDOC.ValorEntero();
                modelo.NUMERO_SID = resultado.NUMERO_SID;
                modelo.NUMERO_ANIO = resultado.NUMERO_ANIO;
                modelo.IDFLUJO = resultado.IDFLUJO;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return modelo;
        }
        #endregion

        #region CERRAR EXPEDIENTE STD
        public STDVM CerrarExpedienteSTD(STDVM std)
        {
            STDVM modelo = new STDVM();
            try
            {
                Servicio_STD.Servicio_STD servicio = new Servicio_STD.Servicio_STD();

                var STD = new Servicio_STD.cerrar()
                {
                    IDDOC = std.IDDOC,
                    IDUNIDAD = Convert.ToString(std.IDUNIDAD_STD),
                    OBSERVACION = "CERRADO AUTOMATICAMENTE POR SISSIT",

                };

                var resultado = servicio.ActualizarHojaRuta(STD);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return modelo;
        }
        #endregion

        #region STD ACUMULADOR
        public STDVM AcumuladorSTD(STDVM std)
        {
            STDVM modelo = new STDVM();
            try
            {
                Servicio_STD.Servicio_STD servicio = new Servicio_STD.Servicio_STD();

                var STD = new Servicio_STD.Acumulador()
                {
                    IDDOC_PADRE = std.IDDOC_PADRE,
                    IDDOC_HIJO = std.IDDOC_HIJO,
                    IDFLUJO_HIJO = std.IDFLUJO //HIJO
                };

                var resultado = servicio.AgregarAcumulador(STD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return modelo;
        }
        #endregion

        #region STD RESOLUCION
        public STDVM GenerarResolucionSTD(STDVM std)
        {
            STDVM modelo = new STDVM();
            try
            {
                Servicio_STD.Servicio_STD servicio = new Servicio_STD.Servicio_STD();

                var STD = new Servicio_STD.Resolucion()
                {
                    IDUNIDAD = 7,
                    IDCLASE = 24,
                    NUMEROCLASE = null,
                    NUMEROANIO = DateTime.Now.Year,
                    ASUNTO = std.ASUNTO, // "SOLICITO",
                    COMENTARIO = "NOTIFICAR AL INTERESADO.",
                    FECHA_DOC = DateTime.Now.ValorFechaCorta(),
                    PARA = std.PARA,// "Solicitante",
                    PARAFUNCIONARIO = std.PARA, // "solicitante",
                    REFERENCIA = null,
                    IDDOC = std.IDDOC_PADRE,
                    IDFLUJO = std.IDFLUJO,
                    IDUSER_PROYECTO_DOC = null,
                    IDUSER_CREA = 20,
                    ESTADO = "3",
                };
                var resultado = servicio.Generar_Resolucion(STD);
                modelo.IDCRTLNUM = resultado.IDCRTLNUM.ValorEntero();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return modelo;
        }
        #endregion

        #region BUSCAR PERSONA STD
        public PersonaVM BuscarPersonaSTD(string DNI)
        {
            PersonaVM persona = new PersonaVM();
            try
            {
                Servicio_STD.Servicio_STD servicioSTD = new Servicio_STD.Servicio_STD();
                var buscaPersona = servicioSTD.BuscarPersona(new Servicio_STD.Usuario() { USULOG = "PTseguro", USUCON = "PTs3gur0" }, new Servicio_STD.Persona() { DNI = DNI });
                persona.ID_PERSONA = buscaPersona.IDPERSON.ValorEntero();
                persona.CODPAIS = buscaPersona.CODPAIS.ValorEntero();
                persona.CODDPTO = buscaPersona.CODDPTO.ValorEntero();
                persona.CODPROV = buscaPersona.CODPROV.ValorEntero();
                persona.CODDIST = buscaPersona.CODDIST.ValorEntero();
                persona.DIRECCION_STD = buscaPersona.DIRECCION;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return persona;
        }
        #endregion

        #region CREAR PERSONA STD
        public PersonaVM CrearPersonaSTD(PersonaModelo modelo)
        {
            PersonaVM persona = new PersonaVM();
            try
            {
                Servicio_STD.Servicio_STD servicioSTD = new Servicio_STD.Servicio_STD();
                var STD = new Servicio_STD.Persona()
                {
                    APELLIDO_PATERNO = modelo.APELLIDO_PATERNO,
                    APELLIDO_MATERNO = modelo.APELLIDO_MATERNO,
                    NOMBRES = modelo.NOMBRES,
                    SEXO = "",
                    DNI = modelo.NRO_DOCUMENTO,
                    CODPAIS = 173,
                    CODDPTO = 15,
                    CODPROV = 1,
                    CODDIST = 1,
                    DIRECCION = modelo.DIRECCION,
                };
                var agregarPersona = servicioSTD.AgregarPersona(new Servicio_STD.Usuario() { USULOG = "PTseguro", USUCON = "PTs3gur0" }, STD);
                persona.APELLIDO_PATERNO = agregarPersona.APELLIDO_PATERNO;
                persona.APELLIDO_MATERNO = agregarPersona.APELLIDO_PATERNO;
                persona.NOMBRES = agregarPersona.NOMBRES;
                persona.NRO_DOCUMENTO = agregarPersona.DNI;
                persona.CODPAIS = agregarPersona.CODPAIS.ValorEntero();
                persona.CODDPTO = agregarPersona.CODDPTO.ValorEntero();
                persona.CODPROV = agregarPersona.CODPROV.ValorEntero();
                persona.CODDIST = agregarPersona.CODDIST.ValorEntero();
                persona.DIRECCION_STD = agregarPersona.DIRECCION;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return persona;
        }
        #endregion

        
    }
}
