using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;
using LanchesMac.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Controllers
{
    public class LancheController : Controller
    {
        private readonly ILancheRepository _lancheRepostiroy;
        public LancheController(ILancheRepository lancheRepository)
        {
            this._lancheRepostiroy = lancheRepository;
        }

        public IActionResult List(string categoria)
        {
            IEnumerable<Lanche> lanches;
            string categoriaAtual = string.Empty;

            if (string.IsNullOrEmpty(categoria))
            {
                lanches = _lancheRepostiroy.Lanches.OrderBy(
                   x => x.LancheId);
                categoriaAtual = "Todos os lanches";
            }
            else
            {

                lanches = _lancheRepostiroy.Lanches
                    .Where(x => x.Categoria.CategoriaNome.Equals(categoria))
                    .OrderBy(x => x.Nome);
                categoriaAtual = categoria;
            }
           

            var lancheListViewModel = new LancheListViewModel
            {
                Lanches = lanches,
                CategoriaAtual = categoriaAtual
            };




            return View(lancheListViewModel);
        }

        public IActionResult Details(int lancheId)
        {
            var lanche = _lancheRepostiroy.Lanches.FirstOrDefault(id => id.LancheId == lancheId);
            return View(lanche);
        
        }

        public ViewResult Search(string searchString)
        {
            IEnumerable<Lanche> lanches;
            string categoriaAtual = string.Empty;

            if (string.IsNullOrEmpty(searchString))
            {
                lanches = _lancheRepostiroy.Lanches.OrderBy(x => x.LancheId);
                categoriaAtual = "Todos os Lanches";
            }
            else
            {
                lanches = _lancheRepostiroy.Lanches
                    .Where(x => x.Nome.ToLower().Contains(searchString.ToLower()));
            }

            return View("~/Views/Lanche/List.cshtml", new LancheListViewModel
            {
                Lanches = lanches,
                CategoriaAtual = categoriaAtual
            });
        }

    }
}
