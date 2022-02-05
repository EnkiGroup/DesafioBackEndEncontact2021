using System.ComponentModel.DataAnnotations;
using TesteBackendEnContact.Models;
using TesteBackendEnContact.Models.Interface;

namespace TesteBackendEnContact.DAO
{
    public class CompanyDao : ICompany
    {
        public int Id { get; set; }
        [Required]
        public int ContactBookId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public ICompany ToCompany() => new Company(Id, ContactBookId, Name);
    }
}
