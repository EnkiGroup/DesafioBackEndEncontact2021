namespace TesteBackendEnContact.Models.Interface
{
    public interface IContact
    {
        public int Id { get; }
        public int ContactBookId { get; }
        public int CompanyId { get; }
        public string Name { get; }
        public string Phone { get; }
        public string Email { get; }
        public string Address { get; }
    }
}
