namespace sql_injection_profile.Core.Interface.Repository.Base;

public interface IBaseRepository<T> where T : class
{
    Task<List<T>> GetByIdAsync(string id);
}