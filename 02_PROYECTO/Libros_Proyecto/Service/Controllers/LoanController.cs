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
    public class LoanController : ApiController
    {
        private JsonSerializerSettings jsonSettings;

        public LoanController()
        {
            // Configuración para ignorar las referencias circulares
            jsonSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
        }
      
[HttpGet]

    public IHttpActionResult GetById(int id, int idLibro)
       {
           var negocio = new Negocio.LPrestamo();
           var prestamo = negocio.RetrieveById(id,idLibro);
           return Json(prestamo, jsonSettings);
       }
        [HttpPost]
        public PRESTAMO Add(PRESTAMO _prestamo)
        {
            var negocio = new Negocio.LPrestamo();
            var prestamo = negocio.Create(_prestamo);
            return prestamo;
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var negocio = new Negocio.LPrestamo();
            var prestamo = negocio.RetrieveAll();
            return Json(prestamo, jsonSettings);
        }

        [HttpGet]
        public IHttpActionResult GetByClient(int id)
        {
            var negocio = new Negocio.LPrestamo();
            var prestamo = negocio.RetrieveByClient(id);
            return Json(prestamo, jsonSettings);
        }


        [HttpGet]
        public IHttpActionResult GetByBook(int id)
        {
            var negocio = new Negocio.LPrestamo();
            var prestamo = negocio.RetrieveByBook(id);
            return Json(prestamo, jsonSettings);
        }


        [HttpPut]
        public bool Update(int id,int idLibro, PRESTAMO _prestamo)
        {
            var negocio = new Negocio.LPrestamo();
            var result = negocio.Update(_prestamo, id,idLibro);
            return result;
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id,int idLibro)
        {
            var negocio = new Negocio.LPrestamo();
            var result = negocio.Delete(id,idLibro);
            if (result)
            {
                return Ok("Libro eliminado correctamente.");
            }
            else
            {
                return BadRequest("No se pudo eliminar el libro.");
            }
        }
    }
}
