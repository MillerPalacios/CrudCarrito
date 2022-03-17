using Blazored.LocalStorage;
using Blazored.Toast.Services;
using CrudCarrito.Data.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrudCarrito.Data.Service
{
    public class CartLocalStorageService : ICartLocalStorageService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly IToastService _toastService;
        private readonly IArticuloService _articuloService;

        public event Action OnChange;

        public CartLocalStorageService(ILocalStorageService localStorage, IArticuloService articuloService, IToastService toastService)
        {
            _localStorage = localStorage;
            _toastService = toastService;
            _articuloService = articuloService;
        }

        public async Task AddToCart(CartItem item)
        {
            var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
            if (cart == null)
            {
                cart = new List<CartItem>();
            }

            var sameItem = cart
                .Find(x => x.IdArticulo == item.IdArticulo && x.Codigo == item.Codigo);
            if (sameItem == null)
            {
                cart.Add(item);
            }
            else
            {
                sameItem.Quantity += item.Quantity;
            }

            await _localStorage.SetItemAsync("cart", cart);

            var articulo = await _articuloService.GetId(item.IdArticulo);
            _toastService.ShowSuccess("Added to cart:");

            OnChange.Invoke();
        }
        public async Task<List<CartItem>> GetCartItems()
        {
            var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
            if (cart == null)
            {
                return new List<CartItem>();
            }
            return cart;
        }

        public async Task DeleteItem(CartItem item)
        {
            var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
            if (cart == null)
            {
                return;
            }

            var cartItem = cart.Find(x => x.IdArticulo == item.IdArticulo && x.Codigo == item.Codigo);
            cart.Remove(cartItem);

            await _localStorage.SetItemAsync("cart", cart);
            OnChange.Invoke();
        }

        public async Task EmptyCart()
        {
            await _localStorage.RemoveItemAsync("cart");
            OnChange.Invoke();
        }
    }
}
