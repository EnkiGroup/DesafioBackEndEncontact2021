using TesteBackendEnContact.Models.Interface;

namespace TesteBackendEnContact.Models
{
    public class ContactBook : IContactBook
    {
        public int Id { get;  set; }
        public string Name { get;  set; }

        public ContactBook(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
