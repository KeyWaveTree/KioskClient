using KioskClient.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace KioskClient.Service
{
    public class ApiService
    {
        private readonly HttpClient httpClient;
        private const string BASEURL = "https://localhost:7286";

        public ApiService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            this.httpClient.BaseAddress = new Uri(BASEURL);
        }
        
        public async Task<List<MenuCategoryDTO>?> GetCategoriesDTOAsync()
        {
            return await httpClient.GetFromJsonAsync<List<MenuCategoryDTO>>("api/siosk/categories");
        }

        public async Task<List<MenuProductDTO>?> GetProductsDTOAsync(string categoryId)
        {
            return await httpClient.GetFromJsonAsync<List<MenuProductDTO>>($"api/kiosk/products?categoryId={categoryId}");
        }
    }
}
