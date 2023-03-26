using AutoMapper;
using DotnetAPI.Data;
using DotnetAPI.Dtos;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController:ControllerBase
    {
        private readonly ITeacherRepository _teacherRepository;

        public TeacherController(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Teacher>>> Get()
        {
            var Teachers = await _teacherRepository.GetTeachersAsync();
            return Ok(Teachers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Subject>> GetById(int id)
        {
            var teacher = await _teacherRepository.GetTeacherByIdAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }

            return Ok(teacher);
        }
        [HttpPost]
        public async Task<ActionResult<Teacher>> Post( TeacherDto teacherDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<TeacherDto, Teacher>();
            });
            IMapper mapper = config.CreateMapper();

            var teacher = mapper.Map<TeacherDto, Teacher>(teacherDTO);

            await _teacherRepository.CreateTeacherAsync<Teacher>(teacher);
            await _teacherRepository.SaveChangesAsync();

            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id,  Teacher teacher)
        {
            if (id != teacher.TeacherID)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _teacherRepository.UpdateTeacherAsync(teacher);
            await _teacherRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var subject = await _teacherRepository.GetTeacherByIdAsync(id);
            if (subject == null)
            {
                return NotFound();
            }

            await _teacherRepository.DeleteTeacherAsync(id);
            await _teacherRepository.SaveChangesAsync();

            return NoContent();
        }

    }
}
