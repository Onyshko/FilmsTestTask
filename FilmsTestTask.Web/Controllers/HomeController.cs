using FilmsTestTask.Services.Interfaces;
using FilmsTestTask.Services.Models;
using FilmsTestTask.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FilmsTestTask.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBaseCrudService<FilmMvcModel> _filmServcice;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IBaseCrudService<FilmMvcModel> filmService, ILogger<HomeController> logger)
        {
            _filmServcice = filmService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var films = await _filmServcice.GetAllAsync();
            return View(films);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(FilmMvcModel film)
        {
            var response = await _filmServcice.AddAsync(film);

            if (response.IsSuccessStatusCode)
            {
                TempData["successMessage"] = "Film Added.";
                return RedirectToAction("Index");
            }

            return View(film);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var film = await _filmServcice.GetAsync(id);

            return View(film);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(FilmMvcModel film)
        {
            var response = await _filmServcice.UpdateAsync(film);

            if (response.IsSuccessStatusCode)
            {
                TempData["successMessage"] = "Film Updated.";
                return RedirectToAction("Index");
            }

            return View(film);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var film = await _filmServcice.GetAsync(id);

            return View(film);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _filmServcice.DeleteAsync(id);

            if (response.IsSuccessStatusCode)
            {
                TempData["successMessage"] = "Film Deleted.";
                return RedirectToAction("Index");
            }

            var film = await _filmServcice.GetAsync(id);
            return View(film);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}