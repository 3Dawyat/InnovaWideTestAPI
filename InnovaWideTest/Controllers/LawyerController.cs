using AutoMapper;
using InnovaWideTest.Application.Common.Interfaces.Repositories;
using InnovaWideTest.Domain.DTOs;
using InnovaWideTest.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace InnovaWideTest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LawyerController : ControllerBase
    {
        private readonly ILawyerRepository _lawyerRepository;
        private readonly IMapper _mapper;

        public LawyerController(ILawyerRepository lawyerRepository, IMapper mapper)
        {
            _lawyerRepository = lawyerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LawyerDto>>> GetLawyers()
        {
            var lawyers = await _lawyerRepository.GetAllAsync();
            var model = _mapper.Map<IEnumerable<LawyerDto>>(lawyers);
            return Ok(model);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LawyerDto>> GetLawyer(int id)
        {
            var entity = await _lawyerRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            var model = _mapper.Map<LawyerDto>(entity);
            return Ok(model);
        }

        [HttpPost]
        public async Task<ActionResult> CreateLawyer([FromBody] LawyerDto lawyerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = _mapper.Map<Lawyer>(lawyerDto);
            await _lawyerRepository.AddAsync(entity);
            await _lawyerRepository.SaveChangesAsync();
            return Ok(lawyerDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateLawyer(int id, [FromBody] LawyerDto lawyerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var entity = await _lawyerRepository.GetByIdAsync(id);
            if (entity == null)
                return NotFound();
            entity.Name = lawyerDto.Name;
            entity.Position = lawyerDto.Position;
            entity.Mobile = lawyerDto.Mobile;
            entity.Address = lawyerDto.Address;
            await _lawyerRepository.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteLawyer(int id)
        {
            await _lawyerRepository.DeleteAsync(id);
            await _lawyerRepository.SaveChangesAsync();
            return Ok();
        }
    }

}
