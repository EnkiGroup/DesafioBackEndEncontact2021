using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteBackendEnContact.Controllers.Models.File;
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
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ContactService(IContactRepository contactRepository, ICompanyRepository companyRepository, IContactBookRepository contactBookRepository, IWebHostEnvironment webHostEnvironment)
        {
            _contactRepository = contactRepository;
            _companyRepository = companyRepository;
            _contactBookRepository = contactBookRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IContact> GetAsync(int id)
        {
            return await _contactRepository.GetAsync(id);
        }

        public async Task<INodeContact> GetAllAsync()
        {
            var listContacts = await _contactRepository.GetAllAsync();
            var contacts = listContacts.OrderBy(x => x.Id);

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
            var contactModel = new ContactModel();

            if (contact.CompanyId > 0)
                companyId = contact.CompanyId;

            if ((contact.CompanyId > 0) && !await _companyRepository.ExistCompany(companyId) && contact.IsCompany == false)
            {
                throw new Exception("Not Found");
            }
            else
            {
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

                #region .: Contact Model .:
                contactModel.ContactBookId = contactBook.Id;
                contactModel.CompanyId = companyId;
                contactModel.Name = contact.Name;
                contactModel.Phone = contact.Phone;
                contactModel.Email = contact.Email;
                contactModel.Address = contact.Address;
                #endregion
            }
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
            var contact = await _contactRepository.GetAsync(id);

            if (contact.CompanyId > 0)
            {
                var company = await _companyRepository.GetAsync(contact.CompanyId);
                if (company.Id == contact.CompanyId && company.Name.Equals(contact.Name))
                    await _companyRepository.DeleteAsync(company.Id);
            }
            if (contact.Id > 0)
            {
                await _contactBookRepository.DeleteAsync(contact.ContactBookId);
                await _contactRepository.DeleteAsync(id);
            }
            else
                throw new Exception("Not found");
        }

        public async Task<INodeContact> SearchContact(int? id, int? contactBookId, int? companyId, string name, string phone, string email, string address, string companyName, int currentPage, int pageSize)
        {
            var listContact = await _contactRepository.SearchContact(id.GetValueOrDefault(), contactBookId.GetValueOrDefault(), companyId.GetValueOrDefault(), name, phone, email, address, companyName);
            var contacts = listContact.OrderBy(x => x.Id).Skip((currentPage - 1) * pageSize).Take(pageSize);

            var nodeContact = new NodeContact()
            {
                Contacts = contacts,
                TotalRows = contacts.Count<IContact>()
            };
            return nodeContact;
        }

        public async Task<List<string>> SaveContactFileAsync(UploadFile file)
        {
            var contactInsertResult = new List<string>();
            var messagem = string.Empty;

            var path = await ReadeFileAsync(file);
            var data = File.ReadLines(path);
            foreach (var line in data)
            {
                ContactModel contact = line;
                try
                {
                    var resultSave = await SaveAsync(contact);
                    if (resultSave is not null)
                        messagem = $"Saved as success: {line}";
                }
                catch
                {
                    messagem = $"Error insert contact: {line}";
                }
                contactInsertResult.Add(messagem);
            }
            return contactInsertResult;
        }

        private async Task<string> ReadeFileAsync(UploadFile file)
        {
            if (file.File.Length > 0)
            {
                try
                {
                    if (!Directory.Exists(_webHostEnvironment.WebRootPath + "\\File\\"))
                    {
                        Directory.CreateDirectory(_webHostEnvironment.WebRootPath + "\\File\\");
                    }
                    using (FileStream fileStream = File.Create(_webHostEnvironment.WebRootPath + "\\File\\" + file.File.FileName))
                    {
                        file.File.CopyTo(fileStream);
                        fileStream.Flush();
                    }
                }
                catch (System.Exception)
                {
                    throw;
                }
            }
            return _webHostEnvironment.WebRootPath + "\\File\\" + file.File.FileName;
        }

        public async Task<string> GenerateFileCSV()
        {
            var fileName = @"\Modelo.csv";
            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var listContact = new List<ContactModel>();

            var contactAll = await GetAllAsync();

            var modelCsv = new FileStream(path + fileName, FileMode.Create, FileAccess.ReadWrite);
            using (var Csv = new StreamWriter(modelCsv, encoding: System.Text.Encoding.UTF8))
            {
                #region .: Modelo CSV :.
                Csv.WriteLine("Id;ContactBookId;CompanyId;Name;Phone;Email;Address");
                #endregion
                foreach (var contactItem in contactAll.Contacts)
                {
                    listContact.Add(new ContactModel(contactItem));
                }

                listContact.ForEach(x =>
                {
                    var str = new StringBuilder();
                    str.AppendLine(String.Join(";", new string[]
                    {
                        x.Id.ToString(),
                        x.ContactBookId.ToString(),
                        x.CompanyId.ToString(),
                        x.Name,
                        x.Phone,
                        x.Email,
                        x.Address
                    }));
                    Csv.Write(str);
                });
            }
            return $"File created in: {path}";
        }

        public async Task<string> ModelCsv()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var fileName = @"\Modelo.csv";
            var modelCsv = new FileStream(path + fileName, FileMode.Create, FileAccess.ReadWrite);

            using (var Csv = new StreamWriter(modelCsv, encoding: System.Text.Encoding.UTF8))
            {
                #region .: Modelo CSV :.
                Csv.WriteLine("CompanyId;Name;Phone;Email;Address;IsCompany");
                #endregion
            }

            return path + fileName;
        }
    }
}
