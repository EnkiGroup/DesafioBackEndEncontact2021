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

        /// <summary>
        /// Update File
        /// </summary>
        /// <param name="file"></param>
        /// <param name="contactService"></param>
        /// <returns></returns>
        [HttpPost("UpdateArquivoContact")]
        public async Task<IActionResult> UpdateFile([FromForm] UploadFile file, [FromServices] IContactService contactService)
        {
            if (file.File.Length > 0)
                await contactService.SaveContactFileAsync(file);
            else
                return BadRequest("File is null!");

            return Ok();
        }

        /// <summary>
        /// Generate File CSV 
        /// </summary>
        /// <param name="contactService"></param>
        /// <returns></returns>
        [HttpGet("GenerateFile")]
        public async Task<ActionResult<string>> GenerateFile([FromServices] IContactService contactService)
        {
            return Ok(await contactService.GenerateFileCSV());
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
        /// <param name="contactService"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<INodeContact> Get([FromServices] IContactService contactService)
        {
            return await contactService.GetAllAsync();
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

        /// <summary>
        /// Search Contact
        /// </summary>
        /// <param name="id"></param>
        /// <param name="contactBookId"></param>
        /// <param name="companyId"></param>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        /// <param name="email"></param>
        /// <param name="address"></param>
        /// <param name="nameCompany"></param>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="contactService"></param>
        /// <returns></returns>
        [HttpGet("{currentPage}/{pageSize}")]
        public async Task<INodeContact> SearchContact(int? id, int? contactBookId, int? companyId, string name, string phone, string email, string address, string nameCompany, int currentPage, int pageSize, [FromServices] IContactService contactService)
        {
            return await contactService.SearchContact(id.GetValueOrDefault(), contactBookId.GetValueOrDefault(), companyId.GetValueOrDefault(), name, phone, email, address, nameCompany, currentPage, pageSize);
        }
    }
}
