using System.Net.Http.Json;

namespace Productos.WinForms
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5069/")
            };
        }

        public async Task<List<Producto>> ObtenerProductosAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Producto>>("api/Productos")
                   ?? new List<Producto>();
        }
        public async Task<bool> CrearProductoAsync(Producto producto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Productos", producto);

            return response.IsSuccessStatusCode;
        }
        public async Task<bool> ActualizarProductoAsync(int id, Producto producto)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Productos/{id}", producto);

            return response.IsSuccessStatusCode;
        }
        public async Task<bool> EliminarProductoAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Productos/{id}");

            return response.IsSuccessStatusCode;
        }
    }
}