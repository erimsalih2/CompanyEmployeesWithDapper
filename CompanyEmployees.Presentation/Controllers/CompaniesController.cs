﻿using CompanyEmployees.Presentation.ModelBinders;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace CompanyEmployees.Presentation.Controllers;

[Route("api/companies")]
[ApiController]
public class CompaniesController : ControllerBase
{
    private readonly IServiceManager _service;
    public CompaniesController(IServiceManager service) => _service = service;
    [HttpGet]
    public async Task<IActionResult> GetCompanies()
    {
        var companies = await _service.CompanyService.GetAllCompanies();
        return Ok(companies);
    }
    [HttpGet("{id:guid}", Name = "CompanyById")]
    public async Task<IActionResult> GetCompany(Guid id)
    {
        var company = await _service.CompanyService.GetCompany(id);

        return Ok(company);
    }
    [HttpGet("withEmployees")]
    public async Task<IActionResult> GetCompaniesWithEmployees()

    {
        var companies = await _service.CompanyService.GetCompaniesWithEmployees();
        return Ok(companies);
    }    [HttpPost]
    public async Task<IActionResult> CreateCompany([FromBody] CompanyForCreationDto
    company)
    {
        if (company is null)
            return BadRequest("CompanyForCreationDto object is null");
        var createdCompany = await _service.CompanyService.CreateCompany(company);
        return CreatedAtRoute("CompanyById",
        new { id = createdCompany.CompanyId }, createdCompany);
    }    [HttpGet("collection/({ids})", Name = "CompanyCollection")]
    public async Task<IActionResult> GetCompanyCollection([ModelBinder(BinderType =
        typeof(ArrayModelBinder))]IEnumerable<Guid> ids)
    {
        var companies = await _service.CompanyService.GetByIds(ids);

        
        return Ok(companies);
    }    [HttpPost("collection")]
    public async Task<IActionResult> CreateCompanyCollection([FromBody]
        IEnumerable<CompanyForCreationDto> companyCollection)
    {
        var result = await _service.CompanyService
        .CreateCompanyCollection(companyCollection);
        return CreatedAtRoute("CompanyCollection",
        new { result.ids }, result.companies);

    }    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteEmployeeForCompany(Guid companyId, Guid id)
    {
        await _service.EmployeeService.DeleteEmployeeForCompany(companyId, id);
        return NoContent();
    }    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteCompany(Guid id)
    {
        await _service.CompanyService.DeleteCompany(id);
        return NoContent();
    }    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateCompany(Guid id, [FromBody] CompanyForUpdateDto
    company)
    {
        if (company is null)
            return BadRequest("CompanyForUpdateDto object is null");
        await _service.CompanyService.UpdateCompany(id, company);
        return NoContent();
    }    [HttpGet("byemployeeid/{id:guid}")]
    public async Task<IActionResult> GetCompanyByEmployeeId(Guid id)


    {
        var company = await _service.CompanyService.GetCompanyByEmployeeId(id);
        return Ok(company);
    }
}
