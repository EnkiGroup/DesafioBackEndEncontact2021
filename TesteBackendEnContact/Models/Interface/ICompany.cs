namespace TesteBackendEnContact.Models.Interface
{
    public interface ICompany
    {
        int Id { get; }
        int ContactBookId { get; }
        string Name { get; }
    }
}
