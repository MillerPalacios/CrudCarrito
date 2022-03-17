using CrudCarrito.Data.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrudCarrito.Data.Service
{
    public interface ICartService
    {
        Task DeleteCarrito(int id);
        Task<IEnumerable<Cart>> GetAllCart();
        Task<IEnumerable<Cart>> GetIdCart(int id);
        Task InsertCart(Cart cart);
        Task UpdateCarrito(Cart cart);
        Task DeleteAll();
    }
}