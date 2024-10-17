namespace sql_injection_login_bypass.Core.Interface.Repository.Base;

public interface IBaseRepository<T> where T : class
{
    Task<List<T>> GetByIdAsync(string id);
    Task<bool> LoginAsync(string username, string password);
}