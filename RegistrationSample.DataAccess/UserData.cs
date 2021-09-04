using System.Collections.Generic;
using RegistrationSample.DataAccess.Models;

namespace RegistrationSample.DataAccess
{
    public class UserData
    {
        public List<UserModel> GetUserById(string id)
        {
            SqlDataAccess sql = new SqlDataAccess();

            var param = new { Id = id };
            var output = sql.LoadData<UserModel, dynamic>("dbo.spUserLookup", param, "DefaultConnection");
            
            return output;
        }
    }
}
