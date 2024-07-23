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
    public class LiteraryGenreController : ApiController
    {
        private JsonSerializerSettings jsonSettings;

        public LiteraryGenreController()
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
            var negocio = new Negocio.LGeneroLiterario();
            var genero = negocio.RetrieveById(id);
            return Json(genero, jsonSettings);
        }
        [HttpPost]
        public GENEROLITERARIO AddGenre(GENEROLITERARIO _genero)
        {
            var negocio = new Negocio.LGeneroLiterario();
            var genero = negocio.Create(_genero);
            return genero;
        }


        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var negocio = new Negocio.LGeneroLiterario();
            var genero = negocio.RetrieveAll();
            return Json(genero, jsonSettings);
        }


        [HttpPut]
        public bool Update(int id, GENEROLITERARIO _genero)
        {
            var negocio = new Negocio.LGeneroLiterario();
            var result = negocio.Update(_genero, id);
            return result;
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var negocio = new Negocio.LGeneroLiterario();
            var result = negocio.Delete(id);
            if (result)
            {
                return Ok("Genero Literario eliminado correctamente.");
            }
            else
            {
                return BadRequest("No se pudo eliminar el genero Literario.");
            }
        }
    }
}
