using AutoMapper;
using FilmsTestTask.APIServices.Models;
using FilmsTestTask.Services.Interfaces;
using FilmsTestTask.Services.Models;
using Newtonsoft.Json;
using System.Text;

namespace FilmsTestTask.Services.Implementations
{
    public class FilmService : BaseService<FilmApiModel>, IBaseCrudService<FilmMvcModel>
    {
        public FilmService(IHttpClientFactory client) : base(client)
        {
        }

        public async Task<HttpResponseMessage> AddAsync(FilmMvcModel model)
        {
            var data = JsonConvert.SerializeObject(model);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(_client.BaseAddress + "/Film", content);

            return response;
        }

        public async Task<HttpResponseMessage> DeleteAsync(int id)
        {
            var response = await _client.DeleteAsync(_client.BaseAddress + "/Film?id=" + id);

            return response;
        }

        public async Task<IList<FilmMvcModel>> GetAllAsync()
        {
            var response = await _client.GetAsync(_client.BaseAddress + "/Film");
            var films = new List<FilmMvcModel>();

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                films = JsonConvert.DeserializeObject<List<FilmMvcModel>>(data);

                foreach (var film in films)
                {
                    var filmCategories = film.FilmCategories.ToList();
                    
                    foreach (var filmCategory in filmCategories)
                    {
                        var responseCategory = await _client.GetAsync(_client.BaseAddress + "/Category/" + filmCategory.CategoryId);
                        var dataCategory = await responseCategory.Content.ReadAsStringAsync();
                        var category = JsonConvert.DeserializeObject<CategoryMvcModel>(dataCategory);

                        var filmCategoryIndex = filmCategories.IndexOf(filmCategory);
                        film.FilmCategories[filmCategoryIndex].CategoryName = category.Name;
                    }

                    film.Categories = string.Join(", ", film.FilmCategories.Select(x => x.CategoryName).ToList());
                }
            }

            return films;
        }

        public async Task<FilmMvcModel> GetAsync(int id)
        {
            var response = await _client.GetAsync(_client.BaseAddress + "/Film/" + id);
            var film = new FilmMvcModel();

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                film = JsonConvert.DeserializeObject<FilmMvcModel>(data);

                var filmCategories = film.FilmCategories.ToList();

                foreach (var filmCategory in filmCategories)
                {
                    var responseCategory = await _client.GetAsync(_client.BaseAddress + "/Category/" + filmCategory.CategoryId);
                    var dataCategory = await responseCategory.Content.ReadAsStringAsync();
                    var category = JsonConvert.DeserializeObject<CategoryMvcModel>(dataCategory);

                    var filmCategoryIndex = filmCategories.IndexOf(filmCategory);
                    film.FilmCategories[filmCategoryIndex].CategoryName = category.Name;
                }
            }

            return film;
        }

        public async Task<HttpResponseMessage> UpdateAsync(FilmMvcModel model)
        {
            var data = JsonConvert.SerializeObject(model);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(_client.BaseAddress + "/Film", content);

            return response;
        }
    }
}
