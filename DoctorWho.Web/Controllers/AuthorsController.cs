using AutoMapper;
using DoctorWho.Db.Entities;
using DoctorWho.Db.Models;
using DoctorWho.Db.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DoctorWho.Web.Controllers
{
    [Route("api/authors")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public AuthorsController(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPut("{authorId}")]
        public async Task<ActionResult> UpdateAuthor(int authorId, AuthorForUpdateDto author)
        {
            var authorEntity = await _authorRepository.GetAuthorAsync(authorId);

            if (authorEntity == default)
                return NotFound();

            _mapper.Map(author, authorEntity);

            await _authorRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
