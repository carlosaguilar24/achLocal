using ApiRecepciones.Controllers;
using RecepcionesApi.Config;
using RecepcionesApi.Models;
using System.Data.Odbc;

namespace RecepcionesApi.Repositorio
{
    public class RegistraEntranteIndirecta
    {
        public static void RegistraEntrantes(RequestRecepciones requestRecepciones)
        {
            conexionAS400 conn = new conexionAS400();
            try
            {
                conn.ConectarBD();
                if (conn != null)
                {
                    Configuraciones configuraciones = new Configuraciones();
                    configuraciones = configuraciones.cargarPropiedades();
                    string sql = "INSERT INTO ACHIDAT00.ACHIPF0051 (@CNTUSR,@CNTCTA,@CNTMNT,@CNTNMB,@CNTIDB,@CNTDRBNF,@CNTTPCTA,@CNTMONED,@CNTPCTOR,@CNTIDORG,@CNTNMBOR,@CNTCTAOR,@CNTBNKOR,@CNTCNLEN,@IDTRA,@CNTIDEST,@CNTIDIND,@CNTIDORD,@CNTRGLAD,@CNTOBSER,@IDBNK,@FCHRE,@HRARE,@CODERR,@CNCODG,@CNTSTAT)" +
                                 "VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
                    OdbcCommand cmd = new OdbcCommand(sql, conn.conexion);
                    cmd.Parameters.Add("@CNTUSR", OdbcType.Char).Value = "b574121f2c7a09a7b4445150e6deb78c";
                    cmd.Parameters.Add("@CNTCTA", OdbcType.Char).Value = requestRecepciones.CUENTAREC;
                    cmd.Parameters.Add("@CNTMNT", OdbcType.Char).Value = requestRecepciones.MONTOTRX;
                    cmd.Parameters.Add("@CNTNMB", OdbcType.Char).Value = requestRecepciones.NOMBRE_BEN;
                    cmd.Parameters.Add("@CNTIDB", OdbcType.Char).Value = requestRecepciones.IDEN_BENE;
                    cmd.Parameters.Add("@CNTDRBNF", OdbcType.Char).Value = requestRecepciones.DIRBENEF;
                    cmd.Parameters.Add("@CNTTPCTA", OdbcType.Char).Value = requestRecepciones.TIPCTADES;
                    cmd.Parameters.Add("@CNTMONED", OdbcType.Char).Value = requestRecepciones.MONEDATRX;
                    cmd.Parameters.Add("@CNTPCTOR", OdbcType.Char).Value = requestRecepciones.TIPCTAORIG;
                    cmd.Parameters.Add("@CNTIDORG", OdbcType.Char).Value = requestRecepciones.IDEN_ORIG;
                    cmd.Parameters.Add("@CNTNMBOR", OdbcType.Char).Value = requestRecepciones.NOMBRE_ORIG;
                    cmd.Parameters.Add("@CNTCTAOR", OdbcType.Char).Value = requestRecepciones.CUENTAORIG;
                    cmd.Parameters.Add("@CNTBNKOR", OdbcType.Char).Value = requestRecepciones.BANCO_ORIGEN;
                    cmd.Parameters.Add("@CNTCNLEN", OdbcType.Char).Value = "10";
                    cmd.Parameters.Add("@IDTRA", OdbcType.Char).Value = requestRecepciones.CNDMGSP;
                    cmd.Parameters.Add("@CNTIDEST", OdbcType.Char).Value = requestRecepciones.coopDest;
                    cmd.Parameters.Add("@CNTIDIND", OdbcType.Char).Value = "D";
                    cmd.Parameters.Add("@CNTIDORD", OdbcType.Char).Value = requestRecepciones.coopOrigen;
                    cmd.Parameters.Add("@CNTRGLAD", OdbcType.Char).Value = "2";
                    cmd.Parameters.Add("@CNTOBSER", OdbcType.Char).Value = requestRecepciones.OBSERTRX;
                    cmd.Parameters.Add("@IDBNK", OdbcType.Char).Value = requestRecepciones.refPaysett;
                    cmd.Parameters.Add("@FCHRE", OdbcType.Char).Value = DateTime.Now.ToString("yyyyMMdd");
                    cmd.Parameters.Add("@HRARE", OdbcType.Char).Value = DateTime.Now.ToString("hhmmss");
                    cmd.Parameters.Add("@@CODERR", OdbcType.Char).Value = "";
                    cmd.Parameters.Add("@CNCODG", OdbcType.Char).Value = "";
                    cmd.Parameters.Add("@CNTSTAT", OdbcType.Char).Value = "";


                    cmd.ExecuteNonQuery();
                    cmd.Dispose();

                }
            }
            catch(Exception ex)
            {

            }
            finally
            {
                conn.DesconectarBD();
            }
        }
    }
}
