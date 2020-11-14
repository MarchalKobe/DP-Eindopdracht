using System;
using Newtonsoft.Json;

namespace Eindopdracht.Models {
    public class Recipe {
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "href")]
        public string RecipeUrl { get; set; }

        [JsonProperty(PropertyName = "ingredients")]
        public string Ingredients { get; set; }

        [JsonProperty(PropertyName = "thumbnail")]
        public string ThumbnailUrl { get; set; }
    }
}
