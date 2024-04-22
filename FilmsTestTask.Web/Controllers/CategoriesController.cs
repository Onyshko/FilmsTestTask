using FilmsTestTask.Services.Implementations;
using FilmsTestTask.Services.Interfaces;
using FilmsTestTask.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmsTestTask.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllAsync();
            return View(categories);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryMvcModel category)
        {
            var response = await _categoryService.AddAsync(category);

            if (response.IsSuccessStatusCode)
            {
                TempData["successMessage"] = "Category Added.";
                return RedirectToAction("Index");
            }

            return View(category);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var film = await _categoryService.GetAsync(id);

            return View(film);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryMvcModel category)
        {
            var response = await _categoryService.UpdateAsync(category);

            if (response.IsSuccessStatusCode)
            {
                TempData["successMessage"] = "Category Updated.";
                return RedirectToAction("Index");
            }

            return View(category);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryService.GetAsync(id);

            return View(category);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _categoryService.DeleteAsync(id);

            if (response.IsSuccessStatusCode)
            {
                TempData["successMessage"] = "Category Deleted.";
                return RedirectToAction("Index");
            }

            var category = await _categoryService.GetAsync(id);
            return View(category);
        }
    }
}
