using System.Collections.Generic;
using System.Threading.Tasks;
using TesteBackendEnContact.Controllers.Models.File;
using TesteBackendEnContact.Core.Interface.ContactBook.Contact;
using TesteBackendEnContact.Core.Interface.Node;

namespace TesteBackendEnContact.Services.Interface
{
    public interface IContactService
    {
        Task<IContact> GetAsync(int id);
        Task<INodeContact> GetAllAsync();
        Task<IContact> SaveAsync(IContact contact);
        Task<IContact> EditAsync(IContact contact);
        Task DeleteAsync(int id);
        Task<INodeContact> SearchContact(int? id, int? contactBookId, int? companyId, string name, string phone, string email, string address, string companyName, int currentPage, int pageSize);
        Task<List<string>> SaveContactFileAsync(UploadFile file);
        Task<string> GenerateFileCSV();
        Task<string> ModelCsv();
    }
}
