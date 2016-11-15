using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieShopCustomer.Models;
using MovieShopDll;
using MovieShopDll.Entities;

namespace MovieShopCustomer.Controllers
{
    public class CheckoutController : Controller
    {
        IServiceGateway<Customer> _cm = new DllFacade().GetCustomerManager();
        IServiceGateway<Movie> _mm = new DllFacade().GetMovieManager();
        IServiceGateway<Order> _om = new DllFacade().GetOrderManager();

        public ActionResult Index(int cId, int mId)
        {
            var myCookie = Request.Cookies["UserSettings"];
            var currency = Convert.ToDouble(myCookie["currency"]);

            var movie = _mm.Read(mId);
            var price = movie.Price*currency;
            price = System.Math.Round(price, 2);
            movie.Price = price;

            var model = new CustomerMovieView()
            {
                Customer = _cm.Read(cId),
                Movie = movie//_mm.Read(mId)
            
            };
            


         /*   List<Movie> movies = new List<Movie>();

            foreach (var movie in _mm.Read())
            {
                var price = movie.Price * currency;
                price = System.Math.Round(price, 2);
                movie.Price = price;
                movies.Add(movie);

            }*/

            return View(model);
        }

        public ActionResult Confirm(int customerId, int movieId)
        {
            var loggedInCustomer = _cm.Read(customerId);
            var movie = _mm.Read(movieId);

            var allMovies = new List<Movie> {movie};


            var order = new Order();

            order.Customer = loggedInCustomer;
            order.Movies = allMovies;
            order.Date = DateTime.Now;

            var createdOrder =_om.Create(order);

            return RedirectToAction("Index", "Confirmation", new { cId = customerId, mId = movieId, oId = createdOrder.OrderId });

        }
    }
}