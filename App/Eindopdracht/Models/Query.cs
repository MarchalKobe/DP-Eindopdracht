using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Eindopdracht.Models {
    public class Query {
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "version")]
        public bool Version { get; set; }

        [JsonProperty(PropertyName = "href")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "results")]
        public List<Recipe> Results { get; set; }
    }
}
