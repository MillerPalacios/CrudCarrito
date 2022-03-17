using CrudCarrito.Data.Model;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CrudCarrito.Data.Service
{
    public class CartService : ICartService
    {
        private readonly SqlConnectionConfiguration _configuration;

        public CartService(SqlConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task InsertCart(Cart cart)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {

                var parameters = new DynamicParameters();
                parameters.Add("IdCarrito", cart.IdCarrito, DbType.Int32);
                parameters.Add("Codigo", cart.Codigo, DbType.Int32);
                parameters.Add("Nombre", cart.Nombre, DbType.String);
                parameters.Add("Descripcion", cart.Descripcion, DbType.String);
                parameters.Add("Imagen", cart.Imagen, DbType.String);
                parameters.Add("Categoria", cart.Categoria, DbType.String);
                parameters.Add("Quantity", cart.Quantity, DbType.Int32);

                const string InsertCarrito = @"INSERT INTO dbo.Carrito (IdCarrito, Codigo, Nombre, Descripcion, Imagen, Categoria, Quantity) " +
                    "VALUES (@IdCarrito, @Codigo, @Nombre, @Descripcion,@Imagen,@Categoria, @Quantity)";

                await conn.ExecuteAsync(InsertCarrito, parameters);
            }
        }
        //Obtener todos los datos
        public async Task<IEnumerable<Cart>> GetAllCart()
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                const string SelectCarrito = @"SELECT * FROM dbo.Carrito";
                var resultCarrito = await conn.QueryAsync<Cart>(SelectCarrito);
                return resultCarrito.ToList();
            }
        }
        //Obtener solo uno por el id
        public async Task<IEnumerable<Cart>> GetIdCart(int id)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                const string SelectCarrito = @"SELECT * FROM dbo.Carrito WHERE IdCarrito = @IdCarrito";
                return (IEnumerable<Cart>)await conn.QuerySingleAsync<Cart>(SelectCarrito, new { IdCarrito = id });
            }
        }
        //actualizar
        public async Task UpdateCarrito(Cart cart)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                var parameters = new DynamicParameters();
                parameters.Add("IdCarrito", cart.IdCarrito, DbType.Int32);
                parameters.Add("Codigo", cart.Codigo, DbType.Int32);
                parameters.Add("Nombre", cart.Nombre, DbType.String);
                parameters.Add("Descripcion", cart.Descripcion, DbType.String);
                parameters.Add("Imagen", cart.Imagen, DbType.String);
                parameters.Add("Categoria", cart.Categoria, DbType.String);
                parameters.Add("Quantity", cart.Quantity, DbType.Int32);

                const string UpdateCarrito = @"UPDATE dbo.Carrito SET Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Descripcion, Imagen = @Imagen, Categoria = @Categoria, Quantity = @Quantity" +
                "WHERE IdCarrito = @IdCarrito";

                await conn.ExecuteAsync(UpdateCarrito, parameters);
            }
        }

        public async Task DeleteCarrito(int id)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                const string DeleteCart = @"DELETE FROM dbo.Carrito WHERE IdCarrito = @IdCarrito";

                await conn.ExecuteAsync(DeleteCart, new { IdCarrito = id });
            }
        }

        public async Task DeleteAll()
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                const string DeleteAllCart = @"DELETE FROM dbo.Carrito";

                await conn.ExecuteAsync(DeleteAllCart);
            }
        }
    }
}
