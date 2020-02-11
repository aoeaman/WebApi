using System;
using System.Collections.Generic;
using System.Text;
using CarPoolApplication.Models;

namespace CodeFirst.Services.Interfaces
{
    public interface IService<T> 
    { 
        void Add(T entity);
        T Create(T entity);
        void Delete(int iD);
        List<T> GetAll();
        T GetByID(int id);
    }
}
