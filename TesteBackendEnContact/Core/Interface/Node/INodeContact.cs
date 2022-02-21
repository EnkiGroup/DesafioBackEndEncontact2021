using System.Collections.Generic;
using TesteBackendEnContact.Core.Interface.ContactBook.Contact;

namespace TesteBackendEnContact.Core.Interface.Node
{
    public interface INodeContact
    {
        IEnumerable<IContact> Contacts { get; }
        int TotalRows { get; }
    }
}
