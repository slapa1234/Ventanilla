using SisATU.Base;
using SisATU.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Negocio
{
    public class VencimientoCITVBLL
    {
        private VehiculoCITVDAL VehiculoCITVDAL;
        private Object bdConn;
        public VencimientoCITVBLL()
        {
            VehiculoCITVDAL = new VehiculoCITVDAL(ref bdConn);
        }
        public ResultadoProcedimientoVM CrearVehiculoCITV(VehiculoCITVModelo VehiculoCITV)
        {
            return VehiculoCITVDAL.CrearVehiculoCITV(VehiculoCITV);
        }
    }
}
