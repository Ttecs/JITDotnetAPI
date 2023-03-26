using DotnetAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DotnetAPI.Data
{
    public class ClassRoomRepository : IClassRoomRepository
    {
        DataContextEF _entityFramwork;

        public ClassRoomRepository(IConfiguration configuration)
        {
            _entityFramwork = new DataContextEF(configuration);
        }

        public async Task CreateTeacherAsync<T>(T classroom)
        {
            if (classroom != null)
            {
                await _entityFramwork.AddAsync(classroom);
            }
        }

        public async Task DeleteClassRoomAsync(int id)
        {
            var classroom = await _entityFramwork.Classrooms.FindAsync(id);
            _entityFramwork.Classrooms.Remove(classroom);
            await _entityFramwork.SaveChangesAsync();
        }

        public async Task<ClassRoom> GetClassRoomByIdAsync(int id)
        {
            return await _entityFramwork.Classrooms.FindAsync(id);
        }

        public async Task<List<ClassRoom>> GetClassRoomsAsync()
        {
            return await _entityFramwork.Classrooms.ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _entityFramwork.SaveChangesAsync();
        }

        public async Task UpdateClassRoomAsync(ClassRoom classRoom)
        {
            _entityFramwork.Classrooms.Update(classRoom);
            await _entityFramwork.SaveChangesAsync();
        }
    }
}
