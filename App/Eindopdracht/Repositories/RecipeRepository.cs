using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Eindopdracht.Models;
using Newtonsoft.Json;

namespace Eindopdracht.Repositories {
    public class RecipeRepository {
        private const string _BASEURI = "http://www.recipepuppy.com/api/";

        private static HttpClient GetHttpClient() {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }

        public static async Task<List<Recipe>> getRecipesAsync(string queryString, int page) {
            string url = $"{_BASEURI}?q={queryString}&p={page}";

            using(HttpClient client = GetHttpClient()) {
                try {
                    string json = await client.GetStringAsync(url);

                    Query query = JsonConvert.DeserializeObject<Query>(json);
                    return query.Results;
                } catch (Exception ex) {
                    throw ex;
                }
            }
        }
    }
}
