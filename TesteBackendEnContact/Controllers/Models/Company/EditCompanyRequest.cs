using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TesteBackendEnContact.Core.Domain.Company;
using TesteBackendEnContact.Core.Interface.ContactBook.Company;

namespace TesteBackendEnContact.Controllers.Models
{
    public class EditCompanyRequest
    {
        public int Id { get; set; }
        public int ContactBookId { get; set; }
        public string Name { get; set; }

        public ICompany ToCompany() => new Core.Domain.Company.Company(Id, ContactBookId, Name);
    }
}
