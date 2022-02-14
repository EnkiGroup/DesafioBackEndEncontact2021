using TesteBackendEnContact.Core.Interface.ContactBook.Contact;

namespace TesteBackendEnContact.Core.Domain.Contact
{
    public class ContactModel : IContactModel
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string NameCompany { get; set; }

        public ContactModel(string nome, string phone, string email, string address, string nameCompany)
        {
            Name = nome;
            Phone = phone;
            Email = email;
            Address = address;
            NameCompany = nameCompany;
        }
    }
}
