using System.Collections.Generic;
using System.Threading.Tasks;
using TesteBackendEnContact.Core.Interface.ContactBook.Contact;
using TesteBackendEnContact.Core.Interface.Node;

namespace TesteBackendEnContact.Services.Interface
{
    public interface IContactService
    {
        Task<IContact> GetAsync(int id);
        Task<INodeContact> GetAllAsync(int currentPage, int pageSize);
        Task<IContact> SaveAsync(IContact contact);
        Task<IContact> EditAsync(IContact contact);
        Task DeleteAsync(int id);
    }
}
