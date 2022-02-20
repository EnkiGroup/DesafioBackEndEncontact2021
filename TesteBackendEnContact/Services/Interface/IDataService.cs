using System.Collections.Generic;
using System.Threading.Tasks;
using TesteBackendEnContact.Controllers.Models.File;
using TesteBackendEnContact.Core.Interface.Data;

namespace TesteBackendEnContact.Services.Interface
{
    public interface IDataService
    {
        Task<bool> UploadFileContact(UploadFile file);
        Task<string> ModelCsv();
    }
}
