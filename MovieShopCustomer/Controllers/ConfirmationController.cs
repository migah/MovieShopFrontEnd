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
    public class ConfirmationController : Controller
    {

        IServiceGateway<Customer> _cm = new DllFacade().GetCustomerManager();
        IServiceGateway<Movie> _mm = new DllFacade().GetMovieManager();
        IServiceGateway<Order> _om = new DllFacade().GetOrderManager();



        // GET: Confirmation
        public ActionResult Index(int mId, int cId, int oId)
        {
            var myCookie = Request.Cookies["UserSettings"];
            var currencyRate = Convert.ToDouble(myCookie["CurrencyRate"]);


            var movie = _mm.Read(mId);
            var price = movie.Price * currencyRate;
            price = System.Math.Round(price, 2);
            movie.Price = price;

            var model = new CustomerMovieOrderModel()
            {
                Customer = _cm.Read(cId),
                Movie = movie,
                Order = _om.Read(oId),
                SelectedCurrency = myCookie["Currency"]
            };

            return View(model);
        }
    }
}