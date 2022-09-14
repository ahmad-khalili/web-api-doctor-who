using System.Text.Json;
using AutoMapper;
using DoctorWho.Db.Models;
using DoctorWho.Db.Repositories;
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

        public EpisodesController(IEpisodeRepository episodeRepository, IMapper mapper)
        {
            _episodeRepository = episodeRepository ?? throw new ArgumentNullException(nameof(episodeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
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
    }
}
