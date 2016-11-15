using MovieShopDll.Entities;
using MovieShopDll.Manager;
using MovieShopDll.ServiceGateway;

namespace MovieShopDll
{
    public class DllFacade
    {
        public IServiceGateway<Genre> GetGenreManager()
        {
            return new GenreServiceGateway();
        }

        public IServiceGateway<Movie> GetMovieManager()
        {
            return new MovieServiceGateway();
        }

        public IServiceGateway<Customer> GetCustomerManager()
        {
            return new CustomerServiceGateway();
        }

        public IServiceGateway<Order> GetOrderManager()
        {
            return new OrderServiceGateway();
        }

        public ICurrencyConverter GetCurrencyConverter()
        {
            return new CurrencyServiceGateway();
        }
    }
}
