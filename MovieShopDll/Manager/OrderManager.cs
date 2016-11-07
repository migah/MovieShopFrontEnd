using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieShopDll.Contexts;
using MovieShopDll.Entities;

namespace MovieShopDll.Manager
{
    class OrderManager : IManager<Order>
    {
        public Order Create(Order t)
        {


            using (var db = new MovieShopContext())
            {

                t.Customer = db.Customers.Include(x => x.Address).FirstOrDefault(x => x.CustomerId == t.Customer.CustomerId);

                var tmpList = new List<Movie>();

                foreach (var movie in db.Movies)
                {
                    foreach (var moviesToBuy in t.Movies)
                    {
                        if (movie.Title == moviesToBuy.Title) 
                            tmpList.Add(movie);
                    }
                }

           
                t.Movies = tmpList;

             
                db.Orders.Add(t);
                db.SaveChanges();
                return t;
            }
        }

        public Order Read(int id)
        {
            using (var db = new MovieShopContext())
            {
               // return db.Movies.Include("Genre").FirstOrDefault(x => x.MovieId == id);

                return db.Orders.Include("Customer").Include("Movies").FirstOrDefault(x => x.OrderId == id);
            }
        }

        public List<Order> Read()
        {
            using (var db = new MovieShopContext())
            {
                return db.Orders.Include("Customer").ToList();
            }
        }

        public void Delete(int id)
        {
            using (var db = new MovieShopContext())
            {
                var orderTodelete = db.Orders.FirstOrDefault(x => x.OrderId == id);
                db.Entry(orderTodelete).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }
        }

        public Order Update(Order t)
        {
            using (var db = new MovieShopContext())
            {
                db.Entry(t).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return t;
            }
        }
    }
}
