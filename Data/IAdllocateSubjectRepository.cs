using DotnetAPI.Dtos;
using DotnetAPI.Models;

namespace DotnetAPI.Data
{
    public interface IAdllocateSubjectRepository
    {
        Task<List<AllocateSubject>> GetAllocateSubjectsAsync();
        Task<AllocateSubject> GetAllocateSubjectAsync(int teacherId, int subjectId);
        Task CreateAllocateSubjectAsync<T>(T allocateSubjectDto);
        Task UpdateAllocateSubjectAsync(AllocateSubjectDto allocateSubjectDto);
        Task DeleteAllocateSubjectAsync(int teacherId, int subjectId);

        Task SaveChangesAsync();
    }
}
