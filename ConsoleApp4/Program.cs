using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;

namespace ConsoleApp4
{
    internal class Program
    {
        /// <summary>
        /// Connection string to the database.
        /// </summary>
        private static readonly string connectionString =
            "Data Source=.;Initial Catalog=test;Integrated Security=True;Trust Server Certificate=True";

        private static async Task Main(string[] args)
        {
            // Example usage of the methods


            // Add a new record
            var addResult = await AddRecord<Item>("addrecord", new Item(){Id = Guid.NewGuid(), Name = "Dima"});

            // Get all records
            var getAllResult = await GetItems<Item>("getrecords");

            // Update a record
            var updateResult = await UpdateRecord<Item>("updaterecord", new Item() { Id = getAllResult.FirstOrDefault(x => x.Name == "Dima1")!.Id, Name = "Dima" });

            // Delete a record
            var deleteResult = await DeleteRecord("deleterecord", getAllResult.First().Id);

            // Get a record by ID
            var getByIdResult = await GetItemById<Item>("getrecordbyid", getAllResult.First().Id);

            // Get all records by a specific parameter
            var getAllByParameterResult = await GetAllByParameter<Item>("getrecordsbyname", "Name", "Dima");

            // Get a single record by a specific parameter
            var getOneByParameterResult = await GetOneByParameter<Item>("getonebyname", "Name", "Dima");
        }

        /// <summary>
        /// Gets all items from the database using a stored procedure.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="procedureName"></param>
        /// <returns></returns>
        private static async Task<List<TResult>> GetItems<TResult>(string procedureName)
        {
            await using var connection = new SqlConnection(connectionString);
            var result = await connection.QueryAsync<TResult>(procedureName, commandType: CommandType.StoredProcedure);
            return result.ToList();
        }

        /// <summary>
        /// Adds a record to the database using a stored procedure.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="procedureName"></param>
        /// <param name="entity"></param>
        /// <returns></returns>

        private static async Task<int> AddRecord<TEntity>(string procedureName, TEntity entity)
        {
            await using var connection = new SqlConnection(connectionString);
            return await connection.ExecuteAsync(procedureName, entity, commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// Updates a record in the database using a stored procedure.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="procedureName"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        private static async Task<int> UpdateRecord<TEntity>(string procedureName, TEntity entity)
        {
            await using var connection = new SqlConnection(connectionString);
            return await connection.ExecuteAsync(procedureName, entity, commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// Deletes a record from the database using a stored procedure.
        /// </summary>
        /// <param name="procedureName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private static async Task<int> DeleteRecord(string procedureName, Guid id)
        {
            await using var connection = new SqlConnection(connectionString);
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);
            return await connection.ExecuteAsync(procedureName, parameters, commandType: CommandType.StoredProcedure);
        }


        /// <summary>
        /// Gets a single item by ID from the database using a stored procedure.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="procedureName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private static async Task<TResult?> GetItemById<TResult>(string procedureName, Guid id) 
        {
            await using var connection = new SqlConnection(connectionString);
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);

            return await connection.QuerySingleOrDefaultAsync<TResult>(procedureName, parameters, commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// Gets all items by a specific parameter from the database using a stored procedure.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="procedureName"></param>
        /// <param name="parameterName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private static async Task<List<TResult>> GetAllByParameter<TResult>(string procedureName, string parameterName, object value) 
        {
            await using var connection = new SqlConnection(connectionString);
            var parameters = new DynamicParameters();
            parameters.Add(parameterName, value);

            var result = await connection.QueryAsync<TResult>(procedureName, parameters, commandType: CommandType.StoredProcedure);
            return result.ToList();
        }

        /// <summary>
        /// Gets a single item by a specific parameter from the database using a stored procedure.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="procedureName"></param>
        /// <param name="parameterName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private static async Task<TResult?> GetOneByParameter<TResult>(string procedureName, string parameterName, object value) 
        {
            await using var connection = new SqlConnection(connectionString);
            var parameters = new DynamicParameters();
            parameters.Add(parameterName, value);

            return await connection.QuerySingleOrDefaultAsync<TResult>(procedureName, parameters, commandType: CommandType.StoredProcedure);
        }
    }

    public class Item
    {
        public Guid Id { get; init; }
        public string? Name { get; init; }
    }
}