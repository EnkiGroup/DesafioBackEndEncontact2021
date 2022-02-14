using TesteBackendEnContact.Core.Interface.ContactBook.Company;

namespace TesteBackendEnContact.Core.Domain.Company
{
    public class Company : ICompany
    {
        public int Id { get; private set; }
        public int ContactBookId { get; private set; }
        public string Name { get; private set; }

        public Company(int id, int contactBookId, string name)
        {
            Id = id;
            ContactBookId = contactBookId;
            Name = name;
        }
    }
}
