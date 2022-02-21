using System.Collections.Generic;
using TesteBackendEnContact.Core.Interface.ContactBook.Company;

namespace TesteBackendEnContact.Core.Interface.Node
{
    public interface INodeCompany
    {
        IEnumerable<ICompany> Companys { get; }
        int TotalRows { get; }
    }
}
