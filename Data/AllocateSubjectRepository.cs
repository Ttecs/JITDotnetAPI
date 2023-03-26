using DotnetAPI.Dtos;
using DotnetAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DotnetAPI.Data
{
    public class AllocateSubjectRepository : IAdllocateSubjectRepository
    {
        DataContextEF _entityFramwork;

        public AllocateSubjectRepository(IConfiguration config)
        {
            _entityFramwork = new DataContextEF(config);
        }
        public async Task CreateAllocateSubjectAsync<T>(T allocateSubjectDto)
        {
            
            
            if (allocateSubjectDto != null)
               

            {
                await _entityFramwork.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT {nameof(AllocateSubject)}s OFF");
                await _entityFramwork.AddAsync(allocateSubjectDto);
            }
        }

        public async Task DeleteAllocateSubjectAsync(int teacherId, int subjectId)
        {
            var allocateSubject = await _entityFramwork.AllocateSubjects.FirstOrDefaultAsync(a => a.TeacherID == teacherId && a.SubjectID == subjectId);
            if (allocateSubject != null)
            {
                _entityFramwork.AllocateSubjects.Remove(allocateSubject);
            }
        }

        public async Task<AllocateSubject> GetAllocateSubjectAsync(int teacherId, int subjectId)
        {
            return await _entityFramwork.AllocateSubjects
           .Include(a => a.Teacher)
           .Include(a => a.Subject)
           .FirstOrDefaultAsync(a => a.TeacherID == teacherId && a.SubjectID == subjectId);
        }

        public async Task<List<AllocateSubject>> GetAllocateSubjectsAsync()
        {
            return await _entityFramwork.AllocateSubjects
             .Include(a => a.Teacher)
             .Include(a => a.Subject)
             .ToListAsync();
        }

        public async Task UpdateAllocateSubjectAsync(AllocateSubjectDto allocateSubjectDto)
        {
            var allocateSubject = await _entityFramwork.AllocateSubjects.FindAsync(allocateSubjectDto.TeacherID, allocateSubjectDto.SubjectID);

            if (allocateSubject == null)
            {
                throw new ArgumentException($"AllocateSubject with TeacherID:{allocateSubjectDto.TeacherID} and SubjectID:{allocateSubjectDto.SubjectID} not found.");
            }

            allocateSubject.TeacherID = allocateSubjectDto.TeacherID;
            allocateSubject.SubjectID = allocateSubjectDto.SubjectID;

            await _entityFramwork.SaveChangesAsync();
        }
         public async Task SaveChangesAsync()
        {
            await _entityFramwork.SaveChangesAsync();
        }
    }
}
