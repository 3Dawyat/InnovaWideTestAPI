using AutoMapper;
using AutoMapper.QueryableExtensions;
using InnovaWideTest.Application.Common.Interfaces.Repositories;
using InnovaWideTest.Domain.DTOs;
using InnovaWideTest.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InnovaWideTest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HearingController : ControllerBase
    {
        private readonly IHearingRepository _hearingRepository;
        private readonly IMapper _mapper;

        public HearingController(IHearingRepository hearingRepository, IMapper mapper)
        {
            _hearingRepository = hearingRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HearingListDto>>> GetHearings()
        {
            var quirable = _hearingRepository.AsQueryable();
            var data = await quirable.ProjectTo<HearingListDto>(_mapper.ConfigurationProvider).ToListAsync();

            var model = _mapper.Map<IEnumerable<HearingListDto>>(data);
            return Ok(model);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HearingDto>> GetHearing(int id)
        {
            var entity = await _hearingRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            var model = _mapper.Map<HearingDto>(entity);
            return Ok(model);
        }

        [HttpPost]
        public async Task<ActionResult> CreateHearing([FromBody] HearingDto hearingDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var entity = _mapper.Map<Hearing>(hearingDto);
            await _hearingRepository.AddAsync(entity);
            await _hearingRepository.SaveChangesAsync();
            return Ok(hearingDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateHearing(int id, [FromBody] HearingDto hearingDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var entity = await _hearingRepository.GetByIdAsync(id);
            if (entity == null)
                return NotFound();

            entity.Date = hearingDto.Date;
            entity.Decision = hearingDto.Decision;
            entity.CaseId = hearingDto.CaseId;
            entity.LawyerId = hearingDto.LawyerId;
            await _hearingRepository.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteHearing(int id)
        {
            await _hearingRepository.DeleteAsync(id);
            await _hearingRepository.SaveChangesAsync();
            return Ok();
        }
    }
}
