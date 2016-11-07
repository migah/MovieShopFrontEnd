using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MovieShopDll;
using MovieShopDll.Contexts;
using MovieShopDll.Entities;

namespace MovieShopAdmin.Controllers
{
    public class CustomersController : Controller
    {
        //private MovieShopContext db = new MovieShopContext();
        private readonly IManager<Customer> customerManager = new DllFacade().GetCustomerManager();


        // GET: Customers
        public ActionResult Index()
        {
            return View(customerManager.Read());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int id)
        {

            var customer = customerManager.Read(id);

            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
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
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Email, Address ")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                customerManager.Create(customer);

                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int id)
        {
            var customer = customerManager.Read(id);// db.Customers.Find(id);

            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerId,FirstName,LastName,Email,Address")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                customerManager.Update(customer);

                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int id)
        {
            var customer = customerManager.Read(id);

            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var customer = customerManager.Read(id);

            customerManager.Delete(id);
        
            return RedirectToAction("Index");
        }
    }
}
