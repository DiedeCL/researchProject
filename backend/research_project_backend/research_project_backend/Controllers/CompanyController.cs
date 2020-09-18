using System.Linq;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using research_project_backend.Models;
using research_project_backend.Services;

namespace research_project_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CompanyController : ControllerBase
    {
        private readonly ICompany _company;

        public CompanyController(ICompany company)
        {
            _company = company;
        }

        [HttpPost("GetCompanyById")]
        public IActionResult GetCompanyById([FromBody] CompanyInfoModel model)
        {
            var company = _company.GetCompanyById(model.CompanyId);

            if (company == null) return BadRequest();

            company.AddressCompany = _address.GetAddressByCompanyId(company.CompanyId);
            company.ContactCompany = _contactCompany.GetContactCompanyByCompanyId(company.CompanyId);
            company.Promoter = _companyPromotor.GetCompanyPromoterByCompanyId(company.CompanyId);

            return Ok(company);
        }

        [HttpGet("GetAllCompanies")]
        public IActionResult GetAllCompanies()
        {
            var companies = _company.GetAllCompanies();

            if (companies == null) return BadRequest();

            foreach (var company in companies)
            {
                company.AddressCompany = _address.GetAddressByCompanyId(company.CompanyId);
                company.ContactCompany = _contactCompany.GetContactCompanyByCompanyId(company.CompanyId);
                company.Promoter = _companyPromotor.GetCompanyPromoterByCompanyId(company.CompanyId);
            }
            return Ok(companies);
        }
    }
}
