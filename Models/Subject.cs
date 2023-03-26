using System.Text.Json.Serialization;

namespace DotnetAPI.Models
{
    public class Subject
    {
        public int SubjectID { get; set; }
        public string? SubjectName { get; set; }
        [JsonIgnore]
        public ICollection<AllocateSubject>? AllocateSubjects { get; set; }
    }
}
