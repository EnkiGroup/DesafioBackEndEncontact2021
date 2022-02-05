using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteBackendEnContact.Database;
using TesteBackendEnContact.Dto;
using TesteBackendEnContact.Models.Interface;
using TesteBackendEnContact.Repository.Interface;

namespace TesteBackendEnContact.Repository
{
    public class ContactRepository : IContactRepository
    {

        private readonly SqliteConnection connection;
        public ContactRepository(DatabaseConfig databaseConfig) {

            this.connection = new SqliteConnection(databaseConfig.ConnectionString);
        }

        public async Task<IContact> SaveAsync(IContact contact) { 


            var dao = new ContactDao(contact);

            try
            { 
                if (dao.Id == 0)
                    dao.Id = await connection.InsertAsync(dao);
                else
                    await connection.UpdateAsync(dao);
            }
            catch {
                
                
            }

            return dao.Export();
        }

        public async Task<IEnumerable<IContact>> GetAsync(int pageSize, int? page, string searchTerm, int contactBookId) {

            var result = await connection.QueryAsync<ContactDao>("Select * from Contact Where Id = @searchTerm OR Name = @searchTerm OR Phone = @searchTerm OR Email = @searchTerm OR Address = @searchTerm OR contactBookId = @contactBookId LIMIT @pageSize OFFSET @page", new { searchTerm, pageSize, page = page*pageSize, contactBookId });

            return result?.Select(item => item.Export());
        }
        public async Task<IEnumerable<IContact>> GetContactBookContacts(int contactBookId, int companyId) {

            var result = await connection.QueryAsync<ContactDao>("Select * from Contact Where ContactBookId = @contactBookId AND Id = @companyId", new { contactBookId, companyId });

            return result?.Select(item => item.Export());
        }
    }
}
