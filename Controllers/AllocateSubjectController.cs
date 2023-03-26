using AutoMapper;
using DotnetAPI.Data;
using DotnetAPI.Dtos;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllocateSubjectController:ControllerBase
    {
        private readonly IAdllocateSubjectRepository _repository;

        public AllocateSubjectController(IAdllocateSubjectRepository allocaterepository)
        {
            _repository = allocaterepository;
        }
        [HttpGet]
        public async Task<ActionResult<List<AllocateSubject>>> GetAllocateSubjects()
        {
            var allocateSubjects = await _repository.GetAllocateSubjectsAsync();
            return Ok(allocateSubjects);
        }

        [HttpGet("{teacherId}/{subjectId}")]
        public async Task<ActionResult<AllocateSubject>> GetAllocateSubject(int teacherId, int subjectId)
        {
            var allocateSubject = await _repository.GetAllocateSubjectAsync(teacherId, subjectId);

            if (allocateSubject == null)
            {
                return NotFound();
            }

            return Ok(allocateSubject);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAllocateSubject(AllocateSubjectDto allocateSubjectDto)
        {

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<AllocateSubjectDto, AllocateSubject>();
            });
            IMapper mapper = config.CreateMapper();
            var allocateSubject = mapper.Map<AllocateSubjectDto, AllocateSubject>(allocateSubjectDto);


            await _repository.CreateAllocateSubjectAsync<AllocateSubject>(allocateSubject);
            await _repository.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{teacherId}/{subjectId}")]
        public async Task<ActionResult> UpdateAllocateSubject( AllocateSubjectDto allocateSubjectDto)
        {
            var existingAllocateSubject = await _repository.GetAllocateSubjectAsync(allocateSubjectDto.TeacherID, allocateSubjectDto.SubjectID);

            if (existingAllocateSubject == null)
            {
                return NotFound();
            }

            existingAllocateSubject.TeacherID = allocateSubjectDto.TeacherID;
            existingAllocateSubject.SubjectID = allocateSubjectDto.SubjectID;

            //await _repository.UpdateAllocateSubjectAsync(existingAllocateSubject);

            return NoContent();
        }

        [HttpDelete("{teacherId}/{subjectId}")]
        public async Task<ActionResult> DeleteAllocateSubject(int teacherId, int subjectId)
        {
            await _repository.DeleteAllocateSubjectAsync(teacherId, subjectId);

            return NoContent();
        }
    }
}
