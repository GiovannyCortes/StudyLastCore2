using Microsoft.AspNetCore.Mvc;
using StudyLastCore2.Extensions;
using StudyLastCore2.Repositories;
using StudyLastCore2.Models;
using StudyLastCore2.Filters;
using StudyLastCore2.Helpers;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace StudyLastCore.Controllers {

    public class StoreController : Controller {

        private RepositoryStore repo;
        private HelperUploadFiles helperUpload;
        private IMemoryCache memoryCache;

        public StoreController(RepositoryStore repo, HelperUploadFiles helperUpload, IMemoryCache memoryCache) {
            this.repo = repo;
            this.helperUpload = helperUpload;
            this.memoryCache = memoryCache;
        }

        //NADA MAS ENTRAR SE VE LA LISTA DE TODOS LOS PRODUCTOS
        public async Task<IActionResult> Index(int? posicion) {
            posicion = (posicion == null) ? 0 : posicion.Value;

            ItemsPaginados itemsPaginados = await this.repo.GetProductsAsync(posicion.Value);
            ViewData["REGISTROS"] = itemsPaginados.NumRegistros;
            return View(itemsPaginados.Items);
        }

        [AuthorizeUsers]
        public IActionResult CreateItem() {
            return View();
        }

        [HttpPost] [AuthorizeUsers]
        public async Task<IActionResult> CreateItem(Item item, IFormFile file) {
            int newid = await this.repo.InsertItem(item.Name, item.Description, item.Amount);
            string file_name = "image_" + newid;
            await this.helperUpload.UploadFileAsync(file, Folders.Images, file_name);
            return View();
        }

        //ACCION BOTON CARRITO
        public IActionResult AgregarCarrito(int idProduct) {
            List<int> listidproducts = HttpContext.Session.GetObject<List<int>>("CARRITO");
         
            //COMPROBAR SI CARRITO ESTA EN SESSION
            if (listidproducts != null) {
                listidproducts.Add(idProduct);
                HttpContext.Session.SetObject("CARRITO", listidproducts);
            } else {
                //CREAR CARRITO EN SESSION
                List<int> Nueva_listidproducts = new List<int> { idProduct };
                HttpContext.Session.SetObject("CARRITO", Nueva_listidproducts);
            }
            return RedirectToAction("Index");
        }

        //ACCION BOTON FAV
        public IActionResult AddFavorito(int idProduct) {
            if(this.memoryCache.Get("FAVORITOS") == null) {
                List<int> ids = new List<int> { idProduct };
                
                string list = JsonConvert.SerializeObject(ids);

                this.memoryCache.Set("FAVORITOS", list);
            }
            return View();
        }


        [AuthorizeUsers]
        public async Task<IActionResult> Carrito(int cantidadItem) {
            List<int> listidproducts = HttpContext.Session.GetObject<List<int>>("CARRITO");
            List<Item> items = new List<Item>();
            if (listidproducts != null) {
                foreach (int id in listidproducts) {
                    Item item = await this.repo.FindItemAsync(id);
                    items.Add(item);
                }
            }
            ViewBag.CANTIDAD = cantidadItem;
            return View(items);
        }

    }
}
