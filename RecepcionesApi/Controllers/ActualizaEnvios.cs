using ApiRecepciones.Controllers;
using RecepcionesApi.Config;
using RecepcionesApi.Models;
using System.Data;
using System.Data.Odbc;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace RecepcionesApi.Controllers
{
    public class ActualizaEnvios
    {
        public static Response actualizaEnviosAch(RequestActualizar requestActualiza)
        {

            Configuraciones configuraciones = new Configuraciones();
            configuraciones = configuraciones.cargarPropiedades();
            conexionAS400 conn = new conexionAS400();
            string codRetorno = string.Empty; 
            string mensaje = string.Empty;
            try
            {
                conn.ConectarBD();
                if (conn != null)
                {
                    OdbcCommand cmd = conn.conexion.CreateCommand();
                    cmd.CommandText = "CALL POPULARPGM.SP_SE_VAR_ACTUALIZA_ACH(?,?,?,?,?,?,?,?,?,?,?,?,?)";
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Salida de código
                    cmd.Parameters.Add("OCODSALIDA", OdbcType.Numeric);
                    cmd.Parameters["OCODSALIDA"].Size = 10;
                    cmd.Parameters["OCODSALIDA"].Direction = ParameterDirection.Output;

                    //Salida de mensaje
                    cmd.Parameters.Add("OMENSAJE", OdbcType.Char);
                    cmd.Parameters["OMENSAJE"].Size = 256;
                    cmd.Parameters["OMENSAJE"].Direction = ParameterDirection.Output;

                    //Demas parametros
                    cmd.Parameters.Add("ITIPOACTU", OdbcType.Int).Value = requestActualiza.ITIPOACTU;
                    cmd.Parameters["ITIPOACTU"].Size = 10;
                    cmd.Parameters["ITIPOACTU"].Direction = ParameterDirection.Input;

                    cmd.Parameters.Add("ISUBACTU", OdbcType.Int).Value = requestActualiza.ISUBACTU;
                    cmd.Parameters["ISUBACTU"].Size = 10;
                    cmd.Parameters["ISUBACTU"].Direction = ParameterDirection.Input;

                    cmd.Parameters.Add("ICTAORIGEN", OdbcType.Char).Value = requestActualiza.ICTAORIGEN;
                    cmd.Parameters["ICTAORIGEN"].Size = 30;
                    cmd.Parameters["ICTAORIGEN"].Direction = ParameterDirection.Input;

                    cmd.Parameters.Add("ITIPOCTAORIGEN", OdbcType.Char).Value = requestActualiza.ITIPOCTAORIGEN;
                    cmd.Parameters["ITIPOCTAORIGEN"].Size = 1;
                    cmd.Parameters["ITIPOCTAORIGEN"].Direction = ParameterDirection.Input;

                    cmd.Parameters.Add("IMONCTAORIGEN", OdbcType.Char).Value = requestActualiza.IMONCTAORIGEN;
                    cmd.Parameters["IMONCTAORIGEN"].Size = 3;
                    cmd.Parameters["IMONCTAORIGEN"].Direction = ParameterDirection.Input;

                    cmd.Parameters.Add("IBNKORIGEN", OdbcType.Char).Value = requestActualiza.IBNKORIGEN;
                    cmd.Parameters["IBNKORIGEN"].Size = 30;
                    cmd.Parameters["IBNKORIGEN"].Direction = ParameterDirection.Input;

                    cmd.Parameters.Add("ICTADESTINO", OdbcType.Char).Value = requestActualiza.ICTADESTINO;
                    cmd.Parameters["ICTADESTINO"].Size = 30;
                    cmd.Parameters["ICTADESTINO"].Direction = ParameterDirection.Input;

                    cmd.Parameters.Add("ICODIGOMENSAJE", OdbcType.Char).Value = requestActualiza.ICODIGOMENSAJE;
                    cmd.Parameters["ICODIGOMENSAJE"].Size = 50;
                    cmd.Parameters["ICODIGOMENSAJE"].Direction = ParameterDirection.Input;

                    cmd.Parameters.Add("ICODIGOTRX", OdbcType.Char).Value = requestActualiza.ICODIGOTRX;
                    cmd.Parameters["ICODIGOTRX"].Size = 50;
                    cmd.Parameters["ICODIGOTRX"].Direction = ParameterDirection.Input;

                    cmd.Parameters.Add("INSTRUCCIONID", OdbcType.Char).Value = requestActualiza.INSTRUCCIONID;
                    cmd.Parameters["INSTRUCCIONID"].Size = 35;
                    cmd.Parameters["INSTRUCCIONID"].Direction = ParameterDirection.Input;

                    cmd.Parameters.Add("IOBSERVACIONES", OdbcType.Char).Value = requestActualiza.IOBSERVACIONES;
                    cmd.Parameters["IOBSERVACIONES"].Size = 300;
                    cmd.Parameters["IOBSERVACIONES"].Direction = ParameterDirection.Input;


                    cmd.Prepare();
                    cmd.ExecuteNonQuery();

                    codRetorno = cmd.Parameters["OCODSALIDA"].Value.ToString();
                    mensaje = cmd.Parameters["OMENSAJE"].Value.ToString();

                    cmd.Dispose();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al procesar actualización : " + ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                conn.DesconectarBD();
            }

            return new Response
            {
                codMensaje = codRetorno,
                mensaje= mensaje
            };
                 
        }

        public static Response insertarTablaAch(RequestActualizar request)
        {
            Configuraciones configuraciones = new Configuraciones();
            configuraciones = configuraciones.cargarPropiedades();
            conexionAS400 conn = new conexionAS400();
            string codRetorno = string.Empty;
            string mensaje = string.Empty;
            string pipe = "|";

            try
            {
                conn.ConectarBD();
                if (conn != null)
                {
                    string sql = "INSERT INTO ACHDAT.ACH0001 (CAMPO) VALUES(?)";
                    OdbcCommand cmd = new OdbcCommand(sql, conn.conexion);
                    cmd.Parameters.Add("@CAMPO", OdbcType.Char).Value = request.ITIPOACTU + pipe + request.ISUBACTU + pipe + request.ICTAORIGEN +
                                                                        pipe + request.ITIPOCTAORIGEN + pipe + request.IMONCTAORIGEN + pipe + request.IBNKORIGEN +
                                                                        pipe + request.ICTADESTINO + pipe + request.ICODIGOMENSAJE + pipe + request.ICODIGOTRX +
                                                                        pipe + request.INSTRUCCIONID + pipe + request.IOBSERVACIONES + pipe + request.BANDERA;
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();

                }
            }
            catch(Exception ex) {
                string e = ex.Message;

            }
            finally
            {
                conn.DesconectarBD();
            }

            return new Response
            {
                codMensaje = "00",
                mensaje = "Exitoso"
            };
        }

      


    }
}
