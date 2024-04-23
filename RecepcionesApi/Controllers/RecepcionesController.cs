using AchEntura;
using ApprovalUtilities.SimpleLogger.Writers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecepcionesApi.Models;
using RecepcionesApi.Repositorio;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace RecepcionesApi.Controllers
{
    [Route("api/")]
    [ApiController]
    public class RecepcionesController : ControllerBase
    {
        [HttpPost]
        [Route("recepcionaAch")]
        public IActionResult Recepciones([FromBody] RequestRecepciones request)
        {

            if (request.BANDERA.Equals("D"))
            {
                Response response = RecepcionesAch.recepcionaTransacciones(request);

                return new JsonResult(response);
            }
            else
            {
                Response response = new Response();
                try
                {

                    RegistraEntranteIndirecta.RegistraEntrantes(request);

                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ValidateServerCertificate);
                    ACH_ServiceSoapClient client = new ACH_ServiceSoapClient(ACH_ServiceSoapClient.EndpointConfiguration.ACH_ServiceSoap);
                    DatosAbono requestDatosAbono = new DatosAbono();
                    RespuestaAbono responseDatosAbono = new RespuestaAbono();
                    String rutaBanco = null;
                    String ruta;

                    //CONSULTAR ID SEGÚN RUTA BANCO
                    RecepcionesAch recepcionesAch = new RecepcionesAch();
                    ruta = request.BANCO_ORIGEN;
                    ruta = ruta.Remove(0,1);
                    rutaBanco = recepcionesAch.obtenerRutaBanco(ruta);

                    requestDatosAbono.Clave_referencia = request.CNDMGSP; //REFERENCIA QUE GENERA BANCO EMISOR
                    requestDatosAbono.Folio_proveedor = request.refPaysett; // REFERENCIA PAYSETT (AGREGAR CAMPO)
                    requestDatosAbono.Fecha_hora_operacion = DateTime.UtcNow; // HORA ACTUAL
                    requestDatosAbono.Tipo_moneda = request.MONEDATRX;
                    requestDatosAbono.Monto = Decimal.Parse(request.MONTOTRX);
                    requestDatosAbono.Comision = 0.00M; //
                    requestDatosAbono.Nombre_ordenante = request.NOMBRE_ORIG;
                    requestDatosAbono.Tipo_cuenta_ordenante = request.TIPCTAORIG;
                    requestDatosAbono.Cuenta_ordenante = request.CUENTAORIG;
                    requestDatosAbono.Nombre_beneficiario = request.NOMBRE_BEN;
                    requestDatosAbono.Tipo_cuenta_beneficiario = request.TIPCTADES;
                    requestDatosAbono.Cuenta_beneficiario = request.CUENTAREC;
                    requestDatosAbono.Banco_origen = rutaBanco; // request.BANCO_ORIGEN; //CONSULTAR DB  SELECT * FROM ACHDAT.@ACHPF6000 WHERE @ACHPFRUT = RUTABANCO
                    requestDatosAbono.Cooperativa_origen = request.coopOrigen; // YA VIENE ID AGREGAR REQUEST
                    requestDatosAbono.Banco_destino = "33"; //SIEMPRE BANCO POPULAR (NO VA REQUEST)
                    requestDatosAbono.Cooperativa_destino = request.coopDest; // YA VIENE ID, AGREGAR CAMPO
                    requestDatosAbono.Identificador_origen = request.IDEN_ORIG;
                    requestDatosAbono.Identificador_beneficiario = request.IDEN_BENE;
                    requestDatosAbono.Concepto_pago = request.OBSERTRX;
                    requestDatosAbono.Firma = "Boy9T/j45MJ8XVBf/6KY+9THrf/+KFaDN7syi6EKxP7Qow42gucx0gUb7ITmFqZpfKdYTgdjpKDN1FeP7F\r\nqnVVdNJzLZIDu85a49aFVihlkVbR5OLXuIQu+CznCMrxMYa9ncXDTW5P7tFvltUtIxXDy08GfO7S3g/OXdl0QZC36n\r\ntK9kJeCKRihOfocE0fxq4Pd4m2Tkx7GW7rVmKGcpl2Y3iPstIC6P1vvqXHC8tjYzmqGTLNiqVs7Bb/YD1/a6cuLnivGi\r\n0I4Tlmtihpq3TBBTJb+24kpirmf5XRU3lJdO9SlRWDxCKlg/pT0nqsAzTPJkweg1NPgoaPmMP8eaNA==";
                    System.Net.ServicePointManager.ServerCertificateValidationCallback += delegate { return true; };
                    responseDatosAbono = client.Deposito_ACH(requestDatosAbono);
                    response.codMensaje = responseDatosAbono.Codigo_respuesta;
                    response.mensaje = responseDatosAbono.Descripcion_respuesta;




                }
                catch(Exception ex) {

                    response.codMensaje = "99";
                    response.mensaje = ex.Message;
                }

                return new JsonResult(response);

               
            }


        }
        public static bool ValidateServerCertificate(Object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}
