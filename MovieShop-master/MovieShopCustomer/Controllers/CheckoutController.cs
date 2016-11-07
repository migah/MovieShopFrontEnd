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
        IManager<Customer> _cm = new DllFacade().GetCustomerManager();
        IManager<Movie> _mm = new DllFacade().GetMovieManager();
        IManager<Order> _om = new DllFacade().GetOrderManager();

        public ActionResult Index(int cId, int mId)
        {
            var model = new CustomerMovieView()
            {
                Customer = _cm.Read(cId),
                Movie = _mm.Read(mId)
            
            };
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