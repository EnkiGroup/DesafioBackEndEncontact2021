using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteBackendEnContact.DAO;
using TesteBackendEnContact.Database;
using TesteBackendEnContact.Models;
using TesteBackendEnContact.Models.Interface;
using TesteBackendEnContact.Repository.Interface;

namespace TesteBackendEnContact.Repository
{
    public class ContactBookRepository : IContactBookRepository
    {

        private readonly SqliteConnection connection;
        public ContactBookRepository(DatabaseConfig databaseConfig)
        {
            this.connection = new SqliteConnection(databaseConfig.ConnectionString);
        }


        public async Task<IContactBook> SaveAsync(IContactBook contactBook)
        {

            var dao = new ContactBookDao(contactBook);

            if (dao.Id == 0)
                dao.Id = await connection.InsertAsync(dao);
            else
                await connection.UpdateAsync(dao);

            return dao.Export();
        }


        public async Task DeleteAsync(int id)
        {

            connection.Open();

            using var transaction = connection.BeginTransaction();

            var sql = new StringBuilder();

            sql.AppendLine("DELETE FROM ContactBook WHERE Id = @id;");

            await connection.ExecuteAsync(sql.ToString(), new { id }, transaction);

            transaction.Commit();

            connection.Close();

        }

        public async Task<IEnumerable<IContactBook>> GetAllAsync()
        {

            var query = "SELECT * FROM ContactBook";
            var result = await connection.QueryAsync<ContactBookDao>(query);

            var returnList = new List<IContactBook>();

            foreach (var AgendaSalva in result.ToList())
            {
                IContactBook Agenda = new ContactBook(AgendaSalva.Id, AgendaSalva.Name.ToString());
                returnList.Add(Agenda);
            }

            return returnList.ToList();
        }
        public async Task<IContactBook> GetAsync(int id)
        {
            var list = await GetAllAsync();

            return list.ToList().Where(item => item.Id == id).FirstOrDefault();
        }
        public bool IsInDatabase(int id)
        {

            var query = "SELECT Id FROM ContactBook where Id = @id";
            var result = connection.QueryAsync<ContactBookDao>(query, new { id });

            if (result != null)
                return true;

            return false;
        }
        public async Task<int> GetId(string contactBookName)
        {

            var query = "SELECT Id FROM ContactBook where Name = @contactBookName";

            var result = await connection.QueryAsync<int>(query, new { contactBookName });

            return result.First();
        }
    }

}