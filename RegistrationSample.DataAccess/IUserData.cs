using RegistrationSample.DataAccess.Models;

namespace RegistrationSample.DataAccess
{
    public interface IUserData
    {
        UserModel GetUserById(string id);
        void RegisterUser(UserModel user);
        void UpdateUser(UserModel user);
    }
}