using System.Collections.Generic;
using RegistrationSample.DataAccess.Models;

namespace RegistrationSample.OdlDataAccess
{
    public class UserData
    {
        public List<UserModel> GetUserById(string id)
        {
            var sql = new SqlDataAccess();

            var param = new { Id = id };
            var output = sql.LoadData<UserModel, dynamic>("dbo.spUserLookup", param, "RegistrationSample");

            return output;
        }

        public void UpdateUser(UserModel user)
        {
            var sql = new SqlDataAccess();

            var param = new
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Birthddate = user.BirthDate
            };
            sql.SaveData("dbo.spUpdateUser", param, "RegistrationSample");
        }
    }
}
