using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TesteBackendEnContact.Controllers.Models.Contact;
using TesteBackendEnContact.Controllers.Models.File;
using TesteBackendEnContact.Core.Interface.ContactBook.Contact;
using TesteBackendEnContact.Core.Interface.Node;
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
        public async Task<IActionResult> UpdateArquivo([FromForm] UploadFile file,[FromServices] IDataService contactService)
        {
            if (file.File.Length > 0)
                await contactService.UploadFileContact(file);
            else
                throw new Exception("");
            return null;

        }
    
        [HttpGet("Modelo")]
        public async Task<ActionResult<string>> GetModeloCSV([FromServices] IDataService contactService)
        {
            return Ok(await contactService.ModelCsv());
        }

        /// <summary>
        /// Edit
        /// </summary>
        /// <param name="contact"></param>
        /// <param name="contactService"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<IContact>> Put(EditContactRequest contact, [FromServices] IContactService contactService)
        {
            return Ok(await contactService.EditAsync(contact.ToContact()));
        }

        /// <summary>
        /// Save
        /// </summary>
        /// <param name="contact"></param>
        /// <param name="contactService"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<IContact>> Post(SaveContactRequest contact, [FromServices] IContactService contactService)
        {
            return Ok(await contactService.SaveAsync(contact.ToContact()));
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <param name="contactService"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task Delete(int id, [FromServices] IContactService contactService)
        {
            await contactService.DeleteAsync(id);
        }

        /// <summary>
        /// Get All
        /// </summary>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="contactService"></param>
        /// <returns></returns>
        [HttpGet("{currentPage}/{pageSize}")]
        public async Task<INodeContact> Get(int currentPage, int pageSize, [FromServices] IContactService contactService)
        {
            return await contactService.GetAllAsync(currentPage, pageSize);
        }

        /// <summary>
        /// Get By Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="contactService"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IContact> Get(int id, [FromServices] IContactService contactService)
        {
            return await contactService.GetAsync(id);
        }
    }
}
