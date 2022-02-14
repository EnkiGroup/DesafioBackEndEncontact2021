using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using TesteBackendEnContact.Core.Interface.ContactBook.Contact;

namespace TesteBackendEnContact.Services.Interface
{
    public interface IContactService
    {
        Task<IEnumerable<IContactModel>> UpdateFileContact(HttpRequest httpRequest);
        Task<string> ModelCsv();
    }
}
