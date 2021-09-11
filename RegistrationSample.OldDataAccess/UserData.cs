using System.Collections.Generic;
using System.Linq;
using RegistrationSample.DataAccess.Models;

namespace RegistrationSample.OdlDataAccess
{
    public class UserData
    {
        public UserModel GetUserById(string id)
        {
            var sql = new SqlDataAccess();

            var param = new { Id = id };
            var output = sql.LoadData<UserModel, dynamic>("dbo.spUserLookup", param, "RegistrationSample").FirstOrDefault();

            return output;
        }

        public void RegisterUser(UserModel user)
        {
            var sql = new SqlDataAccess();

            sql.SaveData("dbo.spUser_Insert", user, "RegistrationSample");
        }

        public void UpdateUser(UserModel user)
        {
            var sql = new SqlDataAccess();

            sql.SaveData("dbo.spUser_Update", user, "RegistrationSample");
        }
    }
}
