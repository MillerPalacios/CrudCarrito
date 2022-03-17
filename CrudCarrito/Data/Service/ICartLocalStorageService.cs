using CrudCarrito.Data.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrudCarrito.Data.Service
{
    public interface ICartLocalStorageService
    {
        event Action OnChange;

        Task AddToCart(CartItem item);
        Task DeleteItem(CartItem item);
        Task EmptyCart();
        Task<List<CartItem>> GetCartItems();
    }
}