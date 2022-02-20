using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteBackendEnContact.Core.Interface.ContactBook.Company;
using TesteBackendEnContact.Database;
using TesteBackendEnContact.Repository.Interface;
using TesteBackendEnContact.Repository.Models;

namespace TesteBackendEnContact.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly DatabaseConfig _databaseConfig;

        public CompanyRepository(DatabaseConfig databaseConfig)
        {
            _databaseConfig = databaseConfig;
        }

        public async Task<ICompany> EditAsync(ICompany company)
        {
            using var connection = new SqliteConnection(_databaseConfig.ConnectionString);

            var dao = new CompanyDao(company);
            await connection.UpdateAsync(dao);

            return dao.Export();
        }

        public async Task<ICompany> SaveAsync(ICompany company)
        {
            using var connection = new SqliteConnection(_databaseConfig.ConnectionString);

            var dao = new CompanyDao(company);
            dao.Id = await connection.InsertAsync(dao);

            return dao.Export();
        }

        public async Task DeleteAsync(int id)
        {

            try
            {
                using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
                connection.Open();
                using var transaction = connection.BeginTransaction();

                var sql = new StringBuilder();
                sql.AppendLine("DELETE FROM Company WHERE Id = @id;");
                sql.AppendLine("UPDATE Contact SET CompanyId = 0 WHERE CompanyId = @id;");

                await connection.ExecuteAsync(sql.ToString(), new { id }, transaction);
                transaction.Commit();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            
        }

        public async Task<IEnumerable<ICompany>> GetAllAsync()
        {
            using var connection = new SqliteConnection(_databaseConfig.ConnectionString);

            var query = "SELECT * FROM Company";
            var result = await connection.QueryAsync<CompanyDao>(query);

            return result?.Select(item => item.Export());
        }

        public async Task<ICompany> GetAsync(int id)
        {
            using var connection = new SqliteConnection(_databaseConfig.ConnectionString);

            var query = "SELECT * FROM Company where Id = @id";
            var result = await connection.QuerySingleOrDefaultAsync<CompanyDao>(query, new { id });

            return result?.Export();
        }

        public async Task<ICompany> GetCompanyByNameAsync(string name)
        {
            using var connection = new SqliteConnection(_databaseConfig.ConnectionString);

            var query = "SELECT * FROM Company where Name = @name";
            var result = await connection.QuerySingleOrDefaultAsync<CompanyDao>(query, new { name });

            return result?.Export();
        }

        public async Task<ICompany> GetCompanyByContactBookId(int contactBookId)
        {
            using (var connection = new SqliteConnection(_databaseConfig.ConnectionString))
            {
                var query = " SELECT Id, ContacBookId, Name FROM Company WHERE ContactBookId = @contactBookId ";
                var result = await connection.QuerySingleOrDefaultAsync<CompanyDao>(query, new { contactBookId });

                return result?.Export();
            }
        }
    }
}
