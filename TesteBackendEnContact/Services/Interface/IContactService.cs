using System.Collections.Generic;
using System.Threading.Tasks;
using TesteBackendEnContact.Core.Interface.ContactBook.Contact;

namespace TesteBackendEnContact.Services.Interface
{
    public interface IContactService
    {
        Task<IContact> GetAsync(int id);
        Task<IEnumerable<IContact>> GetAllAsync();
        Task<IContact> SaveAsync(IContact contact);
        Task<IContact> EditAsync(IContact contact);
        Task DeleteAsync(int id);
    }
}
