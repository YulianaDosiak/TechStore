using System.Collections.Generic;

namespace TechStore.DAL
{
    public interface IGenericDAL<T>
    {
        T Create(T entity);
        List<T> GetAll();
        T GetById(int id);
        T Update(T entity);
        bool Delete(int id);
    }
}
