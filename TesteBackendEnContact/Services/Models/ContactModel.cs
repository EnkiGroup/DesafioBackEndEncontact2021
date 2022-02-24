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

        public ContactModel(IContact contact)
        {
            Id = contact.Id;
            ContactBookId = contact.ContactBookId;
            CompanyId = contact.CompanyId;
            Name = contact.Name;
            Phone = contact.Phone;
            Email = contact.Email;
            Address = contact.Address;
            IsCompany = contact.IsCompany;
        }

        public static implicit operator string(ContactModel contact)
            => $"{contact.Id};{contact.ContactBookId};{contact.CompanyId};{contact.Name};{contact.Phone};{contact.Email};{contact.Address};{contact.IsCompany}";

        public static implicit operator ContactModel(string line)
        {
            var data = line.Split(";");
            return new ContactModel
            {
                //Id = int.Parse(data[0]),
                //ContactBookId=int.Parse(data[1]),
                CompanyId = int.Parse(data[0]),
                Name = data[1],
                Phone = data[2],
                Email = data[3],
                Address = data[4],
                IsCompany = bool.Parse(data[5])
            };
        }
    }
}
