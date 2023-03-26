using AutoMapper;
using DotnetAPI.Data;
using DotnetAPI.Dtos;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassRoomController:ControllerBase
    {
        private readonly IClassRoomRepository _classRoomRepository;
        public ClassRoomController(IClassRoomRepository classRoomRepository)
        {
            _classRoomRepository = classRoomRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClassRoom>>> Get()
        {
            var classrooms = await _classRoomRepository.GetClassRoomsAsync();
            return Ok(classrooms);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClassRoom>> GetById(int id)
        {
            var classRoom = await _classRoomRepository.GetClassRoomByIdAsync(id);
            if (classRoom == null)
            {
                return NotFound();
            }

            return Ok(classRoom);
        }
        [HttpPost]
        public async Task<ActionResult<ClassRoom>> Post(ClassRoomDto classRoomDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<ClassRoomDto, ClassRoom>();
            });
            IMapper mapper = config.CreateMapper();

            var classroom = mapper.Map<ClassRoomDto, ClassRoom>(classRoomDTO);

            await _classRoomRepository.CreateTeacherAsync<ClassRoom>(classroom);
            await _classRoomRepository.SaveChangesAsync();

            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ClassRoom classRoom)
        {
            if (id != classRoom.ClassroomID)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _classRoomRepository.UpdateClassRoomAsync(classRoom);
            await _classRoomRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var subject = await _classRoomRepository.GetClassRoomByIdAsync(id);
            if (subject == null)
            {
                return NotFound();
            }

            await _classRoomRepository.DeleteClassRoomAsync(id);
            await _classRoomRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
