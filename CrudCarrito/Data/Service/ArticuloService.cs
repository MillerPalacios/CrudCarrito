using CrudCarrito.Data.Model;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CrudCarrito.Data.Service
{
    public class ArticuloService : IArticuloService
    {
        private readonly SqlConnectionConfiguration _configuration;

        public ArticuloService(SqlConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task InsertArt(Articulo articulo)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {

                var parameters = new DynamicParameters();
                parameters.Add("IdArticulo", articulo.IdArticulo, DbType.Int32);
                parameters.Add("Codigo", articulo.Codigo, DbType.Int32);
                parameters.Add("Nombre", articulo.Nombre, DbType.String);
                parameters.Add("Descripcion", articulo.Descripcion, DbType.String);
                parameters.Add("Imagen", articulo.Imagen, DbType.String);
                parameters.Add("Categoria", articulo.Categoria, DbType.String);
                parameters.Add("Quantity", articulo.Quantity, DbType.Int32);

                const string InsertArticulo = @"INSERT INTO dbo.Articulo (IdArticulo, Codigo, Nombre, Descripcion, Imagen, Categoria, Quantity) " +
                    "VALUES (@IdArticulo, @Codigo, @Nombre, @Descripcion,@Imagen,@Categoria, @Quantity)";

                await conn.ExecuteAsync(InsertArticulo, parameters);
            }
        }
        //Obtener todos los datos
        public async Task<IEnumerable<Articulo>> GetAll()
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                const string SelectArticulo = @"SELECT * FROM dbo.Articulo";
                var resultArticulos = await conn.QueryAsync<Articulo>(SelectArticulo);
                return resultArticulos.ToList();
            }
        }
        //Obtener solo uno por el id
        public async Task<IEnumerable<Articulo>> GetId(int id)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                const string SelectArticulo = @"SELECT * FROM dbo.Articulo WHERE IdArticulo = @IdArticulo";
                return (IEnumerable<Articulo>)await conn.QuerySingleAsync<Articulo>(SelectArticulo, new { IdArticulo = id });
            }
        }
        //actualizar
        public async Task UpdateArticulo(Articulo articulo)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                var parameters = new DynamicParameters();
                parameters.Add("IdArticulo", articulo.IdArticulo, DbType.Int32);
                parameters.Add("Codigo", articulo.Codigo, DbType.Int32);
                parameters.Add("Nombre", articulo.Nombre, DbType.String);
                parameters.Add("Descripcion", articulo.Descripcion, DbType.String);
                parameters.Add("Imagen", articulo.Imagen, DbType.String);
                parameters.Add("Categoria", articulo.Categoria, DbType.String);
                parameters.Add("Quantity", articulo.Quantity, DbType.Int32);

                const string UpdateArticulo = @"UPDATE dbo.Articulo SET Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Descripcion, Imagen = @Imagen, Categoria = @Categoria, Quantity = @Quantity" +
                "WHERE IdArticulo = @IdArticulo";

                await conn.ExecuteAsync(UpdateArticulo, parameters);
            }
        }

        public async Task Delete(int id)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                const string DeleteArt = @"DELETE FROM dbo.Articulo WHERE IdArticulo = @IdArticulo";

                await conn.ExecuteAsync(DeleteArt, new { IdArticulo = id });
            }
        }
    }
}
