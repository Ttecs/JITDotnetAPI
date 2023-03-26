using AutoMapper;
using DotnetAPI.Data;
using DotnetAPI.Dtos;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController:ControllerBase
    {
        private readonly IStudentRepository _studentRepository;

        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            var students = await _studentRepository.GetStudentsAsync();
            return Ok(students);
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _studentRepository.GetStudentByIdAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        // POST: api/Students
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(StudentDto student)
        {

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<StudentDto, Student>();
            });
            IMapper mapper = config.CreateMapper();

            var subject = mapper.Map<StudentDto, Student>(student);

            await _studentRepository.CreateStudentAsync<Student>(subject);
            _ = _studentRepository.CreateStudentAsync(student);
            await _studentRepository.SaveChangesAsync();

            return Ok();
        }

        // PUT: api/Students/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> PutStudent( StudentDto student,int id)
        {
            //if (id != student.StudentID)
            //{
            //    return BadRequest();
            //}
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<StudentDto, Student>();
            });
            IMapper mapper = config.CreateMapper();

            var subject = mapper.Map<StudentDto, Student>(student);

            await _studentRepository.UpdateStudentAsync(student,id);

            try
            {
                await _studentRepository.SaveChangesAsync();
            }
            catch
            {
                if (!StudentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _studentRepository.GetStudentByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _ = _studentRepository.DeleteStudentAsync(id);
            await _studentRepository.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentExists(int id)
        {
            return _studentRepository.GetStudentByIdAsync(id) != null;
        }
    }
}
