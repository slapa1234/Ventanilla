using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU
{
    public static class DbContextExtensiones
    {
        public static IEnumerable<T> EjecutarProcedimientoAlmacenado<T>(this DbContext db, string procedimientoAlmacenado, params OracleParameter[] parametros)
        {
            StringBuilder comando = new StringBuilder();
            comando.Append("EXEC ");
            comando.Append(procedimientoAlmacenado);
            comando.Append(" ");

            for (int i = 0; i < parametros.Count(); i++)
            {
                if (i > 0)
                    comando.Append(",");
                comando.Append(parametros[i].ParameterName);
            }

            return db.Database.SqlQuery<T>(comando.ToString(), parametros);
        }
    }
}
