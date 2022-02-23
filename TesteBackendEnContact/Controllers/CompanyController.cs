using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using TesteBackendEnContact.Controllers.Models;
using TesteBackendEnContact.Core.Interface.ContactBook.Company;
using TesteBackendEnContact.Core.Interface.Node;
using TesteBackendEnContact.Repository.Interface;
using TesteBackendEnContact.Services.Interface;

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
        /// Edit 
        /// </summary>
        /// <param name="company"></param>
        /// <param name="companyService"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<ICompany>> Put(EditCompanyRequest company, [FromServices] ICompanyService companyService)
        {
            return Ok(await companyService.EditAsync(company.ToCompany()));
        }

        /// <summary>
        /// Save 
        /// </summary>
        /// <param name="company"></param>
        /// <param name="companyService"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ICompany>> Post(SaveCompanyRequest company, [FromServices] ICompanyService  companyService)
        {
            return Ok(await companyService.SaveAsync(company.ToCompany()));
        }

        [HttpDelete]
        public async Task Delete(int id, [FromServices] ICompanyService companyService)
        {
            await companyService.DeleteAsync(id);
        }

        /// <summary>
        /// Get All 
        /// </summary>
        /// <param name="companyService"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<INodeCompany> Get([FromServices] ICompanyService companyService)
        {
            return await companyService.GetAllAsync();
        }

        /// <summary>
        /// Get By Id
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("{id}")]
        public async Task<ICompany> Get(int id, [FromServices] ICompanyService companyService)
        {
            return await companyService.GetAsync(id);
        }
    }
}
