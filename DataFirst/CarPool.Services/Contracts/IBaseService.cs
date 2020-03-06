using System.Collections.Generic;

namespace CarPool.Services.Contracts
{
    public interface IBaseService<T> 
    { 

        T Add(T entity);
        string Delete(int id);
        List<T> GetAll();
        T GetByID(int id);
    }
}
