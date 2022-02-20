using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteBackendEnContact.Core.Interface.ContactBook.Contact;
using TesteBackendEnContact.Database;
using TesteBackendEnContact.Repository.Interface;
using TesteBackendEnContact.Repository.Models;

namespace TesteBackendEnContact.Repository
{
    public class ContactRepository : IContactRepository
    {
        private readonly DatabaseConfig _databaseConfig;
        public ContactRepository(DatabaseConfig databaseConfig)
        {
            _databaseConfig = databaseConfig;
        }

        public async Task<IContact> EditAsync(IContact contact)
        {
            var query = new StringBuilder();
            using (var connection = new SqliteConnection(_databaseConfig.ConnectionString))
            {
                var dao = new ContactDao(contact);
                query.Append(" UPDATE ");
                query.Append(" [Contact] ");
                query.Append(" SET [ContactBookId] = @ContactBookId");
                query.Append(" ,[CompanyId] = @CompanyId ");
                query.Append(" ,[Name] = @Name ");
                query.Append(" ,[Phone] = @Phone ");
                query.Append(" ,[Email] = @Email ");
                query.Append(" ,[Address] = @Address ");
                query.Append(" WHERE Id = @Id ");
                await connection.ExecuteScalarAsync(query.ToString(), dao);
                return dao.Export();
            }
            //using var connection = new SqliteConnection(_databaseConfig.ConnectionString);

            //var dao = new ContactDao(contact);
            //await connection.UpdateAsync(dao);

            //return dao.Export();
        }

        public async Task<IContact> SaveAsync(IContact contact)
        {
            var query = new StringBuilder();
            using (var connection = new SqliteConnection(_databaseConfig.ConnectionString))
            {
                var dao = new ContactDao(contact);

                query.Append(" INSERT INTO [Contact] ");
                query.Append(" ([ContactBookId] ");
                query.Append(" ,[CompanyId] ");
                query.Append(" ,[Name] ");
                query.Append(" ,[Phone] ");
                query.Append(" ,[Email] ");
                query.Append(" ,[Address]) ");
                query.Append(" VALUES ");
                query.Append(" (@ContactBookId ");
                query.Append(" ,@CompanyId ");
                query.Append(" ,@Name ");
                query.Append(" ,@Phone ");
                query.Append(" ,@Email ");
                query.Append(" ,@Address); ");
                query.Append(" SELECT Id");
                query.Append(" FROM [Contact] ");
                query.Append(" WHERE Id = (SELECT MAX(ID)  FROM [Contact]) ");
                dao.Id = await connection.ExecuteScalarAsync<int>(query.ToString(), dao);
                return dao.Export();
            }
        }

        public async Task DeleteAsync(int id)
        {
            using var connection = new SqliteConnection(_databaseConfig.ConnectionString);

            var sql = " DELETE FROM [Contact] WHERE Id = @id ";

            await connection.ExecuteAsync(sql.ToString(), new { id });
        }

        public async Task<IEnumerable<IContact>> GetAllAsync()
        {
            using var connection = new SqliteConnection(_databaseConfig.ConnectionString);

            var query = "SELECT * FROM Contact";
            var result = await connection.QueryAsync<ContactDao>(query);

            return result?.Select(item => item.Export());
        }

        public async Task<IContact> GetAsync(int id)
        {
            using var connection = new SqliteConnection(_databaseConfig.ConnectionString);

            var query = "SELECT * FROM Contact where Id = @id ";
            var result = await connection.QuerySingleOrDefaultAsync<ContactDao>(query, new { id });

            return result?.Export();
        }

        public async Task<IContact> GetContact(int? id = null, int? contactBookId = null, int? companyId = null, string name = null, string phone = null, string email = null, string address = null)
        {
            var query = new StringBuilder();
            using (var conn = new SqliteConnection(_databaseConfig.ConnectionString))
            {
                query.Append(" SELECT ");
                query.Append(" Id ");
                query.Append(" ,ContactBookId ");
                query.Append(" ,CompanyId ");
                query.Append(" ,Name ");
                query.Append(" ,Phone ");
                query.Append(" ,Email ");
                query.Append(" ,Address ");
                query.Append(" FROM Contact ");
                query.Append(" WHERE CompanyId IS NOT NULL");

                if (id is not null)
                {
                    query.Append(" AND Id = @id ");
                }
                if (contactBookId is not null)
                {
                    query.Append(" AND ContactBookId = @contactBookId ");
                }
                if (companyId is not null)
                {
                    query.Append(" AND CompanyId = @companyId ");
                }

                if (name is not null)
                {
                    query.Append(" AND Name = @name ");
                }
                if (phone is not null)
                {
                    query.Append(" AND Phone = @phone ");
                }
                if (email is not null)
                {
                    query.Append(" AND Email = @email ");
                }
                if (address is not null)
                {
                    query.Append(" AND Address = @address ");
                }
                var result = await conn.QuerySingleOrDefaultAsync<ContactDao>(query.ToString(), new { id, contactBookId, companyId, name, phone, email, address });
                return result?.Export();
            }
        }
    }
}
