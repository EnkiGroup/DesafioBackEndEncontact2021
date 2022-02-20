using TesteBackendEnContact.Core.Interface.Data;

namespace TesteBackendEnContact.Core.Domain.Data
{
    public class DataModel : IDataModel
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string NameCompany { get; set; }

        public DataModel() { }

        public DataModel(string name, string phone, string email, string address, string nameCompany)
        {
            Name = name;
            Phone = phone;
            Email = email;
            Address = address;
            NameCompany = nameCompany;
        }
    }       
}
