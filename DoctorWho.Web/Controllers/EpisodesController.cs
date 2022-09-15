using System.Text.Json;
using AutoMapper;
using DoctorWho.Db.Entities;
using DoctorWho.Db.Models;
using DoctorWho.Db.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace DoctorWho.Web.Controllers
{
    [Route("api/episodes")]
    [ApiController]
    public class EpisodesController : ControllerBase
    {
        private const int MaxEpisodesPageSize = 5;
        private readonly IEpisodeRepository _episodeRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<Episode> _validator;

        public EpisodesController(IEpisodeRepository episodeRepository, IMapper mapper, IValidator<Episode> validator)
        {
            _episodeRepository = episodeRepository ?? throw new ArgumentNullException(nameof(episodeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EpisodeDto>>> GetEpisodes
            (int pageNumber = 1, int pageSize = 2)
        {
            if (pageSize > MaxEpisodesPageSize) pageSize = MaxEpisodesPageSize;

            var (episodes, paginationMetadata) = await _episodeRepository
                .GetEpisodesAsync(pageNumber, pageSize);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

            return Ok(_mapper.Map<IEnumerable<EpisodeDto>>(episodes));
        }

        [HttpGet("{episodeId}", Name = "GetEpisode")]
        public async Task<ActionResult<EpisodeDto>> GetEpisode(int episodeId)
        {
            var episode = await _episodeRepository.GetEpisodeAsync(episodeId);

            if (episode == default)
                return NotFound();

            return Ok(_mapper.Map<EpisodeDto>(episode));
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateEpisode(EpisodeForCreationDto episode)
        {
            if (!await _episodeRepository.AuthorExistsAsync(episode.AuthorId) ||
                !await _episodeRepository.DoctorExistsAsync(episode.DoctorId))
            {
                return BadRequest("DoctorId or AuthorId doesn't exist");
            }

            var episodeToAdd = _mapper.Map<Episode>(episode);

            await _validator.ValidateAndThrowAsync(episodeToAdd);

            await _episodeRepository.AddEpisodeAsync(episodeToAdd);

            await _episodeRepository.SaveChangesAsync();

            var createdEpisode = _mapper.Map<EpisodeDto>(episodeToAdd);

            return CreatedAtRoute("GetEpisode", new { createdEpisode.EpisodeId }, new
            {
                createdEpisode.EpisodeId
            });
        }
    }
}
