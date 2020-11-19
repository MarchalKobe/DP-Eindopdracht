using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Eindopdracht.Models;
using Newtonsoft.Json;

namespace Eindopdracht.Repositories {
    public class RecipeRepository {
        private static HttpClient GetHttpClient() {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }

        public static async Task<List<Recipe>> getRecipesAsync(string queryString, int page) {
            string url = $"http://www.recipepuppy.com/api/?q={queryString}&p={page}";

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

        public static async Task<List<Recipe>> getFavoritesAsync() {
            string url = "https://kobemarchaldpeindwerk.azurewebsites.net/api/favorites";

            using(HttpClient client = GetHttpClient()) {
                try {
                    string json = await client.GetStringAsync(url);
                    List<Recipe> favorites = JsonConvert.DeserializeObject<List<Recipe>>(json);
                    return favorites;
                } catch (Exception ex) {
                    throw ex;
                }
            }
        }

        public static async Task<List<Recipe>> getFavoriteAsync(string title) {
            string url = $"https://kobemarchaldpeindwerk.azurewebsites.net/api/favorites/{title}";

            using(HttpClient client = GetHttpClient()) {
                try {
                    string json = await client.GetStringAsync(url);
                    List<Recipe> favorites = JsonConvert.DeserializeObject<List<Recipe>>(json);
                    return favorites;
                } catch (Exception ex) {
                    throw ex;
                }
            }
        }

        public static async Task addFavoriteAsync(Recipe favorite) {
            string url = "https://kobemarchaldpeindwerk.azurewebsites.net/api/favorites";

            using(HttpClient client = GetHttpClient()) {
                try {
                    string jsonData = JsonConvert.SerializeObject(favorite);
                    StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    await client.PostAsync(url, content);
                } catch (Exception ex) {
                    throw ex;
                }
            }
        }

        public static async Task deleteFavoriteAsync(string title) {
            string url = $"https://kobemarchaldpeindwerk.azurewebsites.net/api/favorites/{title}";

            using (HttpClient client = GetHttpClient()) {
                try {
                    await client.DeleteAsync(url);
                } catch (Exception ex) {
                    throw ex;
                }
            }
        }
    }
}
