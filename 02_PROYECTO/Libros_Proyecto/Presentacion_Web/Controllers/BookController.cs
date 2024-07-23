using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Data;
using Newtonsoft.Json;
using Presentacion_Web.Service;

namespace Presentacion_Web.Controllers
{
    public class BookController : Controller
    {
        private readonly BookService _bookService;

        public BookController(BookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<ActionResult> Index()
        {
            var libros = await _bookService.GetBooksAsync();
            return View(libros);
        }

        public ActionResult Create()
        {
            return PartialView("_CrearLibroForm", new LIBRO());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(LIBRO libro)
        {
            Debug.WriteLine(JsonConvert.SerializeObject(libro, Formatting.Indented));
            if (ModelState.IsValid)
            {
                await _bookService.CreateBookAsync(libro);
               // Debug.WriteLine(JsonConvert.SerializeObject(libro, Formatting.Indented));
                return RedirectToAction("Index");
            }
            return PartialView("_CrearLibroForm", libro);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var libro = await _bookService.GetBookByIdAsync(id);
            if (libro == null)
            {
                return HttpNotFound();
            }
            return PartialView("_CrearLibroForm", libro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(LIBRO libro)
        {
            if (ModelState.IsValid)
            {
                await _bookService.UpdateBookAsync(libro.IDLIBRO, libro);
                return RedirectToAction("Index");
            }
            return PartialView("_CrearLibroForm", libro);
        }

        public async Task<ActionResult> Details(int id)
        {
            var libro = await _bookService.GetBookByIdAsync(id);
            if (libro == null)
            {
                return HttpNotFound();
            }
            return View(libro);
        }

        public async Task<ActionResult> Delete(int id)
        {
            var libro = await _bookService.GetBookByIdAsync(id);
            if (libro == null)
            {
                return HttpNotFound();
            }
            return View(libro);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _bookService.DeleteBookAsync(id);
            return RedirectToAction("Index");
        }

    }
}
