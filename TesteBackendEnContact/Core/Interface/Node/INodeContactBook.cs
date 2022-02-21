using System.Collections.Generic;
using TesteBackendEnContact.Core.Interface.ContactBook;

namespace TesteBackendEnContact.Core.Interface.Node
{
    public interface INodeContactBook
    {
        IEnumerable<IContactBook> ContactBook { get; }
        int TotalRows { get; }
    }
}
