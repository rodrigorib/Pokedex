using System.Text.Json.Serialization;

namespace PokedexMVC.DTO
{
    public class PokemonDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("img")]
        public string Img { get; set; }
    }
}
