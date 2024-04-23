using IBM.Data.DB2.iSeries;
using System.Data.Odbc;
using RecepcionesApi.Config;
using System.Diagnostics;

namespace ApiRecepciones.Controllers
{
    public class conexionAS400
    {

        public OdbcConnection conexion = new OdbcConnection();
        public conexionAS400()
        {

            try
            {
                string cadenaConexion = Configuraciones.dbConexion.Trim();
                conexion.ConnectionString = cadenaConexion;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en la cadena de conexion de BD.  {ex.Message}");
            }
        }

        public void ConectarBD()
        {

            try
            {
                conexion.Open();
            }
            catch (Exception ex)
            {


            }
        }


        public void DesconectarBD()
        {

            try
            {
                conexion.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al desconectar BD  {ex.Message}");
            }
        }

        private void eventLog_EntryWritten(object sender, EntryWrittenEventArgs e)
        {

        }

    }
}
