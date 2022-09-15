using System.Text.Json;
using AutoMapper;
using DoctorWho.Db.Entities;
using DoctorWho.Db.Models;
using DoctorWho.Db.Repositories;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace DoctorWho.Web.Controllers
{
    [Route("api/doctors")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private const int MaxDoctorsPageSize = 10;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<Doctor> _validator;

        public DoctorsController(IDoctorRepository doctorRepository, IMapper mapper,
            IValidator<Doctor> validator)
        {
            _doctorRepository = doctorRepository ?? throw new ArgumentNullException(nameof(doctorRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DoctorDto>>> GetDoctors(int pageNumber = 1, int pageSize = 5)
        {
            if (pageSize > MaxDoctorsPageSize) pageSize = MaxDoctorsPageSize;

            var (doctors, paginationMetadata) = await _doctorRepository.GetDoctorsAsync(pageNumber, pageSize);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

            return Ok(_mapper.Map<IEnumerable<DoctorDto>>(doctors));
        }

        [HttpGet("{doctorId}", Name = "GetDoctor")]
        public async Task<ActionResult<DoctorDto>> GetDoctor(int doctorId)
        {
            var doctor = await _doctorRepository.GetDoctorAsync(doctorId);

            if (doctor == default)
                return NotFound();

            return Ok(_mapper.Map<DoctorDto>(doctor));
        }

        [HttpPost]
        public async Task<ActionResult<DoctorDto>> UpsertDoctor(DoctorForUpsertionDto doctor)
        {
            var doctorToAdd = _mapper.Map<Doctor>(doctor);

            await _validator.ValidateAndThrowAsync(doctorToAdd);
            
            var doctorId = await _doctorRepository.UpsertDoctor(doctorToAdd);

            var createdDoctor = _mapper.Map<DoctorDto>(doctorToAdd);

            return CreatedAtRoute("GetDoctor", new {createdDoctor.DoctorId}, new
            {
                doctorId,
                createdDoctor.DoctorNumber,
                createdDoctor.DoctorName,
                createdDoctor.BirthDate,
                createdDoctor.FirstEpisodeDate,
                createdDoctor.LastEpisodeDate
            });
        }
        
    }
}
