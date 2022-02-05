using System.Collections.Generic;
using System.Threading.Tasks;
using TesteBackendEnContact.Models.Interface;

namespace TesteBackendEnContact.Repository.Interface
{
    public interface ICompanyRepository
    {
        Task<ICompany> SaveAsync(ICompany company);
        Task DeleteAsync(int id);
        Task<IEnumerable<ICompany>> GetAllAsync();
        Task<ICompany> GetAsync(int id);
        Task<int> GetContactBookId(string searchTerm);

        Task<int> GetId(int contactBookId);
    }
}
