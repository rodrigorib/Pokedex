using System.Text.Json.Serialization;
using PokedexMVC.Models;

namespace PokedexMVC.DTO
{
    public class RespostaApiPokemonDTO
    {
        [JsonPropertyName("pokemon")]
        public List<PokemonDTO> Pokemon { get; set; }
    }
}
