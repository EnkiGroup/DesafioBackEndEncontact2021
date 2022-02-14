using System.Text.Json.Serialization;
using TesteBackendEnContact.Core.Interface.ContactBook;

namespace TesteBackendEnContact.Core.Domain.ContactBook
{
    public class ContactBook : IContactBook
    {
        [JsonIgnore]public int Id { get;  set; }
        public string Name { get;  set; }

        public ContactBook(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
