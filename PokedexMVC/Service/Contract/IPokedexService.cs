using PokedexMVC.Models;

namespace PokedexMVC.Service.Contract
{
    public interface IPokedexService
    {
        Task<List<Pokemon>> GetAllAsync();
        Task<Pokemon> GetByIdAsync(int id);
        Task<Pokemon> GetByNameAsync(string name);
        Task<Pokemon> First();
        Task<Pokemon> Next();
        Task<Pokemon> Previous();
    }
}
