using BanSach.Components.Data;
using BanSach.Components.IService;
using BanSach.Components.Model;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace BanSach.Components.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext db;
        public UserService(AppDbContext _db)
        {
            db = _db;
        }

        
        public async Task<List<User>> GetAllStaff()
        {
            return await db.Users.Where(u => u.RoleId == 3).ToListAsync();
        }

        
        public async Task<List<User>> GetAllUser()
        {
            return await db.Users.Where(u => u.RoleId == 2).ToListAsync();
        }

        
        public async Task<User> CreateUser(User user, string password)
        {
            if (await UserExists(user.Email))
            {
                throw new Exception("Email already exists.");
            }

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            db.Users.Add(user);
            await db.SaveChangesAsync();
            return user;
        }

        
        public async Task DeleteUser(User user)
        {
            db.Users.Remove(user);
            await db.SaveChangesAsync();
        }

       
        public async Task EditUser(User user, string? newPassword = null)
        {
            if (!string.IsNullOrEmpty(newPassword))
            {
                CreatePasswordHash(newPassword, out byte[] passwordHash, out byte[] passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            db.Users.Update(user);
            await db.SaveChangesAsync();
        }

       
        public async Task<User?> GetUserById(int userId)
        {
            return await db.Users.FirstOrDefaultAsync(p => p.UserId == userId);
        }

        
        private async Task<bool> UserExists(string email)
        {
            return await db.Users.AnyAsync(u => u.Email.ToLower() == email.ToLower());
        }

        
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        // Role
        public async Task<List<Role>> GetAllRole()
        {
            return await db.Roles.ToListAsync();
        }

        public async Task<Role> GetRoleById(int roleId)
        {
            var role = await db.Roles.FirstOrDefaultAsync(p => p.RoleId == roleId);
            return role ?? throw new Exception("Role not found.");
        }
    }
}
