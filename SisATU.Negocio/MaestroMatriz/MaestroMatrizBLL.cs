using SisATU.Base.ViewModel;
using SisATU.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Negocio
{
    public class MaestroMatrizBLL
    {
        public MaestroMatrizVM ConsultaMaestroMatriz(int ID_TIPO_PERSONA, int ANIO_PERIODO, int ID_MODALIDAD_SERVICIO, string ANIO_FABRICACION)
        {
            MaestroMatrizDAL obj = new MaestroMatrizDAL();
            return obj.ConsultaMaestroMatriz(ID_TIPO_PERSONA, ANIO_PERIODO, ID_MODALIDAD_SERVICIO, ANIO_FABRICACION);
        }
    }
}
