using System.Text.Json.Serialization;
using TesteBackendEnContact.Core.Interface.ContactBook;
using TesteBackendEnContact.Core.Domain.ContactBook;

namespace TesteBackendEnContact.Controllers.Models.ContactBook
{
    public class SaveContactBookRequest
    {
        [JsonIgnore] public int Id { get; set; }
        public string Name { get; set; }

   
        public IContactBook ToContactBook() => new Core.Domain.ContactBook.ContactBook(Id, Name);
    }
}
