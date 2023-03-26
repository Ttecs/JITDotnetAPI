using System.Text.Json.Serialization;

namespace DotnetAPI.Models
{
    public class AllocateSubject
    {
        public int AllocateSubjectID { get; set; }
        public int TeacherID { get; set; }
        public int SubjectID { get; set; }
        
        public Teacher? Teacher { get; set; }
        
        public Subject? Subject { get; set; }
    }
}
