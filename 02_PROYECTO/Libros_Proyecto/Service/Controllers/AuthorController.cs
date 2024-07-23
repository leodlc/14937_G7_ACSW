using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Data;
using Newtonsoft.Json;

namespace Service.Controllers
{
    public class AuthorController : ApiController
    {
        private JsonSerializerSettings jsonSettings;

        public AuthorController()
        {
            // Configuración para ignorar las referencias circulares
            jsonSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
        }
        /// <summary>
        /// Devuelve la busca del Autor por su Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(IEnumerable<AUTOR>))]
        public IHttpActionResult GetById(int id)
        {
            var negocio = new Negocio.LAutor();
            var autor = negocio.RetrieveById(id);
            return Json(autor, jsonSettings);
        }
        [HttpPost]
        public AUTOR Add(AUTOR _author)
        {
            var negocio = new Negocio.LAutor();
            var autor = negocio.Create(_author);
            return autor;
        }


        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var negocio = new Negocio.LAutor();
            var autor = negocio.RetrieveAll();
            return Json(autor, jsonSettings);
        }


        [HttpPut]
        public bool Update(int id, AUTOR _autor)
        {
            var negocio = new Negocio.LAutor();
            var result = negocio.Update(_autor, id);
            return result;
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var negocio = new Negocio.LAutor();
            var result = negocio.Delete(id);
            if (result)
            {
                return Ok("Autor eliminado correctamente.");
            }
            else
            {
                return BadRequest("No se pudo eliminar el autor.");
            }
        }
    }
}
