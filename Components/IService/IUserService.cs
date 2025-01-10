using BanSach.Components.Model;

namespace BanSach.Components.IService
{
    public interface IUserService
    {
        Task<List<User>> GetAllStaff();
        Task<User> CreateUser(User user, string password);
        Task EditUser(User user, string? newPassword = null);
        Task DeleteUser(User User);
        Task<User?> GetUserById(int userId);
        Task<List<Role>> GetAllRole();
        Task<Role> GetRoleById(int roleId);
        Task<List<User>> GetAllUser();
    }
}