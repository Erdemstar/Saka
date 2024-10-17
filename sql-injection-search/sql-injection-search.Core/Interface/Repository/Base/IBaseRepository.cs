namespace sql_injection_search.Core.Interface.Repository.Base;

public interface IBaseRepository<T> where T : class
{
    Task<List<T>> GetByUsernameAsync(string username);
}