using DotnetAPI.Dtos;
using DotnetAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DotnetAPI.Data
{
    public class SubjectRepository:ISubjectRepository
    {
        DataContextEF _entityFramwork;

        public SubjectRepository(IConfiguration config)
        {
            _entityFramwork = new DataContextEF(config);
        }

        public async Task<List<Subject>> GetSubjectsAsync()
        {
            return await _entityFramwork.Subjects.ToListAsync();
        }

        public async Task<Subject> GetSubjectByIdAsync(int id)
        {
            return await _entityFramwork.Subjects.FindAsync(id);
        }

        

        public async Task UpdateSubjectAsync(Subject subject)
        {
            _entityFramwork.Subjects.Update(subject);
            await _entityFramwork.SaveChangesAsync();
        }

        public async Task DeleteSubjectAsync(int id)
        {
            var subject = await _entityFramwork.Subjects.FindAsync(id);
            _entityFramwork.Subjects.Remove(subject);
            await _entityFramwork.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _entityFramwork.SaveChangesAsync();
        }

        public async Task CreateSubjectAsync<T>(T subject)
        {
            if (subject != null)
            {
              await  _entityFramwork.AddAsync(subject);
               await _entityFramwork.SaveChangesAsync();
            }
        }
    }
}
