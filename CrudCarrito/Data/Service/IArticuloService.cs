using CrudCarrito.Data.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrudCarrito.Data.Service
{
    public interface IArticuloService
    {
        Task Delete(int id);
        Task<IEnumerable<Articulo>> GetAll();
        Task<IEnumerable<Articulo>> GetId(int id);
        Task InsertArt(Articulo articulo);
        Task UpdateArticulo(Articulo articulo);
    }
}