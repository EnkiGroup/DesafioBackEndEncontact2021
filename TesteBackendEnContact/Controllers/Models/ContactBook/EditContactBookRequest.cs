using TesteBackendEnContact.Core.Interface.ContactBook;

namespace TesteBackendEnContact.Controllers.Models.ContactBook
{
    public class EditContactBookRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }


        public IContactBook ToContactBook() => new Core.Domain.ContactBook.ContactBook(Id, Name);
    }
}
