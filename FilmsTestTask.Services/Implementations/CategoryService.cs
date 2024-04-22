using FilmsTestTask.APIServices.Models;
using FilmsTestTask.Services.Interfaces;
using FilmsTestTask.Services.Models;
using Newtonsoft.Json;
using System.Text;

namespace FilmsTestTask.Services.Implementations
{
    public class CategoryService : BaseService<CategoryApiModel>, ICategoryService
    {
        private readonly IBaseCrudService<FilmMvcModel> _filmService;

        public CategoryService(IBaseCrudService<FilmMvcModel> filmService, IHttpClientFactory client) : base(client)
        {
            _filmService = filmService;
        }

        public async Task<HttpResponseMessage> AddAsync(CategoryMvcModel model)
        {
            var data = JsonConvert.SerializeObject(model);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(_client.BaseAddress + "/Category", content);

            return response;
        }

        public async Task<HttpResponseMessage> DeleteAsync(int id)
        {
            var response = await _client.DeleteAsync(_client.BaseAddress + "/Category?id=" + id);

            return response;
        }

        public async Task<IList<CategoryMvcModel>> GetAllAsync()
        {
            var response = await _client.GetAsync(_client.BaseAddress + "/Category");
            var categories = new List<CategoryMvcModel>();

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                categories = JsonConvert.DeserializeObject<List<CategoryMvcModel>>(data);

                foreach (var category in categories)
                {
                    category.AmountOfFilms = (await _filmService.GetAllAsync())
                                                                .Count(x => x.FilmCategories
                                                                .Any(y => y.CategoryId == category.Id));
                    category.Layer = await GetLayerAsync(category.Id);
                }
            }

            return categories;
        }

        public async Task<CategoryMvcModel> GetAsync(int id)
        {
            var response = await _client.GetAsync(_client.BaseAddress + "/Category/" + id);
            var category = new CategoryMvcModel();

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                category = JsonConvert.DeserializeObject<CategoryMvcModel>(data);
            }

            return category;
        }

        public async Task<HttpResponseMessage> UpdateAsync(CategoryMvcModel model)
        {
            var data = JsonConvert.SerializeObject(model);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(_client.BaseAddress + "/Category", content);

            return response;
        }

        public async Task<int> GetLayerAsync(int id)
        {
            var response = await _client.GetAsync(_client.BaseAddress + "/Category/layer/" + id);
            var layer = new int();

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                layer = JsonConvert.DeserializeObject<int>(data);
            }

            return layer;
        }
    }
}
