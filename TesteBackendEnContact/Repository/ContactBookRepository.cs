using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteBackendEnContact.Core.Domain.ContactBook;
using TesteBackendEnContact.Core.Interface.ContactBook;
using TesteBackendEnContact.Database;
using TesteBackendEnContact.Repository.Interface;
using TesteBackendEnContact.Repository.Models;

namespace TesteBackendEnContact.Repository
{
    public class ContactBookRepository : IContactBookRepository
    {
        private readonly DatabaseConfig _databaseConfig;

        public ContactBookRepository(DatabaseConfig databaseConfig)
        {
            _databaseConfig = databaseConfig;
        }

        public async Task<IContactBook> EditAsync(IContactBook contactBook)
        {
            using var connection = new SqliteConnection(_databaseConfig.ConnectionString);

            var dao = new ContactBookDao(contactBook);
            await connection.UpdateAsync(dao);

            return dao.Export();
        }

        public async Task<IContactBook> SaveAsync(IContactBook contactBook)
        {
            using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
            var dao = new ContactBookDao(contactBook);

            dao.Id = await connection.InsertAsync(dao);

            return dao.Export();
        }

        public async Task DeleteAsync(int id)
        {
            using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
            //connection.Open();
            //using var transaction = connection.BeginTransaction();
            // TODO
            var sql = " DELETE FROM ContactBook WHERE Id = @id ";

            await connection.ExecuteAsync(sql.ToString(), new { id });
        }

        public async Task<IEnumerable<IContactBook>> GetAllAsync()
        {
            using var connection = new SqliteConnection(_databaseConfig.ConnectionString);

            var query = "SELECT * FROM ContactBook";
            var result = await connection.QueryAsync<ContactBookDao>(query);

            return result?.Select(item => item.Export());
        }

        public async Task<IContactBook> GetAsync(int id)
        {
            using var connection = new SqliteConnection(_databaseConfig.ConnectionString);

            var query = "SELECT * FROM ContactBook WHERE Id = @id";
            var result = await connection.QuerySingleOrDefaultAsync<ContactBookDao>(query, new { id });

            return result?.Export();
        }
    }
}
