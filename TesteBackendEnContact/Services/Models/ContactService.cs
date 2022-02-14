using TesteBackendEnContact.Core.Domain.Contact;
using TesteBackendEnContact.Core.Interface.ContactBook.Contact;

namespace TesteBackendEnContact.Services.Models
{
    public class ContactService : IContactModel
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string NameCompany { get; set; }

        public ContactService() { }

        public ContactService(IContactModel contactModel)
        {
            Name = contactModel.Name;
            Phone = contactModel.Phone;
            Email = contactModel.Email;
            Address = contactModel.Address;
            NameCompany = contactModel.NameCompany;
        }

        public IContactModel ToContact() => new ContactModel(Name, Phone, Email, Address, NameCompany);
    }
}
