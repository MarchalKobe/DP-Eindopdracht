using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using EindopdrachtAPI.Models;
using System.Data.SqlClient;

namespace EindopdrachtAPI {
    public static class EindopdrachtAPI {
        [FunctionName("GetFavorites")]
        public static async Task<IActionResult> GetFavorites([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "favorites")] HttpRequest req, ILogger log) {
            try {
                List<Recipe> recipes = new List<Recipe>();
                string connectionString = Environment.GetEnvironmentVariable("SQLServer");

                using (SqlConnection connection = new SqlConnection(connectionString)) {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand()) {
                        command.Connection = connection;
                        command.CommandText = "SELECT * FROM Favorites";

                        SqlDataReader reader = await command.ExecuteReaderAsync();

                        while (await reader.ReadAsync()) {
                            Recipe recipe = new Recipe();
                            recipe.Title = reader["Title"].ToString();
                            recipe.RecipeUrl = reader["RecipeUrl"].ToString();
                            recipe.Ingredients = reader["Ingredients"].ToString();
                            recipe.ThumbnailUrl = reader["ThumbnailUrl"].ToString();
                            recipes.Add(recipe);
                        }
                    }
                }

                return new OkObjectResult(recipes);
            } catch (Exception ex) {
                return new StatusCodeResult(500);
            }
        }

        [FunctionName("Favorite")]
        public static async Task<IActionResult> AddFavorite([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "favorites")] HttpRequest req, ILogger log) {
            try {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                Recipe recipe = JsonConvert.DeserializeObject<Recipe>(requestBody);

                string connectionString = Environment.GetEnvironmentVariable("SQLServer");

                using (SqlConnection connection = new SqlConnection(connectionString)) {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand()) {
                        command.Connection = connection;
                        command.CommandText = "INSERT INTO Favorites VALUES (@Title, @RecipeUrl, @Ingredients, @ThumbnailUrl)";
                        command.Parameters.AddWithValue("@Title", recipe.Title);
                        command.Parameters.AddWithValue("@RecipeUrl", recipe.RecipeUrl);
                        command.Parameters.AddWithValue("@Ingredients", recipe.Ingredients);
                        command.Parameters.AddWithValue("@ThumbnailUrl", recipe.ThumbnailUrl);

                        await command.ExecuteNonQueryAsync();
                    }
                }

                return new OkObjectResult(recipe);
            } catch (Exception ex) {
                return new StatusCodeResult(500);
            }
        }

        [FunctionName("RemoveFavorite")]
        public static async Task<IActionResult> RemoveFavorite([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "favorites")] HttpRequest req, ILogger log) {
            try {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                Recipe recipe = JsonConvert.DeserializeObject<Recipe>(requestBody);

                string connectionString = Environment.GetEnvironmentVariable("SQLServer");

                using (SqlConnection connection = new SqlConnection(connectionString)) {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand()) {
                        command.Connection = connection;
                        command.CommandText = "DELETE FROM Favorites WHERE Title = @Title";
                        command.Parameters.AddWithValue("@Title", recipe.Title);
                        await command.ExecuteReaderAsync();
                    }
                }

                return new StatusCodeResult(200);
            } catch (Exception ex) {
                return new StatusCodeResult(500);
            }
        }
    }
}
