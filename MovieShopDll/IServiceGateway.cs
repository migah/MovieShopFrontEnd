using System.Collections.Generic;

namespace MovieShopDll
{
    public interface IServiceGateway <T>
    {
        T Create(T t);
        T Read(int id);
        List<T> Read();
        bool Delete(int id);
        T Update(T t);
    }
}
