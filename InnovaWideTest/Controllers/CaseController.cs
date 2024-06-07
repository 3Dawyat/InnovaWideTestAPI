using AutoMapper;
using InnovaWideTest.Application.Common.Interfaces.Repositories;
using InnovaWideTest.Domain.DTOs;
using InnovaWideTest.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace InnovaWideTest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CaseController : ControllerBase
    {
        private readonly ICaseRepository _caseRepository;
        private readonly IMapper _mapper;

        public CaseController(ICaseRepository caseRepository, IMapper mapper)
        {
            _caseRepository = caseRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CaseDto>>> GetCases([FromHeader, Required] string Tenant)
        {
            var cases = await _caseRepository.GetAllAsync();

            var model = _mapper.Map<IEnumerable<CaseDto>>(cases);
            return Ok(model);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CaseDto>> GetCase([FromHeader, Required] string Tenant, int id)
        {
            var entity = await _caseRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            var model = _mapper.Map<CaseDto>(entity);
            return Ok(model);
        }

        [HttpPost]
        public async Task<ActionResult> CreateCase([FromHeader, Required] string Tenant, [FromBody] CaseDto caseDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = _mapper.Map<Case>(caseDto);
            await _caseRepository.AddAsync(entity);
            await _caseRepository.SaveChangesAsync();
            return Ok(caseDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCase([FromHeader, Required] string Tenant, int id, [FromBody] CaseDto caseDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var entity = await _caseRepository.GetByIdAsync(id);
            if (entity == null)
                return NotFound();

            entity.FinalVerdict = caseDto.FinalVerdict;
            entity.Year = caseDto.Year;
            entity.Number = caseDto.Number;
            entity.FinalVerdict = caseDto.FinalVerdict;
            entity.LitigationDegree = caseDto.LitigationDegree;
            await _caseRepository.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCase([FromHeader, Required] string Tenant, int id)
        {
            await _caseRepository.DeleteAsync(id);
            await _caseRepository.SaveChangesAsync();
            return Ok();
        }
    }
}
