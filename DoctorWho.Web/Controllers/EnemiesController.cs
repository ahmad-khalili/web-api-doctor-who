using System.Text.Json;
using AutoMapper;
using DoctorWho.Db.Entities;
using DoctorWho.Db.Models;
using DoctorWho.Db.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DoctorWho.Web.Controllers
{
    [Route("api/episodes/{episodeId}/enemies")]
    [ApiController]
    public class EnemiesController : ControllerBase
    {
        private readonly IEnemyRepository _enemyRepository;
        private readonly IMapper _mapper;

        public EnemiesController(IEnemyRepository enemyRepository, IMapper mapper)
        {
            _enemyRepository = enemyRepository ?? throw new ArgumentNullException(nameof(enemyRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost]
        public async Task<ActionResult<EnemyDto>> AddEnemyToEpisode(int episodeId, EnemyForCreationDto enemy)
        {
            if (!await _enemyRepository.EpisodeExistsAsync(episodeId))
                return NotFound();
            
            var enemyToAdd = _mapper.Map<Enemy>(enemy);

            await _enemyRepository.AddEnemyToEpisodeAsync(episodeId, enemyToAdd);

            await _enemyRepository.SaveChangesAsync();

            var createdEnemy = _mapper.Map<EnemyDto>(enemyToAdd);

            return Ok(createdEnemy);
        }
    }
}
