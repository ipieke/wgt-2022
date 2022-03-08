using System.Data;
using System.Data.SqlClient;
using WGT.API.Models;

namespace WGT.API.Repositories;

public interface IProductRepository
{
    Task<IReadOnlyList<Product>> GetProductListAsync();

    Task CreateProductAsync(Product product);
}

public class ProductRepository : IProductRepository
{
    // TODO: should be injected
    private const string ConnectionString = "data source=localhost,1433;initial catalog=WomenGoTech;user id=wgt_user; password=wgt01WGT";

    public Task<IReadOnlyList<Product>> GetProductListAsync()
    {
        var query = "select product_id, name, price, quantity from dbo.Products;";
        var mapper = (IDataRecord record) => new Product
        {
            Id = (int)record["product_id"],
            Name = (string)record["name"],
            Price = (decimal)record["price"],
            Quantity = (int)record["quantity"]
        };

        return ExecuteQueryAsync(CancellationToken.None, mapper, query);
    }

    public Task CreateProductAsync(Product product)
    {
        var query = "insert into dbo.Products (name, price, quantity) values (@name, @price, @quantity);";
        var parameters = new IDataParameter[]
        {
            new SqlParameter("name", product.Name),
            new SqlParameter("price", product.Price),
            new SqlParameter("quantity", product.Quantity)
        };

        return ExecuteNonQueryAsync(CancellationToken.None, query, parameters);
    }


    private static async Task<IReadOnlyList<T>> ExecuteQueryAsync<T>(CancellationToken token, Func<IDataRecord, T> map, string query, params IDataParameter[] parameters)
    {
        var result = new List<T>();

        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync(token);
        
        await using var command = connection.CreateCommand();
        command.CommandType = CommandType.Text;
        command.CommandText = query;
        command.Parameters.AddRange(parameters);

        await using var reader = await command.ExecuteReaderAsync(token);
        while (await reader.ReadAsync(token))
        {
            result.Add(map(reader));
        }

        return result;
    }

    private async Task<int> ExecuteNonQueryAsync(CancellationToken token, string query, params IDataParameter[] parameters)
    {
        await using var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync(token);
        
        await using var command = connection.CreateCommand();
        command.CommandType = CommandType.Text;
        command.CommandText = query;
        command.Parameters.AddRange(parameters);
        
        return await command.ExecuteNonQueryAsync(token);
    }
}

