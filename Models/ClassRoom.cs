namespace DotnetAPI.Models
{
    public class ClassRoom
    {
        public int ClassroomID { get; set; }
        public string? ClassroomName { get; set; }

        public ICollection<Student>? Students { get; set; }
        public ICollection<AllocateClassroom>? AllocateClassrooms { get; set; }
    }
}
