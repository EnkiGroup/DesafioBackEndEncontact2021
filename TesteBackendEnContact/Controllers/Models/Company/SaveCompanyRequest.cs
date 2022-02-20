using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TesteBackendEnContact.Core.Interface.ContactBook.Company;

namespace TesteBackendEnContact.Controllers.Models
{
    public class SaveCompanyRequest
    {
        [JsonIgnore] public int Id { get; set; }
        [JsonIgnore] public int ContactBookId { get; set; }
        public string Name { get; set; }

        public ICompany ToCompany() => new Core.Domain.Company.Company(Id, ContactBookId, Name);
    }
}
