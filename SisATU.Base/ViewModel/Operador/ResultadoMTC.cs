using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisATU.Base.ViewModel.Operador
{

    public class ResultadoMTC
    {
        public ResultadoMTC()
        {
            GetDatosUltimaLicenciaMTCResponse = new Getdatosultimalicenciamtcresponse();
        }
        public Getdatosultimalicenciamtcresponse GetDatosUltimaLicenciaMTCResponse { get; set; }
    }

    public class Getdatosultimalicenciamtcresponse
    {
        public Getdatosultimalicenciamtcresponse()
        {
            GetDatosUltimaLicenciaMTCResult = new Getdatosultimalicenciamtcresult();
        }
        public Getdatosultimalicenciamtcresult GetDatosUltimaLicenciaMTCResult { get; set; }
    }

    public class Getdatosultimalicenciamtcresult
    {
        public Getdatosultimalicenciamtcresult()
        {
            diffgram = new Diffgram();
        }
        public Diffgram diffgram { get; set; }
    }

    public class Diffgram
    {
        public Diffgram()
        {
            NewDataSet = new Newdataset();
        }
        public Newdataset NewDataSet { get; set; }
    }

    public class Newdataset
    {
        public Newdataset()
        {
            Table = new Table();
        }
        public Table Table { get; set; }
    }

    public class Table
    {
        public string id { get; set; }
        public string rowOrder { get; set; }
        public int TIPO_DOC { get; set; }
        public string NUM_DOCUMENTO { get; set; }
        public string NUM_LICENCIA { get; set; }
        public string CATEGORIA { get; set; }
        public string APE_PATERNO { get; set; }
        public string APE_MATERNO { get; set; }
        public string NOMBRE { get; set; }
        public string RESTRICCION { get; set; }
        public string FECREV { get; set; }
        public string FECEXP { get; set; }
        public string ESTADO { get; set; }
    }



}
