using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace RegistrationSample.OdlDataAccess
{
    internal class SqlDataAccess
    {
        public string GetConnectionString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

        public List<TModel> LoadData<TModel, TParam>(string storedProcedure, TParam parameters, string connectionStringName)
        {
            var connectionString = GetConnectionString(connectionStringName);

            using (IDbConnection conn = new SqlConnection(connectionString))
            {
                var rows = conn.Query<TModel>(storedProcedure, parameters, commandType: CommandType.StoredProcedure).ToList();

                return rows;
            }
        }

        public void SaveData<T>(string storedProcedure, T parameters, string connectionStringName)
        {
            var connectionString = GetConnectionString(connectionStringName);

            using (IDbConnection conn = new SqlConnection(connectionString))
            {
                conn.Execute(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
