using TesteBackendEnContact.Core.Interface.ContactBook.Contact;

namespace TesteBackendEnContact.Services.Models
{
    public class ContactModel : IContact
    {
        public int Id { get; set; }
        public int ContactBookId { get; set; }
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public bool IsCompany { get; set; }

        public ContactModel() { }

        public ContactModel(IContact company)
        {
            Id = company.Id;
            ContactBookId = company.ContactBookId;
            Name = company.Name;
            Phone = company.Phone;
            Email = company.Email;
            Address = company.Address;
            IsCompany = company.IsCompany;
        }
    }
}
