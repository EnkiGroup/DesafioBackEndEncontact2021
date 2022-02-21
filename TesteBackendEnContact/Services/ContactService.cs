using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteBackendEnContact.Core.Interface.ContactBook.Contact;
using TesteBackendEnContact.Core.Interface.Node;
using TesteBackendEnContact.Repository.Interface;
using TesteBackendEnContact.Services.Interface;
using TesteBackendEnContact.Services.Models;
using TesteBackendEnContact.Services.Models.Node;

namespace TesteBackendEnContact.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IContactBookRepository _contactBookRepository;
        public ContactService(IContactRepository contactRepository, ICompanyRepository companyRepository, IContactBookRepository contactBookRepository)
        {
            _contactRepository = contactRepository;
            _companyRepository = companyRepository;
            _contactBookRepository = contactBookRepository;
        }

        public async Task<IContact> GetAsync(int id)
        {
            return await _contactRepository.GetAsync(id);
        }

        public async Task<INodeContact> GetAllAsync(int currentPage, int pageSize)
        {
            var listContacts = await _contactRepository.GetAllAsync();
            var contacts = listContacts.OrderBy(x => x.Id).Skip((currentPage - 1) * pageSize).Take(pageSize);

            var nodeContact = new NodeContact()
            {
                Contacts = contacts,
                TotalRows = listContacts.Count<IContact>()
            };
            return nodeContact;
        }

        public async Task<IContact> SaveAsync(IContact contact)
        {
            var companyId = 0;

            if (contact.CompanyId > 0)
                companyId = contact.CompanyId;

            var contactBookModel = new ContactBookModel()
            {
                Name = contact.Name,
            };
            var contactBook = await _contactBookRepository.SaveAsync(contactBookModel);


            if (contact.IsCompany)
            {
                var companyModel = new CompanyModel()
                {
                    ContactBookId = contactBook.Id,
                    Name = contact.Name,
                };
                var company = await _companyRepository.SaveAsync(companyModel);
                companyId = company.Id;
            }

            var contactModel = new ContactModel()
            {
                ContactBookId = contactBook.Id,
                CompanyId = companyId,
                Name = contact.Name,
                Phone = contact.Phone,
                Email = contact.Email,
                Address = contact.Address,
            };
            return await _contactRepository.SaveAsync(contactModel);
        }

        public async Task<IContact> EditAsync(IContact contact)
        {
            var validatorCompany = false;
            var company = await _companyRepository.GetCompanyByNameAsync(contact.Name);
            var contactBook = await _contactBookRepository.GetContactBookByName(contact.Name);

            if (company is not null)
                validatorCompany = contactBook.Id == company.ContactBookId ? true : false;

            if (validatorCompany)
            {
                var companyModel = new CompanyModel()
                {
                    Id = company.Id,
                    Name = contact.Name,
                };
                _ = await _companyRepository.EditAsync(companyModel);
            }

            var contactBookModel = new ContactBookModel()
            {
                Id = contactBook.Id,
                Name = contact.Name
            };
            _ = await _contactBookRepository.EditAsync(contactBookModel);

            return await _contactRepository.EditAsync(contact);
        }

        public async Task DeleteAsync(int id)
        {
            await _contactRepository.DeleteAsync(id);
        }
    }
}
