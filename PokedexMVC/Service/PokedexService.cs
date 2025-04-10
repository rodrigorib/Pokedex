using Microsoft.AspNetCore.Http;
using PokedexMVC.DTO;
using PokedexMVC.Models;
using PokedexMVC.Service.Contract;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PokedexMVC.Service
{
    public class PokedexService : IPokedexService
    {
        private readonly HttpClient _httpClient;
        private const string apiUrl = "https://www.canalti.com.br/api/pokemons.json";
        private List<Pokemon> _listaPokemons = new List<Pokemon>();
        private static int _indice = 0;

        public PokedexService()
        {
            _httpClient = new HttpClient();
        }

        /// <summary>
        /// Verifica se já existe uma lista de pokemons carregada. Se não houver, inicializa a lista de pokemons.
        /// </summary>
        private async Task InitializeAsync()
        {
            if (_listaPokemons == null || _listaPokemons.Count == 0)
            {
                RespostaApiPokemonDTO response = await _httpClient.GetFromJsonAsync<RespostaApiPokemonDTO>(apiUrl)?? throw new NullReferenceException("Não foi possível carregar lista de pokemons.");
                List<PokemonDTO> listaPokemonsDTO = response?.Pokemon ?? new List<PokemonDTO>();

                foreach (var item in listaPokemonsDTO)
                {
                    _listaPokemons.Add(new Pokemon()
                    {
                        Id = item.Id,
                        Nome = item.Name,
                        UrlImagem = item.Img
                    });
                }
            }
        }
        public async Task<List<Pokemon>> GetAllAsync()
        {
            await InitializeAsync();

            return _listaPokemons?? new List<Pokemon>();
        }

        public async Task<Pokemon> GetByIdAsync(int id)
        {
            await InitializeAsync();

            Pokemon? pokemon = _listaPokemons.FirstOrDefault(p => p.Id == id);
            _indice = pokemon == null ? _indice: _listaPokemons.IndexOf(pokemon);

            return pokemon ?? throw new NullReferenceException("Pokemon não encontrado.");
        }

        public async Task<Pokemon> GetByNameAsync(string nome)
        {
            await InitializeAsync();

            Pokemon? pokemon = _listaPokemons.FirstOrDefault(p => p.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));
            _indice = pokemon == null ? _indice : _listaPokemons.IndexOf(pokemon);

            return pokemon ?? throw new NullReferenceException("Pokemon não encontrado.");
        }

        public async Task<Pokemon> Current()
        {
            await InitializeAsync();

            return _listaPokemons[_indice] ?? throw new NullReferenceException("Pokemon não encontrado.");
        }

        public async Task<Pokemon> First()
        {
            await InitializeAsync();

            _indice = 0;
            Pokemon? pokemon = _listaPokemons[_indice];

            return pokemon ?? throw new NullReferenceException("Pokemon não encontrado.");
        }

        public async Task<Pokemon> Next()
        {
            await InitializeAsync();

            if (_indice < _listaPokemons.Count - 1)
                _indice++;

            Pokemon? pokemon = _listaPokemons[_indice];

            return pokemon ?? throw new NullReferenceException("Pokemon não encontrado.");
        }

        public async Task<Pokemon> Previous()
        {
            await InitializeAsync();

            if (_indice > 0)
                _indice--;

            Pokemon? pokemon = _listaPokemons[_indice];

            return pokemon ?? throw new NullReferenceException("Pokemon não encontrado.");
        }
    }
}
