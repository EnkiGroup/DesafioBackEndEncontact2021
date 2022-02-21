using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteBackendEnContact.Core.Interface.ContactBook.Company;
using TesteBackendEnContact.Core.Interface.Node;
using TesteBackendEnContact.Repository.Interface;
using TesteBackendEnContact.Services.Interface;
using TesteBackendEnContact.Services.Models;
using TesteBackendEnContact.Services.Models.Node;

namespace TesteBackendEnContact.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IContactBookRepository _contactBookRepository;
        private readonly IContactRepository _contactRepository;

        public CompanyService(ICompanyRepository companyRepository, IContactBookRepository contactBookRepository, IContactRepository contactRepository)
        {
            _companyRepository = companyRepository;
            _contactBookRepository = contactBookRepository;
            _contactRepository = contactRepository;
        }

        public async Task<ICompany> GetAsync(int id) => await _companyRepository.GetAsync(id);

        public async Task<INodeCompany> GetAllAsync(int currentPage, int pageSize)
        {
            var listCompany = await _companyRepository.GetAllAsync();

            var companys = listCompany.OrderBy(x => x.Id).Skip((currentPage - 1) * pageSize).Take(pageSize);
            var company = new NodeCompany()
            {
                Companys = companys,
                TotalRows=listCompany.Count<ICompany>()
            };
            return company;
        }

        public async Task<ICompany> SaveAsync(ICompany company)
        {
            var model = new CompanyModel(company);
            var contactBookId = await _contactBookRepository.InsertContactBook(model.Name);
            model.ContactBookId = contactBookId;
            var result = await _companyRepository.SaveAsync(model);
            return result;
        }

        public async Task<ICompany> EditAsync(ICompany company)
        {
            var companyModel = await _companyRepository.GetAsync(company.Id);

            if (companyModel is null)
                throw new Exception("Company Not found");

            if (company.ContactBookId != 0 && company.ContactBookId != companyModel.ContactBookId)
                throw new Exception("Id ContactBook cannot be changed");

            var contactBook = await _contactBookRepository.GetContactBookByName(companyModel.Name);
            if (contactBook is not null)
            {
                var contactBookModel = new ContactBookModel()
                {
                    Id = contactBook.Id,
                    Name = company.Name,
                };
                _ = await _contactBookRepository.EditAsync(contactBookModel);
            }

            var contact = await _contactRepository.GetContact(companyId: companyModel.Id, name: companyModel.Name);
            if (contact is not null)
            {
                var newContact = new ContactModel()
                {
                    Id = contact.Id,
                    ContactBookId = contact.ContactBookId,
                    CompanyId = company.Id,
                    Name = company.Name,
                    Phone = contact.Phone,
                    Email = contact.Email,
                    Address = contact.Address
                };
                _ = await _contactRepository.EditAsync(newContact);
            }

            var newCompany = new CompanyModel()
            {
                Id = companyModel.Id,
                ContactBookId = companyModel.ContactBookId,
                Name = company.Name
            };
            var result = await _companyRepository.EditAsync(newCompany);
            return result;
        }

        public async Task DeleteAsync(int id) => await _companyRepository.DeleteAsync(id);
    }
}
