using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU
{
    public static class EnumExtensiones
    {
        public static int ValorEntero<T>(this T source)
        {
            return Convert.ToInt32(source);
        }

        public static string ValorCadena<T>(this T source)
        {
            return Convert.ToString(source);
        }

        public static string ValorFechaCorta<T>(this T source)
        {
            return Convert.ToDateTime(source).ToString("dd/MM/yyyy");
        }

        public static bool BuscaValorArray<T>(this T[]  source, int valor)
        {
            int resultado = Array.IndexOf(source, valor);
            if (resultado == -1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
