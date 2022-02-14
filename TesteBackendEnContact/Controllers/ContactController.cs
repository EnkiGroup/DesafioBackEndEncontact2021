using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using TesteBackendEnContact.Controllers.Models.Contact;
using TesteBackendEnContact.Core.Domain.ContactBook;
using TesteBackendEnContact.Core.Interface.ContactBook;
using TesteBackendEnContact.Core.Interface.ContactBook.Contact;
using TesteBackendEnContact.Repository.Interface;
using TesteBackendEnContact.Services.Interface;

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

        [HttpPost("UpdateArquivoContact")]
        public async Task<IActionResult> UpdateArquivo([FromServices] IContactService contactService)
        {
            var httpRequest = HttpContext.Request;
            return Ok(await contactService.UpdateFileContact(httpRequest));

        }
        /// <summary>
        /// Edit Contact
        /// </summary>
        /// <param name="contact"></param>
        /// <param name="contactRepository"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<IContact>> Put(EditContactRequest contact, [FromServices] IContactRepository contactRepository)
        {
            return Ok(await contactRepository.EditAsync(contact.ToContact()));
        }

        /// <summary>
        /// Save Contact
        /// </summary>
        /// <param name="contact"></param>
        /// <param name="contactRepository"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<IContact>> Post(SaveContactRequest contact, [FromServices] IContactRepository contactRepository)
        {
            return Ok(await contactRepository.SaveAsync(contact.ToContact()));
        }

        /// <summary>
        /// Delete By Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="contactRepository"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task Delete(int id, [FromServices] IContactRepository contactRepository)
        {
            await contactRepository.DeleteAsync(id);
        }

        /// <summary>
        /// Get All
        /// </summary>
        /// <param name="contactRepository"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<IContact>> Get([FromServices] IContactRepository contactRepository)
        {
            return await contactRepository.GetAllAsync();
        }

        /// <summary>
        /// Get By Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="companyRepository"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IContact> Get(int id, [FromServices] IContactRepository companyRepository)
        {
            return await companyRepository.GetAsync(id);
        }

        /// <summary>
        /// Get modelo CSV
        /// </summary>
        /// <param name="contactService"></param>
        /// <returns></returns>
        [HttpGet("Modelo")]
        public async Task<ActionResult<string>> GetModeloCSV([FromServices] IContactService contactService)
        {
            return Ok(await contactService.ModelCsv());
        }


    }
}
