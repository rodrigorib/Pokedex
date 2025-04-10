using Microsoft.AspNetCore.Mvc;
using PokedexMVC.Models;
using PokedexMVC.Service.Contract;

namespace PokedexMVC.Controllers
{
    public class PokedexController : Controller
    {
        private readonly IPokedexService _pokemonService;
        private static Pokemon _currentPokemon;
        public PokedexController(IPokedexService pokedexService)
        {
            _pokemonService = pokedexService;
        }
        public async Task<IActionResult> Index(string? filtro)
        {
            try
            {
                if (Request.Method == "GET")
                {
                    if (!string.IsNullOrEmpty(filtro))
                    {
                        if (int.TryParse(filtro, out int id))
                        {
                            Pokemon pokemon = await _pokemonService.GetByIdAsync(id);
                            _currentPokemon = pokemon ?? _currentPokemon;
                        }
                        else
                        {
                            Pokemon pokemon = await _pokemonService.GetByNameAsync(filtro);
                            _currentPokemon = pokemon ?? _currentPokemon;
                        }

                        return View(_currentPokemon);
                    }

                    _currentPokemon = await _pokemonService.First();
                }

                return View(_currentPokemon);
            }
            catch (NullReferenceException ex)
            {
                ViewBag.Error = ex.Message;
                return View(_currentPokemon);
            }
            catch (Exception ex)
            {
                //Log(ex);
                ViewBag.Error = "Ocorreu um erro inesperado.";
                return View(_currentPokemon);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Next()
        {
            try
            {
                _currentPokemon = await _pokemonService.Next();

                return View("Index", _currentPokemon);
            }
            catch (NullReferenceException ex)
            {
                ViewBag.Error = ex.Message;
                return View("Index", _currentPokemon);
            }
            catch (Exception ex)
            {
                //Log(ex);
                ViewBag.Error = "Ocorreu um erro inesperado.";
                return View("Index", _currentPokemon);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Previous()
        {
            try
            {
                _currentPokemon = await _pokemonService.Previous();

                return View("Index", _currentPokemon);
            }
            catch (NullReferenceException ex)
            {
                ViewBag.Error = ex.Message;
                return View("Index", _currentPokemon);
            }
            catch (Exception ex)
            {
                //Log(ex);
                ViewBag.Error = "Ocorreu um erro inesperado.";
                return View("Index", _currentPokemon);
            }
        }
    }
}
