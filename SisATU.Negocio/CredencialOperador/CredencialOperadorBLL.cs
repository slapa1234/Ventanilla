using SisATU.Base;
using SisATU.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Negocio
{
    public class CredencialOperadorBLL
    {
        private CredencialOperadorDAL CredencialOperadorDAL;
        private Object bdConn;
        public CredencialOperadorBLL()
        {
            CredencialOperadorDAL = new CredencialOperadorDAL(ref bdConn);
        }
        public ResultadoProcedimientoVM CrearCredencialOperador(CredencialOperadorModelo credencialOperador)
        {
            return CredencialOperadorDAL.CrearCredencialOperador(credencialOperador);
        }
    }
}
