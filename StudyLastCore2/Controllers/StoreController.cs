using Microsoft.AspNetCore.Mvc;
using StudyLastCore2.Repositories;
using StudyLastCore2.Models;

namespace StudyLastCore.Controllers {

    public class StoreController : Controller {

        private RepositoryStore repo;

        public StoreController(RepositoryStore repo) {
            this.repo = repo;
        }

        //NADA MAS ENTRAR SE VE LA LISTA DE TODOS LOS PRODUCTOS
        public async Task<IActionResult> Index(int? posicion) {
            posicion = (posicion == null) ? 0 : posicion.Value;

            ItemsPaginados itemsPaginados = await this.repo.GetProductsAsync(posicion.Value);
            ViewData["REGISTROS"] = itemsPaginados.NumRegistros;
            return View(itemsPaginados.Items);
        }

        //[HttpPost]
        //public async Task<IActionResult> Index()
        //{
        //    List<Item> items = await this.repo.GetAllProductsAsync();
        //    return View(items);
        //}

        public async Task<ActionResult> Carrito() {
            return View();
        }
    }
}
