using System.Collections.Generic;
using System.Threading.Tasks;
using TesteBackendEnContact.Core.Interface.ContactBook.Contact;

namespace TesteBackendEnContact.Repository.Interface
{
    public interface IContactRepository
    {
        Task<IContact> EditAsync(IContact contact);
        Task<IContact> SaveAsync(IContact contact);
        Task DeleteAsync(int id);
        Task<IEnumerable<IContact>> GetAllAsync();
        Task<IContact> GetAsync(int id);
        //Task<IContact> GetContactByContactBookIdAsync(int contactBookId);
        Task<IContact> GetContact(int? id = null, int? contactBookId = null, int? companyId = null, string name = null, string phone = null, string email = null, string address = null);
        Task<IEnumerable<IContact>> SearchContact(int? id, int? contactBookId, int? companyId, string name, string phone, string email, string address, string nameCompany);
    }
}
