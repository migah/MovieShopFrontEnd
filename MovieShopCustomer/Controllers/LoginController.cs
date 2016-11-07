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
    public class LoginController : Controller
    {
        private readonly IManager<Customer> customerManager = new DllFacade().GetCustomerManager();
        private readonly IManager<Movie> movieManager = new DllFacade().GetMovieManager();


        // GET: Checkout
        public ActionResult Index(int movieId)
        {
            var model = new CustomerMovieView()
            {
                Movie = movieManager.Read(movieId),
                Customer = new Customer()
            };
            
            return View(model);
        }


       

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Email, Address ")] Customer customer, int movieId)
        {
            if (ModelState.IsValid)
            {
                var newCustomer = customerManager.Create(customer);

                return RedirectToAction("Index", "Checkout", new { cId = newCustomer.CustomerId, mId = movieId });
    
            }

            return View(customer);


        }

        [ActionName("check")]
        public ActionResult CheckUser(String email, int movieId)
        {
            if (ModelState.IsValid)
            {
                var customers = customerManager.Read();

                var customer = customers.FirstOrDefault(x => x.Email == email);

                if (customer != null)
                {
                    return RedirectToAction("Index", "Checkout", new {cId = customer.CustomerId, mId = movieId});
                }

            }
            return RedirectToAction("Index");

        }

    }
}