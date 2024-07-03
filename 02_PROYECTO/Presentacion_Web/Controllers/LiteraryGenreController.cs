using System.Threading.Tasks;
using System.Web.Mvc;
using Data;
using Presentacion_Web.Service;

namespace Presentacion_Web.Controllers
{
    public class LiteraryGenreController : Controller
    {
        private readonly LiteraryGenreService _literaryGenreService;

        public LiteraryGenreController(LiteraryGenreService literaryGenreService)
        {
            _literaryGenreService = literaryGenreService;
        }

        public async Task<ActionResult> Index()
        {
            var genres = await _literaryGenreService.GetGenresAsync();
            return View(genres);
        }

        public ActionResult Create()
        {
            return PartialView("_CrearGeneroForm", new GENEROLITERARIO());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(GENEROLITERARIO genre)
        {
            if (ModelState.IsValid)
            {
                await _literaryGenreService.CreateGenreAsync(genre);
                return RedirectToAction("Index");
            }
            return PartialView("_CrearGeneroForm", genre);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var genre = await _literaryGenreService.GetGenreByIdAsync(id);
            if (genre == null)
            {
                return HttpNotFound();
            }
            return PartialView("_CrearGeneroForm", genre);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(GENEROLITERARIO genre)
        {
            if (ModelState.IsValid)
            {
                await _literaryGenreService.UpdateGenreAsync(genre.IDGL, genre);
                return RedirectToAction("Index");
            }
            return PartialView("_CrearGeneroForm", genre);
        }

        public async Task<ActionResult> Delete(int id)
        {
            var genre = await _literaryGenreService.GetGenreByIdAsync(id);
            if (genre == null)
            {
                return HttpNotFound();
            }
            return View(genre);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _literaryGenreService.DeleteGenreAsync(id);
            return RedirectToAction("Index");
        }
    }
}
