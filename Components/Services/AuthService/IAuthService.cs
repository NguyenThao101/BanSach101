using BanSach.Components.Model;

namespace BanSach.Components.Services.AuthService
{
    public interface IAuthService
    {
        //server
        Task<ServiceResponse<int>> Register(User user, string password);
        Task<bool> UserExists(string email);
        Task<ServiceResponse<string>> Login(string email, string password);
      
        //client
        Task<ServiceResponse<int>> Register(UserRegister request);
        Task<ServiceResponse<string>> Login(UserLogin request);
     
        Task<bool> IsUserAuthenticated();
       
    }
}
