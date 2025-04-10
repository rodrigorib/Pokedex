using System.Text.Json.Serialization;

namespace PokedexMVC.Models
{
    public class Pokemon
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string UrlImagem { get; set; }
    }
}
