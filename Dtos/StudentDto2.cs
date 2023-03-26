using DotnetAPI.Models;

namespace DotnetAPI.Dtos
{
    public class StudentDto2
    {
      
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactPerson { get; set; }
        public string ContactNo { get; set; }
        public string EmailAddress { get; set; }
        public DateTime DateOfBirth { get; set; }
        
        public int ClassroomID { get; set; }
        public int Age { get; set; }


    }
}
