using System.ComponentModel.DataAnnotations.Schema;
using TesteBackendEnContact.Core.Domain.Company;
using TesteBackendEnContact.Core.Interface.ContactBook.Company;

namespace TesteBackendEnContact.Services.Models
{
    public class CompanyModel : ICompany
    {
        public int Id { get; set; }
        public int ContactBookId { get; set; }
        public string Name { get; set; }

        public CompanyModel() { }

        public CompanyModel(ICompany company)
        {
            Id = company.Id;
            ContactBookId = company.ContactBookId;
            Name = company.Name;
        }
    }
}
