using System.Text.Json.Serialization;

namespace DotnetAPI.Models
{
    public class AllocateClassroom
    {
        public int AllocateClassroomID { get; set; }
        public int TeacherID { get; set; }
        public int ClassroomID { get; set; }
        [JsonIgnore]
        public Teacher Teacher { get; set; }
        public ClassRoom Classroom { get; set; }
    }
}
