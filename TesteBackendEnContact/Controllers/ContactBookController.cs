using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using TesteBackendEnContact.Controllers.Models.ContactBook;
using TesteBackendEnContact.Core.Domain.ContactBook;
using TesteBackendEnContact.Core.Interface.ContactBook;
using TesteBackendEnContact.Repository.Interface;
using TesteBackendEnContact.Services.Interface;

namespace TesteBackendEnContact.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactBookController : ControllerBase
    {
        private readonly ILogger<ContactBookController> _logger;

        public ContactBookController(ILogger<ContactBookController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Edit 
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public async Task<IContactBook> Put(EditContactBookRequest contactBook, [FromServices] IContactBookService contactBookService)
        {
            return await contactBookService.EditAsync(contactBook.ToContactBook());
        }

        /// <summary>
        /// Save Contact Book
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IContactBook> Post(SaveContactBookRequest contactBook, [FromServices] IContactBookService contactBookService)
        {
            return await contactBookService.SaveAsync(contactBook.ToContactBook());
        }

        /// <summary>
        /// Delete By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task Delete(int id, [FromServices] IContactBookService contactBookService)
        {
            await contactBookService.DeleteAsync(id);
        }

        /// <summary>
        /// Get All
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<IContactBook>> Get([FromServices] IContactBookService contactBookService)
        {
            return await contactBookService.GetAllAsync();
        }

        /// <summary>
        /// Get Contact Book By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IContactBook> Get(int id, [FromServices] IContactBookService contactBookService)
        {
            return await contactBookService.GetAsync(id);
        }
    }
}
