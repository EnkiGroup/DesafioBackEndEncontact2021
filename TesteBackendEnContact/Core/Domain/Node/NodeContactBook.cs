using System.Collections.Generic;
using TesteBackendEnContact.Core.Interface.ContactBook;
using TesteBackendEnContact.Core.Interface.Node;

namespace TesteBackendEnContact.Core.Domain.Node
{
    public class NodeContactBook : INodeContactBook
    {
        public IEnumerable<IContactBook> ContactBook { get; set; }
        public int TotalRows { get; set; }
    }
}
