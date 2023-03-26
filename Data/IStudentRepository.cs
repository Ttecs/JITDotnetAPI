using DotnetAPI.Dtos;
using DotnetAPI.Models;

namespace DotnetAPI.Data
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetStudentsAsync();
        Task<Student> GetStudentByIdAsync(int id);
        Task CreateStudentAsync<T>(T student);
        Task UpdateStudentAsync(StudentDto student,int id);
        Task DeleteStudentAsync(int id);

        Task SaveChangesAsync();
        
    }
}
