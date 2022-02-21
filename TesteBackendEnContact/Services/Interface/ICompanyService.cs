using System.Collections.Generic;
using System.Threading.Tasks;
using TesteBackendEnContact.Core.Interface.ContactBook.Company;
using TesteBackendEnContact.Core.Interface.Node;

namespace TesteBackendEnContact.Services.Interface
{
    public interface ICompanyService
    {
        Task<ICompany> SaveAsync(ICompany company);
        Task<ICompany> EditAsync(ICompany company);
        Task<ICompany> GetAsync(int id);
        Task<INodeCompany> GetAllAsync(int currentPage, int pageSize);
        Task DeleteAsync(int id);
    }
}
