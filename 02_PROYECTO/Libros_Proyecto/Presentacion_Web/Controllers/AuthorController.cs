using System.Threading.Tasks;
using System.Web.Mvc;
using Data;
using Presentacion_Web.Service;

namespace Presentacion_Web.Controllers
{
    public class AuthorController : Controller
    {
        private readonly AuthorService _authorService;

        public AuthorController(AuthorService authorService)
        {
            _authorService = authorService;
        }

        public async Task<ActionResult> Index()
        {
            var autores = await _authorService.GetAutoresAsync();
            return View(autores);
        }

        public ActionResult Create()
        {
            return PartialView("_CrearAutorForm", new AUTOR());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AUTOR autor)
        {
            if (ModelState.IsValid)
            {
                await _authorService.CreateAutorAsync(autor);
                return RedirectToAction("Index");
            }
            return PartialView("_CrearAutorForm", autor);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var autor = await _authorService.GetAutorByIdAsync(id);
            if (autor == null)
            {
                return HttpNotFound();
            }
            return PartialView("_CrearAutorForm", autor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(AUTOR autor)
        {
            if (ModelState.IsValid)
            {
                await _authorService.UpdateAutorAsync(autor.IDAUTOR, autor);
                return RedirectToAction("Index");
            }
            return PartialView("_CrearAutorForm", autor);
        }

        public async Task<ActionResult> Delete(int id)
        {
            var autor = await _authorService.GetAutorByIdAsync(id);
            if (autor == null)
            {
                return HttpNotFound();
            }
            return View(autor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _authorService.DeleteAutorAsync(id);
            return RedirectToAction("Index");
        }
    }
}
