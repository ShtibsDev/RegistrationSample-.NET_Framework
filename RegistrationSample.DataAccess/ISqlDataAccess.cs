namespace RegistrationSample.DataAccess
{
    public interface ISqlDataAccess
    {
        string GetConnectionString(string name);
        List<TModel> LoadData<TModel, TParam>(string storedProcedure, TParam parameters, string connectionStringName);
        void SaveData<T>(string storedProcedure, T parameters, string connectionStringName);
    }
}