using ApiRecepciones.Controllers;
using RecepcionesApi.Config;
using RecepcionesApi.Models;
using System.Data.Odbc;
using System.Data;
using ApprovalUtilities.SimpleLogger.Writers;

namespace RecepcionesApi.Controllers
{
    public class RecepcionesAch
    {
        public static Response recepcionaTransacciones(RequestRecepciones requestRecepciones)
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
                    cmd.CommandText = "CALL POPULARPGM.SP_SE_VAR_RECIBO_ACH(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Salida de código
                    cmd.Parameters.Add("CODERROR", OdbcType.Int);
                    cmd.Parameters["CODERROR"].Size = 10;
                    cmd.Parameters["CODERROR"].Direction = ParameterDirection.Output;

                    //Salida de mensaje
                    cmd.Parameters.Add("DESERROR", OdbcType.Char);
                    cmd.Parameters["DESERROR"].Size = 256;
                    cmd.Parameters["DESERROR"].Direction = ParameterDirection.Output;

                    //Demas parametros
                    cmd.Parameters.Add("CUENTAREC", OdbcType.Char).Value = requestRecepciones.CUENTAREC;
                    cmd.Parameters["CUENTAREC"].Size = 30;
                    cmd.Parameters["CUENTAREC"].Direction = ParameterDirection.Input;

                    cmd.Parameters.Add("MONTOTRX", OdbcType.Char).Value = requestRecepciones.MONTOTRX;
                    cmd.Parameters["MONTOTRX"].Size = 20;
                    cmd.Parameters["MONTOTRX"].Direction = ParameterDirection.Input;

                    cmd.Parameters.Add("NOMBRE_BEN", OdbcType.Char).Value = requestRecepciones.NOMBRE_BEN;
                    cmd.Parameters["NOMBRE_BEN"].Size = 60;
                    cmd.Parameters["NOMBRE_BEN"].Direction = ParameterDirection.Input;

                    cmd.Parameters.Add("IDEN_BENE", OdbcType.Char).Value = requestRecepciones.IDEN_BENE;
                    cmd.Parameters["IDEN_BENE"].Size = 30;
                    cmd.Parameters["IDEN_BENE"].Direction = ParameterDirection.Input;

                    cmd.Parameters.Add("TIPCTADES", OdbcType.Char).Value = requestRecepciones.TIPCTADES;
                    cmd.Parameters["TIPCTADES"].Size = 1;
                    cmd.Parameters["TIPCTADES"].Direction = ParameterDirection.Input;

                    cmd.Parameters.Add("MONEDATRX", OdbcType.Char).Value = requestRecepciones.MONEDATRX;
                    cmd.Parameters["MONEDATRX"].Size = 3;
                    cmd.Parameters["MONEDATRX"].Direction = ParameterDirection.Input;

                    cmd.Parameters.Add("BANCO_ORIGEN", OdbcType.Char).Value = requestRecepciones.BANCO_ORIGEN;
                    cmd.Parameters["BANCO_ORIGEN"].Size = 30;
                    cmd.Parameters["BANCO_ORIGEN"].Direction = ParameterDirection.Input;

                    cmd.Parameters.Add("CNDMGSP", OdbcType.Char).Value = requestRecepciones.CNDMGSP;
                    cmd.Parameters["CNDMGSP"].Size = 50;
                    cmd.Parameters["CNDMGSP"].Direction = ParameterDirection.Input;

                    cmd.Parameters.Add("CNTRNS", OdbcType.Char).Value = requestRecepciones.CNTRNS;
                    cmd.Parameters["CNTRNS"].Size = 50;
                    cmd.Parameters["CNTRNS"].Direction = ParameterDirection.Input;

                    cmd.Parameters.Add("CANAL", OdbcType.Char).Value = requestRecepciones.CANAL;
                    cmd.Parameters["CANAL"].Size = 2;
                    cmd.Parameters["CANAL"].Direction = ParameterDirection.Input;

                    cmd.Parameters.Add("IDEN_ORIG", OdbcType.Char).Value = requestRecepciones.IDEN_ORIG;
                    cmd.Parameters["IDEN_ORIG"].Size = 30;
                    cmd.Parameters["IDEN_ORIG"].Direction = ParameterDirection.Input;

