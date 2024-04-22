using FilmsTestTask.APIServices.Interfaces;
using FilmsTestTask.APIServices.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmsTestTask.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmController : ControllerBase
    {
        private readonly IBaseCrudService<FilmApiModel> _filmService;

        public FilmController(IBaseCrudService<FilmApiModel> filmService)
        {
            _filmService = filmService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFilms()
        {
            try
            {
                var films = await _filmService.GetAllAsync();
                if (films.Count == 0)
                {
                    return NotFound("Films not found");
                }

                return Ok(films);
            }
            catch (Exception ex) 
            { 
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFilm(int id)
        {
            try
            {
                var film = await _filmService.GetAsync(id);
                if (film is null)
                {
                    return NotFound("Film not found");
                }

                return Ok(film);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddFilm(FilmApiModel model)
        {
            await _filmService.AddAsync(model);

            return Ok("Film added");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFilm(FilmApiModel model)
        {
            await _filmService.UpdateAsync(model);

            return Ok("Film updated");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFilm(int id)
        {
            await _filmService.DeleteAsync(id);

            return Ok("Film deleted");
        }
    }
}
