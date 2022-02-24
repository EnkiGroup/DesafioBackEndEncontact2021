using System.Linq;
using System.Threading.Tasks;
using TesteBackendEnContact.Core.Interface.ContactBook;
using TesteBackendEnContact.Core.Interface.Node;
using TesteBackendEnContact.Repository.Interface;
using TesteBackendEnContact.Services.Interface;
using TesteBackendEnContact.Services.Models;
using TesteBackendEnContact.Services.Models.Node;

namespace TesteBackendEnContact.Services
{
    public class ContactBookService : IContactBookService
    {
        private readonly IContactBookRepository _contactBookRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IContactRepository _contactRepository;
        public ContactBookService(IContactBookRepository contactBookRepository, ICompanyRepository companyRepository, IContactRepository contactRepository)
        {
            _contactBookRepository = contactBookRepository;
            _companyRepository = companyRepository;
            _contactRepository = contactRepository;
        }

        public async Task<IContactBook> GetAsync(int id) => await _contactBookRepository.GetAsync(id);

        public async Task<INodeContactBook> GetAllAsync()
        {
            var listContactBook = await _contactBookRepository.GetAllAsync();
            var contactBook = listContactBook.OrderBy(x => x.Id);

            var node = new NodeContactBook()
            {
                ContactBook = contactBook,
                TotalRows = listContactBook.Count<IContactBook>(),
            };

            return node;
        }

        public async Task DeleteAsync(int id)
        {
            var contactBook = await _contactBookRepository.GetAsync(id);
            if (contactBook is not null)
            {
                var contact = await _contactRepository.GetContact(name: contactBook.Name);
                var company = await _companyRepository.GetAsync(contact.CompanyId);
                if (company is not null)
                {
                    if (contactBook.Id == company.ContactBookId)
                        await _companyRepository.DeleteAsync(company.Id);
                }
                await _contactRepository.DeleteAsync(contact.Id);
            }
            await _contactBookRepository.DeleteAsync(id);
        }

        public async Task<IContactBook> SaveAsync(IContactBook contactBook) => await _contactBookRepository.SaveAsync(contactBook);

        public async Task<IContactBook> EditAsync(IContactBook contactBook)
        {
            var contactBookModel = new ContactBookModel(contactBook);

            var company = await _companyRepository.GetCompanyByContactBookId(contactBookModel.Id);
            if (!(company.Id == 0))
            {
                var companyModel = new CompanyModel()
                {
                    Id = company.Id,
                    ContactBookId = contactBook.Id,
                    Name = contactBookModel.Name,
                };
                _ = await _companyRepository.EditAsync(companyModel);
            }

            var contact = await _contactRepository.GetContact(contactBookId: contactBook.Id);
            var contactModel = new ContactModel()
            {
                Id = contact.Id,
                ContactBookId = contactBook.Id,
                CompanyId = contact.Id,
                Name = contactBookModel.Name,
                Phone = contact.Phone,
                Email = contact.Email,
                Address = contact.Address
            };
            _ = await _contactRepository.EditAsync(contactModel);

            return await _contactBookRepository.EditAsync(contactBookModel);
        }
    }
}
