using DotnetAPI.Models;

namespace DotnetAPI.Data
{
    public interface ITeacherRepository
    {
        Task<List<Teacher>> GetTeachersAsync();
        Task<Teacher> GetTeacherByIdAsync(int id);
        Task CreateTeacherAsync<T>(T teacher);
        Task UpdateTeacherAsync(Teacher teacher);
        Task DeleteTeacherAsync(int id);
        
        Task SaveChangesAsync();
    }
}
