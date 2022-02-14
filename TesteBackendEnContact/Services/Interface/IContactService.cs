using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace TesteBackendEnContact.Services.Interface
{
    public interface IContactService
    {
        Task<bool> UpdateFileContact(HttpRequest httpRequest);
        Task<string> ModelCsv();
    }
}
