namespace RecepcionesApi.Models
{
    public class RequestRecepciones
    {

        public string CUENTAREC { get; set; }
        public string MONTOTRX { get; set; }
        public string NOMBRE_BEN { get; set; }
        public string IDEN_BENE { get; set; }
        public string TIPCTADES { get; set; }
        public string MONEDATRX { get; set; }
        public string BANCO_ORIGEN { get; set; }
        public string CNDMGSP { get; set; } //NUMERO DE REFERENCIA
        public string CNTRNS { get; set; }
        public string CANAL { get; set; }
        public string IDEN_ORIG { get; set; }
        public string NOMBRE_ORIG { get; set; }
        public string CUENTAORIG { get; set; }
        public string TIPCTAORIG { get; set; }
        public string DIRBENEF { get; set; }
        public string OBSERTRX { get; set; }    

        public string coopDest { get; set; }

        public string coopOrigen { get; set; }

        public string refPaysett {get; set; }

        public string BANDERA { get; set; }
    }
}
