using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using MovieShopDll.Contexts;
using MovieShopDll.Entities;

namespace MovieShopDll.Manager
{
    class OrderServiceGateway : IServiceGateway<Order>
    {
        public Order Create(Order t)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:1922/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.PostAsJsonAsync("api/orders", t).Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<Order>().Result;
                }
                return null;
            }

            /*using (var db = new MovieShopContext())
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
            }*/
        }

        public Order Read(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:1922/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.GetAsync($"api/orders/{id}").Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<Order>().Result;
                }
                return null;
            }
        }

        public List<Order> Read()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:1922/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.GetAsync("/api/orders").Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<List<Order>>().Result;
                }
            }
            return new List<Order>();
        }

        public bool Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:1922/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.DeleteAsync($"/api/orders/{id}").Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<Order>().Result != null;
                }
                return false;
            }
        }

        public Order Update(Order t)
        {
            throw new NotImplementedException();

            /*using (var db = new MovieShopContext())
            {
                db.Entry(t).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return t;
            }*/
        }
    }
}
