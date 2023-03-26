using AutoMapper;
using DotnetAPI.Data;
using DotnetAPI.Dtos;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotnetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController:ControllerBase
    {
        private readonly ISubjectRepository _subjectRepository;
       
        public SubjectController(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
            
        }

        // GET api/subject
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Subject>>> Get()
        {
            var subjects = await _subjectRepository.GetSubjectsAsync();
            return Ok(subjects);
        }

        // GET api/subject/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Subject>> GetById(int id)
        {
            var subject = await _subjectRepository.GetSubjectByIdAsync(id);
            if (subject == null)
            {
                return NotFound();
            }

            return Ok(subject);
        }
        [HttpPost]
        public async Task<ActionResult<Subject>> Post([FromBody] SubjectDto subjectDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<SubjectDto, Subject>();
            });
            IMapper mapper = config.CreateMapper();

            var subject = mapper.Map<SubjectDto, Subject>(subjectDTO);

            await _subjectRepository.CreateSubjectAsync<Subject>(subject);
            await _subjectRepository.SaveChangesAsync();

            return Ok();
        }
        //public IActionResult AddUser(SubjectDto user)
        //{

        //    Subject userDB = _mapper.Map<Subject>(user);

        //    _subjectRepository.CreateSubjectAsync<Subject>(userDB);
        //    if (_subjectRepository.SaveChanges())
        //    {
        //        return Ok(user);
        //    }

        //    throw new Exception("failed to edit user");


        //}

        //// POST api/subject
        //[HttpPost]
        //public async Task<ActionResult<SubjectDto>> Post( SubjectDto subject)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    Subject subjectDB = _mapper.Map<Subject>(subject);

        //    await _subjectRepository.CreateSubjectAsync(subjectDB);
        //    await _subjectRepository.SaveChangesAsync();

        //    return Ok();
        //}

        // PUT api/subject/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Subject subject)
        {
            if (id != subject.SubjectID)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _subjectRepository.UpdateSubjectAsync(subject);
            await _subjectRepository.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/subject/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var subject = await _subjectRepository.GetSubjectByIdAsync(id);
            if (subject == null)
            {
                return NotFound();
            }

            await _subjectRepository.DeleteSubjectAsync(id);
            await _subjectRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
