using System.Collections.Generic;
using TesteBackendEnContact.Core.Interface.ContactBook.Contact;
using TesteBackendEnContact.Core.Interface.Node;

namespace TesteBackendEnContact.Services.Models.Node
{
    public class NodeContact : INodeContact
    {
        public IEnumerable<IContact> Contacts { get; set; }
        public int TotalRows { get; set; }
    }
}
