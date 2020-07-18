using SisATU.Base;
using SisATU.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Negocio
{
    public class VehiculoAseguradoraBLL
    {
        private VehiculoAseguradoraDAL VehiculoAseguradoraDAL;
        private Object bdConn;
        public VehiculoAseguradoraBLL()
        {
            VehiculoAseguradoraDAL = new VehiculoAseguradoraDAL(ref bdConn);
        }
        public ResultadoProcedimientoVM CrearVehiculoAseguradora(VehiculoAseguradoraModelo VehiculoAseguradora)
        {
            return VehiculoAseguradoraDAL.CrearVehiculoAseguradora(VehiculoAseguradora);
        }
    }
}
