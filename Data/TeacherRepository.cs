using DotnetAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DotnetAPI.Data
{
    public class TeacherRepository : ITeacherRepository
    {
        DataContextEF _entityFramwork;
        public TeacherRepository(IConfiguration config)
        {
            _entityFramwork = new DataContextEF(config);
        }

        public async Task CreateTeacherAsync<T>(T teacher)
        {
            if (teacher != null)
            {
                await _entityFramwork.AddAsync(teacher);
            }
        }

        public async Task DeleteTeacherAsync(int id)
        {
            var teacher= await _entityFramwork.Teachers.FindAsync(id);
            _entityFramwork.Teachers.Remove(teacher);
            await _entityFramwork.SaveChangesAsync();
        }

        public async Task<Teacher> GetTeacherByIdAsync(int id)
        {
            return await _entityFramwork.Teachers.FindAsync(id);
        }

        public async Task<List<Teacher>> GetTeachersAsync()
        {
            return await _entityFramwork.Teachers.ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _entityFramwork.SaveChangesAsync();
        }

        public async Task UpdateTeacherAsync(Teacher teacher)
        {
            _entityFramwork.Teachers.Update(teacher);
            await _entityFramwork.SaveChangesAsync();
        }
    }
}
