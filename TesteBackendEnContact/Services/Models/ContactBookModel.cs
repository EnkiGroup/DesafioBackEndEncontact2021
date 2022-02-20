using TesteBackendEnContact.Core.Interface.ContactBook;

namespace TesteBackendEnContact.Services.Models
{
    public class ContactBookModel : IContactBook
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ContactBookModel() { }

        public ContactBookModel(IContactBook contactBook)
        {
            Id = contactBook.Id;
            Name = contactBook.Name;
        }
    }
}
