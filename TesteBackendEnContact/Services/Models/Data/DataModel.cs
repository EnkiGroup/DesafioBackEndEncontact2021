using TesteBackendEnContact.Core.Interface.Data;

namespace TesteBackendEnContact.Services.Data.Models
{
    public class DataModel : IDataModel
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string NameCompany { get; set; }

        public DataModel() { }

        //public ContactModel(IDataModel contactModel)
        //{
        //    Name = contactModel.Name;
        //    Phone = contactModel.Phone;
        //    Email = contactModel.Email;
        //    Address = contactModel.Address;
        //    NameCompany = contactModel.NameCompany;
        //}

        public static implicit operator string(DataModel contact)
            => $"{contact.Name};{contact.Phone};{contact.Email};{contact.Address};{contact.NameCompany}";

        public static implicit operator DataModel(string line)
        {
            var data = line.Split(";");
            return new DataModel
            {
                Name = data[0],
                Phone = data[1],
                Email = data[2],
                Address = data[3],
                NameCompany = data[4]
            };
        }
        public IDataModel ToContactModel() => new Core.Domain.Data.DataModel(Name, Phone, Email, Address, NameCompany);
    }
}
