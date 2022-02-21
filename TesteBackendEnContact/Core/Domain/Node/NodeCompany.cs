using System.Collections.Generic;
using TesteBackendEnContact.Core.Interface.ContactBook.Company;
using TesteBackendEnContact.Core.Interface.Node;

namespace TesteBackendEnContact.Core.Domain.Node
{
    public class NodeCompany : INodeCompany
    {
        public IEnumerable<ICompany> Companys { get; set; }
        public int TotalRows { get; set; }
    }
}
