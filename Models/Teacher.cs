using System.Text.Json.Serialization;

namespace DotnetAPI.Models
{
    public class Teacher
    {
        public int TeacherID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ContactNo { get; set; }
        public string? EmailAddress { get; set; }
        [JsonIgnore]
        public ICollection<AllocateSubject>? AllocateSubjects { get; set; }
        [JsonIgnore]
        public ICollection<AllocateClassroom>? AllocateClassrooms { get; set; }
    }
}
