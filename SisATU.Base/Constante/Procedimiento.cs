using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Base.Constante
{
    public static class Procedimiento
    {
        /// <summary>
        /// RA = RENOVACION DE LA AUTORIZACIÓN DE SERVICIO
        /// </summary>
        public static int[] RA = new[] { 28, 34, 47, 52 };

        /// <summary>
        /// TUC = TARJETA UNICA DE CIRCULACION
        /// </summary>
        public static int[] TUC = new[] { 28, 29, 51, 55 };

        /// <summary>
        /// CREDOPE = CREDENCIAL OPERADOR
        /// </summary>
        public static int[] CREDOPE = new[] { 1, 4 };
        /// <summary>
        /// OPE = INCLUSIÓN DEL CONDUCTOR O COBRADOR EN EL PADRON DE LA PERSONA JURÍDICA, QUE CUENTEN CON CREDENCIAL VIGENTE
        /// </summary>
        public static int[] OPE = new[] { 26 };
        /// <summary>
        /// DUPOPE = DEL DUPLICADO POR MODIFICACIÓN DE DATOS PÉRDIDA, DETERIORO ROBO DEL SERVICIO DE TRANSPORTE REGULAR
        /// </summary>
        public static int[] DUPOPE = new[] { 2, 5 };
    }
}
