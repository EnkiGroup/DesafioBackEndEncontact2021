using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using TesteBackendEnContact.Controllers.Models;
using TesteBackendEnContact.Core.Interface.ContactBook.Company;
using TesteBackendEnContact.Repository.Interface;

namespace TesteBackendEnContact.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ILogger<CompanyController> _logger;

        public CompanyController(ILogger<CompanyController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Edit Company 
        /// </summary>
        [HttpPut]
        public async Task<ActionResult<ICompany>> Put(EditCompanyRequest company, [FromServices] ICompanyRepository companyRepository)
        {
            return Ok(await companyRepository.EditAsync(company.ToCompany()));
        }

        /// <summary>Save Company</summary>
        /// <param name="ContactBookId"></param>
        /// <param name="Name"></param>
        [HttpPost]
        public async Task<ActionResult<ICompany>> Post(SaveCompanyRequest company, [FromServices] ICompanyRepository companyRepository)
        {
            return Ok(await companyRepository.SaveAsync(company.ToCompany()));
        }

        [HttpDelete]
        public async Task Delete(int id, [FromServices] ICompanyRepository companyRepository)
        {
            await companyRepository.DeleteAsync(id);
        }

        /// <summary>
        /// Get All Company 
        /// </summary>
        [HttpGet]
        public async Task<IEnumerable<ICompany>> Get([FromServices] ICompanyRepository companyRepository)
        {
            return await companyRepository.GetAllAsync();
        }

        /// <summary>
        /// Get Company By Id
        /// </summary>
        /// <param name="Id"></param>
        [HttpGet("{id}")]
        public async Task<ICompany> Get(int id, [FromServices] ICompanyRepository companyRepository)
        {
            return await companyRepository.GetAsync(id);
        }
    }
}
