using DotnetAPI.Models;

namespace DotnetAPI.Data
{
    public interface IClassRoomRepository
    {
        Task<List<ClassRoom>> GetClassRoomsAsync();
        Task<ClassRoom> GetClassRoomByIdAsync(int id);
        Task CreateTeacherAsync<T>(T classroom);
        Task UpdateClassRoomAsync(ClassRoom classRoom);
        Task DeleteClassRoomAsync(int id);

        Task SaveChangesAsync();
    }
}
