using System.Collections.Generic;

namespace CodeFirst.Services.Interfaces
{
    public interface IBaseService<T> 
    { 
        T Add(T entity);
        string Delete(int id);
        List<T> GetAll();
        T GetByID(int id);
    }
}
