using TesteBackendEnContact.Core.Interface.ContactBook.Contact;

namespace TesteBackendEnContact.Controllers.Models.Contact
{
    public class EditContactRequest
    {
        public int Id { get; set; }
        public int ContactBookId { get; set; }
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public IContact ToContact() => new Core.Domain.Contact.Contact(Id, ContactBookId, CompanyId, Name, Phone, Email, Address);
    }
}
