using System.Collections.Generic;
using System.Threading.Tasks;
using TesteBackendEnContact.Core.Interface.ContactBook.Company;

namespace TesteBackendEnContact.Services.Interface
{
    public interface ICompanyService
    {
        Task<ICompany> SaveAsync(ICompany company);
        Task<ICompany> EditAsync(ICompany company);
        Task<ICompany> GetAsync(int id);
        Task<IEnumerable<ICompany>> GetAllAsync();
        Task DeleteAsync(int id);
    }
}
