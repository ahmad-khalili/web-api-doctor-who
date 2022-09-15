using AutoMapper;
using DoctorWho.Db.Entities;
using DoctorWho.Db.Models;
using DoctorWho.Db.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DoctorWho.Web.Controllers
{
    [Route("api/episodes/{episodeId}/companions")]
    [ApiController]
    public class CompanionsController : ControllerBase
    {
        private readonly ICompanionRepository _companionRepository;
        private readonly IMapper _mapper;

        public CompanionsController(ICompanionRepository companionRepository, IMapper mapper)
        {
            _companionRepository = companionRepository ?? throw new ArgumentNullException(nameof(companionRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost]
        public async Task<ActionResult<CompanionDto>> AddCompanionToEpisode(int episodeId, CompanionForCreationDto companion)
        {
            if (!await _companionRepository.EpisodeExistsAsync(episodeId))
                return NotFound();
            
            var companionToAdd = _mapper.Map<Companion>(companion);

            await _companionRepository.AddCompanionToEpisodeAsync(episodeId, companionToAdd);

            await _companionRepository.SaveChangesAsync();

            var createdCompanion = _mapper.Map<CompanionDto>(companionToAdd);

            return Ok(createdCompanion);
        }
    }
}