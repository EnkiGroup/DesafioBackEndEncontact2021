using System.Collections.Generic;
using System.Threading.Tasks;
using TesteBackendEnContact.Models.Interface;

namespace TesteBackendEnContact.Repository.Interface
{
    public interface IContactRepository
    {
        Task<IContact> SaveAsync(IContact contact);

        Task<IEnumerable<IContact>> GetAsync(int pageSize, int? page, string searchTerm, int contactBookId);

        Task<IEnumerable<IContact>> GetContactBookContacts(int contactBookId, int companyId);
    }
}
