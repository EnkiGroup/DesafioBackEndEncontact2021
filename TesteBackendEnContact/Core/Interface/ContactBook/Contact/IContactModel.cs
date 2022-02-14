namespace TesteBackendEnContact.Core.Interface.ContactBook.Contact
{
    public interface IContactModel
    {
        string Name { get; }
        string Phone { get; }
        string Email { get; }
        string Address { get; }
        string NameCompany { get; }
    }
}
