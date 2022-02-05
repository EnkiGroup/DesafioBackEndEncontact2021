using System.Collections.Generic;
using System.Threading.Tasks;
using TesteBackendEnContact.Models.Interface;

namespace TesteBackendEnContact.Repository.Interface
{
    public interface IContactBookRepository
    {
        Task<IContactBook> SaveAsync(IContactBook contactBook);
        Task DeleteAsync(int id);
        Task<IEnumerable<IContactBook>> GetAllAsync();
        Task<IContactBook> GetAsync(int id);

        bool IsInDatabase(int id);

        Task<int> GetId(string contactBookName);


    }
}
