using DotnetAPI.Dtos;
using DotnetAPI.Models;

namespace DotnetAPI.Data
{
    public interface ISubjectRepository
    {
        Task<List<Subject>> GetSubjectsAsync();
        Task<Subject> GetSubjectByIdAsync(int id);
        Task CreateSubjectAsync<T>(T subject);
        Task UpdateSubjectAsync(Subject subject);
        Task DeleteSubjectAsync(int id);

        Task SaveChangesAsync();
    }
}
