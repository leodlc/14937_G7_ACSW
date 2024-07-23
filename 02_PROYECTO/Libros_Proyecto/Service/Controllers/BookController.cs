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
    public class BookController : ApiController
    {
        private JsonSerializerSettings jsonSettings;

        public BookController()
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
            var negocio = new Negocio.LLibro();
            var libro = negocio.RetrieveById(id);
            return Json(libro, jsonSettings);
        }
        [HttpPost]
        public LIBRO Add(LIBRO _libro)
        {
            var negocio = new Negocio.LLibro();
            var libro = negocio.Create(_libro);
            return libro;
        }


        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var negocio = new Negocio.LLibro();
            var libros = negocio.RetrieveAll();
            return Json(libros, jsonSettings);
        }


        [HttpPut]
        public bool Update(int id, LIBRO _libro)
        {
            var negocio = new Negocio.LLibro();
            var result = negocio.Update(_libro, id);
            return result;
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var negocio = new Negocio.LLibro();
            var result = negocio.Delete(id);
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
