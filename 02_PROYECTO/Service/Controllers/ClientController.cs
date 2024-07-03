using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Data;
using Newtonsoft.Json;

namespace Service.Controllers
{
    public class ClientController : ApiController
    {
        private JsonSerializerSettings jsonSettings;

        public ClientController()
        {
            // Configuración para ignorar las referencias circulares
            jsonSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
        }
        [HttpGet]

        public IHttpActionResult GetById(int id)
        {
            var negocio = new Negocio.LCliente();
            var cliente = negocio.RetrieveById(id);
            return Json(cliente, jsonSettings);
        }
        [HttpPost]
        public CLIENTE Add(CLIENTE _cliente)
        {
            var negocio = new Negocio.LCliente();
            var cliente = negocio.Create(_cliente);
            return cliente;
        }
        

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var negocio = new Negocio.LCliente();
            var clientes = negocio.RetrieveAll();
            return Json(clientes, jsonSettings);
        }


        [HttpPut]
        public bool Update(int id, CLIENTE _cliente)
        {
            var negocio = new Negocio.LCliente();
            var result = negocio.Update(_cliente, id);
            return result;
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var negocio = new Negocio.LCliente();
            var result = negocio.Delete(id);
            if (result)
            {
                return Ok("Cliente eliminado correctamente.");
            }
            else
            {
                return BadRequest("No se pudo eliminar el cliente.");
            }
        }

    }
}
