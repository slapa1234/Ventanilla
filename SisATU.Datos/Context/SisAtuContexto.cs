using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Datos.Context
{
    public class SisAtuContexto: DbContext
    {
        public SisAtuContexto()
            : base("sConexionSISREGISTRO")
        {
                
        }
    }
}
