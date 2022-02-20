using System.Collections.Generic;
using System.Threading.Tasks;
using TesteBackendEnContact.Core.Interface.ContactBook;

namespace TesteBackendEnContact.Services.Interface
{
    public interface IContactBookService
    {
        Task<IContactBook> GetAsync(int id);
        Task<IEnumerable<IContactBook>> GetAllAsync();
        Task DeleteAsync(int id);
        Task<IContactBook> SaveAsync(IContactBook contactBook);
        Task<IContactBook> EditAsync(IContactBook contactBook);
    }
}
