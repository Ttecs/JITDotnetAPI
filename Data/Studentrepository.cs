using DotnetAPI.Dtos;
using DotnetAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace DotnetAPI.Data
{
    public class Studentrepository:IStudentRepository
    {
        DataContextEF _entityFramwork;
        DataContextDapper _dapper;

        public Studentrepository(IConfiguration config)
        {
            _entityFramwork = new DataContextEF(config);
            _dapper = new DataContextDapper(config);
        }

        public async Task<List<Student>> GetStudentsAsync()
        {
            return await _entityFramwork.Students.ToListAsync();
        }

        public async Task<Student> GetStudentByIdAsync(int id)

        {
           
            return await _entityFramwork.Students.FirstOrDefaultAsync(m => m.StudentID == id);
        }

        //public async Task CreateStudentAsync(StudentDto studentDto)
        //{

        //    if (studentDto != null)
        //    {
        //        await _entityFramwork.AddAsync(studentDto);
        //        await _entityFramwork.SaveChangesAsync();
        //    }
            
        //}

        public async Task UpdateStudentAsync(StudentDto student,int id)
        {
            //var student2 = await GetStudentByIdAsync(student.StudentID);
            //if (student2 != null)
            //{
            //    _entityFramwork.Update(student);
            //    await _entityFramwork.SaveChangesAsync();
            //}
            string sql = @"
    UPDATE Students
    SET FirstName = '" + student.FirstName +
    "', LastName = '" + student.LastName +
    "', ContactPerson = '" + student.ContactPerson +
    "', ContactNo = '" + student.ContactNo +
    "', EmailAddress = '" + student.EmailAddress +
    "', DateOfBirth = '" + student.DateOfBirth +
    "', Age = '" + student.Age +
    "', ClassroomID = '" + student.ClassroomID +
    "' WHERE StudentID = " + id;
            bool count = _dapper.ExecuteSql(sql);

        }

        public async Task DeleteStudentAsync(int id)
        {
            var student = await GetStudentByIdAsync(id);
            if (student != null)
            {
                _entityFramwork.Students.Remove(student);
                await _entityFramwork.SaveChangesAsync();
            }
        }

        public async Task SaveChangesAsync()
        {
          await  _entityFramwork.SaveChangesAsync();

        }

        public async Task CreateStudentAsync<T>(T studentDto)
        {
            if (studentDto != null)
            {
                await _entityFramwork.AddAsync(studentDto);
                await _entityFramwork.SaveChangesAsync();
            }

        }

        //public void DeleteStudentAsync(Student student)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
