using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TesteBackendEnContact.Models;
using TesteBackendEnContact.Models.Interface;
using TesteBackendEnContact.Repository.Interface;

namespace TesteBackendEnContact.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly ILogger<ContactController> _logger;

        public ContactController(ILogger<ContactController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<List<IContact>> Upload(IFormFile file, [FromServices] IContactRepository contactRepository, [FromServices] IContactBookRepository contactBookRepository)
        {

            var result = new StringBuilder();

            using (var reader = new StreamReader(file.OpenReadStream())) {

                while (reader.Peek() >= 0)
                    result.AppendLine(await reader.ReadLineAsync());

            }

            var contacts = ImportContact.StringToContacts(result, contactBookRepository);

            var icont = new List<IContact>();

            foreach (var c in contacts)
            {

                icont.Add(await contactRepository.SaveAsync(c));

            }

            return icont;

        }

        [HttpGet]
        public async Task<IEnumerable<IContact>> GetPaged(int pageSize, int? page, string searchTerm, [FromServices] IContactRepository contactRepository, [FromServices] ICompanyRepository companyRepository)
        {
            var pageIndex = (page ?? 1) - 1;

            
            int contactBookId = await companyRepository.GetContactBookId(searchTerm);
            
            var contacts = await contactRepository.GetAsync(pageSize, pageIndex, searchTerm, contactBookId);

            return contacts;
        }
        [HttpGet("ContactBook")]
        public async Task<IEnumerable<IContact>> GetContactBook(string searchTerm, [FromServices] IContactRepository contactRepository, [FromServices] IContactBookRepository contactBookRepository, [FromServices] ICompanyRepository companyRepository)
        {

            int contactBookId = await contactBookRepository.GetId(searchTerm);

            int companyId = await companyRepository.GetId(contactBookId);

            var contacts = await contactRepository.GetContactBookContacts(contactBookId, companyId);

            return contacts;
        }


    }
}
