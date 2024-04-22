using AutoMapper;
using FilmsTestTask.APIServices.Models;
using System.Net.Http;

namespace FilmsTestTask.Services.Implementations
{
    public class BaseService<TEntity>
        where TEntity : BaseApiModel
    {
        protected readonly HttpClient _client;

        public BaseService(IHttpClientFactory client)
        {
            _client = client.CreateClient(); ;
            _client.BaseAddress = new Uri("https://localhost:44360/api");
        }
    }
}
