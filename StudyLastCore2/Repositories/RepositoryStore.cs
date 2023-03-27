using Microsoft.EntityFrameworkCore;
using StudyLastCore2.Data;
using StudyLastCore2.Helpers;
using StudyLastCore2.Models;

namespace StudyLastCore2.Repositories {
    public class RepositoryStore {

        private StoreContext context;

        public RepositoryStore(StoreContext context)
        {
            this.context = context;
        }

        /*
            
            Repository

            List<Items> GetAllProducts()
            List<Items> GetProducts(int posicion)

            Task<int> InsertUserAsync(string name, string password, string salt, string role) "Ya tiene dentro el get nexid"
            Task<int> InsertItem(string name, string descripction, int amount, string image) "Ya tiene dentro el get nexid"

            Task<int> InsertOrder(int idUser, datetime dateOrder) "Se inserta la orden y se recupera el nuevo ID para realizar las posteriores insercciones de items"
            Task InsertItemOrder(int idOrder, int idItem, int amount) 

        */

        public async Task RegisterUserAsync(string username, string password) {
            var newid = this.context.Users.Any() ? this.context.Users.Max(u => u.IdUser) + 1 : 1;
            User user = new User();
            user.IdUser = newid;
            user.Name = username;
            user.Role = "cliente";
            user.Salt = HelperCryptography.GenerateSalt();
            user.Password = HelperCryptography.EncryptPassword(password, user.Salt);

            this.context.Users.Add(user);
            await this.context.SaveChangesAsync();
        }

        public async Task<User> LoginUserAsync (string username, string password) {
            User user = await this.context.Users.FirstOrDefaultAsync(u => u.Name == username);
            if(user == null) {
                return null;
            } else {
                byte[] passUsuario = user.Password;
                string salt = user.Salt;

                byte[] temp = HelperCryptography.EncryptPassword(password, salt);
                bool respuesta = HelperCryptography.CompareArrays(passUsuario, temp);

                if (respuesta == true) {
                    return user;
                } else {
                    return null;
                }
            }
        }

        public async Task<List<Item>> GetAllProductsAsync() {
            return await this.context.Items.ToListAsync();
        }

        public async Task<ItemsPaginados> GetProductsAsync(int posicion) {
            List<Item> items = await this.GetAllProductsAsync();
            int numregistros = items.Count;

            List<Item> listaitemspaginados = items.Skip(posicion).Take(3).ToList();

            ItemsPaginados itemsPaginados = new ItemsPaginados { 
                Items = listaitemspaginados,
                NumRegistros = numregistros
            };

            return itemsPaginados;
        }

        
    }
}
