using AchEntura;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecepcionesApi.Config;
using RecepcionesApi.Models;
using System.Net.Security;
using System.Net;

namespace RecepcionesApi.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ActualizaController : ControllerBase
    {
        [HttpPost]
        [Route("actualizaEnvios")]
        public IActionResult ActualizaAch([FromBody] RequestActualizar request)
        {

            if (request.BANDERA.Equals("D"))
            {
              Response responseEnvios = ActualizaEnvios.insertarTablaAch(request);
        

                return new JsonResult(responseEnvios);
            }
            else
            {

                Response response = new Response();

                try
                {
                    ACH_ServiceSoapClient client = new ACH_ServiceSoapClient(ACH_ServiceSoapClient.EndpointConfiguration.ACH_ServiceSoap);
                    DatosActualizacionEnvio datosActualizacionEnvio = new DatosActualizacionEnvio();
                    RespuestaActualizacionEnvio respuestaActualizacionEnvio = new RespuestaActualizacionEnvio();

                    datosActualizacionEnvio.Clave_referencia = request.ICODIGOTRX;
                    datosActualizacionEnvio.Folio_proveedor = request.refPaysett;
                    datosActualizacionEnvio.Estado = request.ICODIGOMENSAJE;
                    datosActualizacionEnvio.Mensaje_referencia = request.ICODIGOMENSAJE;

                    System.Net.ServicePointManager.ServerCertificateValidationCallback += delegate { return true; };
                    respuestaActualizacionEnvio = client.Actualizacion_Envio_ACH(datosActualizacionEnvio);
                    response.codMensaje = respuestaActualizacionEnvio.Codigo_respuesta;
                    response.mensaje = respuestaActualizacionEnvio.Descripcion_respuesta;

                }
                catch (Exception ex)
                {
                    response.codMensaje = "99";
                    response.mensaje = ex.Message;
                }
                finally
                {

                }
                return new JsonResult(response);
            }
            
          
        }

     
    }
}
