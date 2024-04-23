namespace RecepcionesApi.Models
{
    public class RequestActualizar
    {
        public int ITIPOACTU { get; set; }
        public int ISUBACTU { get; set; }
        public string ICTAORIGEN { get; set; }
        public string ITIPOCTAORIGEN { get; set; }
        public string IMONCTAORIGEN { get; set; }
        public string IBNKORIGEN { get; set; }
        public string ICTADESTINO { get; set; }
        public string ICODIGOMENSAJE { get; set; }
        public string ICODIGOTRX { get; set; }
        public string INSTRUCCIONID { get; set; }
        public string IOBSERVACIONES { get; set; }

        public string refPaysett { get; set; }  

        public string BANDERA { get; set; }
    }
}
