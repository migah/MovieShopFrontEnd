using System.Collections.Generic;

namespace MovieShopDll
{
    public interface IManager <T>
    {
        T Create(T t);
        T Read(int id);
        List<T> Read();
        void Delete(int id);
        T Update(T t);
    }
}