                    cmd.Parameters.Add("NOMBRE_ORIG", OdbcType.Char).Value = requestRecepciones.NOMBRE_ORIG;
                    cmd.Parameters["NOMBRE_ORIG"].Size = 60;
                    cmd.Parameters["NOMBRE_ORIG"].Direction = ParameterDirection.Input;

                    cmd.Parameters.Add("CUENTAORIG", OdbcType.Char).Value = requestRecepciones.CUENTAORIG;
                    cmd.Parameters["CUENTAORIG"].Size = 30;
                    cmd.Parameters["CUENTAORIG"].Direction = ParameterDirection.Input;

                    cmd.Parameters.Add("TIPCTAORIG", OdbcType.Char).Value = requestRecepciones.TIPCTAORIG;
                    cmd.Parameters["TIPCTAORIG"].Size = 1;
                    cmd.Parameters["TIPCTAORIG"].Direction = ParameterDirection.Input;

                    cmd.Parameters.Add("DIRBENEF", OdbcType.Char).Value = requestRecepciones.DIRBENEF;
                    cmd.Parameters["DIRBENEF"].Size = 60;
                    cmd.Parameters["DIRBENEF"].Direction = ParameterDirection.Input;

                    cmd.Parameters.Add("OBSERTRX", OdbcType.Char).Value = requestRecepciones.OBSERTRX;
                    cmd.Parameters["OBSERTRX"].Size = 300;
                    cmd.Parameters["OBSERTRX"].Direction = ParameterDirection.Input;

                    cmd.Prepare();
                    cmd.ExecuteNonQuery();

                    codRetorno = cmd.Parameters["CODERROR"].Value.ToString();
                    mensaje = cmd.  Parameters["DESERROR"].Value.ToString();

                    try
                    {
                        string sql = "SELECT @ACHCDERRO,@ACHCDDSE FROM ACHDAT.@ACHPF0001 WHERE @ACHCODERR = '" + codRetorno + "'";
                        OdbcCommand cmdSelect = new OdbcCommand(sql, conn.conexion);
                        OdbcDataReader reader = cmdSelect.ExecuteReader();
                        while (reader.Read())
                        {
                            codRetorno = Convert.ToString(reader["@ACHCDERRO"]).Trim();
                            mensaje = Convert.ToString(reader["@ACHCDDSE"]).Trim();

                        }

                        reader.Close();
                        cmdSelect.Dispose();

                    }
                    catch(Exception ex) {

                        File.WriteAllText("C:\\path\\catch.txt", ex.Message);
                    }
                                   

                    cmd.Dispose();

                 
                    File.WriteAllText("C:\\path\\codRetorno.txt", codRetorno);
                    File.WriteAllText("C:\\path\\deserror.txt", mensaje);

                    
                }

            }
            catch(Exception ex)
            {
                Console.WriteLine("Error al procesar recepción : " + ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                conn.DesconectarBD();

                File.WriteAllText("C:\\path\\codRetornoFinally.txt", codRetorno);
                File.WriteAllText("C:\\path\\deserrorFinally.txt", mensaje);
            }
            return new Response
            {
                codMensaje = codRetorno,
                mensaje = mensaje
            };
        }

        public String obtenerRutaBanco(String rutaBanco)
        {

            conexionAS400 conn = new conexionAS400();
            string rutaRetorno = string.Empty;

            try
            {
                conn.ConectarBD();
                if (conn != null)
                {
                    Configuraciones configuraciones = new Configuraciones();
                    configuraciones = configuraciones.cargarPropiedades();
                    string sql = "SELECT @ACHPFCOD FROM ACHDAT.@ACHPF6000 WHERE @ACHPFRUT = '" + rutaBanco + "'";
                    OdbcCommand cmd = new OdbcCommand(sql, conn.conexion);
                    OdbcDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        rutaRetorno = Convert.ToString(reader["@ACHPFCOD"]).Trim();
                    }
                    reader.Close();
                    cmd.Dispose();

                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.DesconectarBD();
            }

            return rutaRetorno;
        }
    }
}
