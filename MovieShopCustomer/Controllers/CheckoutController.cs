﻿using System;
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
            var currencyRate = Convert.ToDouble(myCookie["CurrencyRate"]);
            

            var movie = _mm.Read(mId);
            var price = movie.Price*currencyRate;
            price = System.Math.Round(price, 2);
            movie.Price = price;

            var model = new CustomerMovieView()
            {
                Customer = _cm.Read(cId),
                Movie = movie,
                SelectedCurency = myCookie["Currency"]
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