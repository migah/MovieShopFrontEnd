using MovieShopDll.Entities;
using MovieShopDll.Manager;

namespace MovieShopDll
{
    public class DllFacade
    {
        public IManager<Genre> GetGenreManager()
        {
            return new GenreManager();
        }

        public IManager<Movie> GetMovieManager()
        {
            return new MovieManager();
        }

        public IManager<Customer> GetCustomerManager()
        {
            return new CustomerManager();
        }

        public IManager<Order> GetOrderManager()
        {
            return new OrderManager();
        }
    }
}
