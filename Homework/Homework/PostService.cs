using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Homework
{
    public class PostService
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        public async Task<Post> GetPostAsync(int postId)
        {
            var response = await _httpClient.GetAsync($"https://jsonplaceholder.typicode.com/posts/{postId}");

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("Error");
                return default;
            }

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Post>(content);
            return result;
        }
    }
}
