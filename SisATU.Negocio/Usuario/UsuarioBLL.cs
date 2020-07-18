using SisATU.Base;
using SisATU.Base.ViewModel;
using SisATU.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Negocio
{
    public class UsuarioBLL
    {
        public ResultadoProcedimientoVM CrearUsuario(UsuarioVM usuario)
        {
            UsuarioDAL usuarioDAL = new UsuarioDAL();
            return usuarioDAL.CrearUsuario(usuario);
        }

        public List<ComboModalidadServicioVM> BuscarModalidad(string RUC)
        {
            UsuarioDAL usuarioDAL = new UsuarioDAL();
            return usuarioDAL.BuscarModalidad(RUC);
        }

        public ResultadoProcedimientoVM CrearModalidadServicio(string RUC, int ID_MODALIDAD_SERVICIO)
        {
            UsuarioDAL usuarioDAL = new UsuarioDAL();
            return usuarioDAL.CrearModalidadServicio(RUC, ID_MODALIDAD_SERVICIO);
        }

        public UsuarioModelo BuscarRepresentante(string RUC, string NRO_DOCUMENTO, int ID_TIPO_DOCUMENTO)
        {
            UsuarioDAL usuarioDAL = new UsuarioDAL();
            return usuarioDAL.BuscarRepresentante(RUC, NRO_DOCUMENTO, ID_TIPO_DOCUMENTO);
        }

        public UsuarioModelo BuscarUsuario(string NRO_DOCUMENTO, string CLAVE, int ID_MODALIDAD_SERVICIO, int ID_TIPO_PERSONA)
        {
            UsuarioDAL usuarioDAL = new UsuarioDAL();
            return usuarioDAL.BuscarUsuario(NRO_DOCUMENTO, CLAVE, ID_MODALIDAD_SERVICIO, ID_TIPO_PERSONA);
        }

    }
}
